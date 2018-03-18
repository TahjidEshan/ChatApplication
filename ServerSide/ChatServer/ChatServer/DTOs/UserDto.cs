using ChatServer.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please provide First Name.")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please provide Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please provide Username.")]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please provide a Valid Email Address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
