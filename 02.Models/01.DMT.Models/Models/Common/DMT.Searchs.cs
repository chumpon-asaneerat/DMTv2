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

            public class ByGroupId : NSearch<ByGroupId>
            {
                public int GroupId { get; set; }

                public static ByGroupId Create(int groupId)
                {
                    var ret = new ByGroupId();
                    ret.GroupId = groupId;
                    return ret;
                }
            }

            public class ById : NSearch<ById>
            {
                public string UserId { get; set; }
                public string[] Roles { get; set; }

                public static ById Create(string userId, params string[] roles)
                {
                    var ret = new ById();
                    ret.UserId = userId;
                    ret.Roles = roles;
                    return ret;
                }
            }
        }
    }

    #endregion

    #region Lane Attendance related

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

    #endregion

    #region Revenues

    partial class Search
    {
        public static class Revenues
        {
            public class PlazaShift : NSearch<PlazaShift>
            {
                public UserShift Shift { get; set; }
                public PlazaGroup PlazaGroup { get; set; }

                public static PlazaShift Create(UserShift shift, PlazaGroup plazaGroup)
                {
                    var ret = new PlazaShift();
                    ret.Shift = shift;
                    ret.PlazaGroup = plazaGroup;
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

    #region UserCredit

    partial class Search
    {
        public class UserCredits
        {
            public class GetActive : NSearch<GetActive>
            {
                public User User { get; set; }
                public PlazaGroup PlazaGroup { get; set; }

                public static GetActive Create(User user, PlazaGroup plazGroup)
                {
                    var ret = new GetActive();
                    ret.User = user;
                    ret.PlazaGroup = plazGroup;
                    return ret;
                }
            }
            public class GetActiveById : NSearch<GetActive>
            {
                public string UserId { get; set; }
                public string PlazaGroupId { get; set; }

                public static GetActiveById Create(string userId, string plazGroupId)
                {
                    var ret = new GetActiveById();
                    ret.UserId = userId;
                    ret.PlazaGroupId = plazGroupId;
                    return ret;
                }
            }
        }
    }

    #endregion

    #region UserCoupon

    partial class Search
    {
        public class TSBCoupons
        {
            public class ByUser : NSearch<ByUser>
            {
                public TSB TSB { get; set; }
                public User User { get; set; }

                public static ByUser Create(TSB tsb, User user)
                {
                    var ret = new ByUser();
                    ret.User = user;
                    ret.TSB = tsb;
                    return ret;
                }
            }
        }

        public class UserCoupons
        {
            public class BorrowCoupons : NSearch<BorrowCoupons>
            {
                public User User { get; set; }
                public List<TSBCouponTransaction> Coupons { get; set; }

                public static BorrowCoupons Create(User user, List<TSBCouponTransaction> coupons)
                {
                    var ret = new BorrowCoupons();
                    ret.User = user;
                    ret.Coupons = coupons;
                    return ret;
                }
            }

            public class ReturnCoupons : NSearch<ReturnCoupons>
            {
                public User User { get; set; }
                public List<TSBCouponTransaction> Coupons { get; set; }

                public static ReturnCoupons Create(User user, List<TSBCouponTransaction> coupons)
                {
                    var ret = new ReturnCoupons();
                    ret.User = user;
                    ret.Coupons = coupons;
                    return ret;
                }
            }

            public class ByUser : NSearch<ByUser>
            {
                public TSB TSB { get; set; }
                public User User { get; set; }

                public static ByUser Create(TSB tsb, User user)
                {
                    var ret = new ByUser();
                    ret.User = user;
                    ret.TSB = tsb;
                    return ret;
                }
            }

            public class ToTSBCoupons : NSearch<ToTSBCoupons>
            {
                public TSB TSB { get; set; }
                public List<UserCouponTransaction> Coupons { get; set; }

                public static ToTSBCoupons Create(TSB tsb, List<UserCouponTransaction> coupons)
                {
                    var ret = new ToTSBCoupons();
                    ret.TSB = tsb;
                    ret.Coupons = coupons;
                    return ret;
                }
            }
        }
    }

    #endregion

    #endregion
}
