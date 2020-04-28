namespace Hotel.Web.ViewModels.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class RoomTypeCalendarViewModel : IHaveCustomMappings, IMapFrom<RoomType>
    {
        public RoomTypeCalendarViewModel()
        {
            this.color = "#cb6bb2";
        }

        public string id { get; set; }

        public string text { get; set; }

        public string color { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RoomType, RoomTypeCalendarViewModel>()
               .ForMember(d => d.text, o => o.MapFrom(x => x.Name))
               .ForMember(d => d.id, o => o.MapFrom(x => x.Id));
        }
    }
}
