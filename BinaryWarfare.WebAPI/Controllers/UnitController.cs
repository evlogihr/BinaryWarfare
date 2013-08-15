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
    public class UnitController : BaseApiController
    {
        private IUnitsRepository repository;

        public UnitController()
        {
            var context = new BinaryWarfareContext();
            this.repository = new UnitsRepository(context);
        }

        [HttpGet]
        [ActionName("create")]
        public HttpResponseMessage Create(string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(sessionKey);
                var unit = new Unit() { Attack = 10, Defence = 12, Income = 5, IsBusy = false };

                this.repository.Add(unit);

                var squadModel = new List<SquadDetails>();
                foreach (var squad in user.Squads)
                {
                    squadModel.Add(new SquadDetails(squad));
                }

                return squadModel;
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getUnits")]
        public HttpResponseMessage GetUnits(string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(sessionKey);
                
                var squadModel = new List<SquadDetails>();
                foreach (var squad in user.Squads)
                {
                    squadModel.Add(new SquadDetails(squad));
                }

                return squadModel;
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("moveToSquad")]
        public HttpResponseMessage MoveToSquad(SquadMoveModel squadMoveModel, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(sessionKey);
                var squad = user.Squads.FirstOrDefault(s => s.Id == squadMoveModel.SquadId);
                var units = new List<Unit>();
                foreach (var unitId in squadMoveModel.UnitsIds)
                {
                    var unit = this.repository.Get(unitId);
                    units.Add(unit);
                }

                var resultSquads = repository.Update(squad, units);
                var resultSquadModel = new List<SquadDetails>();
                foreach (var reSquad in resultSquads)
                {
                    resultSquadModel.Add(new SquadDetails(reSquad));
                }

                return resultSquadModel;
            });

            return responseMsg;
        }

        private User ValidateUser(string sessionKey)
        {
            var units = this.repository.All().ToList();
            var unit = units.FirstOrDefault(s => s.Squad.User.SessionKey == sessionKey);
            if (unit == null)
            {
                throw new ServerErrorException("Invalid squad", "INV_SQD_AUTH");
            }

            var user = unit.Squad.User;
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            return user;
        }

        private static Squad ValidateSquad(User user)
        {
            var squad = user.Squads.FirstOrDefault(s => s.Name == string.Format(user.Username + "Squad"));
            if (squad == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }
            return squad;
        }
    }
}

/*
var user = this.context.Set<User>().FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            var squad = user.Squads.FirstOrDefault(s => s.Id == squadId);
            if (squad == null)
            {
                throw new ServerErrorException("Invalid squad", "INV_SQD_ID");
            }
*/