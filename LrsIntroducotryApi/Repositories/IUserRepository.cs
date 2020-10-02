using LrsIntroducotryApi.Models;
using LrsIntroducotryApi.Models.Entities.Custom;
using LrsIntroducotryApi.Transfer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LrsIntroducotryApi.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets the users list.
        /// </summary>
        /// <param name="includeInactive">Determines if the list should contain inactive records.</param>
        /// <returns>An <see cref="IEnumerable{UserWithTypeTitle]"/></returns>
        public Task<IEnumerable<UserWithTypeTitle>> GetUsersAsync(bool includeInactive);

        /// <summary>
        /// Gets a user by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A <see cref="UserWithTypeTitle"/></returns>
        public Task<UserWithTypeTitle> GetUserByIdAsync(int userId);

        /// <summary>
        /// Gets the user types.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{UserType]"/></returns>
        public Task<IEnumerable<UserType>> GetUserTypesAsync();

        /// <summary>
        /// Gets the users list.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{UserTitle]"/></returns>
        public Task<IEnumerable<UserTitle>> GetUserTitlesAsync();

        /// <summary>
        /// Inserts a new user.
        /// </summary>
        /// <param name="user">The new user data.</param>
        public Task InsertUserAsync(UserWithTypeTitle user);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">The new user data.</param>
        public Task UpdateUserAsync(UserWithTypeTitle user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public Task DeleteUser(int userId);
    }
}
