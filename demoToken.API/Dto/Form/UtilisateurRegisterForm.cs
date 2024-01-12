using System.ComponentModel.DataAnnotations;

namespace demoToken.API.Dto.Form
{
    public class UtilisateurRegisterForm
    {
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(150)]
        [MinLength(1)]
        public string Prenom { get; set; }

        [Required]
        public string Email { get; set; }

        //[Required]
        public DateTime DateNaissance { get; set; }

        [Required]
        [MaxLength(150)]
        [MinLength(8)]
        public string? Password { get; set; }
    }
}
