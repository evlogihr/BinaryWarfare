using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BinaryWarfare.Repository;
using BinaryWarfare.WebAPI.Models;

namespace BinaryWarfare.WebAPI.Controllers
{
    public class UserController : BaseApiController
    {
        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser(UserLoginModel user)
        {
            var responseMsg =
                this.PerformOperation(() =>
            {
                UsersRepository.CreateUser(user.Username, user.AuthCode);
                var sessionKey = UsersRepository.LoginUser(user.Username, user.AuthCode);
                return new UserLoggedModel()
                {
                    Username = user.Username,
                    SessionKey = sessionKey
                };
            });
            return responseMsg;
        }

        [HttpPost]
        [ActionName("login")]
        public HttpResponseMessage LoginUser(UserLoginModel user)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var sessionKey = UsersRepository.LoginUser(user.Username, user.AuthCode);
                return new UserLoggedModel()
                {
                    Username = user.Username,
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
                UsersRepository.LogoutUser(sessionKey);
            });

            return responseMsg;
        }
    }
}
