using System.Collections.Generic;
using System.Threading.Tasks;
using Catapult.Models.Docker;

namespace Catapult.UI.Providers
{
    public interface IImageProvider
    {
        Task<List<Image>> GetList();
    }
}
