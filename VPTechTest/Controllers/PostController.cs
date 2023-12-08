using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VPTechTestLib;
using VPTechTestLib.Enums;
using VPTechTestLib.Interfaces;
using VPTechTestLib.JSON;


namespace VPTechTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        public IHandleDatabaseRequests _handleDatabaseRequests;
        public IConfiguration _configuration;   
        public IUserHelper _userHelper; 
        public PostController(IHandleDatabaseRequests handleDatabaseRequests, IConfiguration configuration, IUserHelper userHelper)
        {
            _handleDatabaseRequests = handleDatabaseRequests;
            _configuration = configuration;
            _userHelper = userHelper;
        }

        /// <summary>
        /// HTTP POST endpoint for processing order data.
        /// The endpoint is accessible at the route "processData".
        /// </summary>
        /// <param name="order"></param>
        /// <param name="apiKey"></param>
        /// <returns>An ActionResult</returns>
        [HttpPost("processData")]
        public async Task<ActionResult> PostRequest(OrderObj order, [FromQuery] Guid apiKey)
        {
            var key = _configuration.GetValue<Guid>("apiKey");
            if (apiKey != key)
            {
                return BadRequest("ApI key not found");
            }

            switch (ValidateData(order))
            {
                case ValidationResult.Valid:

                    var customer = await _userHelper.GetCustomer(order.CustomerId);

                    if (customer == null)
                    {
                        return StatusCode(500,"Invalid Customer, this customer does not exist");
                    }

                    // Attempt to save order details asynchronously using the '_handleDatabaseRequests' service.
                    if (await _handleDatabaseRequests.SaveOrderDetailsAysnc(order)) 
                    {
                        return Ok("Data processed successfully");
                    }
                    //Only return 500 if there has been a problem saving the details to the database
                    return StatusCode(500, "Something has gone wrong the order has not been placed"); 
                
                case ValidationResult.InvalidCustomerId: 
                    return BadRequest("Invalid Customer, this customer does not exist");
                case ValidationResult.InvalidFormat:
                    return BadRequest("Invalid data format");
                default:
                    return BadRequest("Unknown validation error");
            }
        }

        /// <summary>
        /// Method validates if the order object is in the correct foramt
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ValidationResult</returns>
        private ValidationResult ValidateData(OrderObj data)
        {
            if (string.IsNullOrEmpty(data.CustomerId.ToString()))
            {
                return ValidationResult.InvalidCustomerId;
            }

            if (data == null)
            {
                return ValidationResult.InvalidFormat;
            }

            if (string.IsNullOrEmpty(data.ItemId.ToString())) 
            {
                return ValidationResult.InvalidItemId;
            }

            return ValidationResult.Valid;
        }
    }
}
