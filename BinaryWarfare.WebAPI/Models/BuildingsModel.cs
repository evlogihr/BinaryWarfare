using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using BinaryWarfare.Model;

namespace BinaryWarfare.WebAPI.Models
{
    [DataContract]
    public class BuildingsModel
    {
        public BuildingsModel(User user)
        {
            this.Buildings = new List<Building>();
            this.Buildings.Add(new Academy(user.Academy));
            this.Buildings.Add(new CSharpYard(user.CSharpYard));
            this.Buildings.Add(new JSGraveyard(user.JSGraveyard));
        }

        [DataMember(Name = "buildings")]
        public ICollection<Building> Buildings { get; set; }
    }

    [DataContract]
    public class Building
    {
        public Building()
        {
        }

        public Building(int level)
        {
            this.Level = level;
        }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }
    }

    [DataContract]
    public class Academy : Building
    {
        public Academy(int level)
            : base(level)
        {
            this.Name = "Academy";
        }
    }

    [DataContract]
    public class CSharpYard : Building
    {
        public CSharpYard(int level)
            : base(level)
        {
            this.Name = "CSharpYard";
        }
    }

    [DataContract]
    public class JSGraveyard : Building
    {
        public JSGraveyard(int level)
            : base(level)
        {
            this.Name = "JSGraveyard";
        }
    }
}