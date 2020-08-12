#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;

#endregion

namespace DMT.Services
{
    partial class LocalOperations
    {
        #region Internal Variables

        private UserOperations _User_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Users Operations.
        /// </summary>
        public UserOperations Users
        {
            get
            {
                if (null == _User_Ops)
                {
                    lock (this)
                    {
                        _User_Ops = new UserOperations();
                    }
                }
                return _User_Ops;
            }
        }

        #endregion

        #region UserOperations

        /// <summary>
        /// The UserOperations class.
        /// Used for Manage User and Role's operation(s).
        /// </summary>
        public class UserOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal UserOperations() { }

            #endregion

            #region Public Methods

            #region Role

            public Role GetRole(Search.Roles.ById value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<Role>(
                    RouteConsts.User.GetRole.Url, value);
                return ret;
            }

            public List<Role> GetRoles()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Role>>(
                    RouteConsts.User.GetRoles.Url, new { });
                return ret;
            }

            public Role SaveRole(Role value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<Role>(
                    RouteConsts.User.SaveRole.Url, value);
                return ret;
            }

            #endregion

            #region User

            public List<User> GetUsers(Role role)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<User>>(
                    RouteConsts.User.GetUsers.Url, role);
                return ret;
            }

            public User GetByCardId(Search.Users.ByCardId value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<User>(
                    RouteConsts.User.GetByCardId.Url, value);
                return ret;
            }

            public User GetById(Search.Users.ById value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<User>(
                    RouteConsts.User.GetById.Url, value);
                return ret;
            }

            public List<User> SearchById(Search.Users.ById value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<User>>(
                    RouteConsts.User.SearchById.Url, value);
                return ret;
            }

            public User GetByLogIn(Search.Users.ByLogIn value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<User>(
                    RouteConsts.User.GetByLogIn.Url, value);
                return ret;
            }

            public User SaveUser(User value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<User>(
                    RouteConsts.User.SaveUser.Url, value);
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
