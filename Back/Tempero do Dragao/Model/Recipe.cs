using System.Xml.Linq;

namespace Tempero_do_Dragao.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PreparationMethod { get; set; } = string.Empty;
        public int PreparationTime { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int DifficultyId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public Difficulty Difficulty { get; set; } = null!;
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
