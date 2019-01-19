using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameHubAPI.Models
{
    public class Friendship
    {
        public virtual int UserId1 { get; set; }
        public virtual int UserId2 { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
        public DateTime since;
        public bool pending { get; set; }
    }
}