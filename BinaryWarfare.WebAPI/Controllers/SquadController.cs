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
using BinaryWarfare.WebAPI.Models;

namespace BinaryWarfare.WebAPI.Controllers
{
    [EnableCors(origins: "http://binarywarfareclient.apphb.com/", headers: "*", methods: "*")]
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
        public HttpResponseMessage AddSquad(SquadModel squad)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var newSquad = new Squad() { Name = squad.Name };
                this.repository.Add(newSquad, squad.SessionKey);
            });

            return responseMsg;
        }
        
        [HttpGet]
        [ActionName("getInfo")]
        public HttpResponseMessage GetInfo(SquadModel squad)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var dbSquad = this.repository.Get(squad.Id, squad.SessionKey);
                var squadDetails = new SquadDetails(dbSquad);
                return squadDetails;
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getSquads")]
        public HttpResponseMessage GetSquads(string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                
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
