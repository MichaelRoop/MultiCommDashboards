using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.data;
using LanguageFactory.Net.Messaging;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using LogUtils.Net;
using MultiCommDashboards.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfCustomControlLib.Core.UtilWindows;

namespace MultiCommDashboards {

    /// <summary>Interaction logic for App.xaml</summary>
    public partial class App : Application {

        #region Data

        private log4net.ILog loggerImpl = null;
        private ClassLog log = new ClassLog("App");
        private DateTime currentDate = DateTime.Now;
        private static App staticApp = null;
        LogHelper logHelper = new LogHelper();

        #endregion

        #region Properties

        public static App STATIC_APP {
            get { return staticApp; }
            private set { staticApp = value; }
        }

        /// <summary>Build number to display</summary>
        public static string Build {
            get { return "2021.08.24.01"; }
        }

        #endregion

        #region Events

        public event EventHandler<string> LogMsgEvent;

        #endregion

        #region Constructors

        public App() {
            STATIC_APP = this;
            this.SetupExceptionHandlers();
            this.SetupLogging();
            this.SetupDI();
        }

        #endregion

        #region Setup methods

        private void SetupExceptionHandlers() {
            this.DispatcherUnhandledException += this.App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += this.TaskScheduler_UnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
        }


        private void SetupLogging() {
            this.loggerImpl = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            this.SetupLog4Net();
            this.logHelper.InfoMsgEvent += this.LogHelper_InfoMsgEvent;
            this.logHelper.DebugMsgEvent += this.LogHelper_DebugMsgEvent;
            this.logHelper.WarningMsgEvent += this.LogHelper_WarningMsgEvent;
            this.logHelper.ErrorMsgEvent += this.LogHelper_ErrorMsgEvent;
            this.logHelper.ExceptionMsgEvent += this.LogHelper_ExceptionMsgEvent;
            this.logHelper.EveryMsgEvent += this.LogHelper_EveryMsgEvent;
            this.logHelper.Setup(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, App.Build, MsgLevel.Info, true, 5);
        }


        private void SetupDI() {
            ErrReport err;
            WrapErr.ToErrReport(out err, 9999, () => {
                DI.W.UnexpectedExceptionEvent += this.Wrapper_UnexpectedExceptionEvent;
                this.SetupLanguage();
            });
            if (err.Code != 0) {
                MessageBox.Show(err.Msg, "Critical Error loading DI container");
                Application.Current.Shutdown();
            }
        }


        private void Wrapper_UnexpectedExceptionEvent(object sender, ErrReport report) {
            Dispatcher.Invoke(() => {
                try {
                    Window main = null;
                    if (Application.Current != null && Application.Current.MainWindow != null) {
                        main = Application.Current.MainWindow;
                    }
                    CrashReport.ShowBox(report, main, "Multi Comm Dashboards");
                }
                catch (Exception e) {
                    this.log.Exception(9999, "Wrapper_UnexpectedExceptionEvent", "", e);
                }
            });
        }


        private void SetupLanguage() {
            WpfCustomControlLib.Core.Helpers.CustomTxtBinder.SetLanguageFactory(DI.W.Languages);
            DI.W.LanguageChanged += this.languageChanged;
        }

        private void languageChanged(object sender, SupportedLanguage l) {
            // Save the language when changed anywhere in App
            DI.W.LanguageChanged -= this.languageChanged;
            DI.W.SetLanguage(l.Language.Code, App.ShowErrMsg);
            DI.W.LanguageChanged += this.languageChanged;
        }

        #endregion

        #region Unhandled Exception capture

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null) {
                this.log.Exception(9999, "CurrentDomain_UnhandledException", ex);
            }
            else {
                this.log.Error(9999, "CurrentDomain_UnhandledException", "Null exception object");
            }
            this.ProcessException(ex);

            // TODO - flag is exeption isTerminating but no other handlers has posted report
            // You can also retrieve the IsTerminating to find if thrown during termination
        }


        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
            this.log.Exception(9999, "TaskScheduler_UnobservedTaskException", e.Exception);
            this.ProcessException(e.Exception);
        }


        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
            this.log.Exception(9999, "App_DispatcherUnhandledException", e.Exception);
            // Gets errors out of the UI. Need to decide what to do
            this.ProcessException(e.Exception as Exception);
        }


        private void ProcessException(Exception e) {
            Window main = null;
            if (Application.Current != null && Application.Current.MainWindow != null) {
                main = Application.Current.MainWindow;
            }
            CrashReport.ShowBox(e, main, "Multi Comm Dashboards");
            Application.Current.Shutdown();
        }

        #endregion

        #region Log Event Handlers

        private void LogHelper_EveryMsgEvent(object sender, string msg) {
            STATIC_APP.DispatchProxy(() => {
                try {
                    this.LogMsgEvent?.Invoke(this, msg);
                }
                catch (Exception) { }
            });
        }


        private void LogHelper_InfoMsgEvent(object sender, string msg) {
            this.loggerImpl.Info(msg);
        }


        private void LogHelper_DebugMsgEvent(object sender, string msg) {
            this.loggerImpl.Debug(msg);
        }


        private void LogHelper_WarningMsgEvent(object sender, string msg) {
            this.loggerImpl.Warn(msg);
        }


        private void LogHelper_ErrorMsgEvent(object sender, string msg) {
            this.loggerImpl.Error(msg);
        }


        private void LogHelper_ExceptionMsgEvent(object sender, string msg) {
            this.loggerImpl.Error(msg);
        }

        #endregion

        #region Static helpers that provide Windows dispatch

        private void DispatchProxy(Action action) {
            this.Dispatcher.Invoke(new Action(() => {
                WrapErr.ToErrReport(9999, action.Invoke);
            }));
        }


        public static void ShowErrMsg(string msg) {
            STATIC_APP.DispatchProxy(() => {
                try {
                    MsgBoxSimple.ShowBox(DI.W.GetText(MsgCode.Error), msg);
                }
                catch (Exception) { }
            });
        }


        public static void ShowMsgTitle(string title, string msg) {
            STATIC_APP.DispatchProxy(() => {
                try {
                    MsgBoxSimple.ShowBox(title, msg);
                }
                catch (Exception) { }
            });
        }


        public static bool ShowAreYouSure(string msg) {
            //STATIC_APP.DispatchProxy(() => {
                try {
                    return MsgBoxYesNo.ShowBox(msg) == MsgBoxYesNo.MsgBoxResult.Yes;
                    //MsgBoxSimple.ShowBox(title, msg);
                }
                catch (Exception) {
                    return false;
                }
            //});
        }


        public static bool ShowAreYouSure(string title, string msg) {
            //STATIC_APP.DispatchProxy(() => {
            try {
                return MsgBoxYesNo.ShowBox(title, msg) == MsgBoxYesNo.MsgBoxResult.Yes;
                //MsgBoxSimple.ShowBox(title, msg);
            }
            catch (Exception) {
                return false;
            }
            //});
        }

        #endregion

        #region Log4NEt config

        private void SetupLog4Net() {
            // Replaces the configuration loaded from file
            //https://stackoverflow.com/questions/16336917/can-you-configure-log4net-in-code-instead-of-using-a-config-file
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository(Assembly.GetCallingAssembly());
            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%message%newline";
            patternLayout.ActivateOptions();
            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            // Use manual configuration because the %env for special folders no longer working
            roller.File =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.None),
                    @"MultiCommDashboards\Logs\log.txt");
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "3GB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        #endregion

    }
}
