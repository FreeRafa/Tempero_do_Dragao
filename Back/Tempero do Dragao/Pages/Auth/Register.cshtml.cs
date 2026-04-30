using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Tempero_do_Dragao.Model;
using Tempero_do_Dragao.Services;

namespace Tempero_do_Dragao.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterInput Input { get; set; } = new();

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var newUser = new User
            {
                Name = Input.Name,
                Email = Input.Email,
                Password = Input.Password, // ⚠️ Lembrete: usar hash em produção
                IsAdmin = false,
                Status = 1
            };

            var success = await _userService.RegisterAsync(newUser);

            if (!success)
            {
                ErrorMessage = "Este email já está registado.";
                return Page();
            }

            SuccessMessage = "Conta criada com sucesso! Podes fazer login agora.";
            return Page();
        }
    }

    public class RegisterInput
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A password é obrigatória.")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "A password deve ter pelo menos 6 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirma a tua password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As passwords não coincidem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}