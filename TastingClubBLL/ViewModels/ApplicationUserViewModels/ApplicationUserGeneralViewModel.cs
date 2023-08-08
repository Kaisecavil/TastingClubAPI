using System.ComponentModel;
using TastingClubDAL.Constants.ModelConstants.ApplicationUserConstants;

namespace TastingClubBLL.ViewModels.ApplicationUserViewModels
{
    public class ApplicationUserGeneralViewModel
    {
        public string Id { get; set; }

        [DefaultValue(ApplicationUserDefaultValueConstants.DefaultUserName)]
        public string UserName { get; set; }
    }
}