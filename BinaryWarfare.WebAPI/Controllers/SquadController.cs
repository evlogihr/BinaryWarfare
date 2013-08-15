using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BinaryWarfare.Data;
using BinaryWarfare.Model;
using BinaryWarfare.Repository;
using BinaryWarfare.WebAPI.Models;

namespace BinaryWarfare.WebAPI.Controllers
{
    public class SquadController : BaseApiController
    {
        private SquadsRepository repository;

        public SquadController()
        {
            var context = new BinaryWarfareContext();
            this.repository = new SquadsRepository(context);
        }

        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage AddSquad(string squadName, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var squad = new Squad() { Name = squadName };
                this.repository.Add(squad, sessionKey);
            });

            return responseMsg;
        }


        [HttpGet]
        [ActionName("getInfo")]
        public HttpResponseMessage GetInfo(int squadId, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var squad = this.repository.Get(squadId, sessionKey);
                var squadDetails = new SquadDetails(squad);
                return squadDetails;
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("attack")]
        public HttpResponseMessage Attack(int squadId, string attackedUser, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {

            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("work")]
        public HttpResponseMessage Work(int squadId, int time, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {

            });

            return responseMsg;
        }
    }
}
