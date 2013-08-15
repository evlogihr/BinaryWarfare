﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public interface IUsersRepository : IRepository<User>
    {
        string Login(User user);

        void Logout(string sessionKey);

        User Get(string sessionKey);
    }
}
