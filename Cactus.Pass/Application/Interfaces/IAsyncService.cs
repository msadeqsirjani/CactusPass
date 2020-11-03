using Application.ViewModel.Common;
using Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAsyncService<in TEntity, in TEntityAddDto, TEntityEditDto, TEntityGetDto>
    where TEntity : BaseEntity
    where TEntityAddDto : EntityAddDto
    where TEntityEditDto : EntityEditDto
    {
        Task<IEnumerable<TEntityGetDto>> SelectAsEnumerableAsync();

        IQueryable<TEntityGetDto> SelectAsync();

        Task<TEntityGetDto> SelectAsync(string id);

        Task<TEntityEditDto> WatchAsync(string id);

        Task<TEntityGetDto> AddAsync(TEntityAddDto entityAddDto);

        Task<TEntityGetDto> AddAsync(TEntity entity);

        Task<TEntityGetDto> UpdateAsync(string id, TEntityEditDto entityEditDto);

        Task<TEntityGetDto> UpdateAsync(string id, TEntity entity);

        Task<TEntityGetDto> UpdateAsync(TEntity entity);

        Task DeleteAsync(string id);

        Task<bool> ExistsAsync(string id);
    }
}
