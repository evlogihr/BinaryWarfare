using System;
using System.Data.Entity;
using System.Linq;
using BinaryWarfare.Data;
using BinaryWarfare.Data.Migrations;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public abstract class BaseRepository
    {
        protected const int Sha1CodeLength = 40;

        protected static Random rand = new Random();

        protected DbContext context;

        public BaseRepository()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BinaryWarfareContext, Configuration>());
        }

        public BaseRepository(DbContext context)
            : this()
        {
            this.context = context;
        }
    }
}