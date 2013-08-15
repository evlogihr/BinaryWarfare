using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BinaryWarfare.WebAPI.Models
{
    [DataContract]
    public class BuildingModel
    {
        [DataMember(Name = "buildingName")]
        public int Name { get; set; }
    }
}