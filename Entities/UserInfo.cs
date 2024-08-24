using System.ComponentModel.DataAnnotations;

namespace WatchitWebApp.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Avatar { get; set; } = "/admin/img/avatars/avatar.png";

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Fill Phone Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Fill Adress")]
        public string Adress { get; set; }

        [DataType(DataType.Text)]
        public string Info { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
