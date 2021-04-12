using BluetoothCommon.Net;
using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.Enumerations;
using LanguageFactory.Net.data;
using MultiCommDashboards.DashBuilders;
using MultiCommDashboards.DependencyInjection;
using MultiCommDashboards.UserControls;
using MultiCommDashboards.WindowObjs.BTWins;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiCommDashboards.WindowObjs {

    /// <summary>Logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window {

        InputBuilder<UC_BoolToggle> bool0 = new InputBuilder<UC_BoolToggle>();
        InputBuilder<UC_HorizontalSlider> bool1 = new InputBuilder<UC_HorizontalSlider>();

        public MainWindow() {
            InitializeComponent();
            DI.W.BT_Connected += W_BT_Connected;
            DI.W.MsgEventFloat32 += W_MsgEventFloat32;
            DI.W.SetLanguage(LangCode.Spanish, App.ShowErrMsg);
            this.InitControls();
            this.bool0.Init(this.grdBool, 0, 10);
            this.bool1.Init(this.grdBool, 1, 10);
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


        private void btnSend_Click(object sender, RoutedEventArgs e) {
            //BinaryMsgBool mb = new BinaryMsgBool(10, true);
            //DI.W.BTSend(mb.ToByteArray());

            DashboardEditor.ShowBox(this);

        }

        #region Init Controls

        // This will be done later from saved configurations in dashboards

        private void InitControls() {
            this.sliderBool.SetSendAction(this.sendAction);
            this.sliderBool.SetTrueFalseTranslators(this.translateTrueFalseFunc);
            this.sliderBool.Init(10, "IO 1 LED", BinaryMsgDataType.typeBool, 1, 0, 1);

            this.numericSlider.SetSendAction(this.sendAction);
            this.numericSlider.Init(12, "IO 3 PWM", BinaryMsgDataType.typeUInt8, 1, 0, 254);
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

        //List<UC_BoolToggle> boolToggles0 = new List<UC_BoolToggle>();
        //int boolNext = 1;
        //int id = 0;

        private void brdBool_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (sender is Border) {
                switch ((sender as Border).Name) {
                    case "brdBool_0":
                        this.bool0.Add();
                        break;
                    case "brdBool_1":
                        this.bool1.Add();
                        break;
                }
            }



            //return;
            

            //if (boolNext <= 10) {
            //    UC_BoolToggle bt = new UC_BoolToggle();
            //    id++;
            //    bt.InitAsBool((byte)id, string.Format("DigiIO_{0}", id));
            //    Grid.SetRow(bt, 0);
            //    Grid.SetColumn(bt, boolNext);
            //    this.grdBool.Children.Add(bt);
            //    bt.MouseLeftButtonUp += Bt_MouseLeftButtonUp;
            //    this.boolToggles0.Add(bt);
            //    boolNext++;
            //}
        }

        //private void Bt_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
        //    UC_BoolToggle bt = sender as UC_BoolToggle;
        //    bt.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
        //    this.boolToggles0.Remove(bt);
        //    this.grdBool.Children.Remove(bt);
        //    for (int i = 1; i < this.grdBool.Children.Count; i++) {
        //        UC_BoolToggle b = this.grdBool.Children[i] as UC_BoolToggle;
        //        Grid.SetColumn(b, i);
        //    }

        //    this.grdBool.InvalidateVisual();
        //    boolNext--;
        //    if (boolNext < 1) {
        //        boolNext = 1;
        //    }
        //}




    }
}
