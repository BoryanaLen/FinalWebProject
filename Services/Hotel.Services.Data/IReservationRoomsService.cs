namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data.Models;

    public interface IReservationRoomsService
    {
        Task<bool> AddReservationRoomAsync(ReservationRoom reservationRoom);

        IEnumerable<string> GetAllRoomsByReservationId(string id);
    }
}
