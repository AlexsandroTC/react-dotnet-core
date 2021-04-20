using Microsoft.AspNetCore.Mvc;
using react_dotnet_core.BackEnd.Services;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Details(string nome)
        {
            var teste = string.Empty;
            try
            {
                _ = ServicesBus.PostMessage(nome);
            }
            catch (System.Exception)
            {
                return BadRequest();
                throw;
            }

            return Ok();
            
        }

        [HttpGet]
        [Route("/GetNames")]
        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            var nomes = await ServicesBus.ReceaveMessageAsync();
            return Nome.ToList();
        }
    }
}
