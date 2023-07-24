using Microsoft.EntityFrameworkCore;
using TastingClubDAL.Models.Base;
using TastingClubDAL.Interfaces;
using TastingClubDAL.Database;

namespace TastingClubDAL.Repositories
{
    public class BaseRepository<DbModel> : IBaseRepository<DbModel> where DbModel : BaseModel
    {
        protected readonly ApplicationContext _context;
        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(DbModel model)
        {
            _context.Set<DbModel>().Add(model);
        }

        public void CreateRange(IEnumerable<DbModel> models)
        {
            _context.Set<DbModel>().AddRange(models);
        }

        public void Delete(int id)
        {
            var toDelete = _context.Set<DbModel>().FirstOrDefault(m => m.Id == id);
            _context.Set<DbModel>().Remove(toDelete);
        }

        public IEnumerable<DbModel> GetAll(bool asNoTraking = false)
        {
            return asNoTraking ?
                _context.Set<DbModel>().AsNoTracking().Where(m => m.IsDeleted == false).ToList() :
                _context.Set<DbModel>().Where(m => m.IsDeleted == false).ToList();
        }

        public IQueryable<DbModel> GetAllQueryable(bool asNoTraking = false)
        {
            return asNoTraking ?
               _context.Set<DbModel>().AsNoTracking().Where(m => m.IsDeleted == false) :
               _context.Set<DbModel>().Where(m => m.IsDeleted == false);
        }

        public void Update(DbModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }

        public DbModel Get(int id)
        {
            return _context.Set<DbModel>().Where(m => m.IsDeleted == false).FirstOrDefault(m => m.Id == id);
        }

        public async Task<IEnumerable<DbModel>> GetAllAsync(bool asNoTraking = false)
        {
            return asNoTraking ?
                await _context.Set<DbModel>().AsNoTracking()
                    .Where(m => m.IsDeleted == false).ToListAsync() :
                await _context.Set<DbModel>()
                    .Where(m => m.IsDeleted == false).ToListAsync();
        }

        public async Task<DbModel> GetAsync(int id, bool asNoTraking = false)
        {
            return asNoTraking ?
                await _context.Set<DbModel>().AsNoTracking()
                    .Where(m => m.IsDeleted == false)
                    .FirstOrDefaultAsync(m => m.Id == id) :
                await _context.Set<DbModel>()
                    .Where(m => m.IsDeleted == false)
                    .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateAsync(DbModel model)
        {
            _context.Set<DbModel>().Add(model);
        }

        public async Task UpdateAsync(DbModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var toDelete = await GetAsync(id);
            _context.Set<DbModel>().Remove(toDelete);
        }

        public async Task<bool> EntityExistsAsync(int id)
        {
            return await _context.Set<DbModel>().AsNoTracking()
                .Where(m => m.IsDeleted == false)
                .AnyAsync(e => e.Id == id);
        }

        public async Task<bool> IsEmptyAsync()
        {
            return !await _context.Set<DbModel>().AsNoTracking()
                .Where(m => m.IsDeleted == false)
                .AnyAsync();
        }

        public async Task CreateRangeAsync(IEnumerable<DbModel> models)
        {
            await _context.Set<DbModel>().AddRangeAsync(models);
        }
        public void DeleteRange(IEnumerable<int> ids)
        {
            var models = _context.Set<DbModel>().Where(m => ids.Contains(m.Id))); 
            _context.Set<DbModel>().RemoveRange(models);
        }
    }

}
