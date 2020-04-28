namespace Hotel.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Hotel.Services.Messaging;
    using Hotel.Web.ViewModels.Payments;
    using Hotel.Web.ViewModels.Reservations;
    using HtmlAgilityPack;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReservationsController : AdministrationController
    {
        private readonly IReservationsService reservationsService;
        private readonly IReservationStatusesService reservationStatusesService;
        private readonly IPaymentsService paymentsService;
        private readonly IReservationPaymentsService reservationPaymentsService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<HotelUser> userManager;

        public ReservationsController(
            IReservationsService reservationsService,
            IReservationStatusesService reservationStatusesService,
            IPaymentsService paymentsService,
            IReservationPaymentsService reservationPaymentsService,
            IEmailSender emailSender,
            UserManager<HotelUser> userManager)
        {
            this.reservationsService = reservationsService;
            this.reservationStatusesService = reservationStatusesService;
            this.paymentsService = paymentsService;
            this.reservationPaymentsService = reservationPaymentsService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(int page = GlobalConstants.DefaultPageNumber, int perPage = GlobalConstants.PageSize)
        {
            int reservationsCount = await this.reservationsService.GetAllReservationsCountAsync();

            var pagesCount = (int)Math.Ceiling(reservationsCount / (decimal)perPage);

            var reservations = this.reservationsService
               .GetAllReservations<DetailsReservationViewModel>()
               .OrderByDescending(x => x.StartDate)
               .Skip(perPage * (page - 1))
               .Take(perPage);

            var model = new AllReservationsViewModel
            {
                Reservations = reservations.ToList(),
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var reservationToEdit = await this.reservationsService.GetViewModelByIdAsync<EditReservationViewModel>(id);

            var user = await this.userManager.FindByIdAsync(reservationToEdit.UserId);

            reservationToEdit.UserFirstName = user.FirstName;
            reservationToEdit.UserLastName = user.LastName;

            return this.View(reservationToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditReservationViewModel reservationEditView)
        {
            await this.reservationsService.EditAsync(reservationEditView);

            return this.Redirect($"/Administration/Reservation/All");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var reservationToDelete = await this.reservationsService.GetViewModelByIdAsync<DeleteReservationViewModel>(id);

            var user = await this.userManager.FindByIdAsync(reservationToDelete.UserId);

            reservationToDelete.UserFirstName = user.FirstName;
            reservationToDelete.UserLastName = user.LastName;

            return this.View(reservationToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteReservationViewModel deleteViewModel)
        {
            var id = deleteViewModel.Id;

            await this.reservationsService.DeleteByIdAsync(id);

            return this.Redirect($"/Administration/Reservations/All");
        }

        public async Task<IActionResult> Confirm(string id)
        {
            var reservation = await this.reservationsService.GetReservationByIdAsync(id);

            var status = this.reservationStatusesService.GetReserVationStatusByName("Confirmed");

            await this.reservationsService.ConfirmByIdAsync(reservation.Id, status.Id);

            var paymentIds = this.reservationPaymentsService.GetAllPaymentsByReservationId(reservation.Id);

            List<DetailsPaymentViewModel> payments = new List<DetailsPaymentViewModel>();

            foreach (var paymentId in paymentIds)
            {
                var payment = await this.paymentsService.GetViewModelByIdAsync<DetailsPaymentViewModel>(paymentId);
                payments.Add(payment);
            }

            var confirmationPayment = new ConfirmationPaymentViewModel
            {
                Payments = payments,
            };

            var emailContent = this.GenerateEmailContent(confirmationPayment);

            var emailAttachment = new EmailAttachment
            {
                Content = Encoding.UTF8.GetBytes(emailContent),
                FileName = "Confirmation.doc",
                MimeType = "application/msword",
            };

            var user = await this.userManager.FindByIdAsync(reservation.UserId);

            var attachments = new List<EmailAttachment> { emailAttachment };

            await this.emailSender.SendEmailAsync(
                    GlobalConstants.SystemEmail,
                    GlobalConstants.SystemName,
                    user.Email,
                    "Payment Confirmation",
                    $"This email is to confirmation your reservation No{reservation.Id}. Please find attaced file with details for your payment",
                    attachments);

            return this.Redirect($"/Administration/Reservations/ConfirmPayment");
        }

        public IActionResult ConfirmPayment()
        {
            return this.View();
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

        private string GenerateEmailContent(ConfirmationPaymentViewModel confirmationPaymentViewModel)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var payment in confirmationPaymentViewModel.Payments)
            {
                var paymentsInfoHtml = string.Format(
                   GlobalConstants.PaymentConfitmationHtmlInfo,
                   payment.DateOfPayment.ToString("dd/MM/yyyy"),
                   payment.Amount,
                   payment.PaymentTypeName);

                sb.AppendLine(paymentsInfoHtml);
            }

            var path = GlobalConstants.PaymentReceiptEmailHtmlPath;
            var doc = new HtmlDocument();
            doc.Load(path);

            var content = doc.Text;

            content = content.Replace(GlobalConstants.PaymentInfoPlaceholder, sb.ToString());

            return content;
        }
    }
}
