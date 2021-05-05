using BluetoothCommon.Net;
using CommunicationStack.Net.BinaryMsgs;
using MultiCommDashboards.DependencyInjection;
using System;
using System.Windows;
using WpfCustomControlLib.Core.Helpers;

namespace MultiCommDashboards.WindowObjs.BTWins {

    /// <summary>RunBT.xaml</summary>
    public partial class RunBT : Window {

        public RunBT() {
            InitializeComponent();
        }


        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            //this.ucDashboard.SetDemo();

            this.ucDashboard.ButtonExitEvent += this.buttonExitEvent;
            this.ucDashboard.ButtonConnectEvent += this.buttonConnectEvent;
            this.ucDashboard.ButtonDisconnectEvent += this.buttonDisconnectEvent;
            this.ucDashboard.MsgToDevice += this.msgToDevice;
            DI.W.BT_Connected += this.connectedHandler;
            DI.W.OutputData_BT += this.outputDataHandler;

            this.SizeToContent = SizeToContent.WidthAndHeight;
            // Center to Parent
        }

        private void msgToDevice(object sender, byte[] msg) {
            DI.W.BTSend(msg);
        }

        private void outputDataHandler(object sender, BinaryMsgMinData data) {
            this.ucDashboard.Update(data);
        }


        private void connectedHandler(object sender, bool ok) {
            this.ucDashboard.ConnectionAttemptResult(ok);
        }


        private void buttonConnectEvent(object sender, EventArgs e) {
            BTDeviceInfo device = SelectBT.ShowBox(this);
            if (device != null) {
                DI.W.BTConnectAsync(device);
            }
        }


        private void buttonDisconnectEvent(object sender, EventArgs e) {
            DI.W.BTDisconnect();
            this.ucDashboard.Disconnected();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.ucDashboard.ButtonExitEvent -= this.buttonExitEvent;
            this.ucDashboard.ButtonConnectEvent -= this.buttonConnectEvent;
            this.ucDashboard.ButtonDisconnectEvent -= this.buttonDisconnectEvent;
            DI.W.BT_Connected -= this.connectedHandler;
            DI.W.OutputData_BT -= this.outputDataHandler;
            this.ucDashboard.MsgToDevice -= this.msgToDevice;
        }


        private void buttonExitEvent(object sender, EventArgs e) {
            this.Close();
        }



    }
}
