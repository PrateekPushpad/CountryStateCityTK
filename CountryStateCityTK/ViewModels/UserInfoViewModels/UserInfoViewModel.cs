using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels.UserInfoViewModels
{
    public class UserInfoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "User Name")]
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
