using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastingClubDAL.Models.Base;

namespace TastingClubDAL.Interfaces.IModel
{
    public interface IPhotoModel
    {
        public int PhotoId { get; set; }
        public int EntityId { get; set; }
    }
}
