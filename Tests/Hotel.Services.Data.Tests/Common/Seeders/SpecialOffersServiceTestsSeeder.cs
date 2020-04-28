namespace Hotel.Services.Data.Tests.Common.Seeders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;

    public class SpecialOffersServiceTestsSeeder
    {
        public async Task SeedSpecialOfferAsync(HotelDbContext context)
        {
            await this.SeedHotelAsync(context);

            var specialOffer = new SpecialOffer()
            {
                Title = "Title1",
                Content = "Content1",
                ShortContent = "ShortContenr1",
                HotelDataId = context.Hotels.FirstOrDefault().Id,
            };

            await context.SpecialOffers.AddAsync(specialOffer);

            await context.SaveChangesAsync();
        }

        public async Task SeedSpecialOffersAsync(HotelDbContext context)
        {
            await this.SeedHotelAsync(context);

            var specialOffers = new List<SpecialOffer>()
            {
                new SpecialOffer()
                {
                    Title = "Title1",
                    Content = "Content1",
                    ShortContent = "ShortContenr1",
                    HotelDataId = context.Hotels.FirstOrDefault().Id,
                },
                new SpecialOffer()
                {
                    Title = "Title1",
                    Content = "Content1",
                    ShortContent = "ShortContenr1",
                    HotelDataId = context.Hotels.FirstOrDefault().Id,
                },
                new SpecialOffer()
                {
                    Title = "Title1",
                    Content = "Content1",
                    ShortContent = "ShortContenr1",
                    HotelDataId = context.Hotels.FirstOrDefault().Id,
                },
            };

            await context.SpecialOffers.AddRangeAsync(specialOffers);

            await context.SaveChangesAsync();
        }

        public async Task SeedHotelAsync(HotelDbContext context)
        {
            var hotel = new HotelData { Name = "Hotel Boryana" };

            await context.Hotels.AddAsync(hotel);

            await context.SaveChangesAsync();
        }
    }
}
