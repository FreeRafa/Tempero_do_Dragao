using Tempero_do_Dragao.Model;
using static Tempero_do_Dragao.Interface.InterfaceIrepository;

namespace Tempero_do_Dragao.Repositories.Interfaces
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task<IEnumerable<Favorite>> GetByUserAsync(int userId);
        Task<bool> IsFavoriteAsync(int userId, int recipeId);
        Task RemoveAsync(int userId, int recipeId);
    }
}