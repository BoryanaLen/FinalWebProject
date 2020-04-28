namespace Hotel.Web.ViewModels.UserRequests
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class UserRequestViewModel : IMapFrom<UserRequest>, IMapTo<UserRequest>
    {
        private const int TittleMaxLength = 50;
        private const int ContentMaxLength = 2000;

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
