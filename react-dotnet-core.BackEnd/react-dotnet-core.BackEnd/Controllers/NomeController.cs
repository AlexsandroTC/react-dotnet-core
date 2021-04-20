using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using react_dotnet_core.BackEnd.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace react_dotnet_core.BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NomeController : ControllerBase
    {
        private static readonly string[] Nome = new[]
{
            "Lucas", "Diego", "Alexsandro", "Felipe", "Moises"
        };

        // GET: NomeController
        [HttpGet]
        public string[] Get()
        {
            return Nome;
        }

        // Post: ProdutoController/Details/5
        [HttpPost]
        public ActionResult Details(int id)
        {
            var teste = string.Empty;

            return NoContent();
        }
    }
}
