using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using BinaryWarfare.Model;

namespace BinaryWarfare.WebAPI.Models
{
    [DataContract]
    public class UnitModel
    {
        public UnitModel(Unit unit)
        {
            this.Id = unit.Id;
            this.Attack = unit.Attack;
            this.Defence = unit.Defence;
            this.Income = unit.Income;
            this.IsBusy = unit.Busy;
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "attack")]
        public int Attack { get; set; }

        [DataMember(Name = "defence")]
        public int Defence { get; set; }

        [DataMember(Name = "income")]
        public decimal Income { get; set; }

        [DataMember(Name = "is-busy")]
        public bool IsBusy { get; set; }
    }
}