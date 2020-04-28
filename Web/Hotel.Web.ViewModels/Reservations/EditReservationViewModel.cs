namespace Hotel.Web.ViewModels.Reservations
{
    using System;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class EditReservationViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserId { get; set; }

        public virtual HotelUser User { get; set; }

        public int Adults { get; set; }

        public int Kids { get; set; }

        public int TotalDays { get; set; }

        public string RoomType { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal AdvancedPayment { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string RoomId { get; set; }

        public virtual Room Room { get; set; }

        public bool IsPaid { get; set; }

        public string PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }

        public string ReservationStatusId { get; set; }
    }
}
