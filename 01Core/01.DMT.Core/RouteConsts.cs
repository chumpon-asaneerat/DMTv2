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

            public static class GetCurrent
            {
                public const string Name = "GetCurrent";
                public const string Url = Shift.Url + @"/" + Name;
            }
        }

        public static class Job
        {
            public const string Url = RouteConsts.Url + @"/Job";

            public static class GetUser
            {
                public const string Name = "GetUser";
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
        }
    }
}
