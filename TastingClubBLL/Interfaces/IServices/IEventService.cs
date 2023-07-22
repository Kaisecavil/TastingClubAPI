using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastingClubBLL.DTOs.EventDTOs;
using TastingClubBLL.ViewModels.EventViewModels;
using TastingClubDAL.Models;

namespace TastingClubBLL.Interfaces.IServices
{
    public interface IEventService
    {
        public Task<EventDetailViewModel> GetEventAsync(int id);
        public Task<List<EventGeneralViewModel>> GetAllEventsAsync();
        public Task CreateEventAsync(EventDtoForCreate eventDto);
        public Task UpdateEvent(EventDtoForUpdate eventDto);
        public Task DeleteEvent(int id);
    }
}
