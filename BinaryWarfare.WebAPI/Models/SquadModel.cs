using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using BinaryWarfare.Model;

namespace BinaryWarfare.WebAPI.Models
{
    [DataContract]
    public class SquadModel
    {
        public SquadModel(Squad squad)
        {
            this.Id = squad.Id;
            this.Name = squad.Name;
            this.Units = new List<UnitModel>();
            foreach (var unit in squad.Units)
            {
                this.Units.Add(new UnitModel(unit));
            }
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "units")]
        public ICollection<UnitModel> Units { get; set; }
    }
}