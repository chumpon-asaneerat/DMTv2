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
    #region Enums

    /*
    public enum RevenuDateEnum
    {
        All,
        HasRevenueDate,
        NoRevenueDate
    }
    */

    #endregion

    #region Search nested classes

    public partial class Search
    {
        // for test.
        public partial class Options { }
    }

    partial class Search
    {
        // for test.
        partial class Options 
        {
            public class ById 
            {
                public int Id { get; set; }

                public static ById Create(int id)
                {
                    var inst = new ById();
                    inst.Id = id;
                    return inst;
                }
            }
        }
    }

    partial class Search
    {
        // for test.
        partial class Options
        {
            public class ByCode
            {
                public string Code { get; set; }

                public static ByCode Create(string code)
                {
                    var inst = new ByCode();
                    inst.Code = code;
                    return inst;
                }
            }
        }
    }

    public class Test 
    { 
        public static void Run()
        {
            var optById = Search.Options.ById.Create(123);
            if (null != optById) Console.WriteLine(optById.Id);

            var optByCode = Search.Options.ByCode.Create("mycoe");
            if (null != optByCode) Console.WriteLine(optByCode.Code);
        }
    }

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
    }

    partial class Search
    {
        public static class Lanes
        {
            public static class Current
            {
                public class AttendanceByLane : NSearch<AttendanceByLane>
                {
                    public Lane Lane { get; set; }

                    public static AttendanceByLane Create(Lane lane)
                    {
                        var ret = new AttendanceByLane();
                        ret.Lane = lane;
                        return ret;
                    }
                }

                public class PaymentByLane : NSearch<PaymentByLane>
                {
                    public Lane Lane { get; set; }

                    public static PaymentByLane Create(Lane lane)
                    {
                        var ret = new PaymentByLane();
                        ret.Lane = lane;
                        return ret;
                    }
                }
            }

            public static class Attendances
            {
                public class ByDate : NSearch<ByDate>
                {
                    public DateTime Date { get; set; }

                    public static ByDate Create(DateTime date)
                    {
                        var ret = new ByDate();
                        ret.Date = date;
                        return ret;
                    }
                }

                public class ByUserShift : NSearch<ByUserShift>
                {
                    public UserShift Shift { get; set; }
                    public PlazaGroup PlazaGroup { get; set; }
                    public DateTime RevenueDate { get; set; }

                    public static ByUserShift Create(UserShift shift, PlazaGroup plazaGroup,
                        DateTime revenueDate)
                    {
                        var ret = new ByUserShift();
                        ret.Shift = shift;
                        ret.PlazaGroup = plazaGroup;
                        ret.RevenueDate = revenueDate;
                        return ret;
                    }
                }

                public class ByLane : NSearch<ByLane>
                {
                    public Lane Lane { get; set; }

                    public static ByLane Create(Lane lane)
                    {
                        var ret = new ByLane();
                        ret.Lane = lane;
                        return ret;
                    }
                }
            }

            public static class Payments
            {
                public class ByDate : NSearch<ByDate>
                {
                    public DateTime Date { get; set; }

                    public static ByDate Create(DateTime date)
                    {
                        var ret = new ByDate();
                        ret.Date = date;
                        return ret;
                    }
                }

                public class ByUserShift : NSearch<ByUserShift>
                {
                    public UserShift Shift { get; set; }

                    public static ByUserShift Create(UserShift shift)
                    {
                        var ret = new ByUserShift();
                        ret.Shift = shift;
                        return ret;
                    }
                }

                public class ByLane : NSearch<ByLane>
                {
                    public Lane Lane { get; set; }

                    public static ByLane Create(Lane lane)
                    {
                        var ret = new ByLane();
                        ret.Lane = lane;
                        return ret;
                    }
                }
            }
        }
    }

    partial class Search
    {
        public static class Revenues
        {
            public class PlazaShift : NSearch<PlazaShift>
            {
                public UserShift Shift { get; set; }
                public Plaza Plaza { get; set; }

                public static PlazaShift Create(UserShift shift, Plaza plaza)
                {
                    var ret = new PlazaShift();
                    ret.Shift = shift;
                    ret.Plaza = plaza;
                    return ret;
                }
            }
            public class SaveRevenueShift : NSearch<SaveRevenueShift>
            {
                public UserShiftRevenue RevenueShift { get; set; }
                public string RevenueId { get; set; }
                public DateTime RevenueDate { get; set; }

                public static SaveRevenueShift Create(UserShiftRevenue revenueShift, 
                    string revenueId, DateTime revenueDate)
                {
                    var ret = new SaveRevenueShift();
                    ret.RevenueShift = revenueShift;
                    ret.RevenueId = revenueId;
                    ret.RevenueDate = revenueDate;
                    return ret;
                }
            }
        }
    }

    #endregion
}
