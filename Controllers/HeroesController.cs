using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web.Http;
using WebCore.Models;

///Controlador De los heroes
namespace WebCore.Controllers
{
    /// <summary>
    /// ruta principal para aceder al acontrolador de heroes
    /// </summary>
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/heroes")]
    public class HeroesController : ControllerBase
    {
        /// <summary>
        /// Metodo para obtener todos los heroes por casa de heroe
        /// </summary>
        /// <param name="casaheroe"></param>
        /// <returns>un JSON array </returns>
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
        /// <summary>
        /// Metodo para obtener un heroe por su casa y su id
        /// </summary>
        /// <param name="casaheroe"></param>
        /// <param name="id"></param>
        /// <returns>Devuelve un JSON con el heroe en su defecto si no existe devuelve null</returns>
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
        /// <summary>
        /// MEtodo para añadir  un heroe a una casa de heroe
        /// </summary>
        /// <param name="casaheroe"></param>
        /// <param name="heroe"></param>
        /// <returns></returns>
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
        }/// <summary>
        /// Metodo para actualizar un heroe por su casa y su id
        /// </summary>
        /// <param name="casaheroe"></param>
        /// <param name="id"></param>
        /// <param name="heroe"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Metodo para borrar un heroe por su casa y su id
        /// </summary>
        /// <param name="casaheroe"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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
