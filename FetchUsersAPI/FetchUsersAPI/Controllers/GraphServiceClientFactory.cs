using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FetchUsersAPI.Controllers
{
    /// <summary>
    /// handle token acquisition
    /// </summary>
    public class GraphServiceClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public GraphServiceClientFactory(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GraphServiceClient> GetGraphServiceClientAsync( string tenantId)
        {
            string clientId = _configuration["AzureAd:ClientId"];
            string clientSecret = _configuration["AzureAd:ClientSecret"];
            string scope = _configuration["MicrosoftGraph:Scopes"]; // Retrieve the scope value
            

            // Ensure that tenantId is provided
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException("Tenant ID is required.", nameof(tenantId));
            }

            var authority = $"{_configuration["AzureAd:Instance"]}{tenantId}";

            var appConfidential = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri(authority))
                .Build();

            // Acquire a token with the specified scope
            var result = await appConfidential.AcquireTokenForClient(new[] { scope }) // Use the 'scope' value here
                .ExecuteAsync();

           
            // Create a GraphServiceClient and configure authentication
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            var graphServiceClient = new GraphServiceClient(httpClient)
            {
                AuthenticationProvider = new DelegateAuthenticationProvider((requestMessage) =>
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                    return Task.CompletedTask;
                }),
            };

            return graphServiceClient;
        }
    }
}
