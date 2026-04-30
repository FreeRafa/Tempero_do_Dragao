using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Repositories.Interfaces;

namespace Tempero_do_Dragao.Services
{
    public class RatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<IEnumerable<Rating>> GetByRecipeAsync(int recipeId)
            => await _ratingRepository.GetByRecipeAsync(recipeId);

        public async Task<double> GetAverageScoreAsync(int recipeId)
            => await _ratingRepository.GetAverageScoreAsync(recipeId);

        public async Task AddOrUpdateAsync(int userId, int recipeId, int score)
        {
            // Validação do score
            if (score < 1 || score > 5)
                throw new ArgumentException("Score tem de ser entre 1 e 5.");

            var existing = await _ratingRepository.GetByUserAndRecipeAsync(userId, recipeId);

            if (existing is not null)
            {
                // Atualiza o score existente
                existing.Score = score;
                await _ratingRepository.UpdateAsync(existing);
            }
            else
            {
                // Cria um novo rating
                await _ratingRepository.AddAsync(new Rating
                {
                    UserId = userId,
                    RecipeId = recipeId,
                    Score = score
                });
            }
        }

        public async Task DeleteAsync(int id)
            => await _ratingRepository.DeleteAsync(id);

        public async Task<Rating?> GetByUserAndRecipeAsync(int userId, int recipeId)
            => await _ratingRepository.GetByUserAndRecipeAsync(userId, recipeId);

        public async Task<IEnumerable<Rating>> GetAllAsync()
            => await _ratingRepository.GetAllAsync();
    }
}