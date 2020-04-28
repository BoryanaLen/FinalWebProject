namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Hotel.Services.Data.Common;

    public class ReservationRoomsService : IReservationRoomsService
    {
        private readonly IRepository<ReservationRoom> reservationRoomsRepository;

        public ReservationRoomsService(IRepository<ReservationRoom> reservationRoomsRepository)
        {
            this.reservationRoomsRepository = reservationRoomsRepository;
        }

        public async Task<bool> AddReservationRoomAsync(ReservationRoom reservationRoom)
        {
            if (reservationRoom.ReservationId == null ||
                reservationRoom.RoomId == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            await this.reservationRoomsRepository.AddAsync(reservationRoom);

            var result = await this.reservationRoomsRepository.SaveChangesAsync();

            return result > 0;
        }

        public IEnumerable<string> GetAllRoomsByReservationId(string id)
        {
            var roomIds = this.reservationRoomsRepository
                .AllAsNoTracking()
                .Where(x => x.ReservationId == id)
                .Select(x => x.RoomId)
                .ToList();

            return roomIds;
        }
    }
}
