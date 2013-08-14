using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryWarfare.Data;
using BinaryWarfare.Data.Migrations;
using BinaryWarfare.Model;

namespace BinaryWarfare.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BinaryWarfareContext, Configuration>());

            BinaryWarfareContext context = new BinaryWarfareContext();
            User user = new User() { Username = "Petkan", AuthCode = "6fa9133efe05348e430bd5a4585b595f0cb6cba3" };

            context.Users.Add(user);
            context.SaveChanges();

        }
    }
}
