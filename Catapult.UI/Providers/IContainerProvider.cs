using System.Collections.Generic;
using System.Threading.Tasks;
using Catapult.Models.Docker;

namespace Catapult.UI.Providers
{
    public interface IContainerProvider
    {
        Task<List<Container>> GetList();

        Task Create(string image, string name);

        Task Start(string id);

        Task Stop(string id);

        Task Remove(string id);
    }
}
