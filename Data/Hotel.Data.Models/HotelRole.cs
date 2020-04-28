// ReSharper disable VirtualMemberCallInConstructor
namespace Hotel.Data.Models
{
    using System;

    using Hotel.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class HotelRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public HotelRole()
            : this(null)
        {
        }

        public HotelRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
