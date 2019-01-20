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
        public IQueryable<Game> GetGames()
        {
            return db.Games;
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

    }
}
