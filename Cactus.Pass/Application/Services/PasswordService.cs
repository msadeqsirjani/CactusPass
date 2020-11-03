using Application.Interfaces;
using Application.ViewModel.Password;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PasswordService : AsyncService
        <Password, PasswordAddDto, PasswordEditDto, PasswordGetDto>,
        IPasswordService
    {
        protected readonly IPasswordRepository PasswordRepository;
        protected readonly string CryptoPassword;

        public PasswordService(IPasswordRepository repository, IMapper mapper, IConfiguration config) : base(repository,
            mapper)
        {
            PasswordRepository = repository;
            CryptoPassword = config["Crypto:Password"];
        }

        public virtual async Task<List<PasswordGetDto>> GetPasswordsAsync(string userId)
            => await PasswordRepository.SelectUserPasswords(userId)
                .Select(p => MapEntityToGetDto(p))
                .ToListAsync();

        public async Task<bool> ExistsAsync(string userId, string passwordId)
            => await PasswordRepository.ExistsAsync
                (p => p.Id == passwordId && p.UserId == userId);

        public async Task<PasswordGetDto> AddAsync(string userId, PasswordAddDto passwordAddDto)
        {
            var entity = MapAddDtoToEntity(passwordAddDto);

            entity.UserId = userId;

            return await AddAsync(entity);
        }

        public async Task<PasswordGetDto> UpdateAsync(string userId, string passwordId, PasswordEditDto passwordEditDto)
        {
            var entity = MapEditDtoToEntity(passwordEditDto);

            entity.UserId = userId;

            return await UpdateAsync(passwordId, entity);
        }
    }
}
