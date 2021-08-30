using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Users;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using MapsterMapper;

namespace Application.Queries.Users
{
    public record GetUserQuery(Expression<Func<User,bool>> Predicate) : IRequestWrapper<UserDto>{}
    
    public class GetUserQueryHandler : IHandlerWrapper<GetUserQuery,UserDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUserService userService, IMapper mapper) =>
            (_userService, _mapper) = (userService, mapper);

        public async Task<IResponse<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FirstOrDefaultAsync(request.Predicate, cancellationToken);
            return user is null
                ? Response.Fail<UserDto>("Can't find user.")
                : Response.Success(_mapper.Map<UserDto>(user));
        }
    }
}