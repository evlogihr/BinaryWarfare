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
    public class UnitController : BaseApiController
    {
        private UnitsRepository repository;

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
                var unit = new Unit() { Attack = 10, Defence = 12, Income = 5, Busy = false };

                this.repository.Add(unit, sessionKey);

            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getUnits")]
        public HttpResponseMessage GetUnits(string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                ICollection<Squad> allSquads = this.repository.All(sessionKey);
                ICollection<SquadModel> squadModel = new List<SquadModel>();
                foreach (var squad in allSquads)
                {
                    squadModel.Add(new SquadModel(squad));
                }

                return squadModel;
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("moveToSquad")]
        public HttpResponseMessage MoveToSquad(int squadId, ICollection<int> unitsIds, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var squads = this.repository.Move(squadId, unitsIds, sessionKey);
                return squads;
            });

            return responseMsg;
        }
    }
}
