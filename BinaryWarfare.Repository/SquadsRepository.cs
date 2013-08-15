using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public class SquadsRepository : BaseRepository, IRepository<Squad>
    {
        private DbSet<Squad> entitySet;

        public SquadsRepository(DbContext dbContent)
            : base(dbContent)
        {
            this.entitySet = this.context.Set<Squad>();
        }

        public void Add(Squad squad, string sessionKey)
        {
            var user = this.context.Set<User>().FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            user.Squads.Add(squad);
            this.context.SaveChanges();
        }

        public Squad Get(int squadId, string sessionKey)
        {
            var user = this.context.Set<User>().FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            var squad = user.Squads.FirstOrDefault(s => s.Id == squadId);
            if (squad == null)
            {
                throw new ServerErrorException("Invalid Squad", "INV_SQD_ID");
            }

            return squad;
        }

        public Squad Get(string squadName, string sessionKey)
        {
            var user = this.context.Set<User>().FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            var squad = user.Squads.FirstOrDefault(s => s.Name == squadName);
            if (squad == null)
            {
                throw new ServerErrorException("Invalid Squad", "INV_SQD_ID");
            }

            return squad;
        }

        //not impelemented

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

        public Squad Add(Squad item)
        {
            throw new NotImplementedException();
        }
    }
}
