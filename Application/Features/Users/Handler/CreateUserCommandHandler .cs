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
    public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand, Guid>
    {

        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Salary <= 0)
            {
                throw new ArgumentException("El sueldo no puede ser 0.");
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                SecondLastName = request.SecondLastName,
                BirthDate = request.BirthDate,
                Salary = request.Salary,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user, cancellationToken);
            return user.Id;
        }


    }
}
