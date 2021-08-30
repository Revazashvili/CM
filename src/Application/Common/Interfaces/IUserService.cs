using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Users;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Asynchronously returns the first user of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">Condition for searching user.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        Task<User?> FirstOrDefaultAsync(Expression<Func<User,bool>> predicate,CancellationToken cancellationToken);
        /// <summary>
        /// Asynchronously creates new user.
        /// </summary>
        /// <param name="createUserDto">The Request object for creating user.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> instance.</param>
        /// <returns></returns>
        ValueTask<User?> CreateAsync(CreateUserDto createUserDto,CancellationToken cancellationToken);
    }
}