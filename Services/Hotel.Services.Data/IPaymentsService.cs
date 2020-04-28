namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Web.ViewModels.Payments;

    public interface IPaymentsService
    {
        Task<bool> AddPaymentAsync(Payment payment);

        Task<bool> EditAsync(EditPaymentViewModel paymentEditViewModel);

        Task<bool> DeleteByIdAsync(string id);

        Task<Payment> GetPaymentByIdAsync(string id);

        Task<int> GetAllPaymentsCountAsync();

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id);

        IQueryable<T> GetAllPayments<T>(int? count = null);

        IQueryable<string> GetAllPaymentsForReservation(string reservationId);

        IEnumerable<Payment> GetAllPayments();

        Task<bool> SaveChangesForPaymentAsync(Payment payment);
    }
}
