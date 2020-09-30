using System;
using System.Collections.Generic;

namespace LrsIntroducotryApi.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int UserTypeId { get; set; }
        public int UserTitleId { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }

        public virtual UserTitle UserTitle { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
