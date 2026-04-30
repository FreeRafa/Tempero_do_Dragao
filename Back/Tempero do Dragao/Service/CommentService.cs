using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Repositories.Interfaces;

namespace Tempero_do_Dragao.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetByRecipeAsync(int recipeId)
            => await _commentRepository.GetByRecipeAsync(recipeId);

        public async Task<IEnumerable<Comment>> GetByUserAsync(int userId)
            => await _commentRepository.GetByUserAsync(userId);

        public async Task AddAsync(Comment comment)
        {
            comment.CreatedAt = DateTime.Now;
            await _commentRepository.AddAsync(comment);
        }

        public async Task UpdateAsync(Comment comment)
            => await _commentRepository.UpdateAsync(comment);

        public async Task DeleteAsync(int id)
            => await _commentRepository.DeleteAsync(id);

        public async Task<IEnumerable<Comment>> GetAllAsync()
            => await _commentRepository.GetAllAsync();
    }
}