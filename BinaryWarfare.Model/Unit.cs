using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryWarfare.Model
{
    public class Unit
    {
        public int Id { get; set; }

        public int Attack { get; set; }

        public int Defence { get; set; }

        public decimal Income { get; set; }

        public bool Busy { get; set; }

        public virtual Squad Squad { get; set; }
    }
}
