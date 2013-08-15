using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public class SquadsRepository: BaseRepository, IRepository<Squad>
    {
        private DbSet<Squad> entitySet;

        public SquadsRepository(DbContext dbContent)
            : base(dbContent)
        {
            this.entitySet = this.context.Set<Squad>();
        }

        public Squad Add(Squad item)
        {
            throw new NotImplementedException();
        }

        public Squad Update(int id, Squad item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Squad item)
        {
            throw new NotImplementedException();
        }

        public Squad Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Squad> All()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Squad> Find(System.Linq.Expressions.Expression<Func<Squad, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
