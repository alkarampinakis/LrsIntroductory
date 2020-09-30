using LrsIntroducotryApi.Models;
using LrsIntroducotryApi.Models.Entities.Custom;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LrsIntroducotryApi.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
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
    }
}
