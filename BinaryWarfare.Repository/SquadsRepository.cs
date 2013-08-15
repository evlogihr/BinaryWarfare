using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public class SquadsRepository : BaseRepository, ISquadsRepository
    {
        private DbSet<Squad> entitySet;

        public SquadsRepository(DbContext dbContent)
            : base(dbContent)
        {
            this.entitySet = this.context.Set<Squad>();
        }

        public Squad Add(Squad squad)
        {
            this.entitySet.Add(squad);
            this.context.SaveChanges();
            return squad;
        }

        public Squad Get(int squadId)
        {
            var squad = this.entitySet.FirstOrDefault(s => s.Id == squadId);
            if (squad == null)
            {
                throw new ServerErrorException("Invalid Squad", "INV_SQD_ID");
            }

            return squad;
        }

        public IQueryable<Squad> All()
        {
            return this.entitySet;
        }

        public Squad Update(int id, Squad updatedSquad)
        {
            var squad = this.entitySet.FirstOrDefault(s => s.Id == id);
            squad = updatedSquad;
            this.context.SaveChanges();
            return squad;
        }

        //not impelemented

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Squad item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Squad> Find(System.Linq.Expressions.Expression<Func<Squad, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
