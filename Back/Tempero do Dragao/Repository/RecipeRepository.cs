using Tempero_do_Dragao.Interface;
using Tempero_do_Dragao.Data;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tempero_do_Dragao.Repository
{
    namespace Tempero_do_Dragao.Repositories
    {
        public class RecipeRepository : Repository<Recipe>, IRecipeRepository
        {
            public RecipeRepository(AppDbContext context) : base(context) { }

            public async Task<IEnumerable<Recipe>> GetAllWithDetailsAsync()
                => await _dbSet
                    .Include(r => r.User)
                    .Include(r => r.Category)
                    .Include(r => r.Difficulty)
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Measurement)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

            public async Task<Recipe?> GetByIdWithDetailsAsync(int id)
                => await _dbSet
                    .Include(r => r.User)
                    .Include(r => r.Category)
                    .Include(r => r.Difficulty)
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Measurement)
                    .Include(r => r.Comments)
                        .ThenInclude(c => c.User)
                    .Include(r => r.Ratings)
                    .FirstOrDefaultAsync(r => r.Id == id);

            public async Task<IEnumerable<Recipe>> GetByCategoryAsync(int categoryId)
                => await _dbSet
                    .Where(r => r.CategoryId == categoryId)
                    .Include(r => r.Category)
                    .Include(r => r.Difficulty)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

            public async Task<IEnumerable<Recipe>> GetByDifficultyAsync(int difficultyId)
                => await _dbSet
                    .Where(r => r.DifficultyId == difficultyId)
                    .Include(r => r.Category)
                    .Include(r => r.Difficulty)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

            public async Task<IEnumerable<Recipe>> GetByUserAsync(int userId)
                => await _dbSet
                    .Where(r => r.UserId == userId)
                    .Include(r => r.Category)
                    .Include(r => r.Difficulty)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

            public async Task<IEnumerable<Recipe>> SearchByNameAsync(string name)
                => await _dbSet
                    .Where(r => r.Name.Contains(name))
                    .Include(r => r.Category)
                    .Include(r => r.Difficulty)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();
        }
    }
}
