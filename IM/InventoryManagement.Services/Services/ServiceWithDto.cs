using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryManagement.Services.Services
{
    /* CustomResponseDto şeklinde yapmak için */
    public class ServiceWithDto<TEntity, Dto> : IServiceWithDto<TEntity, Dto> where TEntity : BaseEntity where Dto : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork; //protected verdiğim için üst sınıflarda da kullanabilirim.
        protected readonly IMapper _mapper;

        public ServiceWithDto(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<Dto>> AddAsync(Dto dto)
        {
            TEntity newEntity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<Dto>(dto);

            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtoList)
        {
            var newEntities = _mapper.Map<IEnumerable<TEntity>>(dtoList);

            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();

            var newDtoList = _mapper.Map<IEnumerable<Dto>>(newEntities);

            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, newDtoList);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            var anyEntity = await _repository.AnyAsync(expression);
            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK, anyEntity);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync() // Bu alana filtreleme alanı eklenebilir
        {
            var entities = await _repository.GetAll().ToListAsync();
            var dtoList = _mapper.Map<IEnumerable<Dto>>(entities);

            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtoList);
        }

        public async Task<CustomResponseDto<Dto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            var dto = _mapper.Map<Dto>(entity);

            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<CustomResponseDto<NoContent>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContent>> RemoveRangeAsync(IEnumerable<int> idList)
        {
            var entities = await _repository.Where(x => idList.Contains(x.Id)).ToListAsync();

            _repository.RemoveRange(entities);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContent>> UpdateAsync(Dto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<TEntity, bool>> expression)
        {
            var entities = await _repository.Where(expression).ToListAsync();

            var dtoList = _mapper.Map<IEnumerable<Dto>>(entities);

            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtoList);
        }

    }
}
