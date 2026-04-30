using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Repositories.Interfaces;

namespace Tempero_do_Dragao.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _categoryRepository.GetAllAsync();

        public async Task<Category?> GetByIdAsync(int id)
            => await _categoryRepository.GetByIdAsync(id);

        public async Task AddAsync(Category category)
            => await _categoryRepository.AddAsync(category);

        public async Task UpdateAsync(Category category)
            => await _categoryRepository.UpdateAsync(category);

        public async Task DeleteAsync(int id)
            => await _categoryRepository.DeleteAsync(id);
    }

    public class DifficultyService
    {
        private readonly IDifficultyRepository _difficultyRepository;

        public DifficultyService(IDifficultyRepository difficultyRepository)
        {
            _difficultyRepository = difficultyRepository;
        }

        public async Task<IEnumerable<Difficulty>> GetAllAsync()
            => await _difficultyRepository.GetAllAsync();

        public async Task<Difficulty?> GetByIdAsync(int id)
            => await _difficultyRepository.GetByIdAsync(id);

        public async Task AddAsync(Difficulty difficulty)
            => await _difficultyRepository.AddAsync(difficulty);

        public async Task UpdateAsync(Difficulty difficulty)
            => await _difficultyRepository.UpdateAsync(difficulty);

        public async Task DeleteAsync(int id)
            => await _difficultyRepository.DeleteAsync(id);
    }

    public class IngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
            => await _ingredientRepository.GetAllAsync();

        public async Task<Ingredient?> GetByIdAsync(int id)
            => await _ingredientRepository.GetByIdAsync(id);

        public async Task AddAsync(Ingredient ingredient)
            => await _ingredientRepository.AddAsync(ingredient);

        public async Task UpdateAsync(Ingredient ingredient)
            => await _ingredientRepository.UpdateAsync(ingredient);

        public async Task DeleteAsync(int id)
            => await _ingredientRepository.DeleteAsync(id);
    }

    public class MeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository;

        public MeasurementService(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }

        public async Task<IEnumerable<Measurement>> GetAllAsync()
            => await _measurementRepository.GetAllAsync();

        public async Task<Measurement?> GetByIdAsync(int id)
            => await _measurementRepository.GetByIdAsync(id);

        public async Task AddAsync(Measurement measurement)
            => await _measurementRepository.AddAsync(measurement);

        public async Task UpdateAsync(Measurement measurement)
            => await _measurementRepository.UpdateAsync(measurement);

        public async Task DeleteAsync(int id)
            => await _measurementRepository.DeleteAsync(id);
    }
}