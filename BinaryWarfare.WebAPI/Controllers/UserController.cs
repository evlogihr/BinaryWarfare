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
    public class UserController : BaseApiController
    {
        private IUsersRepository repository;

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
                user.Money = 1024M;
                user.Squads.Add(new Squad()
                {
                    Name = string.Format(userModel.Username + "Squad")
                });
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
                var user = this.repository.All()
                    .FirstOrDefault(usr => usr.Username == userModel.Username.ToLower() &&
                        usr.AuthCode == userModel.AuthCode);
                if (user == null)
                {
                    throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
                }

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
                var user = this.repository.Get(sessionKey);
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getUsers")]
        public HttpResponseMessage GetUsers(string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var allUsers = this.repository.All();
                var meUser = allUsers.FirstOrDefault(usr => usr.SessionKey == sessionKey);
                if (meUser == null)
                {
                    throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
                }
                var users = allUsers.Where(usr => usr.Id != meUser.Id);
                var usersModels = new List<UserModel>();
                foreach (var user in users)
                {
                    usersModels.Add(new UserModel(user));
                }

                return users;
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
