namespace Hotel.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Microsoft.Extensions.DependencyInjection;

    public class SpecialOffersSeeder : ISeeder
    {
        public SpecialOffersSeeder()
        {
        }

        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SpecialOffers.Any())
            {
                return;
            }

            var specialOffersService = serviceProvider.GetRequiredService<ISpecialOffersService>();

            var hotelsService = serviceProvider.GetRequiredService<IHotelsService>();

            var hotel = hotelsService.GetHotelByName("Hotel Boryana");

            var offers = new List<SpecialOffer>();

            var offerOne = new SpecialOffer
            {
                Title = "Saint Valentine",
                ShortContent = "Romance and Relax",
                Content = @"Only 200.00 EUR for two nights
                          Stay in the beautiful room facing the mountain.                                   
                          Rich breakfast buffet with homemade pastries and croissants,
                          cold cuts and cheeses of our territory, eggs, bacon, yoghurt, fresh fruit, cereals,
                          freshly baked bread and Gluten Free corner
                          Soft bedroom slippers
                          Romantic setting with petals and chocolates in the chosen room",
                HotelDataId = hotel.Id,
            };

            var offerTwo = new SpecialOffer
            {
                Title = "Easter",
                ShortContent = "Holiday and Relax",
                Content = @"Only 200.00 EUR for two nights.
                          Easter dinner with selected dishes, specially prepared by our experienced chefs and accompanied by a musical-artistic program;
                        Specially selected festive musical-artistic program;
                        Gifts for all hotel guests;
                        Children's party with gifts for children;
                        Use of swimming pool with mineral water 30-31 ° C;
                        Use of Jacuzzi with mineral water 36-38 ° C;
                        Use of sauna;
                        Use of fitness;
                        Use of steam room;
                        Using a relax zone;
                        Children's play area with children's animation every day from 9:30 to 12:00 and from 16:30 to 22:00 for children from 4 to 12 years old.",
                HotelDataId = hotel.Id,
            };

            var offerThree = new SpecialOffer
            {
                Title = "Christmas",
                ShortContent = "Relax and Holiday",
                Content = @"Only 200.00 EUR for two nights.
                    Overnight with breakfast and dinner;                   
                    Traditional Christmas dinner with festive Christmas turkey and roast pig, and specially selected festive musical-artistic program;
                    Christmas gifts for all hotel guests;
                    Children's party with gifts for children;
                    Use of swimming pool with mineral water 30-31 ° C;
                    Use of Jacuzzi with mineral water 36-38 ° C;
                    Use of sauna;
                    Use of fitness;
                    Use of steam room;
                    Using a relax zone;
                    Children's play area with children's animation every day from 9:30 to 12:00 and from 16:30 to 22:00 for children from 4 to 12 years old.",
                HotelDataId = hotel.Id,
            };

            var offerFour = new SpecialOffer
            {
                Title = "New Year",
                ShortContent = "Fun and Holiday",
                Content = @"Only 200.00 EUR for two nights. 
                    New Year's Eve dinner consisting of a multi-level set menu and selected beverages (brand alcohol, selected wine and champagne glass for New Year's Eve), fireworks on 31.12;
                    New Year's Eve gifts for all hotel guests;
                    Special children's party with gifts for children;
                    Use of swimming pool with mineral water 30-31 ° C;
                    Use of Jacuzzi with mineral water 36-38 ° C;
                    Use of sauna;
                    Use of fitness;
                    Use of steam room;
                    Using a relax zone;
                    Children's play area with children's animation every day from 9:30 to 12:00 and from 16:30 to 22:00 for children from 4 to 12 years old;",
                HotelDataId = hotel.Id,
            };

            offers.AddRange(new List<SpecialOffer> { offerOne, offerTwo, offerThree, offerFour });
            offers.Add(offerTwo);
            offers.Add(offerThree);
            offers.Add(offerFour);

            await specialOffersService.CreateAllAsync(offers);
        }
    }
}
