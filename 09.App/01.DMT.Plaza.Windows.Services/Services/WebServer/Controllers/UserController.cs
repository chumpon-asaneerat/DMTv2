#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;

#endregion

namespace DMT.Services
{
    public class UserController : ApiController
    {
        public UserController()
        {
            Console.WriteLine("UserController created.");
        }

        [HttpPost]
        [ActionName(RouteConsts.User.GetById.Name)]
        public User GetById([FromBody] Search.Users.ById value)
        {
            return Models.User.Get(value.UserId);
        }

        [HttpPost]
        [ActionName(RouteConsts.User.GetByCardId.Name)]
        public User GetByCardId([FromBody] Search.Users.ByCardId value)
        {
            return Models.User.GetByCardId(value.CardId);
        }

        [HttpPost]
        [ActionName(RouteConsts.User.GetByLogIn.Name)]
        public User GetByLogIn([FromBody] Search.Users.ByLogIn value)
        {
            return Models.User.GetByUserId(value.UserId, value.Password);
        }
    }
}
