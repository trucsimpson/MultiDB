using MediatR;
using MultiDB.Core.Entities;
using MultiDB.Core.Repositories;

namespace MultiDB.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IWriteRepository<User> _writeRepository;

        public CreateUserCommandHandler(IWriteRepository<User> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = command.Name,
                Email = command.Email
            };

            await _writeRepository.Add(user);
            return user.Id;
        }
    }
}
