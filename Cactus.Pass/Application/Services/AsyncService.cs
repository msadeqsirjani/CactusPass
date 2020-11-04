using Application.Interfaces;
using Application.ViewModel.Common;
using AutoMapper;
using Domain.Common;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public abstract class AsyncService<TEntity, TEntityAddDto, TEntityEditDto, TEntityGetDto>
        : IAsyncService<TEntity, TEntityAddDto, TEntityEditDto, TEntityGetDto>
        where TEntity : BaseEntity
        where TEntityAddDto : EntityAddDto
        where TEntityEditDto : EntityEditDto
        where TEntityGetDto : EntityGetDto
    {
        private readonly IAsyncRepository<TEntity> _repository;

        protected readonly IMapper Mapper;

        protected AsyncService(IAsyncRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<TEntityGetDto>> SelectAsEnumerableAsync() =>
            await SelectAsync().ToListAsync();

        public virtual IQueryable<TEntityGetDto> SelectAsync() => _repository.SelectAsync()
            .Select(e => MapEntityToGetDto(e));

        public virtual async Task<TEntityGetDto> SelectAsync(string id)
        {
            var entity = await _repository.SelectAsync(id);

            return MapEntityToGetDto(entity);
        }

        public virtual async Task<TEntityEditDto> WatchAsync(string id)
        {
            var entity = await _repository.SelectAsync(id);

            return MapEntityToEditDto(entity);
        }

        public virtual async Task<TEntityGetDto> AddAsync(TEntityAddDto entityAddDto)
        {
            var entity = MapAddDtoToEntity(entityAddDto);

            return await AddAsync(entity);
        }

        public virtual async Task<TEntityGetDto> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);

            return MapEntityToGetDto(entity);
        }

        public virtual async Task<TEntityGetDto> UpdateAsync(string id, TEntityEditDto entityEditDto)
        {
            var entity = MapEditDtoToEntity(entityEditDto);

            return await UpdateAsync(id, entity);
        }

        public virtual async Task<TEntityGetDto> UpdateAsync(string id, TEntity entity)
        {
            var actualEntity = await _repository.SelectAsync(id);

            entity.Id = id;
            entity.CreateDateTime = actualEntity.CreateDateTime;

            return await UpdateAsync(entity);
        }

        public virtual async Task<TEntityGetDto> UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);

            return Mapper.Map<TEntity, TEntityGetDto>(entity);
        }

        public virtual async Task DeleteAsync(string id) => await _repository.DeleteAsync(id);

        public virtual async Task<bool> ExistsAsync(string id) => await _repository.ExistsAsync(id);

        #region Map Methods

        protected TDestination Map<TSource, TDestination>(TSource source)
            => Mapper.Map<TSource, TDestination>(source);

        protected TEntityGetDto MapEntityToGetDto(TEntity entity)
            => Map<TEntity, TEntityGetDto>(entity);

        protected TEntityEditDto MapEntityToEditDto(TEntity entity)
            => Map<TEntity, TEntityEditDto>(entity);

        protected TEntity MapAddDtoToEntity(TEntityAddDto entityAddDto)
            => Map<TEntityAddDto, TEntity>(entityAddDto);

        protected TEntity MapEditDtoToEntity(TEntityEditDto entityEditDto)
            => Map<TEntityEditDto, TEntity>(entityEditDto);

        #endregion
    }
}
