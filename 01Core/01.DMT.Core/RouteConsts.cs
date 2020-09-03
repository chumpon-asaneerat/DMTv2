using System;
using System.Collections.Generic;

namespace DMT
{
    public static class RouteConsts
    {
        #region Base Url

        public const string Url = @"api";

        #endregion

        #region Master

        public static class Master
        {
            public const string Url = RouteConsts.Url + @"/Master";

            #region MCurrency

            public static class GetCurrencies
            {
                public const string Name = "GetCurrencies";
                public const string Url = Master.Url + @"/" + Name;
            }

            #endregion

            #region MCoupon

            public static class GetCoupons
            {
                public const string Name = "GetCoupons";
                public const string Url = Master.Url + @"/" + Name;
            }

            #endregion
        }

        #endregion

        #region TSB

        public static class TSB
        {
            public const string Url = RouteConsts.Url + @"/TSB";

            #region TSB

            public static class GetTSBs
            {
                public const string Name = "GetTSBs";
                public const string Url = TSB.Url + @"/" + Name;
            }

            public static class GetCurrent
            {
                public const string Name = "GetCurrent";
                public const string Url = TSB.Url + @"/" + Name;
            }

            public static class SetActive
            {
                public const string Name = "SetActive";
                public const string Url = TSB.Url + @"/" + Name;
            }
            // NEW!! 2020-07-29 - Save methods
            public static class SaveTSB
            {
                public const string Name = "SaveTSB";
                public const string Url = TSB.Url + @"/" + Name;
            }

            #endregion

            #region Plaza

            public static class GetTSBPlazas
            {
                public const string Name = "GetTSBPlazas";
                public const string Url = TSB.Url + @"/" + Name;
            }

            public static class GetPlazaGroupPlazas
            {
                public const string Name = "GetPlazaGroupPlazas";
                public const string Url = TSB.Url + @"/" + Name;
            }

            public static class SavePlaza
            {
                public const string Name = "SavePlaza";
                public const string Url = TSB.Url + @"/" + Name;
            }

            #endregion

            #region PlazaGroup

            public static class GetTSBPlazaGroups
            {
                public const string Name = "GetTSBPlazaGroups";
                public const string Url = TSB.Url + @"/" + Name;
            }

            public static class SavePlazaGroup
            {
                public const string Name = "SavePlazaGroup";
                public const string Url = TSB.Url + @"/" + Name;
            }

            #endregion

            #region Lane

            public static class GetTSBLanes
            {
                public const string Name = "GetTSBLanes";
                public const string Url = TSB.Url + @"/" + Name;
            }

            public static class GetPlazaLanes
            {
                public const string Name = "GetPlazaLanes";
                public const string Url = TSB.Url + @"/" + Name;
            }

            public static class GetPlazaLane
            {
                public const string Name = "GetPlazaLane";
                public const string Url = TSB.Url + @"/" + Name;
            }

            public static class GetPlazaGroupLanes
            {
                public const string Name = "GetPlazaGroupLanes";
                public const string Url = TSB.Url + @"/" + Name;
            }

            public static class SaveLane
            {
                public const string Name = "SaveLane";
                public const string Url = TSB.Url + @"/" + Name;
            }

            #endregion
        }

        #endregion

        #region User

        public static class User
        {
            public const string Url = RouteConsts.Url + @"/User";

            #region Role

            public static class GetRole
            {
                public const string Name = "GetRole";
                public const string Url = User.Url + @"/" + Name;
            }

            public static class GetRoles
            {
                public const string Name = "GetRoles";
                public const string Url = User.Url + @"/" + Name;
            }
            // NEW!! 2020-07-29 - Save methods
            public static class SaveRole
            {
                public const string Name = "SaveRole";
                public const string Url = User.Url + @"/" + Name;
            }

            #endregion

            #region User

            public static class GetUsers
            {
                public const string Name = "GetUsers";
                public const string Url = User.Url + @"/" + Name;
            }

            public static class GetById
            {
                public const string Name = "GetById";
                public const string Url = User.Url + @"/" + Name;
            }

            public static class SearchByGroupId
            {
                public const string Name = "SearchByGroupId";
                public const string Url = User.Url + @"/" + Name;
            }

            public static class SearchById
            {
                public const string Name = "SearchById";
                public const string Url = User.Url + @"/" + Name;
            }

            public static class GetByCardId
            {
                public const string Name = "GetByCardId";
                public const string Url = User.Url + @"/" + Name;
            }

            public static class GetByLogIn
            {
                public const string Name = "GetByLogIn";
                public const string Url = User.Url + @"/" + Name;
            }

            public static class SaveUser
            {
                public const string Name = "SaveUser";
                public const string Url = User.Url + @"/" + Name;
            }

            #endregion
        }

        #endregion

        #region Shift

        public static class Shift
        {
            public const string Url = RouteConsts.Url + @"/Shift";

            #region Shift

            public static class GetShifts
            {
                public const string Name = "GetShifts";
                public const string Url = Shift.Url + @"/" + Name;
            }

            // NEW!! 2020-07-29 - Save methods.
            public static class SaveShift
            {
                public const string Name = "SaveShift";
                public const string Url = Shift.Url + @"/" + Name;
            }

            #endregion

            #region TSB Shift

            public static class Create
            {
                public const string Name = "Create";
                public const string Url = Shift.Url + @"/" + Name;
            }

            public static class GetCurrent
            {
                public const string Name = "GetCurrent";
                public const string Url = Shift.Url + @"/" + Name;
            }

            public static class ChangeShift
            {
                public const string Name = "ChangeShift";
                public const string Url = Shift.Url + @"/" + Name;
            }

            #endregion
        }

        #endregion

        #region UserShift

        public static class UserShift
        {
            public const string Url = RouteConsts.Url + @"/UserShift";

            public static class Create
            {
                public const string Name = "Create";
                public const string Url = UserShift.Url + @"/" + Name;
            }

            public static class GetUsers
            {
                public const string Name = "GetUsers";
                public const string Url = UserShift.Url + @"/" + Name;
            }

            public static class GetCurrent
            {
                public const string Name = "GetCurrent";
                public const string Url = UserShift.Url + @"/" + Name;
            }

            public static class BeginUserShift
            {
                public const string Name = "BeginUserShift";
                public const string Url = UserShift.Url + @"/" + Name;
            }

            public static class EndUserShift
            {
                public const string Name = "EndUserShift";
                public const string Url = UserShift.Url + @"/" + Name;
            }

            public static class GetUserShifts
            {
                public const string Name = "GetUserShifts";
                public const string Url = UserShift.Url + @"/" + Name;
            }

            public static class GetUnCloseUserShifts
            {
                public const string Name = "GetUnCloseUserShifts";
                public const string Url = UserShift.Url + @"/" + Name;
            }
        }

        #endregion

        #region Lane (Attendance/Payment)

        public static class Lane
        {
            public const string Url = RouteConsts.Url + @"/Lane";

            #region Lane Attendance

            public static class CreateAttendance
            {
                public const string Name = "CreateAttendance";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class SaveAttendance
            {
                public const string Name = "SaveAttendance";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class SaveAttendances
            {
                public const string Name = "SaveAttendances";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetAttendancesByDate
            {
                public const string Name = "GetAttendancesByDate";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetAttendancesByUserShift
            {
                public const string Name = "GetAttendancesByUserShift";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetAllAttendancesByUserShift
            {
                public const string Name = "GetAllAttendancesByUserShift";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetAttendancesByLane
            {
                public const string Name = "GetAttendancesByLane";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetCurrentAttendancesByLane
            {
                public const string Name = "GetCurrentAttendancesByLane";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetAllNotHasRevenueEntry
            {
                public const string Name = "GetAllNotHasRevenueEntry";
                public const string Url = Lane.Url + @"/" + Name;
            }

            #endregion

            #region Lane Payment

            public static class CreatePayment
            {
                public const string Name = "CreatePayment";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class SavePayment
            {
                public const string Name = "SavePayment";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetPaymentsByDate
            {
                public const string Name = "GetPaymentsByDate";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetPaymentsByUserShift
            {
                public const string Name = "GetPaymentsByUserShift";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetPaymentsByLane
            {
                public const string Name = "GetPaymentsByLane";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class GetCurrentPaymentsByLane
            {
                public const string Name = "GetCurrentPaymentsByLane";
                public const string Url = Lane.Url + @"/" + Name;
            }

            #endregion
        }

        #endregion

        #region Revenue

        public static class Revenue
        {
            public const string Url = RouteConsts.Url + @"/Revenue";

            #region UserShiftRevenue

            public static class CreatePlazaRevenue
            {
                public const string Name = "CreatePlazaRevenue";
                public const string Url = Revenue.Url + @"/" + Name;
            }

            public static class SavePlazaRevenue
            {
                public const string Name = "SavePlazaRevenue";
                public const string Url = Revenue.Url + @"/" + Name;
            }

            public static class GetPlazaRevenue
            {
                public const string Name = "GetPlazaRevenue";
                public const string Url = Revenue.Url + @"/" + Name;
            }

            #endregion

            #region Revenue Entry

            public static class SaveRevenue
            {
                public const string Name = "SaveRevenue";
                public const string Url = Revenue.Url + @"/" + Name;
            }

            public static class GetRevenues
            {
                public const string Name = "GetRevenues";
                public const string Url = Revenue.Url + @"/" + Name;
            }

            #endregion
        }

        #endregion

        #region Credit

        public static class Credit
        {
            public const string Name = "Credit";
            public const string Url = RouteConsts.Url + @"/" + Name;

            #region TSB Credit Balance

            public static class GetTSBBalance
            {
                public const string Name = "GetTSBBalance";
                public const string Url = Credit.Url + @"/" + Name;
            }

            #endregion

            #region TSB Credit Transaction

            public static class GetInitialTSBCreditTransaction
            {
                public const string Name = "GetInitialTSBCreditTransaction";
                public const string Url = Credit.Url + @"/" + Name;
            }

            public static class SaveTSBCreditTransaction
            {
                public const string Name = "SaveTSBCreditTransaction";
                public const string Url = Credit.Url + @"/" + Name;
            }

            #endregion

            #region User Credit Balance

            public static class GetActiveUserCreditBalances
            {
                public const string Name = "GetActiveUserCreditBalances";
                public const string Url = Credit.Url + @"/" + Name;
            }

            public static class GetActiveUserCreditBalanceById
            {
                public const string Name = "GetActiveUserCreditBalanceById";
                public const string Url = Credit.Url + @"/" + Name;
            }

            public static class SaveUserCreditBalance
            {
                public const string Name = "SaveUserCreditBalance";
                public const string Url = Credit.Url + @"/" + Name;
            }

            #endregion

            #region User Credit Transaction

            public static class SaveUserCreditTransaction
            {
                public const string Name = "SaveUserCreditTransaction";
                public const string Url = Credit.Url + @"/" + Name;
            }

            #endregion

            /*
            public static class GetCurrentInitial
            {
                public const string Name = "GetCurrentInitial";
                public const string Url = Credit.Url + @"/" + Name;
            }

            public static class GetActiveUserCredit
            {
                public const string Name = "GetActiveUserCredit";
                public const string Url = Credit.Url + @"/" + Name;
            }
            */
        }

        #endregion

        #region Coupon

        public static class Coupon
        {
            public const string Name = "Coupon";
            public const string Url = RouteConsts.Url + @"/" + Name;

            #region TSB Coupon Balance

            public static class GetTSBBalance
            {
                public const string Name = "GetTSBBalance";
                public const string Url = Coupon.Url + @"/" + Name;
            }

            #endregion

            #region TSB Coupon Summary

            public static class GetTSBCouponSummaries
            {
                public const string Name = "GetTSBCouponSummaries";
                public const string Url = Coupon.Url + @"/" + Name;
            }

            #endregion

            #region TSB Coupon Transaction

            public static class GetTSBCouponTransactions
            {
                public const string Name = "GetTSBCouponTransactions";
                public const string Url = Coupon.Url + @"/" + Name;
            }

            public static class SaveTSBCouponTransaction
            {
                public const string Name = "SaveTSBCouponTransaction";
                public const string Url = Coupon.Url + @"/" + Name;
            }

            public static class SaveTSBCouponTransactions
            {
                public const string Name = "SaveTSBCouponTransactions";
                public const string Url = Coupon.Url + @"/" + Name;
            }

            public static class SyncTSBCouponTransaction
            {
                public const string Name = "SyncTSBCouponTransaction";
                public const string Url = Coupon.Url + @"/" + Name;
            }

            public static class SyncTSBCouponTransactions
            {
                public const string Name = "SyncTSBCouponTransactions";
                public const string Url = Coupon.Url + @"/" + Name;
            }

            #endregion
        }

        #endregion

        #region Exchange

        public static class Exchange
        {
            public const string Name = "Exchange";
            public const string Url = RouteConsts.Url + @"/" + Name;

            #region Exchange Transaction

            public static class GetTSBExchangeTransactions
            {
                public const string Name = "GetTSBExchangeTransactions";
                public const string Url = Exchange.Url + @"/" + Name;
            }

            public static class SaveTSBExchangeTransaction
            {
                public const string Name = "SaveTSBExchangeTransaction";
                public const string Url = Exchange.Url + @"/" + Name;
            }

            #endregion
        }

        #endregion
    }
}
