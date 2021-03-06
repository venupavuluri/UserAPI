using Microsoft.AspNetCore.Mvc;
using UserAPI.Logic;
using UserAPI.Model;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private IUserLogic _userLogic;
        private readonly ILogger _logger;        

        /// <summary>
        /// User controller constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public UserController(IServiceProvider serviceProvider, ILogger<UserController> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _userLogic = _serviceProvider.GetRequiredService<IUserLogic>();            
        }

        /// <summary>
        /// returns available user for given email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserResponseModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{emailAddress}")]
        public async Task<IActionResult> GetUserAsync(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return BadRequest("EmailAddress can't be null or empty");

            _logger.LogInformation("Requested email address {emailAddress}", emailAddress);

            try
            {
                var result = await _userLogic.GetUserByEmail(emailAddress);

                //if user does not exist, return 204
                return result == null ? NoContent() : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetUser with email address ", emailAddress);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// creates an user with given user request
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] PostUserRequestModel userRequest)
        {
            try
            {
                _logger.LogInformation("Create User Object {@userRequest}", userRequest);
                var result = await _userLogic.CreateUser(userRequest);                 
                return Ok(result.ToString());
            }            
            catch (Exception ex)
            {
                var errorObject = new ErrorResponseModel();
                _logger.LogError(ex, "Error creating user with request {@userRequest}", userRequest);

                if (ex.Message.Equals("UserAlreadyExist"))
                {
                    errorObject = new ErrorResponseModel { ErrorMessage = "Duplicate User", StatusCode = StatusCodes.Status409Conflict };
                    return StatusCode(StatusCodes.Status409Conflict, errorObject);
                }

                errorObject = new ErrorResponseModel { ErrorMessage = "Internal Server Error", StatusCode = StatusCodes.Status500InternalServerError };
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// creates an user with given user request
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] PostUserRequestModel userRequest)
        {
            try
            {
                _logger.LogInformation("Update User Object {@userRequest}", userRequest);
                var result = await _userLogic.UpdateUser(userRequest);
                return Ok(result.ToString());
            }
            catch (Exception ex)
            {
                var errorObject = new ErrorResponseModel();
                _logger.LogError(ex, "Error creating user with request {@userRequest}", userRequest);

                if (ex.Message.Equals("UpdateUserFailed"))
                {
                    errorObject = new ErrorResponseModel { ErrorMessage = "Update User Failed", StatusCode = StatusCodes.Status403Forbidden };
                    return StatusCode(StatusCodes.Status403Forbidden, errorObject);
                }

                errorObject = new ErrorResponseModel { ErrorMessage = "Internal Server Error", StatusCode = StatusCodes.Status500InternalServerError };
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// deletes user with given email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{emailAddress}")]
        public async Task<IActionResult> DeleteUserAsync(string emailAddress)
        {
            try
            {
                var result = await _userLogic.DeleteUser(emailAddress);
                return Ok(result);
            }
            catch(Exception ex)
            {
                var errorObject = new ErrorResponseModel();
                _logger.LogError(ex, "User not deleted by email address {@emailAddress}", emailAddress);

                if (ex.Message.Equals("UserNotExist"))
                {
                    errorObject = new ErrorResponseModel { ErrorMessage = "User not exist", StatusCode = StatusCodes.Status400BadRequest};
                    return StatusCode(StatusCodes.Status400BadRequest, errorObject);
                }

                errorObject = new ErrorResponseModel { ErrorMessage = "Internal Server Error", StatusCode = StatusCodes.Status500InternalServerError };
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
