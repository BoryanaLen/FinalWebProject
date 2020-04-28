namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Hotel.Services.Data.Common;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.Payments;
    using Microsoft.EntityFrameworkCore;

    public class PaymentsService : IPaymentsService
    {
        private readonly IDeletableEntityRepository<Payment> paymentRepository;

        public PaymentsService(IDeletableEntityRepository<Payment> paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<bool> AddPaymentAsync(Payment payment)
        {
            if (payment.DateOfPayment == null ||
                payment.PaymentTypeId == null ||
                payment.ReservationPayments.Count == 0)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            await this.paymentRepository.AddAsync(payment);

            var result = await this.paymentRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditAsync(EditPaymentViewModel paymentEditViewModel)
        {
            var payment = this.paymentRepository.All().FirstOrDefault(p => p.Id == paymentEditViewModel.Id);

            if (payment == null)
            {
                throw new ArgumentNullException(string.Format(string.Format(ServicesDataConstants.InvalidPaymentIdErrorMessage, paymentEditViewModel.Id)));
            }

            if (paymentEditViewModel.DateOfPayment == null ||
                paymentEditViewModel.PaymentTypeId == null ||
                paymentEditViewModel.ReservationPayments.Count == 0)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            payment.DateOfPayment = paymentEditViewModel.DateOfPayment;
            payment.PaymentTypeId = paymentEditViewModel.PaymentTypeId;
            payment.Amount = paymentEditViewModel.Amount;

            this.paymentRepository.Update(payment);

            var result = await this.paymentRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var payment = await this.paymentRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (payment == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPaymentIdErrorMessage, id));
            }

            payment.IsDeleted = true;

            this.paymentRepository.Update(payment);

            var result = await this.paymentRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Payment> GetPaymentByIdAsync(string id)
        {
            var payment = await this.paymentRepository.GetByIdWithDeletedAsync(id);

            if (payment == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPaymentIdErrorMessage, id));
            }

            return payment;
        }

        public async Task<int> GetAllPaymentsCountAsync()
        {
            var rooms = await this.paymentRepository.All()
                .ToArrayAsync();
            return rooms.Count();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id)
        {
            var payment = await this.paymentRepository
                 .All()
                 .Where(d => d.Id == id)
                 .To<TViewModel>()
                 .FirstOrDefaultAsync();

            if (payment == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPaymentIdErrorMessage, id));
            }

            return payment;
        }

        public IQueryable<T> GetAllPayments<T>(int? count = null)
        {
            IQueryable<Payment> query =
                this.paymentRepository
                .All()
                .OrderBy(x => x.DateOfPayment);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>();
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            var payments =
                this.paymentRepository
                .All()
                .OrderBy(x => x.CreatedOn);

            return payments;
        }

        public IQueryable<string> GetAllPaymentsForReservation(string reservationId)
        {
            IQueryable<string> payments = this.paymentRepository
               .AllAsNoTracking()
               .SelectMany(x => x.ReservationPayments)
               .Where(x => x.ReservationId == reservationId)
               .Select(x => x.PaymentId);

            return payments;
        }

        public async Task<bool> SaveChangesForPaymentAsync(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(string.Format(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage)));
            }

            this.paymentRepository.Update(payment);

            var result = await this.paymentRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
