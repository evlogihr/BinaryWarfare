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
        public SquadModel()
        {
        }

        public SquadModel(Squad squad)
        {
            this.Id = squad.Id;
            this.Name = squad.Name;
            this.IsBusy = false;
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isBusy")]
        public bool IsBusy { get; set; }

        [DataMember(Name = "units")]
        public ICollection<UnitModel> Units { get; set; }
    }

    [DataContract]
    public class SquadAttackModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "attackedUserId")]
        public int AttackedUserId { get; set; }

        [DataMember(Name = "squadId")]
        public int SquadId { get; set; }
    }

    [DataContract]
    public class SquadMoveModel
    {
        [DataMember(Name = "squadId")]
        public int SquadId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "unitsIds")]
        public ICollection<int> UnitsIds { get; set; }
    }

    [DataContract]
    public class SquadDetails : SquadModel
    {
        public SquadDetails()
        {
        }

        public SquadDetails(Squad squad)
            : base(squad)
        {
            this.Name = squad.Name;
            this.Attack = squad.Units.ToList().Sum(u => u.Attack);
            this.Income = squad.Units.ToList().Sum(u => u.Income);
            if (base.Units == null)
            {
                base.Units = new List<UnitModel>();
            }

            foreach (var unit in squad.Units)
            {
                base.Units.Add(new UnitModel(unit));
            }
        }

        [DataMember(Name = "attack")]
        public int Attack { get; set; }

        [DataMember(Name = "income")]
        public decimal Income { get; set; }
    }
}