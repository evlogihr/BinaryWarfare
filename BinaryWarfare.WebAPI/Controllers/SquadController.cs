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
        public HttpResponseMessage AddSquad(SquadModel squad, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(sessionKey);
                ValidateSquad(squad);

                var newSquad = new Squad() { Name = squad.Name };
                user.Squads.Add(newSquad);

                this.repository.Add(newSquad);
            });

            return responseMsg;
        }

        private void ValidateSquad(SquadModel squad)
        {
            if (squad.Name == null)
            {
                throw new ServerErrorException("Invalid squad name", "INV_SQD_AUTH");
            }
        }

        [HttpPost]
        [ActionName("getInfo")]
        public HttpResponseMessage GetInfo(SquadModel squad, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(sessionKey);

                var dbSquad = this.repository.Get(squad.Id);
                return new SquadDetails(dbSquad);
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getSquads")]
        public HttpResponseMessage GetSquads(string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(sessionKey);

                ICollection<SquadModel> dbSquad = new List<SquadModel>();
                var squads = this.repository.All().Where(s => s.User.Id == user.Id);
                foreach (var s in squads)
                {
                    dbSquad.Add(new SquadModel(s));
                }

                return dbSquad;
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

        private User ValidateUser(string sessionKey)
        {
            var squad = this.repository.All().FirstOrDefault(s => s.User.SessionKey == sessionKey);

            if (squad == null)
            {
                throw new ServerErrorException("Invalid squad", "INV_SQD_AUTH");
            }

            var user = squad.User;
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            return user;
        }
    }
}
