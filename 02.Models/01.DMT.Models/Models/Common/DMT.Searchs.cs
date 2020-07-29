#region Using

using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
    #region Search nested classes

    #region Main class (empty method).

    public partial class Search { }

    #endregion

    #region Role

    partial class Search
    {
        public static class Roles
        {
            public class ById : NSearch<ById>
            {
                public string RoleId { get; set; }

                public static ById Create(string roleId)
                {
                    var ret = new ById();
                    ret.RoleId = roleId;
                    return ret;
                }
            }
        }
    }

    #endregion

    #region User

    partial class Search
    {
        public static class Users
        {
            public class ByCardId : NSearch<ByCardId>
            {
                public string CardId { get; set; }

                public static ByCardId Create(string cardId)
                {
                    var ret = new ByCardId();
                    ret.CardId = cardId;
                    return ret;
                }
            }

            public class ByLogIn : NSearch<ByLogIn>
            {
                public string UserId { get; set; }
                public string Password { get; set; }

                public static ByLogIn Create(string userId, string pwd)
                {
                    var ret = new ByLogIn();
                    ret.UserId = userId;
                    ret.Password = pwd;
                    return ret;
                }
            }

            public class ById : NSearch<ById>
            {
                public string UserId { get; set; }

                public static ById Create(string userId)
                {
                    var ret = new ById();
                    ret.UserId = userId;
                    return ret;
                }
            }
        }

        #endregion
    }

    #endregion
}
