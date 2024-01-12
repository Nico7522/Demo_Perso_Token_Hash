using System.ComponentModel.DataAnnotations;

namespace demoToken.API.Dto.Form
{
    public class UtilisateurLoginForm
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(150)]
        [MinLength(8)]
        public string? Password { get; set; }
    }
}
