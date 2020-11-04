using Application.ViewModel.Password;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPasswordService : IAsyncService
        <Password, PasswordAddDto, PasswordEditDto, PasswordGetDto>
    {
        Task<List<PasswordGetDto>> GetPasswordsAsync(string userId);

        Task<bool> ExistsAsync(string userId, string passwordId);

        Task<PasswordGetDto> AddAsync(string userId, PasswordAddDto passwordAddDto);

        Task<PasswordGetDto> UpdateAsync(string userId, string passwordId, PasswordEditDto passwordEditDto);
    }
}