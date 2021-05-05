using BluetoothCommon.Net;
using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.Enumerations;
using LanguageFactory.Net.data;
using LogUtils.Net;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.DependencyInjection;
using MultiCommDashboards.WindowObjs.BTWins;
using System;
using System.Windows;

namespace MultiCommDashboards.WindowObjs {

    /// <summary>Logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window {

        ClassLog log = new ClassLog("MainWindow");

        public MainWindow() {
            InitializeComponent();
            DI.W.BT_Connected += W_BT_Connected;
            DI.W.OutputData_BT += W_OutputData_BT;
            DI.W.SetLanguage(LangCode.Spanish, App.ShowErrMsg);
            this.InitControls();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }


        private void W_OutputData_BT(object sender, BinaryMsgMinData e) {
            this.Dispatcher.Invoke(() => {
                if (this.numericOutput.IsMine(e.MsgId)) {
                    this.log.Error(8, "W_OutputData_BT (numeric)", e.MsgValue.ToString());
                    this.numericOutput.Process(e);
                }
                if (this.temperature.IsMine(e.MsgId)) {
                    this.log.Error(8, "W_OutputData_BT (temperature)", e.MsgValue.ToString());
                    this.temperature.Process(e);
                }
            });
        }


        private void W_BT_Connected(object sender, bool ok) {
            if (ok) {
                //App.ShowMsgTitle("BT", "Connected");
            }
            else {
                App.ShowErrMsg("Failed connection");
            }
        }


        private void btnConnect_Click(object sender, RoutedEventArgs e) {
            BTDeviceInfo device = SelectBT.ShowBox(this);
            if (device != null) {
                DI.W.BTConnectAsync(device);
            }
            else {
                App.ShowErrMsg("No device selected");
            }
        }


        private void btnDisconnect_Click(object sender, RoutedEventArgs e) {
            DI.W.BTDisconnect();
        }


        private void btnDashboard_Click(object sender, RoutedEventArgs e) {
            //DashboardEditor.ShowBox(this);


            RunBT win = new RunBT();
            
            win.ShowDialog();
        }

        #region Init Controls

        // This will be done later from saved configurations in dashboards

        private void InitControls() {
            this.sliderBool.SetSendAction(this.sendAction);
            DashboardControlDataModel bDataModel = new DashboardControlDataModel() {
                Id = 10,
                IOName = "IO 1 LED",
                DataType = BinaryMsgDataType.typeBool,
                SendAtStep = 1,
                Minimum = 0,
                Maximum = 1,
            };
            this.sliderBool.Update(bDataModel);

            this.numericSlider.SetSendAction(this.sendAction);
            DashboardControlDataModel nDataModel = new DashboardControlDataModel() {
                Id = 12,
                IOName = "IO 3 PWM",
                DataType = BinaryMsgDataType.typeUInt8,
                SendAtStep = 1,
                Minimum = 0,
                Maximum = 255,
            };
            this.numericSlider.Update(nDataModel);


            DashboardControlDataModel out1 = new DashboardControlDataModel() {
                Id = 12,
                IOName = "output ID 12",
                DataType = BinaryMsgDataType.typeUInt8,
                Minimum = 0,
                Maximum = 255,
            };
            this.numericOutput.Update(out1);

            DashboardControlDataModel out2 = new DashboardControlDataModel() {
                Id = 20,
                IOName = "Temp ID:20",
                DataType = BinaryMsgDataType.typeFloat32,
                Precision = 2,
                Minimum = -10,
                Maximum = 100,
            };
            this.temperature.Update(out2);
        }

        #endregion

        #region Actions to pass to the input sliders


        private void sendAction(byte id, BinaryMsgDataType dataType, double value) {
            // TODO Can do some validation here of range
            switch (dataType) {
                case BinaryMsgDataType.typeBool:
                    DI.W.BTSend(new BinaryMsgBool(id, (value != 0)).ToByteArray());
                    break;
                case BinaryMsgDataType.typeInt8:
                    DI.W.BTSend(new BinaryMsgInt8(id, (sbyte)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeInt16:
                    DI.W.BTSend(new BinaryMsgInt16(id, (Int16)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeInt32:
                    DI.W.BTSend(new BinaryMsgInt32(id, (Int32)value).ToByteArray());
                    break;

                case BinaryMsgDataType.typeUInt8:
                    this.log.Info("", () => string.Format("UINT8 value:{0}", value));
                    DI.W.BTSend(new BinaryMsgUInt8(id, (byte)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeUInt16:
                    DI.W.BTSend(new BinaryMsgUInt16(id, (UInt16)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeUInt32:
                    DI.W.BTSend(new BinaryMsgUInt32(id, (UInt32)value).ToByteArray());
                    break;

                case BinaryMsgDataType.typeFloat32:
                    DI.W.BTSend(new BinaryMsgFloat32(id, (Single)value).ToByteArray());
                    break;
                case BinaryMsgDataType.tyepUndefined:
                case BinaryMsgDataType.typeInvalid:
                    break;
            }
        }

        #endregion

    }
}
