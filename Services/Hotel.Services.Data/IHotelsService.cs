namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data.Models;

    public interface IHotelsService
    {
        Task<bool> AddRHotelAsync(HotelData hotel);

        HotelData GetHotelByName(string name);
    }
}
