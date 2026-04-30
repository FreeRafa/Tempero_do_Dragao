namespace Tempero_do_Dragao.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
