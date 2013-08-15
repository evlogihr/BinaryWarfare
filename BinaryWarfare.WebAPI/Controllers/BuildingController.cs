using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BinaryWarfare.Data;
using BinaryWarfare.Model;
using BinaryWarfare.Repository;

namespace BinaryWarfare.WebAPI.Controllers
{
    [EnableCors(origins: "http://binarywarfareclient.apphb.com/", headers: "*", methods: "*")]
    public class BuildingController : BaseApiController
    {
         private UsersRepository repository;

         public BuildingController()
        {
            var context = new BinaryWarfareContext();
            this.repository = new UsersRepository(context);
        }

        [HttpGet]
        [ActionName("getBuildings")]
        public HttpResponseMessage GetBuildings(string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                User buildings = this.repository.Get(sessionKey);


                return buildings;
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage Create(string buildingName, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {

            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("destroy")]
        public HttpResponseMessage Destroy(string buildingName, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {

            });

            return responseMsg;
        }
    }
}
