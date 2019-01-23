using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IdentityModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GameHubAPI.Models;

namespace GameHubAPI.Controllers
{

    public class UsersController : ApiController
    {
        private GameHubAPIContext db = new GameHubAPIContext();

        // GET: api/Users
        public IEnumerable<object> GetUsers()
        {
            List<object> ret = new List<object>();
            var us = db.Users;
            foreach(User u in us)
            {
                ret.Add(new {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    Email = u.Email,
                    PicUrl = u.PicUrl,
                    isOnline = (DateTime.Now - u.LastLogin) < TimeSpan.FromMinutes(10)
                });
            }
            return ret; 
        }

        //POST: api/Users/addfriend
        [HttpPost]
        [Route("api/users/addfriend")]
        public IHttpActionResult AddFriend(int id, int fid)
        {
            if (id == fid) return BadRequest();

            User user1 = db.Users.Find(id);
            User user2 = db.Users.Find(fid);
            if (user1 == null || user2 == null)
                return BadRequest();

            var fe = db.Friendships.FirstOrDefault(f => f.User1.Id == id && f.User2.Id == fid);
            if(fe != null)
            {
                return Ok(new { message = "Friend request is already added !" });
            }

            db.Friendships.Add(new Friendship
            {
                UserId1 = id,
                UserId2 = fid,
                pending = true
            });
            db.SaveChanges();
            return Ok(new { message = "Friend is added successfully." });
        }


        //POST: api/Users/getfrindrequests
        [HttpPost]
        [Route("api/Users/getfriendrequests")]
        public IEnumerable<object> GetFriendRequests(int id)
        {
            User user = db.Users.FirstOrDefault(t => t.Id == id);
            if (user == null)
                throw new BadRequestException();

            List<object> ret = new List<object>();
            IQueryable<Friendship> friendships = db.Friendships.Where(t => t.UserId2 == id && t.pending);
            foreach(Friendship fs in friendships)
            {
                var u = db.Users.Find(fs.UserId1);
                ret.Add(new
                {
                    Id = u.Id,
                    Name = u.DisplayName
                });
            }
            return ret;
        }

        //POST: api/Users/acceptfriend
        [HttpPost]
        [Route("api/Users/acceptfriend")]
        public IHttpActionResult AcceptFriend(int id, int fid)
        {
            User user1 = db.Users.Find(id);
            User user2 = db.Users.Find(fid);
            if (user1 == null || user2 == null)
                return BadRequest();
            var friend = db.Friendships.FirstOrDefault(u => u.UserId2 == id && u.UserId1 == fid);
            if (friend == null)
                return BadRequest();
            friend.pending = false;
            db.SaveChanges();
            return Ok(new { message = "Friend is accepted successfully." });
        }

        //POST: api/Users/getFriends
        [HttpPost]
        [Route("api/users/getfriends")]
        public IEnumerable<object> GetFriends(int id)
        {
            User user = db.Users.FirstOrDefault(t => t.Id == id);
            if (user == null)
                throw new BadRequestException();
            IQueryable<Friendship> friendships = db.Friendships.Where(t => (t.UserId2 == id || t.UserId1 == id) && t.pending == false);
            List<object> res = new List<object>();
            foreach (Friendship f in friendships)
            {
                if (f.UserId1 == id)
                {
                    User u = db.Users.Find(f.UserId2);
                    res.Add(new
                    {
                        Id = u.Id,
                        DisplayName = u.DisplayName,
                        Email = u.Email,
                        PicUrl = u.PicUrl,
                        isOnline = (DateTime.Now - u.LastLogin) < TimeSpan.FromMinutes(10)
                    });
                }
                else
                {
                    User u = db.Users.Find(f.UserId1);
                    res.Add(new
                    {
                        Id = u.Id,
                        DisplayName = u.DisplayName,
                        Email = u.Email,
                        PicUrl = u.PicUrl,
                        isOnline = (DateTime.Now - u.LastLogin) < TimeSpan.FromMinutes(10)
                    });
                }
            }
            return res;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}