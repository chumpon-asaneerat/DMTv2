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

            public NRestResult<Role> GetRole(Search.Roles.ById value)
            {
                NRestResult<Role> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<Role>();
                    ret.RestInvalidConfig();
                    return ret;
                }
                if (null != value)
                {
                    ret = client.Execute<Role>(RouteConsts.User.GetRole.Url, value);
                }
                else
                {
                    ret = new NRestResult<Role>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<Role>> GetRoles()
            {
                NRestResult<List<Role>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<Role>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<Role>>(RouteConsts.User.GetRoles.Url, new { });

                return ret;
            }

            public NRestResult<Role> SaveRole(Role value)
            {
                NRestResult<Role> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<Role>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<Role>(RouteConsts.User.SaveRole.Url, value);
                }
                else
                {
                    ret = new NRestResult<Role>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            #endregion

            #region User

            public NRestResult<List<User>> GetUsers(Role value)
            {
                NRestResult<List<User>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<User>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<User>>(RouteConsts.User.GetUsers.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<User>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<User> GetByCardId(Search.Users.ByCardId value)
            {
                NRestResult<User> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<User>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<User>(RouteConsts.User.GetByCardId.Url, value);
                }
                else
                {
                    ret = new NRestResult<User>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<User> GetById(Search.Users.ById value)
            {
                NRestResult<User> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<User>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<User>(RouteConsts.User.GetById.Url, value);
                }
                else
                {
                    ret = new NRestResult<User>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<User>> SearchByGroupId(Search.Users.ByGroupId value)
            {
                NRestResult<List<User>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<User>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<User>>(
                        RouteConsts.User.SearchByGroupId.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<User>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<User>> SearchById(Search.Users.ById value)
            {
                NRestResult<List<User>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<User>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<User>>(RouteConsts.User.SearchById.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<User>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<User> GetByLogIn(Search.Users.ByLogIn value)
            {
                NRestResult<User> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<User>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<User>(RouteConsts.User.GetByLogIn.Url, value);
                }
                else
                {
                    ret = new NRestResult<User>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<User> SaveUser(User value)
            {
                NRestResult<User> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<User>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<User>(RouteConsts.User.SaveUser.Url, value);
                }
                else
                {
                    ret = new NRestResult<User>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
