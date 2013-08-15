using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public class UnitsRepository : BaseRepository, IUnitsRepository
    {
        private DbSet<Unit> entitySet;

        public UnitsRepository(DbContext dbContent)
            : base(dbContent)
        {
            this.entitySet = this.context.Set<Unit>();
        }

        /* public members */

        public Unit Add(Unit unit)
        {
            this.entitySet.Add(unit);
            context.SaveChanges();
            return unit;
        }

        public IQueryable<Unit> All()
        {
            return this.entitySet;
        }

        public ICollection<Squad> Update(Squad squad, ICollection<Unit> units)
        {
            foreach (var unit in units)
            {
                squad.Units.Add(unit);
            }

            this.context.SaveChanges();

            var user = squad.User;
            var squads = user.Squads;

            return squads;
        }

        public Unit Get(int id)
        {
            var unit = this.entitySet.FirstOrDefault(u => u.Id == id);
            if (unit == null)
            {
                throw new ServerErrorException("Invalid unit Id", "INV_UNT_AUTH");
            }

            return unit;
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

        public IQueryable<Unit> Find(System.Linq.Expressions.Expression<Func<Unit, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
