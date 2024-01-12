using demoToken.DAL.Data;
using DemoToken.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoToken.BLL.Mapper
{
    public static class UtilisateurMappers
    {

        // DalToBll
        internal static UtilisateurModel DalToBll(this UtilisateurData data)
        {
            return new UtilisateurModel()
            {
                Id = data.Id,
                Nom = data.Nom,
                Prenom = data.Prenom,
                Email = data.Email,
                DateNaissance = data.DateNaissance,
                Password = data.Password,
            };
        }

        // BllToDal
        internal static UtilisateurData BllToDal(this UtilisateurModel data)
        {
            return new UtilisateurData()
            {
                Nom = data.Nom,
                Prenom = data.Prenom,
                Email = data.Email,
                DateNaissance = data.DateNaissance,
                Password = data.Password,
            };
        }
    }
}
