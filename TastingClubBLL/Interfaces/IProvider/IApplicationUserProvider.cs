namespace TastingClubBLL.Interfaces.IProvider
{
    public interface IApplicationUserProvider
    {
        string GetUserEmail();
        Task<string> GetUserIdAsync();
    }
}