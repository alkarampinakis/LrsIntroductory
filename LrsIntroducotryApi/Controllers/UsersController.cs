using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Description;
using LrsIntroducotryApi.Business;
using LrsIntroducotryApi.Transfer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LrsIntroducotryApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the users list.
        /// </summary>
        /// <param name="includeInactive">Determines if the list should contain inactive records.</param>
        /// <returns>An <see cref="IEnumerable{UserWithTypeTitleDTO]"/></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<UserWithTypeTitleDTO>))]
        public async Task<IEnumerable<UserWithTypeTitleDTO>> GetUsers(bool includeInactive = false)
        {
            return await _userService.GetUsersAsync(includeInactive).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a user by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A <see cref="UserWithTypeTitleDTO"/></returns>
        [Route("/User")]
        [HttpGet]
        [ResponseType(typeof(UserWithTypeTitleDTO))]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                return Ok(await _userService.GetUserByIdAsync(userId).ConfigureAwait(false));
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation(message: "Api was called to get a user by his id " +
                                                "but there seems to be a problem with the inserted id", userId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: "Internal server error");
                return StatusCode(500, ex.Message);
            };
        }

        /// <summary>
        /// Gets the user types.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{UserTypeDTO]"/></returns>
        [Route("/Types")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<UserTypeDTO>))]
        public async Task<IEnumerable<UserTypeDTO>> GetUserTypes()
        {
            return await _userService.GetUserTypesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the user titles.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{UserTitleDTO]"/></returns>
        [Route("/Titles")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<UserTitleDTO>))]
        public async Task<IEnumerable<UserTitleDTO>> GetUserTitles()
        {
            return await _userService.GetUserTitlesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Inserts a new user.
        /// </summary>
        /// <param name="user">The new user.</param>
        [HttpPost]
        [ResponseType(typeof(IActionResult))]
        public async Task<IActionResult> InsertUser(UserWithTypeTitleDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _userService.InsertUserAsync(user).ConfigureAwait(false);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation(message: "Api was called to insert a user " +
                                           "but there seems to be a problem with the inserted user", user);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: "Internal server error");
                return StatusCode(500, ex.Message);
            };
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">The update user data.</param>
        [HttpPut]
        [ResponseType(typeof(IActionResult))]
        public async Task<IActionResult> UpdateUser(UserWithTypeTitleDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _userService.UpdateUserAsync(user).ConfigureAwait(false);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation(message: "Api was called to update a user " +
                           "but there seems to be a problem with the inserted user data", user);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: "Internal server error");
                return StatusCode(500, ex.Message);
            };
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        [HttpDelete]
        [ResponseType(typeof(IActionResult))]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userService.DeleteUser(userId).ConfigureAwait(false);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation(message: "Api was called to get a user by his id " +
                                                "but there seems to be a problem with the inserted id", userId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: "Internal server error");
                return StatusCode(500, ex.Message);
            };
        }
    }
}
