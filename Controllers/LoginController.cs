using DBCModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebCore.Models;

namespace WebCore.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/jwt")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

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
