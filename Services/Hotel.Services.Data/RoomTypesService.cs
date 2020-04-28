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
    using Hotel.Web.ViewModels.RoomTypes;
    using Microsoft.EntityFrameworkCore;

    public class RoomTypesService : IRoomTypesService
    {
        private readonly IDeletableEntityRepository<RoomType> roomTypeRepository;

        public RoomTypesService(IDeletableEntityRepository<RoomType> roomTypesRepository)
        {
            this.roomTypeRepository = roomTypesRepository;
        }

        public async Task<bool> AddRoomTypeAsync(RoomType roomType)
        {
            if (roomType.Name == null || roomType.Image == null || roomType.Description == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyNameErrorMessage));
            }

            await this.roomTypeRepository.AddAsync(roomType);

            var result = await this.roomTypeRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditAsync(EditRoomTypeViewModel roomTypeEditViewModel)
        {
            var roomType = this.roomTypeRepository.All().FirstOrDefault(r => r.Id == roomTypeEditViewModel.Id);

            if (roomType == null)
            {
                throw new ArgumentNullException(string.Format(string.Format(ServicesDataConstants.InvalidRoomTypeIdErrorMessage, roomTypeEditViewModel.Id)));
            }

            if (roomTypeEditViewModel.Name == null || roomTypeEditViewModel.Image == null || roomTypeEditViewModel.Description == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyNameErrorMessage));
            }

            if (roomTypeEditViewModel.RoomImage == null)
            {
                roomTypeEditViewModel.Image = roomType.Image;
            }

            roomType.Name = roomTypeEditViewModel.Name;
            roomType.Image = roomTypeEditViewModel.Image;
            roomType.Price = roomTypeEditViewModel.Price;
            roomType.CapacityAdults = roomTypeEditViewModel.CapacityAdults;
            roomType.CapacityKids = roomTypeEditViewModel.CapacityKids;
            roomType.Description = roomTypeEditViewModel.Description;

            this.roomTypeRepository.Update(roomType);

            var result = await this.roomTypeRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var roomType = await this.roomTypeRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (roomType == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidRoomTypeIdErrorMessage, id));
            }

            roomType.IsDeleted = true;

            this.roomTypeRepository.Update(roomType);

            var result = await this.roomTypeRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<RoomType> GetRoomTypeByIdAsync(string id)
        {
            var roomType = await this.roomTypeRepository.GetByIdWithDeletedAsync(id);

            if (roomType == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidRoomTypeIdErrorMessage, id));
            }

            return roomType;
        }

        public async Task<int> GetAllRoomTypesCountAsync()
        {
            var roomTypes = await this.roomTypeRepository.All()
                .ToArrayAsync();
            return roomTypes.Count();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id)
        {
            var roomType = await this.roomTypeRepository
                 .All()
                 .Where(d => d.Id == id)
                 .To<TViewModel>()
                 .FirstOrDefaultAsync();

            if (roomType == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidRoomTypeIdErrorMessage, id));
            }

            return roomType;
        }

        public IEnumerable<T> GetAllRoomTypes<T>(int? count = null)
        {
            IQueryable<RoomType> query =
                 this.roomTypeRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public RoomType GetRoomTypeByName(string name)
        {
            var roomType = this.roomTypeRepository
                .AllAsNoTracking()
                .Where(x => x.Name == name)
                .FirstOrDefault();

            if (roomType == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidRoomTypeNameErrorMessage, name));
            }

            return roomType;
        }
    }
}
