namespace Hotel.Services.Data.Tests.Common
{
    using System.Reflection;

    using Hotel.Services.Mapping;

    using Hotel.Web.ViewModels.SpecialOffers;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(AddSpecialOfferInputModel).GetTypeInfo().Assembly);
        }
    }
}
