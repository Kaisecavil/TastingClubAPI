using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastingClubDAL.Enums;
using TastingClubDAL.Models;

namespace TastingClubBLL.DTOs.EventParticipantDTOs
{
    public class EventParticipantDtoForUpdate
    {
        public int Id { get; set; }
        public EventPartisipantStatus EventParticipantStatus { get; set; }
    }
}
