using DBCModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebCore.Models;

namespace WebCore.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/jwt")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.Route("login")]

        [Microsoft.AspNetCore.Mvc.HttpPost]
        //[System.Web.Http.AllowAnonymous]
        //[EnableCors]
        public async Task<string> GetAsync([FromUri] string correo, string pass)
        {
            if (await Usuario.LoginUsuario(correo, pass))
            {

                var token = JwtManager.GenerateToken(correo, "user");
                return token;
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
        //[System.Web.Http.Route("test")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        //[System.Web.Http.AllowAnonymous]
        //[EnableCors]
        public string Testweb()
        {
            return "test";
        }
    }

}
