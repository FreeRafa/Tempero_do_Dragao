namespace Tempero_do_Dragao.Model
{
    public class Difficulty
    {
        public int Id { get; set; }
        public string Level { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
