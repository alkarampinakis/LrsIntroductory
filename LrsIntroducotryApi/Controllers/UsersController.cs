﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LrsIntroducotryApi.Business;
using LrsIntroducotryApi.Transfer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LrsIntroducotryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets the users list.
        /// </summary>
        /// <param name="includeInactive">Determines if the list should contain inactive records.</param>
        /// <returns>An <see cref="IEnumerable{UserWithTypeTitleDTO]"/></returns>
        [HttpGet]
        public async Task<IEnumerable<UserWithTypeTitleDTO>> GetUsersAsync(bool includeInactive = false)
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
        public async Task<UserWithTypeTitleDTO> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _userService.GetUserByIdAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        /// <summary>
        /// Gets the user types.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{UserTypeDTO]"/></returns>
        [Route("/Types")]
        [HttpGet]
        public async Task<IEnumerable<UserTypeDTO>> GetUserTypesAsync()
        {
            return await _userService.GetUserTypesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the user titles.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{UserTitleDTO]"/></returns>
        [Route("/Titles")]
        [HttpGet]
        public async Task<IEnumerable<UserTitleDTO>> GetUserTitlesAsync()
        {
            return await _userService.GetUserTitlesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Inserts a new user.
        /// </summary>
        /// <param name="user">The new user.</param>
        /// <returns>A <see cref="UserWithTypeTitleDTO"/></returns>
        [Route("/User")]
        [HttpPost]
        public async Task<System.Web.Http.IHttpActionResult> InsertUserAsync(UserWithTypeTitleDTO user)
        {
            try
            {
                //var result = await _userService.InsertUserAsync(user).ConfigureAwait(false);
                return (System.Web.Http.IHttpActionResult)Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
    }
}
