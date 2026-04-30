using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Services;

namespace Tempero_do_Dragao.Pages.Recipes
{
    public class DetailModel : PageModel
    {
        private readonly RecipeService _recipeService;
        private readonly FavoriteService _favoriteService;
        private readonly CommentService _commentService;
        private readonly RatingService _ratingService;

        public DetailModel(
            RecipeService recipeService,
            FavoriteService favoriteService,
            CommentService commentService,
            RatingService ratingService)
        {
            _recipeService = recipeService;
            _favoriteService = favoriteService;
            _commentService = commentService;
            _ratingService = ratingService;
        }

        public Recipe? Recipe { get; set; }
        public bool IsFavorite { get; set; }
        public int UserScore { get; set; }

        public async Task OnGetAsync(int id)
        {
            Recipe = await _recipeService.GetByIdAsync(id);

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null && Recipe != null)
            {
                IsFavorite = await _favoriteService.IsFavoriteAsync(userId.Value, Recipe.Id);

                var userRating = await _ratingService.GetByUserAndRecipeAsync(userId.Value, Recipe.Id);
                UserScore = userRating?.Score ?? 0;
            }
        }

        public async Task<IActionResult> OnPostToggleFavoriteAsync(int recipeId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null) return Redirect("/Auth/Login");

            var isFav = await _favoriteService.IsFavoriteAsync(userId.Value, recipeId);
            if (isFav)
                await _favoriteService.RemoveAsync(userId.Value, recipeId);
            else
                await _favoriteService.AddAsync(userId.Value, recipeId);

            return Redirect($"/Recipes/Detail?id={recipeId}");
        }

        public async Task<IActionResult> OnPostAddCommentAsync(int recipeId, string content)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null) return Redirect("/Auth/Login");

            if (!string.IsNullOrWhiteSpace(content))
            {
                await _commentService.AddAsync(new Comment
                {
                    RecipeId = recipeId,
                    UserId = userId.Value,
                    Content = content,
                    CreatedAt = DateTime.Now
                });
            }

            return Redirect($"/Recipes/Detail?id={recipeId}");
        }

        public async Task<IActionResult> OnPostRateAsync(int recipeId, int score)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null) return Redirect("/Auth/Login");

            if (score >= 1 && score <= 5)
                await _ratingService.AddOrUpdateAsync(userId.Value, recipeId, score);

            return Redirect($"/Recipes/Detail?id={recipeId}");
        }
    }
}