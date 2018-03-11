using ChatServer.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.Models.Data
{
    public class User: BaseClass
    {
        [Required(ErrorMessage = "Please provide User Name.")]
        [StringLength(50, MinimumLength = 2)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please provide First Name.")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please provide Last Name.")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please provide a Valid Email Address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        public string Address { get; set; }
    }
}
