using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FetchUsersAPI.Controllers;
using FetchUsersAPI.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FetchUsersAPI.Controllers
{
    /// <summary>
    /// usersController
    /// </summary>

    [ApiController]
    [Route("api/users")]
    
    public class usersController : ControllerBase
    {
        //variables for DI
        private readonly IConfiguration _configuration;
        private readonly GraphServiceClientFactory _graphServiceClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
                
        /// <summary>
        ///  DI in constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="graphServiceClientFactory"></param>
        /// <param name="httpContextAccessor"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public usersController(IConfiguration configuration, GraphServiceClientFactory graphServiceClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _graphServiceClientFactory = graphServiceClientFactory ?? throw new ArgumentNullException(nameof(graphServiceClientFactory));
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
       
        [HttpGet]
        public async Task<IActionResult> GetUsers(string tenantId)
        {
            

            if (string.IsNullOrEmpty(tenantId))
            {
                return BadRequest("Tenant ID is required.");
            }

            var graphServiceClient = await _graphServiceClientFactory.GetGraphServiceClientAsync(tenantId);
            var users = await graphServiceClient.Users.Request().GetAsync();

            if (users == null)
            {
                throw new NotFoundException($"User details with tenant ID: {tenantId} could not be found.");
            }

            return Ok(users);

            
        }


    }
}
