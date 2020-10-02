using AutoMapper;
using LrsIntroducotryApi.Models.Entities.Custom;
using LrsIntroducotryApi.Repositories;
using LrsIntroducotryApi.Transfer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LrsIntroducotryApi.Business.Implementation
{
    public class UserService : IUserService
    {
        #region Private Members
        /// <summary>
        /// The user repository
        /// </summary>
        private IUserRepository _userRepository;
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;
        #endregion

        #region Constructor
        public UserService(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<UserWithTypeTitleDTO>> GetUsersAsync(bool includeInactive)
        {
            return _mapper.Map<IEnumerable<UserWithTypeTitleDTO>>(await _userRepository.GetUsersAsync(includeInactive).ConfigureAwait(false));
        }

        public async Task<UserWithTypeTitleDTO> GetUserByIdAsync(int userId)
        {
            if (userId <= default(int))
            {
                throw new ArgumentException(
                   "userId",
                   "user identifier is required.");
            }
            return _mapper.Map<UserWithTypeTitleDTO>(await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false));
        }

        public async Task<IEnumerable<UserTypeDTO>> GetUserTypesAsync()
        {
            return _mapper.Map<IEnumerable<UserTypeDTO>>(await _userRepository.GetUserTypesAsync().ConfigureAwait(false));
        }

        public async Task<IEnumerable<UserTitleDTO>> GetUserTitlesAsync()
        {
            return _mapper.Map<IEnumerable<UserTitleDTO>>(await _userRepository.GetUserTitlesAsync().ConfigureAwait(false));
        }

        public async Task InsertUserAsync(UserWithTypeTitleDTO user)
        {
            ValidateUser(user);

            await _userRepository.InsertUserAsync(_mapper.Map<UserWithTypeTitle>(user)).ConfigureAwait(false);
        }

        public async Task UpdateUserAsync(UserWithTypeTitleDTO user)
        {
            ValidateUser(user);

            await _userRepository.UpdateUserAsync(_mapper.Map<UserWithTypeTitle>(user)).ConfigureAwait(false);
        }

        public async Task DeleteUser(int userId)
        {
            if (userId <= default(int))
            {
                throw new ArgumentException(
                   "userId",
                   "user identifier is required.");
            }
            await _userRepository.DeleteUser(userId).ConfigureAwait(false);
        }

        #endregion

        #region Private methods
        private void ValidateUser(UserWithTypeTitleDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentException(
                   "user.Name",
                   "user name is required.");
            }
            if (string.IsNullOrEmpty(user.Surname))
            {
                throw new ArgumentException(
                   "user.Surname",
                   "user Surname is required.");
            }
            if (string.IsNullOrEmpty(user.EmailAddress))
            {
                throw new ArgumentException(
                   "user.EmailAddress",
                   "user Email Address is required.");
            }
            if (user.UserTitleId <= default(int))
            {
                throw new ArgumentException(
                   "user.UserTitleId",
                   "user title id is required.");
            }
            if (user.UserTypeId <= default(int))
            {
                throw new ArgumentException(
                   "user.UserTypeId",
                   "user type id is required.");
            }
        }
        #endregion
    }
}
