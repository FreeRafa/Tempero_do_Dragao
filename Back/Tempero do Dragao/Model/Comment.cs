namespace Tempero_do_Dragao.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
