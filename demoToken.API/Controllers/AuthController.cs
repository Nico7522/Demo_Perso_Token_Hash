using demoToken.API.Dto.Form;
using demoToken.API.Infrastructure;
using demoToken.API.Mappers;
using DemoToken.BLL.Interfaces;
using DemoToken.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demoToken.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly TokenManager _tokenManager;
        public AuthController(IUtilisateurService utilisateurService, TokenManager tokenManager)
        {
            _utilisateurService = utilisateurService;
            _tokenManager = tokenManager;
        }

        //[Authorize]
        [HttpPost(nameof(Register))]
        public ActionResult Register(UtilisateurRegisterForm form)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _utilisateurService.RegisterUtilisateur(form.ApiToBll());
                return Ok("Enregistré avec succès !");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return BadRequest();
            }

        }

        [HttpPost(nameof(Login))]
        public ActionResult Login(UtilisateurLoginForm form)
        {

            try
            {
            if(!ModelState.IsValid)
                BadRequest(ModelState);

            UtilisateurModel? currentUser = _utilisateurService.LoginUtilisateur(form.Email, form.Password);

                if (currentUser is null)
                    NotFound("L'utilisateur n'existe pas");

                string token = _tokenManager.GenerateJwt(currentUser);
                return Ok(token);

            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
    }
}
