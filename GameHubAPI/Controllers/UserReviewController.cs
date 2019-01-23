using GameHubAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameHubAPI.Controllers
{
    public class UserReviewController : ApiController
    {
        private GameHubAPIContext db = new GameHubAPIContext();

        //GET api/UserReviews/getreviews
        [HttpGet]
        [Route("api/userreviews/getreviews")]
        public IEnumerable<object> GetReviews()
        {
            List<object> ret = new List<object>();
            var urs = db.UserReviews;
            foreach (UserReview ur in urs)
            {
                ret.Add(new
                {
                    ur.Id,
                    ur.UserId,
                    ur.AuthorId,
                    ur.Comment,
                    ur.Rating
                });
            }
            return ret;
        }

    }
}
