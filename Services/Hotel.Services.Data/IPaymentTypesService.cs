namespace Hotel.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Web.ViewModels.PaymentTypes;

    public interface IPaymentTypesService
    {
        Task<bool> AddPaymentTypeAsync(PaymentType paymentType);

        Task<bool> EditAsync(EditPaymentTypeViewModel paymentTypeEditViewModel);

        Task<bool> DeleteByIdAsync(string id);

        Task<PaymentType> GetPaymentTypeByIdAsync(string id);

        Task<int> GetAllPaymentTypesCountAsync();

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id);

        IEnumerable<T> GetAllPaymentTypes<T>(int? count = null);

        Task<bool> CreateAllAsync(string[] types);

        Task<PaymentType> GetPaymentTypeByNameAsync(string name);
    }
}
