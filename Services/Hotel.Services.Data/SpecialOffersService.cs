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
    using Hotel.Web.ViewModels.SpecialOffers;
    using Microsoft.EntityFrameworkCore;

    public class SpecialOffersService : ISpecialOffersService
    {
        private readonly IDeletableEntityRepository<SpecialOffer> specialOfferRepository;

        public SpecialOffersService(
            IDeletableEntityRepository<SpecialOffer> specialOfferRepository)
        {
            this.specialOfferRepository = specialOfferRepository;
        }

        public async Task<bool> AddSpecialOfferAsync(SpecialOffer offer)
        {
            if (offer.Title == null ||
                offer.Content == null ||
                offer.ShortContent == null ||
                offer.HotelDataId == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            await this.specialOfferRepository.AddAsync(offer);

            var result = await this.specialOfferRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditAsync(EditSpecialOfferViewModel specialOfferEditViewModel)
        {
            var specialOffer = this.specialOfferRepository.All().FirstOrDefault(r => r.Id == specialOfferEditViewModel.Id);

            if (specialOffer == null)
            {
                throw new ArgumentNullException(string.Format(string.Format(ServicesDataConstants.InvalidSpecialOfferIdErrorMessage, specialOfferEditViewModel.Id)));
            }

            if (specialOfferEditViewModel.Title == null || specialOfferEditViewModel.Content == null || specialOffer.ShortContent == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyNameErrorMessage));
            }

            specialOffer.Title = specialOfferEditViewModel.Title;
            specialOffer.Content = specialOfferEditViewModel.Content;
            specialOffer.ShortContent = specialOfferEditViewModel.ShortContent;

            this.specialOfferRepository.Update(specialOffer);

            var result = await this.specialOfferRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var specialOffer = await this.specialOfferRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (specialOffer == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidSpecialOfferIdErrorMessage, id));
            }

            specialOffer.IsDeleted = true;

            this.specialOfferRepository.Update(specialOffer);

            var result = await this.specialOfferRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<int> GetAllSpecialOffersCountAsync()
        {
            var roomTypes = await this.specialOfferRepository.All()
                .ToArrayAsync();
            return roomTypes.Count();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id)
        {
            var specialOffer = await this.specialOfferRepository
                 .All()
                 .Where(d => d.Id == id)
                 .To<TViewModel>()
                 .FirstOrDefaultAsync();

            if (specialOffer == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidSpecialOfferIdErrorMessage, id));
            }

            return specialOffer;
        }

        public IQueryable<T> GetAllSpecialOffers<T>(int? count = null)
        {
            IQueryable<SpecialOffer> query =
                this.specialOfferRepository
                .All()
                .OrderByDescending(x => x.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>();
        }

        public async Task<bool> CreateAllAsync(IEnumerable<SpecialOffer> offers)
        {
            foreach (var offer in offers)
            {
                await this.specialOfferRepository.AddAsync(offer);
            }

            var result = await this.specialOfferRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
