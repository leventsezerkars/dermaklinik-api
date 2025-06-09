using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Application.Features.User.Commands.ChangePassword;
using DermaKlinik.API.Application.Features.User.Commands.CreateUser;
using DermaKlinik.API.Application.Features.User.Commands.DeleteUser;
using DermaKlinik.API.Application.Features.User.Commands.Login;
using DermaKlinik.API.Application.Features.User.Commands.UpdateUser;
using DermaKlinik.API.Application.Features.User.Queries.GetAllUsers;
using DermaKlinik.API.Application.Features.User.Queries.GetUserByEmail;
using DermaKlinik.API.Application.Features.User.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("email/{email}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetByEmail(string email)
        {
            var query = new GetUserByEmailQuery { Email = email };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto createUserDto)
        {
            var command = new CreateUserCommand { CreateUserDto = createUserDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id)
                return BadRequest();

            var command = new UpdateUserCommand { UpdateUserDto = updateUserDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            var command = new LoginCommand { LoginDto = loginDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}