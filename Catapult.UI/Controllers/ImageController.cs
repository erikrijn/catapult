using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Catapult.UI.Providers;
using System.Threading.Tasks;

namespace Catapult.UI.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageProvider imageProvider;

        public ImageController(IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Images([FromQuery(Name = "from")] int from = 0, [FromQuery(Name = "to")] int to = 4)
        {
            var quantity = to - from;

            if (quantity <= 0)
                return BadRequest("You cannot have the 'to' parameter higher than 'from' parameter.");

            if (from < 0)
                return BadRequest("You cannot go in the negative with the 'from' parameter");

            var allImages = await imageProvider.GetList();
            var result = new
            {
                Total = allImages.Count,
                Images = allImages.Skip(from).Take(quantity).ToArray()
            };

            return Ok(result);
        }
    }
}
