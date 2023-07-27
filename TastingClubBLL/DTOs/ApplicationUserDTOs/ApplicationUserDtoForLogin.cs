using System.ComponentModel.DataAnnotations;

namespace TastingClubBLL.DTOs.ApplicationUserDTOs
{
    public class ApplicationUserDtoForLogin
    {
        [Required]
        //[DefaultValue(DefaultValueConstants.AdminEmail)]
        public string Email { get; set; }
        [Required]
        //[DefaultValue(DefaultValueConstants.BasicPassword)]
        public string Password { get; set; }
    }
}