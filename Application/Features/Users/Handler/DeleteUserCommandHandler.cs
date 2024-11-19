using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handler
{
    public record DeleteUserCommand(Guid Id) : IRequest<bool?>;
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool?>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteUserAsync(request.Id, cancellationToken);
            return result;
        }
    }
}
