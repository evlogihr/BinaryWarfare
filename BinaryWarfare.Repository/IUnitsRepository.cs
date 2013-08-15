using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public interface IUnitsRepository : IRepository<Unit>
    {
        ICollection<Squad> Update(Squad squad, ICollection<Unit> units);
    }
}
