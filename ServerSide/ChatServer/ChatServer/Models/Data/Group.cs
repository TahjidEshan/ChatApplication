using System.ComponentModel.DataAnnotations;

namespace ChatServer.Models.Data
{
    public class Group: BaseClass
    {
        [Required(ErrorMessage = "Please provide Group Name.")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
