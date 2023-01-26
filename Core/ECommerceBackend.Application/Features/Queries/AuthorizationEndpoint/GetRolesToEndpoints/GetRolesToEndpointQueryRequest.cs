using MediatR;

namespace ECommerceBackend.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoints;

public class GetRolesToEndpointQueryRequest : IRequest<GetRolesToEndpointQueryResponse>
{
    public string Code { get; set; }
    public string Menu { get; set; }
}