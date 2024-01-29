using Microsoft.EntityFrameworkCore;
using Newshore.Viajes.Repository.Data;
using Newshore.Viajes.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly AppDbContext _appDBcontext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ApiDBcontext"></param>
        public GenericRepository(AppDbContext appDBcontext)
        {
            _appDBcontext = appDBcontext;
        }
        /// <summary>
        /// Agrega un registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> Add(TEntity entity)
        {
            await _appDBcontext.AddAsync(entity);
            await _appDBcontext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        ///  Agrega un rango de registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> AddRange(List<TEntity> entity)
        {
            await _appDBcontext.AddRangeAsync(entity);
            await _appDBcontext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Borra registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Delete(TEntity entity)
        {
            var addedEntry = _appDBcontext.Remove(entity);
            return await _appDBcontext.SaveChangesAsync();
        }

        /// <summary>
        /// Ontiene un registro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _appDBcontext.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        /// <summary>
        /// Ontiene na lista de registros
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return await (filter == null ? _appDBcontext.Set<TEntity>().ToListAsync() : _appDBcontext.Set<TEntity>().Where(filter).ToListAsync());
        }

        /// <summary>
        /// Actualiza registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> Update(TEntity entity)
        {
            var addedEntry = _appDBcontext.Update(entity);
            await _appDBcontext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Actualiza un rango de registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> UpdateRange(List<TEntity> entity)
        {
            _appDBcontext.UpdateRange(entity);
            await _appDBcontext.SaveChangesAsync();
            return entity;
        }
    }
}
