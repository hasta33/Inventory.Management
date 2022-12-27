using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Models;
using System.Linq.Expressions;

namespace InventoryManagement.Core.Services
{
    /* CustomResponseDto şeklinde yapmak için */
    public interface IServiceWithDto<Entity, Dto> where Entity : BaseEntity where Dto : class
    {
        Task<CustomResponseDto<Dto>> GetByIdAsync(int id);
        Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync();
        Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDto<Dto>> AddAsync(Dto dto);
        Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtoList);
        Task<CustomResponseDto<NoContent>> UpdateAsync(Dto dto);


        Task<CustomResponseDto<NoContent>> RemoveAsync(int id);
        Task<CustomResponseDto<NoContent>> RemoveRangeAsync(IEnumerable<int> idList); 
    }
}
