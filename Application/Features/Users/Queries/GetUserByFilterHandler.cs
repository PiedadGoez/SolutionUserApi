using Application.Common;
using Application.Interfaces;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{

    public record UsersQuery(string? FirstName, string? LastName, int Page, int PageSize) : IRequest<Pagination<User>>;
    public class GetUserByFilterHandler : IRequestHandler<UsersQuery, Pagination<User>>
    {

        private readonly IUserRepository _userRepository;

        public GetUserByFilterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Pagination<User>> Handle(UsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByFilterAsync(request.FirstName, request.LastName, request.Page, request.PageSize, cancellationToken);
        }
    }
}
