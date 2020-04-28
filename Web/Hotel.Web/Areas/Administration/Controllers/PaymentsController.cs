namespace Hotel.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.Payments;
    using Hotel.Web.ViewModels.PaymentTypes;
    using Hotel.Web.ViewModels.Reservations;
    using Microsoft.AspNetCore.Mvc;

    public class PaymentsController : AdministrationController
    {
        private readonly IPaymentsService paymentsService;
        private readonly IReservationsService reservationsService;
        private readonly IReservationPaymentsService reservationPaymentsService;
        private readonly IPaymentTypesService paymentTypesService;

        public PaymentsController(
            IPaymentsService paymentsService,
            IReservationsService reservationsService,
            IReservationPaymentsService reservationPaymentsService,
            IPaymentTypesService paymentTypesService)
        {
            this.paymentsService = paymentsService;
            this.reservationsService = reservationsService;
            this.reservationPaymentsService = reservationPaymentsService;
            this.paymentTypesService = paymentTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = GlobalConstants.DefaultPageNumber, int perPage = GlobalConstants.PageSize)
        {
            int paymentsCount = await this.paymentsService.GetAllPaymentsCountAsync();

            var pagesCount = (int)Math.Ceiling(paymentsCount / (decimal)perPage);

            var payments = this.paymentsService
               .GetAllPayments<DetailsPaymentViewModel>()
               .OrderByDescending(x => x.DateOfPayment)
               .Skip(perPage * (page - 1))
               .Take(perPage)
               .ToList();

            foreach (var payment in payments)
            {
                var ids = this.reservationPaymentsService
                     .GetAllReservationsByPaymentId(payment.Id).ToList();

                payment.ReservationIds = ids;

                payment.ReservationNumbers = string.Join(", ", ids);
            }

            var model = new AllPaymentsViewModel
            {
                Payments = payments,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var paymentTypes = this.paymentTypesService
               .GetAllPaymentTypes<PaymentTypeDropDownViewModel>();

            var reservations = this.reservationsService
                .GetAllReservations<DetailsReservationViewModel>()
                .Where(x => x.TotalAmount > x.AdvancedPayment)
                .ToList();

            var model = new AddPaymentInputModel()
            {
                ListOfPaymentTypes = paymentTypes,
                ListOfNotPaidReservations = reservations,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPaymentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.ListOfPaymentTypes = this.paymentTypesService
                .GetAllPaymentTypes<PaymentTypeDropDownViewModel>();

                model.ListOfNotPaidReservations = this.reservationsService
                .GetAllReservations<DetailsReservationViewModel>()
                .Where(x => x.TotalAmount > x.AdvancedPayment)
                .ToList();

                return this.View(model);
            }

            Payment payment = AutoMapperConfig.MapperInstance.Map<Payment>(model);

            List<Reservation> reservations = new List<Reservation>();

            foreach (var res in model.ReservationIds)
            {
                var reservation = await this.reservationsService.GetReservationByIdAsync(res);

                reservations.Add(reservation);
            }

            decimal totaAmoumtForAllReservations = reservations.Sum(x => x.TotalAmount);

            foreach (var res in reservations)
            {
                res.AdvancedPayment += model.Amount * res.TotalAmount / totaAmoumtForAllReservations;

                await this.reservationsService.SaveChangesForReservationAsync(res);

                var reservationPayment = new ReservationPayment
                {
                    PaymentId = payment.Id,
                    ReservationId = res.Id,
                };

                payment.ReservationPayments.Add(reservationPayment);
            }

            await this.paymentsService.AddPaymentAsync(payment);

            return this.Redirect($"/Administration/Payments/All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var paymentToDelete = await this.paymentsService.GetViewModelByIdAsync<DeletePaymentViewModel>(id);

            return this.View(paymentToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePaymentViewModel deleteViewModel)
        {
            var id = deleteViewModel.Id;

            await this.paymentsService.DeleteByIdAsync(id);

            return this.Redirect($"/Administration/Payments/All");
        }
    }
}
