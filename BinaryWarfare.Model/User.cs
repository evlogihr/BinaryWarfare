using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryWarfare.Model
{
    public class User
    {
        private string username;

        public User()
        {
            this.Squads = new HashSet<Squad>();
        }

        public int Id { get; set; }

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
                this.Squads.Add(new Squad() { Name = this.Username + "Squad" });
            }
        }

        public string AuthCode { get; set; }

        public string SessionKey { get; set; }

        public decimal Money { get; set; }

        public int AcademyLevel { get; set; }

        public virtual ICollection<Squad> Squads { get; set; }
    }
}
