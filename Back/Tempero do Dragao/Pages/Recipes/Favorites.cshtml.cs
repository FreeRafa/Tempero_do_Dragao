using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Services;

namespace Tempero_do_Dragao.Pages.Recipes
{
    public class FavoritesModel : PageModel
    {
        private readonly FavoriteService _favoriteService;

        public FavoritesModel(FavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        public IEnumerable<Favorite> Favorites { get; set; } = new List<Favorite>();

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
                return Redirect("/Auth/Login");

            Favorites = await _favoriteService.GetByUserAsync(userId.Value);
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int recipeId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
                return Redirect("/Auth/Login");

            await _favoriteService.RemoveAsync(userId.Value, recipeId);
            return RedirectToPage();
        }
    }
}