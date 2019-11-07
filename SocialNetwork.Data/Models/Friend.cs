using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Models
{
    public class Friend
    {
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int FriendId { get; set; }
        public virtual User FriendWith { get; set; }
        public string FriendshipStartDate { get; set; } = String.Format("{0:g}", DateTime.Now);
        public bool IsDeleted { get; set; } = false;
    }
}
