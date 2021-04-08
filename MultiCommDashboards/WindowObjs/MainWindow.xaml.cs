using BluetoothCommon.Net;
using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.Enumerations;
using LanguageFactory.Net.data;
using MultiCommDashboards.DependencyInjection;
using MultiCommDashboards.UserControls;
using MultiCommDashboards.WindowObjs.BTWins;
using System.Windows;

namespace MultiCommDashboards.WindowObjs {

    /// <summary>Logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            DI.W.BT_Connected += W_BT_Connected;
            DI.W.MsgEventFloat32 += W_MsgEventFloat32;
            DI.W.SetLanguage(LangCode.Spanish, App.ShowErrMsg);
            this.InitControls();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }


        private void W_MsgEventFloat32(object sender, BinaryMsgFloat32 e) {
            this.Dispatcher.Invoke(() => {
                this.txtInput.Text = e.Value.ToString();
            });
        }


        private void W_BT_Connected(object sender, bool ok) {
            if (ok) {
                App.ShowMsgTitle("BT", "Connected");
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


        bool selectMode = false;
        private void btnSend_Click(object sender, RoutedEventArgs e) {
            //BinaryMsgBool mb = new BinaryMsgBool(10, true);
            //DI.W.BTSend(mb.ToByteArray());
            this.selectMode = !this.selectMode;
            this.vSlider1.SetSelectMode(this.selectMode);

        }

        #region Init Controls

        // This will be done later from saved configurations in dashboards

        private void InitControls() {
            this.sliderBool.SetSendAction(this.sendAction);
            this.sliderBool.SetTrueFalseTranslators(this.translateTrueFalseFunc);
            this.sliderBool.InitAsBool(10, "IO 1 LED");

            this.numericSlider.SetSendAction(this.sendAction);
            this.numericSlider.SetTrueFalseTranslators(this.translateTrueFalseFunc);
            this.numericSlider.InitAsNumeric(12, "IO 3 PWM", BinaryMsgDataType.typeUInt8, 1, 0, 254);
        }

        #endregion

        #region Actions to pass to the sliders

        private string translateTrueFalseFunc(bool trueFalse) {
            return DI.W.GetText(trueFalse ? MsgCode.True : MsgCode.False);
        }


        private void sendAction(byte id, BinaryMsgDataType dataType, double value) {
            // TODO Can do some validation here of range
            switch (dataType) {
                case BinaryMsgDataType.typeBool:
                    DI.W.BTSend(new BinaryMsgBool(id, (value != 0)).ToByteArray());
                    break;
                case BinaryMsgDataType.typeInt8:
                    break;
                case BinaryMsgDataType.typeUInt8:
                    DI.W.BTSend(new BinaryMsgUInt8(id, (byte)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeInt16:
                    break;
                case BinaryMsgDataType.typeUInt16:
                    break;
                case BinaryMsgDataType.typeInt32:
                    break;
                case BinaryMsgDataType.typeUInt32:
                    break;
                case BinaryMsgDataType.typeFloat32:
                    break;
                case BinaryMsgDataType.tyepUndefined:
                case BinaryMsgDataType.typeInvalid:
                    break;
            }
        }



        #endregion

    }
}
