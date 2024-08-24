using System;
using System.ComponentModel.DataAnnotations;

namespace WatchitWebApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool IsVerified { get; set; } = false;
    }
}
