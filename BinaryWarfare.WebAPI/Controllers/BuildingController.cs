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
    public class BuildingController : BaseApiController
    {
        private IUsersRepository repository;

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
                var user = ValidateUser(sessionKey);
                var userBuildings = new BuildingsModel(user);

                return userBuildings;
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage Create(Building building, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(sessionKey);
                switch (building.Name.ToLower())
                {
                    case "academy":
                        if (user.Money < (user.Academy + 1) * 1000)
                        {
                            user.Academy++;
                            user.Money -= user.Academy * 1000;
                        }
                        break;
                    case "csharpyard": 
                        if (user.Money < (user.CSharpYard + 1) * 1000)
                        {
                            user.CSharpYard++;
                            user.Money -= user.CSharpYard * 1000;
                        }
                        break;
                    case "jsgraveyard":
                        if (user.Money < (user.JSGraveyard + 1) * 1000)
                        {
                            user.JSGraveyard++;
                            user.Money -= user.JSGraveyard * 1000;
                        }
                        break;
                    default: break;
                }

                this.repository.Update(user.Id, user);

                var result = new BuildingsModel(user);
                return result;
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("destroy")]
        public HttpResponseMessage Destroy(Building building, string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                var user = ValidateUser(sessionKey);
                switch (building.Name.ToLower())
                {
                    case "academy":
                        if (user.Academy > 0)
                        {
                            user.Academy--;
                            user.Money += 500;
                        }
                        break;
                    case "csharpyard":
                        if (user.CSharpYard > 0)
                        {
                            user.CSharpYard--;
                            user.Money += 500;
                        }
                        break;
                    case "jsgraveyard":
                        if (user.JSGraveyard > 0)
                        {
                            user.JSGraveyard--;
                            user.Money += 500;
                        }
                        break;
                    default: break;
                }

                this.repository.Update(user.Id, user);

                return new BuildingsModel(user);

            });

            return responseMsg;
        }

        private User ValidateUser(string sessionKey)
        {
            var users = this.repository.All().ToList();
            var user = users.FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user sessionkey", "INV_USR_AUTH");
            }

            return user;
        }
    }
}
