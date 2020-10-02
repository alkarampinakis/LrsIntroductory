using LrsIntroducotryApi.Models;
using LrsIntroducotryApi.Models.Entities.Custom;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LrsIntroducotryApi.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The db context
        /// </summary>
        private readonly LrsIntroductoryDBContext _context;

        public UserRepository(LrsIntroductoryDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserWithTypeTitle>> GetUsersAsync(bool includeInactive)
        {
            var query = _context.User
                        .Select(x => new UserWithTypeTitle
                        {
                            BirthDate = x.BirthDate,
                            EmailAddress = x.EmailAddress.Trim(),
                            Id = x.Id,
                            Surname = x.Surname.Trim(),
                            Name = x.Name.Trim(),
                            IsActive = x.IsActive,
                            UserTitle = x.UserTitle.Description.Trim(),
                            UserType = x.UserType.Description.Trim(),
                            UserTitleId = x.UserTitleId,
                            UserTypeId = x.UserTypeId
                        });

            if (!includeInactive)
            {
                query = query
                        .Where(x => x.IsActive.HasValue &&
                                    x.IsActive.Value);
            }

            return await query
                    .ToListAsync()
                    .ConfigureAwait(false);
        }

        public async Task<UserWithTypeTitle> GetUserByIdAsync(int userId)
        {
            if (userId <= default(int))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _context.User
                .Where(x => x.Id == userId)
                .Select(x => new UserWithTypeTitle
                {
                    BirthDate = x.BirthDate,
                    EmailAddress = x.EmailAddress.Trim(),
                    Id = x.Id,
                    Surname = x.Surname.Trim(),
                    Name = x.Name.Trim(),
                    IsActive = x.IsActive,
                    UserTitle = x.UserTitle.Description.Trim(),
                    UserType = x.UserType.Description.Trim(),
                    UserTitleId = x.UserTitleId,
                    UserTypeId = x.UserTypeId
                })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserType>> GetUserTypesAsync()
        {
            return await _context.UserType
                            .ToListAsync()
                            .ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserTitle>> GetUserTitlesAsync()
        {
            return await _context.UserTitle
                           .ToListAsync()
                           .ConfigureAwait(false);
        }

        public async Task InsertUserAsync(UserWithTypeTitle user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var newUser = new User
            {
                Name = user.Name,
                Surname = user.Surname,
                BirthDate = user.BirthDate,
                EmailAddress = user.EmailAddress,
                UserTitleId = user.UserTitleId,
                UserTypeId = user.UserTypeId,
                IsActive = user.IsActive
            };

            _ = _context.User.Add(newUser);
            _ = await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateUserAsync(UserWithTypeTitle user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var oldUser = await _context.User
                                    .SingleOrDefaultAsync(x => x.Id == user.Id)
                                    .ConfigureAwait(false);
            if (oldUser == null)
            {
                throw new KeyNotFoundException();
            }

            oldUser.Name = user.Name;
            oldUser.Surname = user.Surname;
            oldUser.BirthDate = user.BirthDate;
            oldUser.EmailAddress = user.EmailAddress;
            oldUser.IsActive = user.IsActive;
            oldUser.UserTitleId = user.UserTitleId;
            oldUser.UserTypeId = user.UserTypeId;

            _ = _context.Update(oldUser);
            _ = await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteUser(int userId)
        {
            if (userId <= default(int))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var user = await _context.User
                               .SingleOrDefaultAsync(x => x.Id == userId &&
                                                          x.IsActive.HasValue &&
                                                          x.IsActive.Value)
                               .ConfigureAwait(false);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            user.IsActive = false;

            _ = _context.Update(user);
            _ = await _context.SaveChangesAsync().ConfigureAwait(false);
        }

    }
}
