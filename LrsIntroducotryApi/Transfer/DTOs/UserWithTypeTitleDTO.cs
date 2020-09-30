using System;

namespace LrsIntroducotryApi.Transfer.DTOs
{
    public class UserWithTypeTitleDTO
    {
        /// <summary>
        /// The User identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The User Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The User Surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// The User date of birth.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// The User type identifier.
        /// </summary>
        public int UserTypeId { get; set; }

        /// <summary>
        /// The User type.
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// The User title identifier.
        /// </summary>
        public int UserTitleId { get; set; }

        /// <summary>
        /// The User title.
        /// </summary>
        public string UserTitle { get; set; }

        /// <summary>
        /// The User email.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Determines if the user is active.
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
