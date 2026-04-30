using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Services;

namespace Tempero_do_Dragao.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly RecipeService _recipeService;
        private readonly CategoryService _categoryService;
        private readonly DifficultyService _difficultyService;
        private readonly IngredientService _ingredientService;
        private readonly MeasurementService _measurementService;

        public EditModel(
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
        public EditRecipeInput Input { get; set; } = new();

        [BindProperty]
        public List<IngredientInput> Ingredients { get; set; } = new();

        public SelectList Categories { get; set; } = default!;
        public SelectList Difficulties { get; set; } = default!;
        public SelectList IngredientsList { get; set; } = default!;
        public SelectList Measurements { get; set; } = default!;
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null) return Redirect("/Auth/Login");

            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe is null) return Redirect("/Recipes/Index");

            // Só o autor ou admin pode editar
            var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
            if (recipe.UserId != userId && !isAdmin)
                return Redirect("/Recipes/Index");

            // Preenche o formulário com os dados existentes
            Input = new EditRecipeInput
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                PreparationMethod = recipe.PreparationMethod,
                PreparationTime = recipe.PreparationTime,
                CategoryId = recipe.CategoryId,
                DifficultyId = recipe.DifficultyId
            };

            // Preenche ingredientes existentes
            Ingredients = recipe.RecipeIngredients.Select(ri => new IngredientInput
            {
                IngredientId = ri.IngredientId,
                Quantity = ri.Quantity,
                MeasurementId = ri.MeasurementId
            }).ToList();

            if (!Ingredients.Any())
                Ingredients.Add(new IngredientInput());

            await LoadSelectListsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null) return Redirect("/Auth/Login");

            if (!ModelState.IsValid)
            {
                await LoadSelectListsAsync();
                return Page();
            }

            var recipe = await _recipeService.GetByIdAsync(Input.Id);
            if (recipe is null) return Redirect("/Recipes/Index");

            var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
            if (recipe.UserId != userId && !isAdmin)
                return Redirect("/Recipes/Index");

            // Atualiza os dados
            recipe.Name = Input.Name;
            recipe.Description = Input.Description;
            recipe.PreparationMethod = Input.PreparationMethod;
            recipe.PreparationTime = Input.PreparationTime;
            recipe.CategoryId = Input.CategoryId;
            recipe.DifficultyId = Input.DifficultyId;

            // Atualiza ingredientes
            recipe.RecipeIngredients = Ingredients
                .Where(i => i.IngredientId > 0 && i.MeasurementId > 0 && i.Quantity > 0)
                .Select(i => new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = i.IngredientId,
                    MeasurementId = i.MeasurementId,
                    Quantity = i.Quantity
                }).ToList();

            await _recipeService.UpdateAsync(recipe);

            return Redirect($"/Recipes/Detail?id={recipe.Id}");
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

    public class EditRecipeInput
    {
        public int Id { get; set; }

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

    
}