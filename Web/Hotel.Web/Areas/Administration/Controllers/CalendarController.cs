namespace Hotel.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Hotel.Web.ViewModels.Calendar;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CalendarController : AdministrationController
    {
        private readonly IRoomsService roomsService;
        private readonly IRoomTypesService roomTypesService;
        private readonly IReservationRoomsService reservationRoomsService;
        private readonly IReservationsService reservationsService;
        private readonly UserManager<HotelUser> userManager;

        public CalendarController(
            IRoomsService roomsService,
            IRoomTypesService roomTypesService,
            IReservationRoomsService reservationRoomsService,
            IReservationsService reservationsService,
            UserManager<HotelUser> userManager)
        {
            this.roomsService = roomsService;
            this.roomTypesService = roomTypesService;
            this.reservationRoomsService = reservationRoomsService;
            this.reservationsService = reservationsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<ReservationCalendarViewModel> resrevationsList = new List<ReservationCalendarViewModel>();

            List<Reservation> allReservations = this.reservationsService
                .GetAllReservationsList()
                .ToList();

            foreach (var res in allReservations)
            {
                var roomStrings = this.reservationRoomsService.GetAllRoomsByReservationId(res.Id);
                var user = await this.userManager.FindByIdAsync(res.UserId);

                foreach (var roomString in roomStrings)
                {
                    Room room = await this.roomsService.GetRoomByIdAsync(roomString);

                    var resCalendar = new ReservationCalendarViewModel
                    {
                        Subject = user.FirstName + " " + user.LastName,
                        StartTime = res.StartDate,
                        EndTime = res.EndDate,
                        ProjectId = room.RoomTypeId,
                        TaskId = room.Id,
                    };

                    resrevationsList.Add(resCalendar);
                }
            }

            this.ViewBag.datasource = resrevationsList;

            List<RoomTypeCalendarViewModel> roomTypes = this.roomTypesService
                .GetAllRoomTypes<RoomTypeCalendarViewModel>()
                .ToList();
            this.ViewBag.Projects = roomTypes;

            List<RoomCalendarViewModel> categories = this.roomsService
                .GetAllRooms<RoomCalendarViewModel>()
                .ToList();
            this.ViewBag.Categories = categories;

            this.ViewBag.Resources = new string[] { "Projects", "Categories" };
            return this.View();
        }
    }
}
