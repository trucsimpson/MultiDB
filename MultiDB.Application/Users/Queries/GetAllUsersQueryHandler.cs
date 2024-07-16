using MediatR;
using MultiDB.Core.Entities;
using MultiDB.Core.Repositories;

namespace MultiDB.Application.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly IReadRepository<User> _readRepository;

        public GetAllUsersQueryHandler(IReadRepository<User> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            return await _readRepository.GetAll(query.TenantId);
        }
    }
}
