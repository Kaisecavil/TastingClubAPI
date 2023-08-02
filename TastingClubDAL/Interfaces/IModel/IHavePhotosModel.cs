using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastingClubDAL.Models;

namespace TastingClubDAL.Interfaces.IModel
{
    public interface IHavePhotosModel
    {
        List<IPhotoModel> Photos { get; }
    }
}
