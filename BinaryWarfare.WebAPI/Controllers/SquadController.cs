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
        private ISquadsRepository repository;

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
                var user = ValidateUser(squad);

                var newSquad = new Squad() { Name = squad.Name };
                user.Squads.Add(newSquad);

                this.repository.Add(newSquad);
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getInfo")]
        public HttpResponseMessage GetInfo(SquadModel squad)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(squad);

                var dbSquad = this.repository.Get(squad.Id);
                return new SquadDetails(dbSquad);
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getSquads")]
        public HttpResponseMessage GetSquads(SquadModel squad)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(squad);

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

        private Model.User ValidateUser(SquadModel squad)
        {
            var user = this.repository.All().FirstOrDefault(s => s.UserId.SessionKey == squad.SessionKey).UserId;
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }
            return user;
        }
    }
}
