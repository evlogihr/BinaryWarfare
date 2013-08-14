using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryWarfare.Model;

namespace BinaryWarfare.Data
{
    public class BinaryWarfareContext
        : DbContext
    {
        public BinaryWarfareContext()
            : base("BinaryWarfare")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Squad> Squads { get; set; }

        public DbSet<Unit> Hackers { get; set; }
    }
}
