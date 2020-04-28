namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Hotel.Services.Data.Common;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.Rooms;
    using Microsoft.EntityFrameworkCore;

    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomRepository;
        private readonly IDeletableEntityRepository<RoomType> roomTypeRepository;

        public RoomsService(
            IDeletableEntityRepository<Room> roomRepository,
            IDeletableEntityRepository<RoomType> roomTypeRepository)
        {
            this.roomRepository = roomRepository;
            this.roomTypeRepository = roomTypeRepository;
        }

        public async Task<bool> AddRoomAsync(Room room)
        {
            if (room.RoomNumber == null ||
                room.Description == null ||
                room.RoomTypeId == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            await this.roomRepository.AddAsync(room);

            var result = await this.roomRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditAsync(EditRoomViewModel roomEditViewModel)
        {
            var room = this.roomRepository.All().FirstOrDefault(r => r.Id == roomEditViewModel.Id);

            if (room == null)
            {
                throw new ArgumentNullException(string.Format(string.Format(ServicesDataConstants.InvalidRoomIdErrorMessage, roomEditViewModel.Id)));
            }

            if (roomEditViewModel.RoomNumber == null ||
                roomEditViewModel.Description == null ||
                roomEditViewModel.RoomTypeId == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            room.RoomNumber = roomEditViewModel.RoomNumber;
            room.Description = roomEditViewModel.Description;
            room.RoomTypeId = roomEditViewModel.RoomTypeId;

            this.roomRepository.Update(room);

            var result = await this.roomRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var room = await this.roomRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (room == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidRoomIdErrorMessage, id));
            }

            room.IsDeleted = true;

            this.roomRepository.Update(room);

            var result = await this.roomRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Room> GetRoomByIdAsync(string id)
        {
            var room = await this.roomRepository.GetByIdWithDeletedAsync(id);

            if (room == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidRoomIdErrorMessage, id));
            }

            return room;
        }

        public async Task<int> GetAllRoomsCountAsync()
        {
            var rooms = await this.roomRepository.All()
                .ToArrayAsync();
            return rooms.Count();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id)
        {
            var room = await this.roomRepository
                 .All()
                 .Where(d => d.Id == id)
                 .To<TViewModel>()
                 .FirstOrDefaultAsync();

            if (room == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidRoomIdErrorMessage, id));
            }

            return room;
        }

        public IQueryable<T> GetAllRooms<T>(int? count = null)
        {
            IQueryable<Room> query =
                this.roomRepository
                .All()
                .OrderBy(x => x.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>();
        }

        public Room GetRoomByRoomTypeName(string name)
        {
            var room = this.roomRepository
               .AllAsNoTracking()
               .Where(x => x.RoomType.Name == name)
               .FirstOrDefault();

            if (room == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidRoomTypeNameErrorMessage, name));
            }

            return room;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            var rooms =
                this.roomRepository
                .All()
                .OrderBy(x => x.CreatedOn);

            return rooms;
        }
    }
}
