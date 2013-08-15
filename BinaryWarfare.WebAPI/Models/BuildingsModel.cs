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

        }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }
    }
}