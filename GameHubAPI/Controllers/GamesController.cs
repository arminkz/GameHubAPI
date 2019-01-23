using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using GameHubAPI.Models;


namespace GameHubAPI.Controllers
{
    public class GamesController : ApiController
    {
        private GameHubAPIContext db = new GameHubAPIContext();

        // GET: api/games
        public IEnumerable<object> GetGames()
        {
            List<object> ret = new List<object>();
            var gs = db.Games;
            foreach (Game g in gs)
            {
                ret.Add(new
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    PicUrl = g.PicUrl,
                    GameConfig = g.GameConfig
                });
            }
            return ret;
        }

        // GET: api/games/5
        [ResponseType(typeof(Game))]
        public IHttpActionResult GetGame(int id)
        {
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpPost]
        [Route("api/games/add")]
        public IHttpActionResult AddGame(int id, string name, string description, string config)
        {
            User u = db.Users.Find(id);
            if (u == null)
            {
                return BadRequest("no such user.");
            }

            Game g = db.Games.FirstOrDefault(p => p.Name == name);
            if (g != null)
            {
                return BadRequest("Game already exists.");
            }

            db.Games.Add(new Game() { Name = name, Description = description, GameConfig = config, Author = u });
            db.SaveChanges();
            return Ok(new { message = "Game added successfully !" });
        }


    }
}
