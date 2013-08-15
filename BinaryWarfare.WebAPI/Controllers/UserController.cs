using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BinaryWarfare.Data;
using BinaryWarfare.Model;
using BinaryWarfare.Repository;
using BinaryWarfare.WebAPI.Models;

namespace BinaryWarfare.WebAPI.Controllers
{
    [EnableCors(origins: "http://http://binarywarfare.apphb.com/", headers: "*", methods: "*")]
    public class UserController : BaseApiController
    {
        private UsersRepository repository;

        public UserController()
        {
            var context = new BinaryWarfareContext();
            this.repository = new UsersRepository(context);
        }

        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser(UserLoginModel userModel)
        {
            var responseMsg =
                this.PerformOperation(() =>
            {
                var user = userModel.ToUser();
                this.repository.Add(user);
                var sessionKey = this.repository.Login(user);

                return new UserLoggedModel()
                {
                    Username = userModel.Username,
                    SessionKey = sessionKey
                };
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("login")]
        public HttpResponseMessage LoginUser(UserLoginModel userModel)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = userModel.ToUser();
                var sessionKey = this.repository.Login(user);

                return new UserLoggedModel()
                {
                    Username = userModel.Username,
                    SessionKey = sessionKey
                };
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("logout")]
        public HttpResponseMessage LogoutUser(string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                this.repository.Logout(sessionKey);
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("uploadAvatar")]
        public HttpResponseMessage UploadAvatar(string sessionKey, string avatarPath)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = this.repository.Get(sessionKey);
                user.AvatarUrl = AvatarModel.GetMediaLink(avatarPath, user.Username.ToLower());
            });

            return responseMsg;
        }
    }
}
