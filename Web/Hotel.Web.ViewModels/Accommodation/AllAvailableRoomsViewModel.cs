namespace Hotel.Web.ViewModels.Accommodation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Hotel.Data.Models;
    using Hotel.Web.ViewModels.Attributes.Validation;
    using Hotel.Web.ViewModels.PaymentTypes;
    using Hotel.Web.ViewModels.Rooms;
    using Microsoft.AspNetCore.Mvc;

    public class AllAvailableRoomsViewModel : PagedListViewModel 
    {
        public AllAvailableRoomsViewModel()
        {
            this.ListOfRoomsInReservation = new HashSet<DetailsRoomViewModel>();
            this.ListOfPaymentTypes = new HashSet<PaymentTypeDropDownViewModel>();
            this.RoomIds = new List<string>();
        }

        public IEnumerable<AvailableRoomViewModel> Rooms { get; set; }

        public string CheckIn { get; set; }

        public string CheckOut { get; set; }

        public int Adults { get; set; }

        public int Kids { get; set; }

        [BindProperty]
        [EnsureOneElementAttribute]
        public List<string> RoomIds { get; set; }

        public string PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }

        public string ReservationStatusId { get; set; }

        public HotelUser User { get; set; }

        [Display(Name ="First name")]
        public string UserFirstName { get; set; }

        [Display(Name = "Last name")]
        public string UserLastName { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        [Display(Name = "Price per day")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Total days")]
        public int TotalDays { get; set; }

        [Display(Name = "Total amount")]
        public decimal TotalAmount { get; set; }

        public IEnumerable<DetailsRoomViewModel> ListOfRoomsInReservation { get; set; }

        public IEnumerable<PaymentTypeDropDownViewModel> ListOfPaymentTypes { get; set; }
    }
}
