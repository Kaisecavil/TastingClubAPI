using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Interfaces
{
    public interface IBaseRepository<DbModel> where DbModel : BaseModel
    {
        void Create(DbModel model);
        void CreateRange(IEnumerable<DbModel> models);
        void Delete(int id);
        void DeleteRange(IEnumerable<DbModel> models);
        DbModel Get(int id);
        IEnumerable<DbModel> GetAll(bool asNoTraking = false);
        IQueryable<DbModel> GetAllQueryable(bool asNoTraking = false);
        void Update(DbModel model);
        Task CreateAsync(DbModel model);
        Task CreateRangeAsync(IEnumerable<DbModel> models);
        Task DeleteAsync(int id);
        Task<bool> EntityExistsAsync(int id);
        Task<IEnumerable<DbModel>> GetAllAsync(bool asNoTraking = false);
        Task<DbModel> GetAsync(int id, bool asNoTraking = false);
        Task<bool> IsEmptyAsync();
        Task UpdateAsync(DbModel model);
    }
}
