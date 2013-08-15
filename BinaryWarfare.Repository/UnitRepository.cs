using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public class UnitsRepository : BaseRepository, IRepository<Unit>
    {
        private DbSet<Unit> entitySet;

        public UnitsRepository(DbContext dbContent)
            : base(dbContent)
        {
            this.entitySet = this.context.Set<Unit>();
        }

        /* public members */

        public void Add(Unit unit, string sessionKey)
        {
            var user = this.context.Set<User>().FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            var squad = user.Squads.FirstOrDefault(s => s.Name == user.Username + "Squad");
            if (squad == null)
            {
                squad = new Squad() { Name = user.Username + "Squad" };
                user.Squads.Add(squad);
            }

            squad.Units.Add(unit);
            context.SaveChanges();
        }

        public ICollection<Squad> All(string sessionKey)
        {
            var user = this.context.Set<User>().FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            var squads = user.Squads;

            return squads;
        }

        public void Move(int squadId, ICollection<int> unitsIds, string sessionKey)
        {
            var user = this.context.Set<User>().FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            var squad = user.Squads.FirstOrDefault(s => s.Id == squadId);
            if (squad == null)
            {
                throw new ServerErrorException("Invalid squad", "INV_SQD_ID");
            }

            foreach (var unitId in unitsIds)
            {
                var unit = this.entitySet.FirstOrDefault(u => u.Id == unitId);
                if (unit == null)
                {
                    throw new ServerErrorException("Invalid unit", "INV_UNT_ID");
                }

                squad.Units.Add(unit);
            }

            this.context.SaveChanges();
        }

        //not implemented

        public Unit Update(int id, Unit item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Unit item)
        {
            throw new NotImplementedException();
        }

        public Unit Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Unit> Find(System.Linq.Expressions.Expression<Func<Unit, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Unit> All()
        {
            throw new NotImplementedException();
        }

        public Unit Add(Unit item)
        {
            throw new NotImplementedException();
        }
    }
}
