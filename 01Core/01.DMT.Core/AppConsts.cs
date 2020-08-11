using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT
{
    public static class AppConsts
    {
        // common properties
        public static string Version = "1";
        public static string Minor = "1";
        public static string Build = "1";
        public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);

        public static class Application
        {
            public static class TA
            {
                public static string ApplicationName = @"DMT Toll Admin Application";
                // common
                public static string Version = AppConsts.Version;
                public static string Minor = AppConsts.Minor;
                public static string Build = AppConsts.Build;
                public static DateTime LastUpdate = AppConsts.LastUpdate;
            }
            public static class TOD
            {
                public static string ApplicationName = @"DMT Toll of Duty Application";
                // common
                public static string Version = AppConsts.Version;
                public static string Minor = AppConsts.Minor;
                public static string Build = AppConsts.Build;
                public static DateTime LastUpdate = AppConsts.LastUpdate;
            }
            public static class Account
            {
                public static string ApplicationName = @"DMT Toll Account Application";
                // common
                public static string Version = AppConsts.Version;
                public static string Minor = AppConsts.Minor;
                public static string Build = AppConsts.Build;
                public static DateTime LastUpdate = AppConsts.LastUpdate;
            }
            public static class PlazaConfig
            {
                public static string ApplicationName = @"DMT TOD-TA Plaza Config";
                // common
                public static string Version = AppConsts.Version;
                public static string Minor = AppConsts.Minor;
                public static string Build = AppConsts.Build;
                public static DateTime LastUpdate = AppConsts.LastUpdate;
            }
            public static class PlazaSumulator
            {
                public static string ApplicationName = @"DMT TOD-TA Plaza Simulator";
                // common
                public static string Version = AppConsts.Version;
                public static string Minor = AppConsts.Minor;
                public static string Build = AppConsts.Build;
                public static DateTime LastUpdate = AppConsts.LastUpdate;
            }
        }
        public static class WindowsService
        {
            public static class Local
            {
                public static string ServiceName = "DMT Local Plaza Windows Service";
                public static string DisplayName = "DMT Local Plaza Windows Service";
                public static string Description = "DMT Local Plaza Windows Service";
                public static string ExecutableFileName = @"DMT.Local.Plaza.Windows.Services.exe";
                // common
                public static string Version = AppConsts.Version;
                public static string Minor = AppConsts.Minor;
                public static string Build = AppConsts.Build;
                public static DateTime LastUpdate = AppConsts.LastUpdate;

                public static class WebServer
                {
                    public static string Protocol = "http";
                    public static string HostName = "localhost";
                    public static int PortNumber = 9000;
                }

                public static class WebSocket
                {
                    public static string Protocol = "ws";
                    public static string HostName = "localhost";
                    public static int PortNumber = 9100;
                }
            }

            public static class DC
            {
                public static string ServiceName = "DMT Data Center Plaza Windows Service";
                public static string DisplayName = "DMT Data Center Plaza Windows Service";
                public static string Description = "DMT Data Center Plaza Windows Service";
                public static string ExecutableFileName = @"DMT.DC.Plaza.Windows.Services.exe";
                // common
                public static string Version = AppConsts.Version;
                public static string Minor = AppConsts.Minor;
                public static string Build = AppConsts.Build;
                public static DateTime LastUpdate = AppConsts.LastUpdate;

                public static class WebServer
                {
                    public static string Protocol = "http";
                    public static string HostName = "localhost";
                    public static int PortNumber = 9000;
                }
            }

            public static class TAxTOD
            {
                public static string ServiceName = "DMT TAxTOD Plaza Windows Service";
                public static string DisplayName = "DMT TAxTOD Plaza Windows Service";
                public static string Description = "DMT TAxTOD Plaza Windows Service";
                public static string ExecutableFileName = @"DMT.TAxTOD.Plaza.Windows.Services.exe";
                // common
                public static string Version = AppConsts.Version;
                public static string Minor = AppConsts.Minor;
                public static string Build = AppConsts.Build;
                public static DateTime LastUpdate = AppConsts.LastUpdate;

                public static class WebServer
                {
                    public static string Protocol = "http";
                    public static string HostName = "localhost";
                    public static int PortNumber = 9000;
                }
            }
        }
    }
}
