#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;
//using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for manage users and roles.
    /// </summary>
    public class UserController : ApiController
    {
        #region Role

        [HttpPost]
        [ActionName(RouteConsts.User.GetRole.Name)]
        public NDbResult<Role> GetRole([FromBody] Search.Roles.ById value)
        {
            NDbResult<Role> result;
            if (null == value)
            {
                result = new NDbResult<Role>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = Role.GetRole(value.RoleId);

            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.User.GetRoles.Name)]
        public NDbResult<List<Role>> GetRoles()
        {
            NDbResult<List<Role>> result = Role.GetRoles();
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.User.SaveRole.Name)]
        public NDbResult<Role> SaveRole([FromBody] Role value)
        {
            NDbResult<Role> result;
            if (null == value)
            {
                result = new NDbResult<Role>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = Role.Save(value);
            }
            return result;
        }

        #endregion

        #region User

        [HttpPost]
        [ActionName(RouteConsts.User.GetUsers.Name)]
        public NDbResult<List<User>> GetUsers(Role value)
        {
            int status = 1; // active only
            NDbResult<List<User>> result;

            if (null == value)
            {
                result = new NDbResult<List<User>>();
                result.ParameterIsNull();
                result.data = new List<User>();
            }
            else
            {
                result = Models.User.FindByRole(value.RoleId, status);

            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.User.GetById.Name)]
        public NDbResult<User> GetById([FromBody] Search.Users.ById value)
        {
            NDbResult<User> result;
            if (null == value)
            {
                result = new NDbResult<User>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = Models.User.GetUser(value.UserId);

            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.User.SearchByGroupId.Name)]
        public NDbResult<List<User>> SearchByGroupId([FromBody] Search.Users.ByGroupId value)
        {
            int status = 1; // active only
            NDbResult<List<User>> result;

            if (null == value)
            {
                result = new NDbResult<List<User>>();
                result.ParameterIsNull();
                result.data = new List<User>();
            }
            else
            {
                result = Models.User.FindByGroupId(value.GroupId, status);

            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.User.SearchById.Name)]
        public NDbResult<List<User>> SearchById([FromBody] Search.Users.ById value)
        {
            NDbResult<List<User>> result;
            if (null == value)
            {
                result = new NDbResult<List<User>>();
                result.ParameterIsNull();
                result.data = new List<User>();
            }
            else
            {
                result = Models.User.SearchById(value.UserId);

            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.User.GetByCardId.Name)]
        public NDbResult<User> GetByCardId([FromBody] Search.Users.ByCardId value)
        {
            NDbResult<User> result;
            if (null == value)
            {
                result = new NDbResult<User>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = Models.User.GetByCardId(value.CardId);

            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.User.GetByLogIn.Name)]
        public NDbResult<User> GetByLogIn([FromBody] Search.Users.ByLogIn value)
        {
            NDbResult<User> result;
            if (null == value)
            {
                result = new NDbResult<User>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = Models.User.GetByUserId(value.UserId, value.Password);

            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.User.SaveUser.Name)]
        public NDbResult<User> SaveUser([FromBody] User value)
        {
            NDbResult<User> result;
            if (null == value)
            {
                result = new NDbResult<User>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = Models.User.SaveUser(value);
            }
            return result;
        }

        #endregion
    }
}
