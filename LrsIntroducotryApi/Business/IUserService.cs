using LrsIntroducotryApi.Transfer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LrsIntroducotryApi.Business
{
    public interface IUserService
    {
        /// <summary>
        /// Gets the users list.
        /// </summary>
        /// <param name="includeInactive">Determines if the list should contain inactive records.</param>
        /// <returns>An <see cref="IEnumerable{UserWithTypeTitleDTO]"/></returns>
        public Task<IEnumerable<UserWithTypeTitleDTO>> GetUsersAsync(bool includeInactive);

        /// <summary>
        /// Gets a user by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A <see cref="UserWithTypeTitleDTO"/></returns>
        public Task<UserWithTypeTitleDTO> GetUserByIdAsync(int userId);

        /// <summary>
        /// Gets the user types.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{UserTypeDTO]"/></returns>
        public Task<IEnumerable<UserTypeDTO>> GetUserTypesAsync();

        /// <summary>
        /// Gets the users list.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{UserTitleDTO]"/></returns>
        public Task<IEnumerable<UserTitleDTO>> GetUserTitlesAsync();
    }
}
