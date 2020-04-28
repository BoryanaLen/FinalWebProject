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
    using Hotel.Web.ViewModels.PaymentTypes;
    using Microsoft.EntityFrameworkCore;

    public class PaymentTypesService : IPaymentTypesService
    {
        private readonly IDeletableEntityRepository<PaymentType> paymentTypesRepository;

        public PaymentTypesService(IDeletableEntityRepository<PaymentType> paymentTypesRepository)
        {
            this.paymentTypesRepository = paymentTypesRepository;
        }

        public async Task<bool> AddPaymentTypeAsync(PaymentType paymentType)
        {
            if (paymentType.Name == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyNameErrorMessage));
            }

            await this.paymentTypesRepository.AddAsync(paymentType);

            var result = await this.paymentTypesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditAsync(EditPaymentTypeViewModel paymentTypeEditViewModel)
        {
            var paymentType = this.paymentTypesRepository.All().FirstOrDefault(r => r.Id == paymentTypeEditViewModel.Id);

            if (paymentType == null)
            {
                throw new ArgumentNullException(string.Format(string.Format(ServicesDataConstants.InvalidPaymentTypeIdErrorMessage, paymentTypeEditViewModel.Id)));
            }

            if (paymentTypeEditViewModel.Name == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyNameErrorMessage));
            }

            paymentType.Name = paymentTypeEditViewModel.Name;

            this.paymentTypesRepository.Update(paymentType);

            var result = await this.paymentTypesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var paymentType = await this.paymentTypesRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (paymentType == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPaymentTypeIdErrorMessage, id));
            }

            paymentType.IsDeleted = true;

            this.paymentTypesRepository.Update(paymentType);

            var result = await this.paymentTypesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<PaymentType> GetPaymentTypeByIdAsync(string id)
        {
            var paymentType = await this.paymentTypesRepository.GetByIdWithDeletedAsync(id);

            if (paymentType == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPaymentTypeIdErrorMessage, id));
            }

            return paymentType;
        }

        public async Task<int> GetAllPaymentTypesCountAsync()
        {
            var roomTypes = await this.paymentTypesRepository.All()
                .ToArrayAsync();
            return roomTypes.Count();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id)
        {
            var paymentType = await this.paymentTypesRepository
                 .All()
                 .Where(d => d.Id == id)
                 .To<TViewModel>()
                 .FirstOrDefaultAsync();

            if (paymentType == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPaymentTypeIdErrorMessage, id));
            }

            return paymentType;
        }

        public IEnumerable<T> GetAllPaymentTypes<T>(int? count = null)
        {
            IQueryable<PaymentType> query =
                 this.paymentTypesRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<bool> CreateAllAsync(string[] types)
        {
            foreach (var paymentType in types)
            {
                var model = new PaymentType
                {
                    Name = paymentType,
                };

                await this.paymentTypesRepository.AddAsync(model);
            }

            var result = await this.paymentTypesRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<PaymentType> GetPaymentTypeByNameAsync(string name)
        {
            var paymentType = this.paymentTypesRepository
                 .All()
                 .Where(x => x.Name == name)
                 .FirstOrDefault();

            if (paymentType == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPaymentTypeNameErrorMessage, name));
            }

            return paymentType;
        }
    }
}
