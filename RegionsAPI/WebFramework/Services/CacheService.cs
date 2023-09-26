using AutoMapper;
using Data;
using Data.Dtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebFramework.Services
{
    public class CacheService<TDto, TEntity>
        where TDto : BaseDto 
        where TEntity : class, IEntity
    {
        private readonly Dictionary<Int32, TDto> _cache;

        public CacheService()
        {
            _cache = new Dictionary<Int32, TDto>();
        }

        public bool Any() => _cache.Count != 0;

        public void Set(Int32 id, TDto tDto) => _cache[id] = tDto;
        
        public TDto Get(Int32 id) => _cache[id];

        public IEnumerable<TDto> GetAll() => _cache.Values;

        public async Task SaveToDatabase(ApplicationDbContext context, DbSet<TEntity> dbSet, IMapper mapper)
        {
            try
            {
                var items = GetAll();

                foreach (var item in items)
                {
                    TEntity entity = mapper.Map<TEntity>(item);
                    dbSet.Add(entity);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception) { }

        }
    }
}
