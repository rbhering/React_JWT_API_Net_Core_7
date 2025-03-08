using api.Models.Entities;
using System.Threading.Tasks;

namespace api.Repositories
{
    public interface IUserRepository
    {
        Task<int> Delete(int id);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User> GetById(int id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByRefreshTokenAsync(string refreshToken);
        Task CommitAsync();
    }
}
