namespace Hotel.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class DeleteReservationViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        public int Adults { get; set; }

        public int Kids { get; set; }

        public string UserId { get; set; }

        public virtual HotelUser User { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }
    }
}
