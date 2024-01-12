using demoToken.API.Dto.Form;
using DemoToken.BLL.Models;

namespace demoToken.API.Mappers
{
    public static class UtilisateurMappers
    {

        internal static UtilisateurModel ApiToBll(this UtilisateurRegisterForm form)
        {
            return new UtilisateurModel
            {
                Nom = form.Nom,
                Prenom = form.Prenom,
                DateNaissance = form.DateNaissance,
                Email = form.Email,
                Password = form.Password,
            };
        }
    }
}
