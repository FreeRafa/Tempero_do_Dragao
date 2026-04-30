using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Repositories.Interfaces;

namespace Tempero_do_Dragao.Services
{
    public class FavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<Favorite>> GetByUserAsync(int userId)
            => await _favoriteRepository.GetByUserAsync(userId);

        public async Task<bool> IsFavoriteAsync(int userId, int recipeId)
            => await _favoriteRepository.IsFavoriteAsync(userId, recipeId);

        public async Task AddAsync(int userId, int recipeId)
        {
            // Verifica se já existe antes de adicionar
            if (await _favoriteRepository.IsFavoriteAsync(userId, recipeId))
                return;

            await _favoriteRepository.AddAsync(new Favorite
            {
                UserId = userId,
                RecipeId = recipeId
            });
        }

        public async Task RemoveAsync(int userId, int recipeId)
            => await _favoriteRepository.RemoveAsync(userId, recipeId);
    }
}