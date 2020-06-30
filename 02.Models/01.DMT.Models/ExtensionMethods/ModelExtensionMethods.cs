#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DMT.Models;

#endregion

namespace DMT.Models.ExtensionMethods
{
    #region TSB

    /// <summary>
    /// The TSB Extension Methods
    /// </summary>
    public static class TSBExtensionMethods
    {
        public static List<Plaza> GetPlazas(this TSB value)
        {
            if (null == value) return new List<Plaza>();
            return Plaza.GetTSBPlazas(value);
        }

        public static List<Lane> GetLanes(this TSB value)
        {
            if (null == value) return new List<Lane>();
            return Lane.GetTSBLanes(value);
        }

        public static void SetActive(this TSB value)
        {
            if (null == value) return;
            TSB.SetActive(value.TSBId);
        }
    }

    #endregion

    #region Plaza

    /// <summary>
    /// The Plaza Extension Methods
    /// </summary>
    public static class PlazaExtensionMethods
    {
        public static List<Lane> GetLanes(this Plaza value)
        {
            return Lane.GetPlazaLanes(value);
        }
    }

    #endregion

    #region Role

    /// <summary>
    /// The Role Extension Methods
    /// </summary>
    public static class RoleExtensionMethods
    {
        public static List<User> GetUsers(this Role value)
        {
            if (null == value) return new List<User>();
            return User.FindByRole(value.RoleId);
        }
        public static List<User> GetUsers(this Role value, int status)
        {
            if (null == value) return new List<User>();
            return User.FindByRole(value.RoleId, status);
        }
    }

    #endregion
}
