using Tempero_do_Dragao.Model;
using static Tempero_do_Dragao.Interface.InterfaceIrepository;

namespace Tempero_do_Dragao.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByEmailAndPasswordAsync(string email, string password);
        Task<bool> EmailExistsAsync(string email);
    }
}