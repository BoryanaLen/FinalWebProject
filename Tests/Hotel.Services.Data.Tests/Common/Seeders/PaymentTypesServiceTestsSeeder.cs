namespace Hotel.Services.Data.Tests.Common.Seeders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;

    public class PaymentTypesServiceTestsSeeder
    {
        public async Task SeedPaymentTypeAsync(HotelDbContext context)
        {
            var paymentType = new PaymentType
            {
                Name = "Test-1",
            };

            await context.PaymentTypes.AddAsync(paymentType);

            await context.SaveChangesAsync();
        }

        public async Task SeedPaymentTypesAsync(HotelDbContext context)
        {
            var paymenrTypes = new List<PaymentType>()
            {
                new PaymentType()
                {
                    Name = "Test-1",
                },
                new PaymentType()
                {
                    Name = "Test-2",
                },
                new PaymentType()
                {
                    Name = "Test-3",
                },
            };

            await context.PaymentTypes.AddRangeAsync(paymenrTypes);

            await context.SaveChangesAsync();
        }
    }
}
