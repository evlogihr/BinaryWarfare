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

        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }

        [DataMember(Name = "units")]
        public ICollection<UnitModel> Units { get; set; }
    }

    [DataContract]
    public class SquadAttackModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "attackedUser")]
        public string UserName { get; set; }

        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }

        [DataMember(Name = "units")]
        public ICollection<UnitModel> Units { get; set; }
    }

    [DataContract]
    public class SquadMoveModel
    {
        //int squadId, ICollection<int> unitsIds, string sessionKey
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "attackedUser")]
        public string UserName { get; set; }

        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }

        [DataMember(Name = "units")]
        public ICollection<UnitModel> Units { get; set; }
    }

    [DataContract]
    public class SquadDetails
    {
        public SquadDetails(Squad squad)
        {
            this.Attack = squad.Units.Sum(u => u.Attack);
            this.Income = squad.Units.Sum(u => u.Income); ;
        }

        [DataMember(Name = "attack")]
        public int Attack { get; set; }

        [DataMember(Name = "income")]
        public decimal Income { get; set; }
    }
}