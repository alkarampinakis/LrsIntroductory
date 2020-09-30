using AutoMapper;
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
                throw new ArgumentNullException();
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
        #endregion
    }
}
