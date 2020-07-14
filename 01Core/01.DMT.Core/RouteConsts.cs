using System;
using System.Collections.Generic;

namespace DMT
{
    public static class RouteConsts
    {
        public const string Url = @"api";

        public static class TSB
        {
            public const string Url = RouteConsts.Url + @"/TSB";

            public static class GetTSBs
            {
                public const string Name = "GetTSBs";
                public const string Url = TSB.Url + @"/" + Name;
            }
            public static class GetTSBPlazas
            {
                public const string Name = "GetTSBPlazas";
                public const string Url = TSB.Url + @"/" + Name;
            }
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
            public static class SetActive
            {
                public const string Name = "SetActive";
                public const string Url = TSB.Url + @"/" + Name;
            }
            public static class GetCurrent
            {
                public const string Name = "GetCurrent";
                public const string Url = TSB.Url + @"/" + Name;
            }
        }

        public static class User
        {
            public const string Url = RouteConsts.Url + @"/User";

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
        }

        public static class Shift
        {
            public const string Url = RouteConsts.Url + @"/Shift";

            public static class GetShifts
            {
                public const string Name = "GetShifts";
                public const string Url = Shift.Url + @"/" + Name;
            }

            public static class ChangeShift
            {
                public const string Name = "ChangeShift";
                public const string Url = Shift.Url + @"/" + Name;
            }

            public static class GetCurrent
            {
                public const string Name = "GetCurrent";
                public const string Url = Shift.Url + @"/" + Name;
            }

            public static class Create
            {
                public const string Name = "Create";
                public const string Url = Shift.Url + @"/" + Name;
            }
        }

        public static class Job
        {
            public const string Url = RouteConsts.Url + @"/Job";

            public static class GetUsers
            {
                public const string Name = "GetUsers";
                public const string Url = Job.Url + @"/" + Name;
            }

            public static class BeginJob
            {
                public const string Name = "BeginJob";
                public const string Url = Job.Url + @"/" + Name;
            }

            public static class EndJob
            {
                public const string Name = "EndJob";
                public const string Url = Job.Url + @"/" + Name;
            }

            public static class GetCurrent
            {
                public const string Name = "GetCurrent";
                public const string Url = Job.Url + @"/" + Name;
            }

            public static class Create
            {
                public const string Name = "Create";
                public const string Url = Job.Url + @"/" + Name;
            }

            public static class GetUserShifts
            {
                public const string Name = "GetUserShifts";
                public const string Url = Job.Url + @"/" + Name;
            }
        }

        public static class Lane
        {
            public const string Url = RouteConsts.Url + @"/Lane";

            public static class CreateAttendance
            {
                public const string Name = "CreateAttendance";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class CreatePayment
            {
                public const string Name = "CreatePayment";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class SaveAttendance
            {
                public const string Name = "SaveAttendance";
                public const string Url = Lane.Url + @"/" + Name;
            }

            public static class SavePayment
            {
                public const string Name = "SavePayment";
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
        }

        public static class Revenue
        {
            public const string Url = RouteConsts.Url + @"/Revenue";

            public static class CreatePlazaRevenue
            {
                public const string Name = "CreatePlazaRevenue";
                public const string Url = Revenue.Url + @"/" + Name;
            }

            public static class GetPlazaRevenue
            {
                public const string Name = "GetPlazaRevenue";
                public const string Url = Revenue.Url + @"/" + Name;
            }

            public static class SavePlazaRevenue
            {
                public const string Name = "SavePlazaRevenue";
                public const string Url = Revenue.Url + @"/" + Name;
            }

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
        }
    }
}
