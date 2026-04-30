using Tempero_do_Dragao.Model;
using static Tempero_do_Dragao.Interface.InterfaceIrepository;

namespace Tempero_do_Dragao.Repositories.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByRecipeAsync(int recipeId);
        Task<IEnumerable<Comment>> GetByUserAsync(int userId);
    }
}