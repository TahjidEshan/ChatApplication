using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatServer.Models.Data
{
    public class GroupUser: BaseClass
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Group")]
        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
