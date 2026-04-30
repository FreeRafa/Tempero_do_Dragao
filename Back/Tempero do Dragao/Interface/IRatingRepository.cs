using Tempero_do_Dragao.Model;
using static Tempero_do_Dragao.Interface.InterfaceIrepository;

namespace Tempero_do_Dragao.Repositories.Interfaces
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Task<IEnumerable<Rating>> GetByRecipeAsync(int recipeId);
        Task<Rating?> GetByUserAndRecipeAsync(int userId, int recipeId);
        Task<double> GetAverageScoreAsync(int recipeId);
    }
}