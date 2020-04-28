namespace Hotel.Web.ViewModels.PaymentTypes
{
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class DetailsPaymentTypeViewModel : IMapFrom<PaymentType>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
