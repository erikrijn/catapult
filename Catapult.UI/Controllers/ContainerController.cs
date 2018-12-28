using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Catapult.UI.Providers;
using System.Threading.Tasks;

namespace Catapult.UI.Controllers
{
    [Route("api/[controller]")]
    public class ContainerController : Controller
    {
        private readonly IContainerProvider containerProvider;

        public ContainerController(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Containers([FromQuery(Name = "from")] int from = 0, [FromQuery(Name = "to")] int to = 4)
        {
            var quantity = to - from;

            if (quantity <= 0)
                return BadRequest("You cannot have the 'to' parameter higher than 'from' parameter.");

            if (from < 0)
                return BadRequest("You cannot go in the negative with the 'from' parameter");

            var allContainers = await containerProvider.GetList();
            var result = new
            {
                Total = allContainers.Count,
                Containers = allContainers.Skip(from).Take(quantity).ToArray()
            };

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromQuery(Name = "image")] string image, [FromQuery(Name = "name")] string name)
        {
            await containerProvider.Create(image, name);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Start([FromQuery(Name = "id")] string id)
        {
            await containerProvider.Start(id);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Stop([FromQuery(Name = "id")] string id)
        {
            await containerProvider.Stop(id);
            return Ok();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove([FromQuery(Name = "id")] string id)
        {
            await containerProvider.Remove(id);
            return Ok();
        }
    }
}
