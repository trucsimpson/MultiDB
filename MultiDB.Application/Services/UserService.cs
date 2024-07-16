using MultiDB.Core.Entities;
using MultiDB.Core.Repositories;

namespace MultiDB.Application.Services
{
    public class UserService
    {
        private readonly IWriteRepository<User> _writeRepository;
        private readonly IReadRepository<User> _readRepository;

        public UserService(IWriteRepository<User> writeRepository, IReadRepository<User> readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<User> GetUser(int id, string tenantId)
        {
            return await _readRepository.GetById(id, tenantId);
        }

        public async Task<IEnumerable<User>> GetAllUsers(string tenantId)
        {
            return await _readRepository.GetAll(tenantId);
        }

        public async Task CreateUser(User user)
        {
            await _writeRepository.Add(user);
        }
    }
}
