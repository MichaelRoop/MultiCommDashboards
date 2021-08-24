using LanguageFactory.Net.data;
using LanguageFactory.Net.Messaging;
using LogUtils.Net;
using MultiCommDashboards.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.WindowObjs {

    /// <summary>Logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window {

        ClassLog log = new ClassLog("MainWindow");
        MenuWin menu = null;

        public MainWindow() {
            InitializeComponent();
            DI.W.LanguageChanged += this.languageChanged;
        }


        #region Window events

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight; // TODO move to constructor
        }


        private void Window_ContentRendered(object sender, EventArgs e) {
            try {
                this.menu = new MenuWin(this);
                this.menu.Collapse();
            }
            catch (Exception ex) {
                this.log.Exception(9999, "Window_ContentRendered", "", ex);
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            DI.W.LanguageChanged -= this.languageChanged;
            if (this.menu != null) {
                this.menu.Close();
            }
            DI.W.Teardown();
        }


        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            try {
                this.HideMenu();
            }
            catch (Exception ex) {
                this.log.Exception(9999, "Window_MouseDown", "", ex);
            }
        }

        #endregion

        private void btnMenu_Click(object sender, RoutedEventArgs e) {
            try {
                if (this.menu != null) {
                    if (this.menu.IsVisible) {
                        this.menu.Hide();
                    }
                    else {
                        // Need to get offset from current position of main window at click time
                        this.menu.Left = this.Left;
                        this.menu.Top = this.Top + this.taskBar.ActualHeight;
                        this.menu.Show();
                    }
                }
            }
            catch (Exception ex) {
                this.log.Exception(9999, "btnMenu_Click", "", ex);
            }
        }


        private void titleBarBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            try {
                this.HideMenu();
                this.DragMove();
            }
            catch (Exception ex) {
                this.log.Exception(9999, "titleBarBorder_MouseDown", "", ex);
            }
        }


        private void HideMenu() {
            try {
                if (this.menu != null && this.menu.IsVisible) {
                    this.menu.Hide();
                }
            }
            catch (Exception ex) {
                this.log.Exception(9999, "HideMenu", "", ex);
            }
        }


        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }


        private void languageChanged(object sender, SupportedLanguage l) {
            try {
                this.btnExit.Content = l.GetText(MsgCode.exit);
                //this.lbAuthor.Content = l.GetText(MsgCode.Author);
                //this.lbIcons.Content = l.GetText(MsgCode.Icons);
                //this.txtUserManual.Text = l.GetText(MsgCode.UserManual);
                //this.txtSupport.Text = l.GetText(MsgCode.Support);
            }
            catch (Exception ex) {
                this.log.Exception(9999, "TitleBarBorder_MouseDown", "", ex);
            }
        }


        /// <summary>Opens the browser because of the execution of a hyperlink</summary>
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e) {
            try {
                Task.Run(async () => {
                    try {
                        await Windows.System.Launcher.LaunchUriAsync(e.Uri);
                    }
                    catch (Exception ex) {
                        this.log.Exception(9999, "Hyperlink_RequestNavigate", "", ex);
                    }
                });
                e.Handled = true;
            }
            catch (Exception ex) {
                Log.Exception(9999, "", ex);
            }
        }



        #region Init Controls

        // This will be done later from saved configurations in dashboards

        //private void InitControls() {
        //    this.sliderBool.SetSendAction(this.sendAction);
        //    DashboardControlDataModel bDataModel = new DashboardControlDataModel() {
        //        Id = 10,
        //        IOName = "IO 1 LED",
        //        DataType = BinaryMsgDataType.typeBool,
        //        SendAtStep = 1,
        //        Minimum = 0,
        //        Maximum = 1,
        //    };
        //    this.sliderBool.Update(bDataModel);

        //    this.numericSlider.SetSendAction(this.sendAction);
        //    DashboardControlDataModel nDataModel = new DashboardControlDataModel() {
        //        Id = 12,
        //        IOName = "IO 3 PWM",
        //        DataType = BinaryMsgDataType.typeUInt8,
        //        SendAtStep = 1,
        //        Minimum = 0,
        //        Maximum = 255,
        //    };
        //    this.numericSlider.Update(nDataModel);


        //    DashboardControlDataModel out1 = new DashboardControlDataModel() {
        //        Id = 12,
        //        IOName = "output ID 12",
        //        DataType = BinaryMsgDataType.typeUInt8,
        //        Minimum = 0,
        //        Maximum = 255,
        //    };
        //    this.numericOutput.Update(out1);

        //    DashboardControlDataModel out2 = new DashboardControlDataModel() {
        //        Id = 20,
        //        IOName = "Temp ID:20",
        //        DataType = BinaryMsgDataType.typeFloat32,
        //        Precision = 2,
        //        Minimum = -10,
        //        Maximum = 100,
        //    };
        //    this.temperature.Update(out2);
        //}

        #endregion

        #region Actions to pass to the input sliders


        //private void sendAction(byte id, BinaryMsgDataType dataType, double value) {
        //    // TODO Can do some validation here of range
        //    switch (dataType) {
        //        case BinaryMsgDataType.typeBool:
        //            DI.W.BTSend(new BinaryMsgBool(id, (value != 0)).ToByteArray());
        //            break;
        //        case BinaryMsgDataType.typeInt8:
        //            DI.W.BTSend(new BinaryMsgInt8(id, (sbyte)value).ToByteArray());
        //            break;
        //        case BinaryMsgDataType.typeInt16:
        //            DI.W.BTSend(new BinaryMsgInt16(id, (Int16)value).ToByteArray());
        //            break;
        //        case BinaryMsgDataType.typeInt32:
        //            DI.W.BTSend(new BinaryMsgInt32(id, (Int32)value).ToByteArray());
        //            break;

        //        case BinaryMsgDataType.typeUInt8:
        //            this.log.Info("", () => string.Format("UINT8 value:{0}", value));
        //            DI.W.BTSend(new BinaryMsgUInt8(id, (byte)value).ToByteArray());
        //            break;
        //        case BinaryMsgDataType.typeUInt16:
        //            DI.W.BTSend(new BinaryMsgUInt16(id, (UInt16)value).ToByteArray());
        //            break;
        //        case BinaryMsgDataType.typeUInt32:
        //            DI.W.BTSend(new BinaryMsgUInt32(id, (UInt32)value).ToByteArray());
        //            break;

        //        case BinaryMsgDataType.typeFloat32:
        //            DI.W.BTSend(new BinaryMsgFloat32(id, (Single)value).ToByteArray());
        //            break;
        //        case BinaryMsgDataType.tyepUndefined:
        //        case BinaryMsgDataType.typeInvalid:
        //            break;
        //    }
        //}

        #endregion

    }
}
