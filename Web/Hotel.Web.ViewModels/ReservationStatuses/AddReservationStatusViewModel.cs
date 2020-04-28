namespace Hotel.Web.ViewModels.ReservationStatuses
{
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class AddReservationStatusViewModel : IMapFrom<ReservationStatus>, IMapTo<ReservationStatus>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
