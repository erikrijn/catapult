using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catapult.Docker.Services;
using Catapult.Models.Docker;

namespace Catapult.UI.Providers
{
    public class ContainerProvider : IContainerProvider
    {
        public async Task<List<Container>> GetList()
        {
            using (var containerService = new ContainerService("http://localhost:2375"))
            {
                return await containerService.GetList();
            }
        }

        public async Task Create(string image, string name)
        {
            using (var containerService = new ContainerService("http://localhost:2375"))
            {
                await containerService.Create(image, name);
            }
        }

        public async Task Start(string id)
        {
            using (var containerService = new ContainerService("http://localhost:2375"))
            {
                await containerService.Start(id);
            }
        }

        public async Task Stop(string id)
        {
            using (var containerService = new ContainerService("http://localhost:2375"))
            {
                await containerService.Stop(id);
            }
        }

        public async Task Remove(string id)
        {
            using (var containerService = new ContainerService("http://localhost:2375"))
            {
                await containerService.Remove(id);
            }
        }
    }
}
