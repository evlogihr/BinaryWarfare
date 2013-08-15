using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using BinaryWarfare.Model;

namespace BinaryWarfare.WebAPI.Models
{
    [DataContract]
    public class UnitDetails
    {
        public UnitDetails()
        {
        }

        public UnitDetails(Unit unit)
        {
            this.Id = unit.Id;
            this.Attack = unit.Attack;
            this.Defence = unit.Defence;
            this.Income = unit.Income;
            this.IsBusy = unit.IsBusy;
            this.SquadID = unit.Squad.Id;
            this.SquadName = unit.Squad.Name;
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "attack")]
        public int Attack { get; set; }

        [DataMember(Name = "defence")]
        public int Defence { get; set; }

        [DataMember(Name = "income")]
        public decimal Income { get; set; }

        [DataMember(Name = "isBusy")]
        public bool IsBusy { get; set; }

        [DataMember(Name = "squadId")]
        public int SquadID { get; set; }

        [DataMember(Name = "squadName")]
        public string SquadName { get; set; }
    }

    [DataContract]
    public class UnitModel
    {
        public UnitModel()
        {
        }

        public UnitModel(Unit unit)
        {
            this.Id = unit.Id;
            this.Attack = unit.Attack;
            this.Defence = unit.Defence;
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "attack")]
        public int Attack { get; set; }

        [DataMember(Name = "defence")]
        public int Defence { get; set; }
    }
}