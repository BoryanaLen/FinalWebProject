namespace Hotel.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Hotel.Services.Data.Common;

    public class HotelsService : IHotelsService
    {
        private readonly IDeletableEntityRepository<HotelData> hotelRepository;

        public HotelsService(IDeletableEntityRepository<HotelData> hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public async Task<bool> AddRHotelAsync(HotelData hotel)
        {
            if (hotel.UniqueIdentifier == null || hotel.Name == null || hotel.Address == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            await this.hotelRepository.AddAsync(hotel);

            var result = await this.hotelRepository.SaveChangesAsync();

            return result > 0;
        }

        public HotelData GetHotelByName(string name)
        {
            var hotel = this.hotelRepository
                .AllAsNoTracking()
                .Where(x => x.Name == name)
                .FirstOrDefault();

            if (hotel == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidHotelNameErrorMessage, name));
            }

            return hotel;
        }
    }
}
