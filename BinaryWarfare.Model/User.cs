﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryWarfare.Model
{
    public class User
    {
        public User()
        {
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string AuthCode { get; set; }

        public string SessionKey { get; set; }

        public decimal Money { get; set; }

        public int AcademyLevel { get; set; }

        public virtual ICollection<Squad> Squads { get; set; }
    }
}
