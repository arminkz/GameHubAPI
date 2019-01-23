using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameHubAPI.Models
{
    public class UserReview
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }
        public bool Pending { get; set; }
    }
}