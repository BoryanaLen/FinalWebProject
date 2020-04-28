namespace Hotel.Web.ViewModels.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class ReservationCalendarViewModel
    {
        public ReservationCalendarViewModel()
        {
            this.IsAllDay = false;
        }

        public string Id { get; set; }

        public string Subject { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsAllDay { get; set; }

        public string ProjectId { get; set; }

        public string TaskId { get; set; }
    }
}
