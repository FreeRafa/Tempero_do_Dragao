using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Repository.Tempero_do_Dragao.Repositories;
using Tempero_do_Dragao.Repositories.Interfaces;
using Tempero_do_Dragao.Data;

namespace Tempero_do_Dragao.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    }

    public class DifficultyRepository : Repository<Difficulty>, IDifficultyRepository
    {
        public DifficultyRepository(AppDbContext context) : base(context) { }
    }

    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext context) : base(context) { }
    }

    public class MeasurementRepository : Repository<Measurement>, IMeasurementRepository
    {
        public MeasurementRepository(AppDbContext context) : base(context) { }
    }
}