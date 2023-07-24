using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastingClubDAL.Enums;

namespace TastingClubBLL.DTOs.EventParticipantDTOs
{
    public class EventParticipantDtoForCreate
    {
        public int EventId { get; set; }
        public string UserId { get; set; }
    }
}
