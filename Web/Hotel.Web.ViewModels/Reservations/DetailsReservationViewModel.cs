namespace Hotel.Web.ViewModels.Reservations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class DetailsReservationViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public virtual HotelUser User { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserFullName => this.UserFirstName + " " + this.UserLastName;

        public int Persons { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal AdvancedPayment { get; set; }

        public bool IsPaid { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int TotalDays => (int)(this.EndDate - this.StartDate).TotalDays;

        public string RoomId { get; set; }

        public virtual Room Room { get; set; }

        public string RoomRoomNumber { get; set; }

        public string ReservationStatusId { get; set; }

        public string ReservationStatusName{ get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
