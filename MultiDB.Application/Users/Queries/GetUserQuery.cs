using MediatR;
using MultiDB.Core.Entities;

namespace MultiDB.Application.Users.Queries
{
    public class GetUserQuery : IRequest<User>
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
    }
}
