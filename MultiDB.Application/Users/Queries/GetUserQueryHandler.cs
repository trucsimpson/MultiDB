using MultiDB.Core.Entities;
using MultiDB.Core.Repositories;

namespace MultiDB.Application.Users.Queries
{
    public class GetUserQueryHandler
    {
        private readonly IReadRepository<User> _readRepository;

        public GetUserQueryHandler(IReadRepository<User> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<User> Handle(GetUserQuery query)
        {
            return await _readRepository.GetById(query.Id, query.TenantId);
        }
    }
}
