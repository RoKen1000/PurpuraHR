using AutoMapper;
using Purpura.Common;
using Purpura.DataAccess.DataContext;
using Purpura.Repositories.Interfaces;
using Purpura.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IMapper mapper, IBaseRepository<TEntity> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public async Task<Result> Delete(TEntity viewmodel)
        {
            var result = await _baseRepository.Delete(viewmodel);

            if (result > 0)
                return Result.Success();
            else 
                return Result.Failure("Delete failed.");
        }

        public async Task<Result> Edit(TEntity viewmodel)
        {
            var result = _baseRepository.Update(viewmodel);

            if (result > 0)
                return Result.Success();
            else
                return Result.Failure("Delete failed.");
        }

        public async Task<Result> Create(TEntity viewmodel)
        {
            var result = _baseRepository.Create(viewmodel);

            if (result > 0)
                return Result.Success();
            else
                return Result.Failure("Delete failed.");
        }

        public Task<IEnumerable<TEntity>> GetAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return _baseRepository.GetAll(filter);
        }

        public Task<TEntity> GetSingle(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return _baseRepository.GetSingle(filter);
        }
    }
}
