using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Models
{
    public class Friend
    {
        
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User UserForeignKey { get; set; }
        public string FriendId { get; set; }
        [ForeignKey("FriendId")]
        public virtual User FriendForeignKey { get; set; }
        [Required]
        public DateTime FriendshipStartDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
