using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Services;

namespace Tempero_do_Dragao.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;
        private readonly RecipeService _recipeService;
        private readonly CommentService _commentService;
        private readonly RatingService _ratingService;

        public IndexModel(
            UserService userService,
            RecipeService recipeService,
            CommentService commentService,
            RatingService ratingService)
        {
            _userService = userService;
            _recipeService = recipeService;
            _commentService = commentService;
            _ratingService = ratingService;
        }

        public IEnumerable<User> Users { get; set; } = new List<User>();
        public IEnumerable<Recipe> Recipes { get; set; } = new List<Recipe>();
        public int TotalUsers { get; set; }
        public int TotalRecipes { get; set; }
        public int TotalComments { get; set; }
        public int TotalRatings { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");
            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostToggleUserStatusAsync(int userId)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            var user = await _userService.GetByIdAsync(userId);
            if (user != null)
            {
                user.Status = user.Status == 1 ? 0 : 1;
                await _userService.UpdateAsync(user);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(int userId)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            await _userService.DeleteAsync(userId);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteRecipeAsync(int recipeId)
        {
            if (!IsAdmin()) return Redirect("/Recipes/Index");

            await _recipeService.DeleteAsync(recipeId);
            return RedirectToPage();
        }

        private async Task LoadDataAsync()
        {
            Users = await _userService.GetAllAsync();
            Recipes = await _recipeService.GetAllAsync();

            var allComments = await _commentService.GetAllAsync();
            var allRatings = await _ratingService.GetAllAsync();

            TotalUsers = Users.Count();
            TotalRecipes = Recipes.Count();
            TotalComments = allComments.Count();
            TotalRatings = allRatings.Count();
        }

        private bool IsAdmin()
            => HttpContext.Session.GetString("IsAdmin") == "True";
    }
}