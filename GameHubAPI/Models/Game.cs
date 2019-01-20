using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameHubAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string GameConfig { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}