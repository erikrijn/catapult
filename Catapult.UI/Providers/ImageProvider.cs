using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catapult.Docker.Services;
using Catapult.Models.Docker;

namespace Catapult.UI.Providers
{
    public class ImageProvider : IImageProvider
    {
        public async Task<List<Image>> GetList()
        {
            using (var imageService = new ImageService("http://localhost:2375"))
            {
                return await imageService.GetList();
            }
        }
    }
}
