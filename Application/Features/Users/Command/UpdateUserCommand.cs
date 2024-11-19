using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Command
{
    public record UpdateUserCommand(
        Guid Id,
          [Required(ErrorMessage = "El primer nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El primer nombre no puede exceder los 50 caracteres.")]
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "El primer nombre no puede contener números.")]
    string FirstName,

    [StringLength(50, ErrorMessage = "El segundo nombre no puede exceder los 50 caracteres.")]
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "El segundo nombre no puede contener números.")]
    string? MiddleName,

    [Required(ErrorMessage = "El primer apellido es obligatorio.")]
    [StringLength(50, ErrorMessage = "El primer apellido no puede exceder los 50 caracteres.")]
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "El primer apellido no puede contener números.")]
    string LastName,

    [StringLength(50, ErrorMessage = "El segundo apellido no puede exceder los 50 caracteres.")]
    [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "El segundo apellido no puede contener números.")]
    string? SecondLastName,

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    DateTime BirthDate,

    [Required(ErrorMessage = "El sueldo es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El sueldo no puede ser 0.")]
    decimal Salary)
        : IRequest<User?>;

}
