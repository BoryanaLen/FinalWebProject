namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Hotel.Data.Common.Models;

    public class UserRequest : BaseDeletableModel<string>
    {
        private const int TittleMaxLength = 50;
        private const int ContentMaxLength = 2000;

        public UserRequest()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Seen = false;
        }

        [Required]
        [MaxLength(TittleMaxLength)]
        public string Title { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public bool Seen { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
