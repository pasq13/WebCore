using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebCore.Models;

///Controlador para el login con token jwt
namespace WebCore.Controllers
{
    /// <summary>
    /// ruta principal de acceso al controlador
    /// </summary>
    [Microsoft.AspNetCore.Mvc.Route("api/jwt")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        
        private IConfiguration _config;
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="config"></param>
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Metodo para realizar el login
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="pass"></param>
        /// <returns>un JSON con el jwt</returns>
        [Microsoft.AspNetCore.Mvc.Route("login")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<JsonResult> GetAsync([FromUri] string correo, string pass)
        {
            if (await Usuario.LoginUsuario(correo, pass))
            {
                var jwt = new JwtManager(_config);
                var token = jwt.GenerateSecurityToken(correo);
                return new JsonResult(token);
            }
            else
            {
                return new JsonResult("error");
            }

            
        }

        //[Microsoft.AspNetCore.Mvc.Route("test")]
        [Microsoft.AspNetCore.Mvc.HttpGet]

        public string Testweb()
        {
            return "test";
        }
    }

}
