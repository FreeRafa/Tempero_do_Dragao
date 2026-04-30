namespace Tempero_do_Dragao.Model
{
    public class Favorite
    {
        public int Id { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
