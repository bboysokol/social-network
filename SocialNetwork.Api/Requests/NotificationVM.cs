using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.ViewModels.NotificationVMs
{
    public class NotificationVM
    {
        public int Id { get; set; }
        public string CreateTime { get; set; }
        public string Content { get; set; }
        public bool IsReaded { get; set; }
    }
}
