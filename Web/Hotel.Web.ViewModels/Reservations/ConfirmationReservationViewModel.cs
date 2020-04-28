namespace Hotel.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.Rooms;

    public class ConfirmationReservationViewModel : IMapFrom<Reservation>
    {
        public ConfirmationReservationViewModel()
        {
            this.Rooms = new List<DetailsRoomViewModel>();
        }

        public string Id { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserPhoneNumber { get; set; }

        public string UserEmail { get; set; }

        public int Adults { get; set; }

        public int Kids { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalDays => (int)(this.EndDate - this.StartDate).TotalDays;

        public string ReservationStatusName { get; set; }

        public PaymentType PaymentType { get; set; }

        public string PaymentTypeName { get; set; }

        public List<DetailsRoomViewModel> Rooms { get; set; }
    }
}
