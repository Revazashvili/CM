using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Users;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;

        public UserService(IApplicationDbContext context) => _context = context;
        public async Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate,
            CancellationToken cancellationToken) => await _context
            .Users
            .FirstOrDefaultAsync(predicate, cancellationToken);

        public async ValueTask<User?> CreateAsync(CreateUserDto createUserDto,CancellationToken cancellationToken)
        {
            var user = new User(createUserDto.Pin, createUserDto.FirstName, createUserDto.LastName);
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}