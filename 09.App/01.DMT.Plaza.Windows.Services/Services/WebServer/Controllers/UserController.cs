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
        [HttpPost]
        [ActionName(RouteConsts.User.GetRoles.Name)]
        public List<Role> GetRoles()
        {
            var results = Role.Gets();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.User.GetUsers.Name)]
        public List<User> GetUsers(Role value)
        {
            int status = 1; // active only
            var results = value.GetUsers(status);
            return results;
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
