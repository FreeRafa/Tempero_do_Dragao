using Microsoft.AspNetCore.Mvc.RazorPages;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Services;

namespace Tempero_do_Dragao.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly RecipeService _recipeService;
        private readonly DifficultyService _difficultyService;

        public IndexModel(RecipeService recipeService, DifficultyService difficultyService)
        {
            _recipeService = recipeService;
            _difficultyService = difficultyService;
        }

        public IEnumerable<Model.Recipe> Recipes { get; set; } = new List<Model.Recipe>();
        public IEnumerable<Difficulty> Difficulties { get; set; } = new List<Difficulty>();
        public string? Search { get; set; }
        public string? Difficulty { get; set; }

        public async Task OnGetAsync(string? search, string? difficulty)
        {
            Search = search;
            Difficulty = difficulty;

            // Carrega dificuldades para os filtros
            Difficulties = await _difficultyService.GetAllAsync();

            // Filtra receitas
            if (!string.IsNullOrEmpty(search))
            {
                Recipes = await _recipeService.SearchByNameAsync(search);
            }
            else if (!string.IsNullOrEmpty(difficulty) && int.TryParse(difficulty, out int diffId))
            {
                Recipes = await _recipeService.GetByDifficultyAsync(diffId);
            }
            else
            {
                Recipes = await _recipeService.GetAllAsync();
            }
        }
    }
}