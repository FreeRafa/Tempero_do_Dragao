using Tempero_do_Dragao.Interface;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Repositories.Interfaces;

namespace Tempero_do_Dragao.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _userRepository.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id)
            => await _userRepository.GetByIdAsync(id);

        public async Task<User?> LoginAsync(string email, string password)
            => await _userRepository.GetByEmailAndPasswordAsync(email, password);

        public async Task<bool> RegisterAsync(User user)
        {
            if (await _userRepository.EmailExistsAsync(user.Email))
                return false;

            await _userRepository.AddAsync(user);
            return true;
        }

        public async Task UpdateAsync(User user)
            => await _userRepository.UpdateAsync(user);

        public async Task DeleteAsync(int id)
            => await _userRepository.DeleteAsync(id);

        public async Task<bool> EmailExistsAsync(string email)
            => await _userRepository.EmailExistsAsync(email);
    }
}