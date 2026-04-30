namespace Tempero_do_Dragao.Model
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }

        // Foreign Keys
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int MeasurementId { get; set; }

        // Navigation Properties
        public Recipe Recipe { get; set; } = null!;
        public Ingredient Ingredient { get; set; } = null!;
        public Measurement Measurement { get; set; } = null!;
    }
}
