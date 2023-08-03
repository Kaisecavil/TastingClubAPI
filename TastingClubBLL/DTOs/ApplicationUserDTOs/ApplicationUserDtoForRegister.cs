using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TastingClubDAL.Constants.ModelConstants.ApplicationUserConstants;

namespace TastingClubBLL.DTOs.ApplicationUserDTOs
{
    public class ApplicationUserDtoForRegister
    {
        [Required]
        [DefaultValue(ApplicationUserDefaultValueConstants.DefaultAdminEmail)]
        public string Email { get; set; }
        [Required]
        [DefaultValue(ApplicationUserDefaultValueConstants.DefaultAdminPassword)]
        public string Password { get; set; }
        [Required]
        [DefaultValue(ApplicationUserDefaultValueConstants.DefaultAdminPassword)]
        public string ConfirmedPassword { get; set; }
        [Required]
        [DefaultValue(ApplicationUserDefaultValueConstants.DefaultFirstName)]
        [MinLength(ApplicationUserValueConstraintConstants.FirstNameMinLengthConstraint)]
        public string FirstName { get; set; }
        [Required]
        [DefaultValue(ApplicationUserDefaultValueConstants.DefaultLastName)]
        [MinLength(ApplicationUserValueConstraintConstants.LastNameMinLengthConstraint)]
        public string LastName { get; set; }
    }
}
