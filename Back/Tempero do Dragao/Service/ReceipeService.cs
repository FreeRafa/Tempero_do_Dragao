using Tempero_do_Dragao.Interface;
using Tempero_do_Dragao.Model;

namespace Tempero_do_Dragao.Services
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
            => await _recipeRepository.GetAllWithDetailsAsync();

        public async Task<Recipe?> GetByIdAsync(int id)
            => await _recipeRepository.GetByIdWithDetailsAsync(id);

        public async Task<IEnumerable<Recipe>> GetByCategoryAsync(int categoryId)
            => await _recipeRepository.GetByCategoryAsync(categoryId);

        public async Task<IEnumerable<Recipe>> GetByDifficultyAsync(int difficultyId)
            => await _recipeRepository.GetByDifficultyAsync(difficultyId);

        public async Task<IEnumerable<Recipe>> GetByUserAsync(int userId)
            => await _recipeRepository.GetByUserAsync(userId);

        public async Task<IEnumerable<Recipe>> SearchByNameAsync(string name)
            => await _recipeRepository.SearchByNameAsync(name);

        public async Task AddAsync(Recipe recipe)
        {
            recipe.CreatedAt = DateTime.Now;
            recipe.Status = 0;
            await _recipeRepository.AddAsync(recipe);
        }

        public async Task UpdateAsync(Recipe recipe)
            => await _recipeRepository.UpdateAsync(recipe);

        public async Task DeleteAsync(int id)
            => await _recipeRepository.DeleteAsync(id);
    }
}