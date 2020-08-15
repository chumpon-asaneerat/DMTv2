#region Using

using System;
using System.Windows;
using System.Reflection;

using NLib;
using NLib.Logs;

#endregion

namespace DMT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool usedForm = false;

        /// OnStartup.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                if (null != AppDomain.CurrentDomain)
                {
                    if (null != System.Threading.Thread.CurrentContext)
                    {
                        Console.WriteLine("Thread CurrentContext is not null.");
                    }
                }

                #region Create Application Environment Options

                EnvironmentOptions option = new EnvironmentOptions()
                {
                    /* Setup Application Information */
                    AppInfo = new NAppInformation()
                    {
                        /*  This property is required */
                        CompanyName = "DMT",
                        /*  This property is required */
                        ProductName = AppConsts.WindowsService.Local.ServiceName,
                        /* For Application Version */
                        Version = AppConsts.WindowsService.Local.Version,
                        Minor = AppConsts.WindowsService.Local.Minor,
                        Build = AppConsts.WindowsService.Local.Build,
                        LastUpdate = AppConsts.WindowsService.Local.LastUpdate
                    },
                    /* Setup Storage */
                    Storage = new NAppStorage()
                    {
                        StorageType = NAppFolder.ProgramData
                    },
                    /* Setup Behaviors */
                    Behaviors = new NAppBehaviors()
                    {
                        /* Set to true for allow only one instance of application can execute an runtime */
                        IsSingleAppInstance = true,
                        /* Set to true for enable Debuggers this value should always be true */
                        EnableDebuggers = true
                    }
                };

                #endregion

                #region Check Arguments


                if (null != e.Args && e.Args.Length > 0)
                {
                    if (string.Compare(e.Args[0], "-debug", true) == 0 ||
                        string.Compare(e.Args[0], "-d", true) == 0)
                    {
                        usedForm = true;
                    }
                }

                #endregion

                #region Setup Option to Controller and check instance


                if (usedForm)
                {
                    WpfAppContoller.Instance.Setup(option);

                    if (option.Behaviors.IsSingleAppInstance &&
                        WpfAppContoller.Instance.HasMoreInstance)
                    {
                        return;
                    }
                }
                else
                {
                    WinServiceContoller.Instance.Setup(option);
                }

                #endregion

                #region Init Preload classes

                ApplicationManager.Instance.Preload(() =>
                {

                });

                #endregion

                #region Load Main UI or Service

                med.Err("App Instance GUID : " +
                    ApplicationManager.InstanceGuid.ToString());

                if (usedForm)
                {
                    Window window = null;
                    window = new MainWindow();

                    // Start log manager
                    LogManager.Instance.Start();

                    if (null != window)
                    {
                        WpfAppContoller.Instance.Run(window);
                    }
                }
                else
                {
                    // Start log manager
                    LogManager.Instance.Start();

                    var manager = new Services.PlazaDataServiceManager();
                    WinServiceContoller.Instance.Run(manager, e.Args);
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (usedForm)
                {
                    MessageBox.Show(ex.ToString());
                }
                med.Err(ex);
            }
            finally
            {
                // Shutdown log manager
                LogManager.Instance.Shutdown();
                /*
                if (usedForm)
                    WpfAppContoller.Instance.Shutdown(true);
                else WinServiceContoller.Instance.Shutdown(true);
                */
            }
        }
        /// <summary>
        /// OnExit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            // Shutdown log manager
            LogManager.Instance.Shutdown();

            // Wpf shutdown process required exit code.

            /* If auto close the single instance must be true */
            bool autoCloseProcess = true;
            if (usedForm)
                WpfAppContoller.Instance.Shutdown(autoCloseProcess, e.ApplicationExitCode);
            else WinServiceContoller.Instance.Shutdown(autoCloseProcess);

            base.OnExit(e);
        }
    }
}
