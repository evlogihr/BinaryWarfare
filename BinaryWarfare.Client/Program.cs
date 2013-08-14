using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryWarfare.Data;
using BinaryWarfare.Data.Migrations;
using BinaryWarfare.Model;

namespace BinaryWarfare.Client
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BinaryWarfareContext, Configuration>());

            var context = new BinaryWarfareContext();

            var user = new User() { Username = "Spas", AuthCode = "6fa9133efe05348e430bd5a4585b595f0cb6cba3" };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
