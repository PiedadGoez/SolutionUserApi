using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Command
{
    public record CreateUserCommand(
    string FirstName,
    string? MiddleName,
    string LastName,
    string? SecondLastName,
    DateTime BirthDate,
    decimal Salary
) : IRequest<Guid>;
}
