namespace Tempero_do_Dragao.Model
{
    public class Rating
    {
        public int Id { get; set; }

        /// <summary>
        /// Score must be between 1 and 5.
        /// Validated via CHECK constraint in DB and via annotation in application layer.
        /// </summary>
        public int Score { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
