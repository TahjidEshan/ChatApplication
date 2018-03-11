using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatServer.Models.Data
{
    public class Chat: BaseClass
    {
        [Required]
        public string Message { get; set; }
        [ForeignKey("SentByUser")]
        public Guid? SentByUserId { get; set; }
        public virtual User SentByUser { get; set; }
        public bool WasSentToGroup { get; set; }
        [ForeignKey("SentToUser")]
        public Guid? SentToUserId { get; set; }
        public virtual User SentToUser { get; set; }
        [ForeignKey("SentToGroup")]
        public Guid? SentToGroupId { get; set; }
        public virtual Group SentToGroup { get; set; }

    }
}
