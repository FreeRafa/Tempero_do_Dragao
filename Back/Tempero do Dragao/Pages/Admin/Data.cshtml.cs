using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Services;

namespace Tempero_do_Dragao.Pages.Admin
{
    public class DataModel : PageModel
    {
        private readonly CategoryService _categoryService;
        private readonly DifficultyService _difficultyService;
        private readonly IngredientService _ingredientService;
        private readonly MeasurementService _measurementService;

        public DataModel(
            CategoryService categoryService,
            DifficultyService difficultyService,
            IngredientService ingredientService,
            MeasurementService measurementService)
        {
            _categoryService = categoryService;
            _difficultyService = difficultyService;
            _ingredientService = ingredientService;
            _measurementService = measurementService;
        }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Difficulty> Difficulties { get; set; } = new List<Difficulty>();
        public IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public IEnumerable<Measurement> Measurements { get; set; } = new List<Measurement>();
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");
            await LoadDataAsync();
            return Page();
        }

        // ── CATEGORY ────────────────────────────────────────────
        public async Task<IActionResult> OnPostAddCategoryAsync(string name)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            if (!string.IsNullOrWhiteSpace(name))
            {
                await _categoryService.AddAsync(new Category { Name = name.Trim() });
                SuccessMessage = $"Categoria '{name}' adicionada!";
            }

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteCategoryAsync(int id)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            await _categoryService.DeleteAsync(id);
            SuccessMessage = "Categoria apagada!";
            await LoadDataAsync();
            return Page();
        }

        // ── DIFFICULTY ──────────────────────────────────────────
        public async Task<IActionResult> OnPostAddDifficultyAsync(string level)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            if (!string.IsNullOrWhiteSpace(level))
            {
                await _difficultyService.AddAsync(new Difficulty { Level = level.Trim() });
                SuccessMessage = $"Dificuldade '{level}' adicionada!";
            }

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteDifficultyAsync(int id)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            await _difficultyService.DeleteAsync(id);
            SuccessMessage = "Dificuldade apagada!";
            await LoadDataAsync();
            return Page();
        }

        // ── INGREDIENT ──────────────────────────────────────────
        public async Task<IActionResult> OnPostAddIngredientAsync(string name)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            if (!string.IsNullOrWhiteSpace(name))
            {
                await _ingredientService.AddAsync(new Ingredient { Name = name.Trim() });
                SuccessMessage = $"Ingrediente '{name}' adicionado!";
            }

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteIngredientAsync(int id)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            await _ingredientService.DeleteAsync(id);
            SuccessMessage = "Ingrediente apagado!";
            await LoadDataAsync();
            return Page();
        }

        // ── MEASUREMENT ─────────────────────────────────────────
        public async Task<IActionResult> OnPostAddMeasurementAsync(string unit)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            if (!string.IsNullOrWhiteSpace(unit))
            {
                await _measurementService.AddAsync(new Measurement { Unit = unit.Trim() });
                SuccessMessage = $"Medida '{unit}' adicionada!";
            }

            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteMeasurementAsync(int id)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            await _measurementService.DeleteAsync(id);
            SuccessMessage = "Medida apagada!";
            await LoadDataAsync();
            return Page();
        }

        // ── HELPERS ─────────────────────────────────────────────
        private async Task LoadDataAsync()
        {
            Categories = await _categoryService.GetAllAsync();
            Difficulties = await _difficultyService.GetAllAsync();
            Ingredients = await _ingredientService.GetAllAsync();
            Measurements = await _measurementService.GetAllAsync();
        }

        private bool IsAdmin()
            => HttpContext.Session.GetString("IsAdmin") == "True";
    }
}