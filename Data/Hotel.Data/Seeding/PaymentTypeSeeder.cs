namespace Hotel.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Services.Data;
    using Microsoft.Extensions.DependencyInjection;

    public class PaymentTypeSeeder : ISeeder
    {
        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PaymentTypes.Any())
            {
                return;
            }

            var paymentTypesService = serviceProvider.GetRequiredService<IPaymentTypesService>();

            var paymentTypes = new string[]
            {
                "Cash",
                "Direct bank transfer",
                "PayPal",
            };

            await paymentTypesService.CreateAllAsync(paymentTypes);
        }
    }
}
