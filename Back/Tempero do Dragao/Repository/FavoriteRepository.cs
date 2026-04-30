using Microsoft.EntityFrameworkCore;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Repository.Tempero_do_Dragao.Repositories;
using Tempero_do_Dragao.Data;
using Tempero_do_Dragao.Repositories.Interfaces;

namespace Tempero_do_Dragao.Repositories
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Favorite>> GetByUserAsync(int userId)
            => await _dbSet
                .Where(f => f.UserId == userId)
                .Include(f => f.Recipe)
                    .ThenInclude(r => r.Category)
                .Include(f => f.Recipe)
                    .ThenInclude(r => r.Difficulty)
                .ToListAsync();

        public async Task<bool> IsFavoriteAsync(int userId, int recipeId)
            => await _dbSet.AnyAsync(f => f.UserId == userId && f.RecipeId == recipeId);

        public async Task RemoveAsync(int userId, int recipeId)
        {
            var favorite = await _dbSet
                .FirstOrDefaultAsync(f => f.UserId == userId && f.RecipeId == recipeId);

            if (favorite is not null)
            {
                _dbSet.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }
    }
}