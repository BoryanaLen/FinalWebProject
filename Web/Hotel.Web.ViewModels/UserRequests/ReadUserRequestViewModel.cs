namespace Hotel.Web.ViewModels.UserRequests
{
    using System;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class ReadUserRequestViewModel : IMapFrom<UserRequest>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public bool Seen { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
