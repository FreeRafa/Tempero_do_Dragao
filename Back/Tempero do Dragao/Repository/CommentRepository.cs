using Microsoft.EntityFrameworkCore;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Data;
using Tempero_do_Dragao.Repositories;
using Tempero_do_Dragao.Repositories.Interfaces;

namespace Tempero_do_Dragao.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Comment>> GetByRecipeAsync(int recipeId)
            => await _dbSet
                .Where(c => c.RecipeId == recipeId)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

        public async Task<IEnumerable<Comment>> GetByUserAsync(int userId)
            => await _dbSet
                .Where(c => c.UserId == userId)
                .Include(c => c.Recipe)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
    }
}