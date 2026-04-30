using Tempero_do_Dragao.Model;
using static Tempero_do_Dragao.Interface.InterfaceIrepository;

namespace Tempero_do_Dragao.Interface
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetAllWithDetailsAsync();
        Task<Recipe?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Recipe>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Recipe>> GetByDifficultyAsync(int difficultyId);
        Task<IEnumerable<Recipe>> GetByUserAsync(int userId);
        Task<IEnumerable<Recipe>> SearchByNameAsync(string name);
    }
}
