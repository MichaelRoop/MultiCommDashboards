using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_Dashboard.xaml</summary>
    public partial class UC_Dashboard : UserControl {

        private List<UC_OutputBase> outputs = new List<UC_OutputBase>();

        /// <summary>Event raised when a control value to be sent to the device</summary>
        public event EventHandler<byte[]> MsgToDevice;

        /// <summary>Raised when Connect button is pressed</summary>
        public event EventHandler ButtonConnectEvent;

        /// <summary>Raised when Disconnect button is pressed</summary>
        public event EventHandler ButtonDisconnectEvent;

        /// <summary>Raised when Exit button is pressed</summary>
        public event EventHandler ButtonExitEvent;


        public UC_Dashboard() {
            InitializeComponent();
        }


        public void SetAsPreview(DashboardConfiguration config) {
            this.btnConnect.Collapse();
            this.btnDisconnect.Collapse();
            this.spList.Collapse();
            this.Init(config);
        }


        private void Init(DashboardConfiguration config) {
            foreach (var dataModel in config.InputsBool) {
                this.InitItem(new UC_BoolToggle(dataModel), this.grdInputsBool);
            }

            foreach (var dataModel in config.InputsNumericHorizontal) {
                this.InitItem(new UC_HorizontalSlider(dataModel), this.grdInputsNumHorizontal);
            }

            // Outputs
            foreach (var dataModel in config.OutputsBool) {
                this.InitItem(new UC_BoolProgress(dataModel), this.grdOutputsBool);
            }

            foreach (var dataModel in config.OutputsNumericHorizontal) {
                this.InitItem(new UC_HorizontalProgressBar(dataModel), this.grdOutputsNumHorizontal);
            }
        }


        public void Update(BinaryMsgMinData data) {
            foreach (var output in this.outputs) {
                if (output.Process(data)) {
                    break;
                }
            }
        }



        private void InitItem(UC_InputBase input, Grid grid) {
            input.SetSendAction(this.sendAction);
            Grid.SetRow(input, input.Row);
            Grid.SetColumn(input, input.Column);
            grid.Children.Add(input);
        }


        private void InitItem(UC_OutputBase output, Grid grid) {
            // TODO - register to receive. subscribe
            this.outputs.Add(output);
            Grid.SetRow(output, output.Row);
            Grid.SetColumn(output, output.Column);
            grid.Children.Add(output);
        }


        /// <summary>This function is passed in to the controls so they can send their values</summary>
        /// <param name="id">The message ID</param>
        /// <param name="dataType">The value data type</param>
        /// <param name="value">The value to send</param>
        private void sendAction(byte id, BinaryMsgDataType dataType, double value) {
            // TODO Can do some validation here of range
            // TODO - move to an event that the window picks up
            switch (dataType) {
                case BinaryMsgDataType.typeBool:
                    this.MsgToDevice?.Invoke(this, new BinaryMsgBool(id, (value != 0)).ToByteArray());
                    //DI.W.BTSend(new BinaryMsgBool(id, (value != 0)).ToByteArray());
                    break;

                case BinaryMsgDataType.typeInt8:
                    this.MsgToDevice?.Invoke(this, new BinaryMsgInt8(id, (sbyte)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeInt16:
                    this.MsgToDevice?.Invoke(this, new BinaryMsgInt16(id, (Int16)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeInt32:
                    this.MsgToDevice?.Invoke(this, new BinaryMsgInt32(id, (Int32)value).ToByteArray());
                    break;

                case BinaryMsgDataType.typeUInt8:
                    this.MsgToDevice?.Invoke(this, new BinaryMsgUInt8(id, (byte)value).ToByteArray());
                    //DI.W.BTSend(new BinaryMsgUInt8(id, (byte)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeUInt16:
                    this.MsgToDevice?.Invoke(this, new BinaryMsgUInt16(id, (UInt16)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeUInt32:
                    this.MsgToDevice?.Invoke(this, new BinaryMsgUInt32(id, (UInt32)value).ToByteArray());
                    break;
                
                case BinaryMsgDataType.typeFloat32:
                    this.MsgToDevice?.Invoke(this, new BinaryMsgFloat32(id, (Single)value).ToByteArray());
                    break;
                case BinaryMsgDataType.tyepUndefined:
                case BinaryMsgDataType.typeInvalid:
                    // TODO - raise error
                    break;
            }
        }


        private void btnConnect_Click(object sender, RoutedEventArgs e) {
            this.ButtonConnectEvent?.Invoke(this, e);
        }


        private void btnDisconnect_Click(object sender, RoutedEventArgs e) {
            this.ButtonDisconnectEvent?.Invoke(this, e);
        }


        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.ButtonExitEvent?.Invoke(this, e);
        }
    }
}
