using BinaryWarfare.Data;
using BinaryWarfare.Data.Migrations;
using BinaryWarfare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BinaryWarfare.Client
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BinaryWarfareContext, Configuration>());

            var context = new BinaryWarfareContext();

<<<<<<< HEAD
            var user = new User() { Username = "Spas", AuthCode = "6fa9133efe05348e430bd5a4585b595f0cb6cba3" };
=======
            var user = new User();
            user.Username = "Panko";
            user.AcademyLevel = 1;
            user.Money = 119.50M;
            user.AuthCode = "6fa9133efe05348e430bd5a4585b595f0cb6cba3";
            user.SessionKey = "6fa9133efe05348e430bd5a4585b595f0cb6cba3";
>>>>>>> b9c8a23d2237b30012691259f8d56699789494f1

            context.Users.Add(user);
            context.SaveChanges();

            //DropboxImageManager.Start();            
        }
    }
}
