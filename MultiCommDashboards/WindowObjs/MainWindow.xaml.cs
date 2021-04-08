using BluetoothCommon.Net;
using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.Enumerations;
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

            this.sliderBool.InitAsBool();
            this.numericSlider.InitAsNumeric(BinaryMsgDataType.typeUInt8, 1, 0, 254);
            this.sliderBool.OnStateChange += SliderBool_OnStateChange;
            this.numericSlider.OnStateChange += NumericSlider_OnStateChange;
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

        private void btnTest_Click(object sender, RoutedEventArgs e) {

            //MsgBoxSimple.ShowBox(this, "BLIPO");
            //MsgBoxYesNo.ShowBox(this, "Don't do it");
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e) {
            BTDeviceInfo device = SelectBT.ShowBox(this);
            if (device != null) {
                DI.W.BTConnectAsync(device);
            }
            else {
                App.ShowErrMsg("No device");
            }
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e) {
            DI.W.BTDisconnect();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e) {
            //this.sliderTest.Init(0, 0, 1023);

            BinaryMsgBool mb = new BinaryMsgBool(10, true);
            DI.W.BTSend(mb.ToByteArray());
        }


        private void NumericSlider_OnStateChange(object sender, UserControls.UC_BoolToggle.StateChange e) {
            
        }

        private void SliderBool_OnStateChange(object sender, UC_BoolToggle.StateChange state) {
            DI.W.BTSend(new BinaryMsgBool(state.Id, state.Value).ToByteArray());
        }



    }
}
