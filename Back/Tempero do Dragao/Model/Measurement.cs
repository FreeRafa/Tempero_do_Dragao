namespace Tempero_do_Dragao.Model
{
    public class Measurement
    {
        public int Id { get; set; }
        public string Unit { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
