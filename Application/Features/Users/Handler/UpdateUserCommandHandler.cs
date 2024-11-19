using Application.Features.Users.Command;
using Application.Interfaces;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handler
{   
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User?>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null) throw new Exception("Usuario no encontrado.");

            user.FirstName = request.FirstName;
            user.MiddleName = request.MiddleName;
            user.LastName = request.LastName;
            user.SecondLastName = request.SecondLastName;
            user.BirthDate = request.BirthDate;
            user.Salary = request.Salary;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userRepository.UpdateAsync(user, cancellationToken);

            return result ? user : null;
        }
    }
}
