using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryWarfare.Model
{
    public class Squad
    {
        public Squad()
        {
            this.Units = new HashSet<Unit>();
        }

        public int Id { get; set; }

        public virtual User UserId { get; set; }

        public string Name { get; set; }

        public bool IsBusy { get; set; }

        public virtual ICollection<Unit> Units { get; set; }
    }
}
