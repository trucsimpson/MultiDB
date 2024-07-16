using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiDB.Application.Users.Commands;
using MultiDB.Application.Users.Queries;

namespace MultiDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id, [FromQuery] string tenantId)
        {
            var query = new GetUserQuery { Id = id, TenantId = tenantId };
            var user = await _mediator.Send(query);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] string tenantId)
        {
            var query = new GetAllUsersQuery { TenantId = tenantId };
            var users = await _mediator.Send(query);
            return Ok(users);
        }
    }
}
