using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork_Backend.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required]
        public string Content { get; set; }
        public string CreateTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsReaded { get; set; }
    }
}
