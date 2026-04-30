using Microsoft.EntityFrameworkCore;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Repositories.Interfaces;
using Tempero_do_Dragao.Data;

namespace Tempero_do_Dragao.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.AsQueryable().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
            => await _dbSet.AsQueryable().FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        public async Task<bool> EmailExistsAsync(string email)
            => await _dbSet.AsQueryable().AnyAsync(u => u.Email == email);
    }
}