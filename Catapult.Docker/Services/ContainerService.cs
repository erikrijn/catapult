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
    public class ContainerService : IDisposable
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
            {
                return;
            }

            _apiBaseUrl = null;
            _disposed = true;
        }
        #endregion

        private string _apiBaseUrl;

        public ContainerService(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
        }

        public async Task<List<Container>> GetList()
        {
            var client = new RestClient(_apiBaseUrl);

            var request = new RestRequest("/containers/json?all=1", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(request);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<Container[]>(restResponse.Content);
                    return response.OrderByDescending(x => x.State).ThenBy(x => x.Names.First()).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                if (string.IsNullOrEmpty(restResponse.Content))
                    throw new Exception($"Error getting containers: {restResponse.ErrorException}");
                throw new Exception($"Error getting containers: {restResponse.Content}");
            }
        }

        public async Task Start(string id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"/containers/{id}/start", Method.POST);

            var restResponse = await client.ExecuteTaskAsync(request);
            if (restResponse.StatusCode != HttpStatusCode.NoContent)
            {
                if (string.IsNullOrEmpty(restResponse.Content))
                {
                    throw new Exception($"Error starting container: {restResponse.ErrorException}");
                }
                else
                {
                    throw new Exception($"Error starting container: {restResponse.Content}");
                }
            }
        }

        public async Task Stop(string id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"/containers/{id}/stop", Method.POST);

            var restResponse = await client.ExecuteTaskAsync(request);
            if (restResponse.StatusCode != HttpStatusCode.NoContent)
            {
                if (string.IsNullOrEmpty(restResponse.Content))
                    throw new Exception($"Error stopping container: {restResponse.ErrorException}");
                throw new Exception($"Error stopping container: {restResponse.Content}");
            }
        }

        public async Task Create(string image, string name)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"/containers/create?name={name}", Method.POST);

            var createRequest = new ContainerCreateRequest()
            {
                Hostname = name,
                Image = image,
                HostConfig = new Hostconfig()
                {
                    NetworkMode = "bridge",
                    Isolation = "process",
                    Memory = 4294967296
                },
                Env = new string[]
                {
                    "auth=NavUserPassword",
                    "username=Erik",
                    "password=mypassword",
                    "enableApiServices=Y",
                    "useSSL=N",
                    "accept_eula=Y",
                    "ExitOnError=N",
                    "locale=en-US",
                    "licenseFile=c:\\run\\my\\license.flf",
                    "databaseServer=",
                    "databaseInstance="
                },
                Labels = new Labels()
                {
                    created_by = "API"
                }
            };

            request.AddJsonBody(createRequest);
            var restResponse = await client.ExecuteTaskAsync(request);
            if (restResponse.StatusCode != HttpStatusCode.Created)
            {
                if (string.IsNullOrEmpty(restResponse.Content))
                    throw new Exception($"Error creating container: {restResponse.ErrorException}");
                throw new Exception($"Error creating container: {restResponse.Content}");
            }
        }

        public async Task Remove(string id)
        {
            var client = new RestClient(_apiBaseUrl);
            var request = new RestRequest($"/containers/{id}", Method.DELETE);

            var restResponse = await client.ExecuteTaskAsync(request);
            if (restResponse.StatusCode != HttpStatusCode.NoContent)
            {
                if (string.IsNullOrEmpty(restResponse.Content))
                    throw new Exception($"Error removing container: {restResponse.ErrorException}");
                throw new Exception($"Error removing container: {restResponse.Content}");
            }
        }
    }
}
