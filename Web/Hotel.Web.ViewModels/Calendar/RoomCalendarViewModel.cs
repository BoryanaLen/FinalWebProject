namespace Hotel.Web.ViewModels.Calendar
{
    using AutoMapper;
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class RoomCalendarViewModel : IHaveCustomMappings, IMapFrom<Room>
    {
        public RoomCalendarViewModel()
        {
            this.color = "#5978ee";
        }

        public string id { get; set; }

        public string text { get; set; }

        public string groupId { get; set; }

        public string color { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Room, RoomCalendarViewModel>()
               .ForMember(d => d.text, o => o.MapFrom(x => "Room number - " + x.RoomNumber))
               .ForMember(d => d.groupId, o => o.MapFrom(x => x.RoomType.Id));
        }
    }
}
