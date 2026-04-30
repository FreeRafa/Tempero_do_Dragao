using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Services;

namespace Tempero_do_Dragao.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly RecipeService _recipeService;
        private readonly CategoryService _categoryService;
        private readonly DifficultyService _difficultyService;
        private readonly IngredientService _ingredientService;
        private readonly MeasurementService _measurementService;

        public CreateModel(
            RecipeService recipeService,
            CategoryService categoryService,
            DifficultyService difficultyService,
            IngredientService ingredientService,
            MeasurementService measurementService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
            _difficultyService = difficultyService;
            _ingredientService = ingredientService;
            _measurementService = measurementService;
        }

        [BindProperty]
        public RecipeInput Input { get; set; } = new();

        [BindProperty]
        public List<IngredientInput> Ingredients { get; set; } = new();

        public SelectList Categories { get; set; } = default!;
        public SelectList Difficulties { get; set; } = default!;
        public SelectList IngredientsList { get; set; } = default!;
        public SelectList Measurements { get; set; } = default!;
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Redireciona se não estiver autenticado
            if (HttpContext.Session.GetInt32("UserId") is null)
                return Redirect("/Auth/Login");

            await LoadSelectListsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
                return Redirect("/Auth/Login");

            if (!ModelState.IsValid)
            {
                await LoadSelectListsAsync();
                return Page();
            }

            // Cria a receita
            var recipe = new Recipe
            {
                Name = Input.Name,
                Description = Input.Description,
                PreparationMethod = Input.PreparationMethod,
                PreparationTime = Input.PreparationTime,
                CategoryId = Input.CategoryId,
                DifficultyId = Input.DifficultyId,
                UserId = userId.Value,
                Status = 1,
                CreatedAt = DateTime.Now
            };

            // Adiciona ingredientes válidos
            var validIngredients = Ingredients
                .Where(i => i.IngredientId > 0 && i.MeasurementId > 0 && i.Quantity > 0)
                .Select(i => new RecipeIngredient
                {
                    IngredientId = i.IngredientId,
                    MeasurementId = i.MeasurementId,
                    Quantity = i.Quantity
                }).ToList();

            recipe.RecipeIngredients = validIngredients;

            await _recipeService.AddAsync(recipe);

            return Redirect("/Recipes/Index");
        }

        private async Task LoadSelectListsAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            var difficulties = await _difficultyService.GetAllAsync();
            var ingredients = await _ingredientService.GetAllAsync();
            var measurements = await _measurementService.GetAllAsync();

            Categories = new SelectList(categories, "Id", "Name");
            Difficulties = new SelectList(difficulties, "Id", "Level");
            IngredientsList = new SelectList(ingredients, "Id", "Name");
            Measurements = new SelectList(measurements, "Id", "Unit");
        }
    }

    public class RecipeInput
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "O modo de preparação é obrigatório.")]
        public string PreparationMethod { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tempo de preparação é obrigatório.")]
        [Range(1, 9999, ErrorMessage = "Tempo inválido.")]
        public int PreparationTime { get; set; }

        [Required(ErrorMessage = "Seleciona uma categoria.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Seleciona uma dificuldade.")]
        public int DifficultyId { get; set; }
    }

    public class IngredientInput
    {
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public int MeasurementId { get; set; }
    }
}