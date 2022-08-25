using System.Security.Claims;
using System.Text;
using ECommerceBackend.API.Configurations.ColumnWriters;
using ECommerceBackend.Application;
using ECommerceBackend.Application.Validators.Products;
using ECommerceBackend.Infrastructure.Filter;
using ECommerceBackend.Infrastructure;
using ECommerceBackend.Infrastructure.Enums;
using ECommerceBackend.Infrastructure.Services.Storage.Azure;
using ECommerceBackend.Infrastructure.Services.Storage.Local;
using ECommerceBackend.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationService();

//builder.Services.AddStorage(StorageType.Azure);
builder.Services.AddStorage<AzureStorage>();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy
    => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
    ));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
Logger log = new LoggerConfiguration()
.WriteTo.Console().WriteTo.File("logs/log.txt").WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", needAutoCreateTable: true, columnOptions: new Dictionary<string, ColumnWriterBase>()
{
    {"message",new RenderedMessageColumnWriter()},
    {"message_template",new MessageTemplateColumnWriter()},
    {"level",new LevelColumnWriter()},
    {"time_stamp",new TimestampColumnWriter()},
    {"exception",new ExceptionColumnWriter()},
    {"log_event",new LogEventSerializedColumnWriter()},
    {"user_name", new UserNameColumnWriter() }
}).WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
.Enrich.FromLogContext()
.MinimumLevel.Information()
.CreateLogger();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Host.UseSerilog(log);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //Olu�turulacak token de�erini kimlerin/hangi originlerin/sitelerin kullan�c� belirledi�imiz de�erdir. -> www.bilmemne.com
            ValidateIssuer = true, //Olu�turulacak token de�erini kimin da��tt�n� ifade edece�imiz aland�r. -> www.myapi.com
            ValidateLifetime = true, //Olu�turulan token de�erinin s�resini kontrol edecek olan do�rulamad�r.
            ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden suciry key verisinin do�rulanmas�d�r.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

            NameClaimType = ClaimTypes.Name //JWT �zerinde Name claimne kar��l�k gelen de�eri User.Identity.Name propertysinden elde edebiliriz.
        };
    });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var userName = context.User?.Identity?.Name != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", userName);
    await next();
});
app.MapControllers();

app.Run();
