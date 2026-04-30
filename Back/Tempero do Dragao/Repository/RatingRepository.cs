using Microsoft.EntityFrameworkCore;
using Tempero_do_Dragao.Model;

using Tempero_do_Dragao.Data;
using Tempero_do_Dragao.Repositories.Interfaces;

namespace Tempero_do_Dragao.Repositories
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Rating>> GetByRecipeAsync(int recipeId)
            => await _dbSet
                .Where(r => r.RecipeId == recipeId)
                .Include(r => r.User)
                .ToListAsync();

        public async Task<Rating?> GetByUserAndRecipeAsync(int userId, int recipeId)
            => await _dbSet
                .FirstOrDefaultAsync(r => r.UserId == userId && r.RecipeId == recipeId);

        public async Task<double> GetAverageScoreAsync(int recipeId)
        {
            var ratings = await _dbSet
                .Where(r => r.RecipeId == recipeId)
                .ToListAsync();

            return ratings.Any() ? ratings.Average(r => r.Score) : 0;
        }
    }
}   