using Catapult.Models.Docker;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catapult.Docker.Services
{
    public class ImageService : IDisposable
    {
        #region Disposable implementation
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _apiBaseUrl = null;
            _disposed = true;
        }
        #endregion

        private string _apiBaseUrl;

        public ImageService(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
        }

        public async Task<List<Image>> GetList()
        {
            var client = new RestClient(_apiBaseUrl);

            var request = new RestRequest("/images/json", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(request);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<Image[]>(restResponse.Content);
                    return response.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
            else
            {
                if (string.IsNullOrEmpty(restResponse.Content))
                    throw new Exception($"Error getting images: {restResponse.ErrorException}");
                else
                    throw new Exception($"Error getting images: {restResponse.Content}");
            }
        }
    }
}
