using DapperUserCRUD.Objects.Entity;

namespace DapperUserCRUD.DataAccess.Repository.Interface
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);

        Task DeleteAsync(Guid id);

        Task<User> GetByIdAsync(Guid id);

        Task<IEnumerable<User>> GetUsersAsync();

        Task UpdateAsync(User user);

        Task<User> GetByLoginAsync(string login);
    }
}
