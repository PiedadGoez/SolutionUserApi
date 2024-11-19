using Application.Features.Users.Command;
using Application.Features.Users.Handler;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateUser), new { id = userId });
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("GetUserByFilter")]
        public async Task<IActionResult> GetUserByFilter([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new UsersQuery(firstName, lastName, page, pageSize));
            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserCommand command)
        {
            if (id != command.Id) return BadRequest("El ID del usuario no coincide.");

            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            return NoContent();
        }


        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));
            if (result == false)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            return NoContent();
        }



    }
}
