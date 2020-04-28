namespace Hotel.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Services.Data;
    using Hotel.Web.ViewModels.Administration.Dashboard;
    using Hotel.Web.ViewModels.Reservations;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IReservationsService reservationsService;
        private readonly IRoomsService roomsService;

        public DashboardController(
            ISettingsService settingsService,
            IReservationsService reservationsService,
            IRoomsService roomsService)
        {
            this.settingsService = settingsService;
            this.reservationsService = reservationsService;
            this.roomsService = roomsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            int roomsCount = await this.roomsService.GetAllRoomsCountAsync();

            viewModel.ReservedRooms = this.reservationsService.GetReservedRooms();
            viewModel.ExpectedRoomsArrivals = this.reservationsService.GetRoomsArrivals();
            viewModel.ExpectedRoomsDepartures = this.reservationsService.GetRoomsDeparture();
            viewModel.RoomsEndOfDay = viewModel.ReservedRooms + viewModel.ExpectedRoomsArrivals - viewModel.ExpectedRoomsDepartures;
            viewModel.OccupiedRooms = this.reservationsService.GetAllOccupiedRooms();
            viewModel.AvailableRooms = roomsCount - viewModel.OccupiedRooms;

            var allReservations = this.reservationsService
                .GetAllReservations<DetailsReservationViewModel>()
                .Where(x => x.ReservationStatusName == "Pending" && x.AdvancedPayment > 0)
                .ToList();

            viewModel.AllReservations = new AllReservationsViewModel { Reservations = allReservations };

            return this.View(viewModel);
        }

        public async Task<JsonResult> RoomsChart()
        {
            int occupiedRooms = this.reservationsService.GetAllOccupiedRooms();
            int totalRooms = await this.roomsService.GetAllRoomsCountAsync();

            List<PieChartViewModel> data = new List<PieChartViewModel>()
            {
                new PieChartViewModel
                {
                    Status = "Available",
                    Count = totalRooms - occupiedRooms,
                },
                new PieChartViewModel
                {
                    Status = "Occupied",
                    Count = occupiedRooms,
                },
            };

            return this.Json(data);
        }

        public JsonResult IncomesChart()
        {
            List<ColumnChartViewModel> data2 = this.reservationsService.IncomesForCurrentYear().ToList();

            return this.Json(data2);
        }
    }
}
