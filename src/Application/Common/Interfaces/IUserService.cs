using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Users;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByPin(string pin,CancellationToken cancellationToken);
        ValueTask<User?> Create(CreateUserDto createUserDto,CancellationToken cancellationToken);
    }
}