using DBCModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebCore.Controllers
{
    [ApiController]
    //[Microsoft.AspNetCore.Authorization.Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/heroes")]
    public class HeroesController : ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.Route("{casaheroe}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<JsonResult> GetHeroesByHouse([FromUri] string casaheroe)
        {
            string baseDatos = string.Empty;
            switch (casaheroe)
            {
                case "marvel":
                    baseDatos = "BBDD_Marvel";
                    break;
                case "dc":
                    baseDatos = "BBDD_DC";
                    break;
                default:
                    baseDatos = "todas";
                    break;
            }
            var heroes = await Heroe.GetAllHeroesFromHouse(baseDatos);

            return heroes.Count != 0 ? new JsonResult(heroes) : new JsonResult(null);
        }

        [Microsoft.AspNetCore.Mvc.Route("{casaheroe}/{id}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<JsonResult> GetHeroeById([FromUri] string casaheroe, [FromUri] int id)
        {
            string baseDatos = string.Empty;
            switch (casaheroe)
            {
                case "marvel":
                    baseDatos = "BBDD_Marvel";
                    break;
                case "dc":
                    baseDatos = "BBDD_DC";
                    break;
                default:
                    baseDatos = "BBDD_Marvel";
                    break;
            }

            var heroe = await Heroe.GetHeroeById(baseDatos, id);

            return heroe != null ? new JsonResult(heroe) : new JsonResult(null);
        }
        [Microsoft.AspNetCore.Mvc.Route("{casaheroe}")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<bool> PostHeroe([FromUri] string casaheroe, [Microsoft.AspNetCore.Mvc.FromBody] Heroe heroe)
        {
            string baseDatos = string.Empty;
            switch (casaheroe)
            {
                case "marvel":
                    baseDatos = "BBDD_Marvel";
                    break;
                case "dc":
                    baseDatos = "BBDD_DC";
                    break;
                default:
                    baseDatos = "BBDD_Marvel";
                    break;
            }

            var confirm = await Heroe.AddHeroeToDB(baseDatos, heroe);

            return confirm;
        }
        [Microsoft.AspNetCore.Mvc.Route("{casaheroe}/{id}")]
        [Microsoft.AspNetCore.Mvc.HttpPut]
        public async Task<bool> UpdateHeroe([FromUri] string casaheroe, [FromUri] int id, [Microsoft.AspNetCore.Mvc.FromBody] Heroe heroe)
        {
            string baseDatos = string.Empty;
            switch (casaheroe)
            {
                case "marvel":
                    baseDatos = "BBDD_Marvel";
                    break;
                case "dc":
                    baseDatos = "BBDD_DC";
                    break;
                default:
                    baseDatos = "BBDD_Marvel";
                    break;
            }

            var confirm = await Heroe.UpdateHeroeInDB(baseDatos, id, heroe);

            return confirm;
        }

        [Microsoft.AspNetCore.Mvc.Route("{casaheroe}/{id}")]
        [Microsoft.AspNetCore.Mvc.HttpDelete]
        public async Task<bool> DeleteHeroe([FromUri] string casaheroe, [FromUri] int id)
        {
            string baseDatos = string.Empty;
            switch (casaheroe)
            {
                case "marvel":
                    baseDatos = "BBDD_Marvel";
                    break;
                case "dc":
                    baseDatos = "BBDD_DC";
                    break;
                default:
                    baseDatos = "BBDD_Marvel";
                    break;
            }

            var confirm = await Heroe.DeleteHeroeInDB(baseDatos, id);

            return confirm;
        }
    }
}
