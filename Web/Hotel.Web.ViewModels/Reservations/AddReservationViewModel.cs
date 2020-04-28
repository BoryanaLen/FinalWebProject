namespace Hotel.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.PaymentTypes;
    using Hotel.Web.ViewModels.Rooms;
    using Hotel.Web.ViewModels.RoomTypes;

    public class AddReservationViewModel : IMapFrom<Reservation>, IMapTo<Reservation>
    {
        public AddReservationViewModel()
        {
            this.IsPaid = false;
            this.ListOfPaymentTypes = new HashSet<PaymentTypeDropDownViewModel>();
            this.ListOfFreeRooms = new HashSet<DetailsRoomViewModel>();
        }

        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        public int Adults { get; set; }

        public int Kids { get; set; }

        public string UserId => this.UserId;

        public virtual HotelUser User { get; set; }

        [Required]
        public string ReservationStatusId { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }

        public string PaymentTypeId { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public decimal PricePerDay => this.ListOfRoomsInReservation.Sum(x => x.RoomTypePrice);

        public int TotalDays => (int)(this.EndDate.Date - this.StartDate.Date).TotalDays;

        public decimal TotalAmount => this.TotalDays * this.PricePerDay;

        [Required]
        public bool IsPaid { get; set; }

        public string UserFirstName => this.User.FirstName;

        public string UserLastName => this.User.LastName;

        public IEnumerable<PaymentTypeDropDownViewModel> ListOfPaymentTypes { get; set; }

        public IEnumerable<DetailsRoomViewModel> ListOfFreeRooms { get; set; }

        public IEnumerable<DetailsRoomViewModel> ListOfRoomsInReservation { get; set; }

        public ICollection<ReservationPayment> ReservationPayments { get; set; }
    }
}
