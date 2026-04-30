using Tempero_do_Dragao.Model;
using static Tempero_do_Dragao.Interface.InterfaceIrepository;

namespace Tempero_do_Dragao.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category> { }
    public interface IDifficultyRepository : IRepository<Difficulty> { }
    public interface IIngredientRepository : IRepository<Ingredient> { }
    public interface IMeasurementRepository : IRepository<Measurement> { }
}