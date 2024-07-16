using MediatR;
using MultiDB.Core.Entities;

namespace MultiDB.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
        public string TenantId { get; set; }
    }
}
