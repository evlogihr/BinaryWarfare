﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using BinaryWarfare.Model;

namespace BinaryWarfare.WebAPI.Models
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }
    }

    [DataContract]
    public class UserLoginModel : UserModel
    {
        [DataMember(Name = "authCode")]
        public string AuthCode { get; set; }

        internal User ToUser()
        {
            var user = new User() { Username = base.Username, AuthCode = this.AuthCode };
            return user;
        }
    }

    [DataContract]
    public class UserLoggedModel : UserModel
    {
        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }
    }
}