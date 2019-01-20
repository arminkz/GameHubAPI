using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using GameHubAPI.Core;
using GameHubAPI.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GameHubAPI.Filters;
using System.Threading.Tasks;
using System.Web;

namespace GameHubAPI.Controllers
{
    public class AccountsController : ApiController
    {
        private GameHubAPIContext db = new GameHubAPIContext();

        [AllowAnonymous]
        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Login(string email, string password)
        {
            User u = db.Users.FirstOrDefault(p => p.Email == email);
            if(u == null)
            {
                return BadRequest("no such email.");
            }

            if(u.Password != password)
            {
                return BadRequest("invalid password.");
            }

            return Ok(new
            {
                id = u.Id,
                name = u.DisplayName,
                email = u.Email,
                picUrl = u.PicUrl,
                role = u.Role,
                token = JwtManager.GenerateToken(email)
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/register")]
        public IHttpActionResult Register(string name, string email, string password)
        {
            User u = db.Users.FirstOrDefault(p => p.Email == email);
            if(u != null)
            {
                return BadRequest("email already exists.");
            }

            db.Users.Add(new User() {DisplayName = name, Email = email, Password = password });
            db.SaveChanges();

            return Ok(new {message = "user created successfully !" });
        }

        [HttpPost]
        [JwtAuthentication]
        [Route("api/account/update")]
        public IHttpActionResult UpdateInfo(int id,string DisplayName)
        {
            User u = db.Users.Find(id);
            if (u == null)
            {
                return BadRequest("no such user.");
            }

            u.DisplayName = DisplayName;
            db.SaveChanges();

            return Ok("updated.");
        }

        [JwtAuthentication]
        [Route("api/account/profilepic")]
        public async Task<HttpResponseMessage> PostUserImage(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {
                            var message = string.Format("Please Upload a file upto 1 mb.");
                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            User u = db.Users.Find(id);
                            if (u == null)
                            {
                                var message = string.Format("invalid id.");
                                dict.Add("error", message);
                                return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                            }
                            string picPath = u.Id + extension;
                            u.PicUrl = picPath;
                            //  where you want to attach your imageurl
                            db.SaveChanges();
                            //if needed write the code to update the table

                            var filePath = HttpContext.Current.Server.MapPath("~/UserImage/" + picPath);
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(filePath);
                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var res = string.Format("server encountered error.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
    }
}
