using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Domain.Entities.Common;
using ECommerceBackend.Domain.Entities.Concrete;
using ECommerceBackend.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using File = ECommerceBackend.Domain.Entities.Concrete.File;

namespace ECommerceBackend.Persistence.Contexts
{
    public class ECommerceBackendDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ECommerceBackendDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasKey(b => b.Id);

            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(b => b.Id);

            builder.Entity<Order>()
                .HasOne(o => o.CompletedOrder)
                .WithOne(c => c.Order)
                .HasForeignKey<CompletedOrder>(c => c.OrderId);


            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow,
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<File>? Files { get; set; }
        public DbSet<ProductImageFile>? ProductImageFiles { get; set; }
        public DbSet<InvoiceFile>? InvoiceFiles { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Basket>? Baskets { get; set; }
        public DbSet<BasketItem>? BasketItems { get; set; }
        public DbSet<CompletedOrder>? CompletedOrders { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
    }
}
