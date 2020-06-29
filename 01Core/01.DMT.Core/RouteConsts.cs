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
