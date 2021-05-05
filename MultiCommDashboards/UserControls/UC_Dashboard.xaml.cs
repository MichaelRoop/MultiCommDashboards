using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.Enumerations;
using LogUtils.Net;
using MultiCommDashboardData.Storage;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_Dashboard.xaml</summary>
    public partial class UC_Dashboard : UserControl {

        #region Data

        private List<UC_OutputBase> outputs = new List<UC_OutputBase>();
        private ClassLog log = new ClassLog("UC_Dashboard");

        #endregion

        #region Events

        /// <summary>Event raised when a control value to be sent to the device</summary>
        public event EventHandler<byte[]> MsgToDevice;

        /// <summary>Raised when Connect button is pressed</summary>
        public event EventHandler ButtonConnectEvent;

        /// <summary>Raised when Disconnect button is pressed</summary>
        public event EventHandler ButtonDisconnectEvent;

        /// <summary>Raised when Exit button is pressed</summary>
        public event EventHandler ButtonExitEvent;

        #endregion

        #region Public

        public UC_Dashboard() {
            InitializeComponent();
            this.SetDemo();
        }


        /// <summary>When the dashboard is only used for preview on build</summary>
        /// <param name="config">The current state of the configuration</param>
        public void SetAsPreview(DashboardConfiguration config) {
            this.btnConnect.Collapse();
            this.btnDisconnect.Collapse();
            this.spList.Collapse();
            this.Init(config);
        }


        public void SetDemo() {
            DashboardConfiguration cfg = new DashboardConfiguration();
            cfg.InputsNumericHorizontal.Add(this.dm(12, "Tst PWM 3", BinaryMsgDataType.typeUInt8, 5, 0, 255,1,1));
            cfg.OutputsNumericHorizontal.Add(this.dm(12, "Feedback 12", BinaryMsgDataType.typeUInt8, 1, 0, 255,1,1));
            cfg.OutputsNumericHorizontal.Add(this.dm(20, "Temperature", BinaryMsgDataType.typeFloat32, 1, -10, 100,1,2, 2));
            this.Init(cfg);
        }


        private DashboardControlDataModel dm(
            byte id, string name, BinaryMsgDataType dataType, double step, double min, double max, 
            int row, int column,
            int precision = 0) {
            return new DashboardControlDataModel() {
                Id = id,
                IOName = name,
                DataType = dataType,
                SendAtStep = step,
                Minimum = min,
                Maximum = max,
                Precision = precision,
                Row = row,
                Column = column,
            };
        }


        /// <summary>Called when parent receives connected event from the comm</summary>
        public void ConnectionAttemptResult(bool isConnected) {
            this.Dispatcher.Invoke(() => {
                try {
                    this.gridWait.Collapse();
                    if (isConnected) {
                        this.btnConnect.Collapse();
                        this.btnDisconnect.Show();
                    }
                }
                catch(Exception ex) {
                    this.log.Exception(9999, "ConnectionAttemptResult", "", ex);
                }
            });
        }


        /// <summary>Called when parent receives a disconnected event from the comm</summary>
        public void Disconnected() {
            this.gridWait.Collapse();
            this.btnDisconnect.Collapse();
            this.btnConnect.Show();
        }


        /// <summary>Update appropriate control with data from comm</summary>
        /// <param name="data">The update data</param>
        public void Update(BinaryMsgMinData data) {
            this.Dispatcher.Invoke(() => {
                try {
                    foreach (var output in this.outputs) {
                        if (output.Process(data)) {
                            break;
                        }
                    }
                    // TODO - add error processing if not found
                }
                catch (Exception ex) {
                    this.log.Exception(9999, "ConnectionAttemptResult", "", ex);
                }
            });
        }

        #endregion

        #region Button events

        private void btnConnect_Click(object sender, RoutedEventArgs e) {
            this.gridWait.Show();
            this.ButtonConnectEvent?.Invoke(this, e);
        }


        private void btnDisconnect_Click(object sender, RoutedEventArgs e) {
            this.btnDisconnect.Collapse();
            this.btnConnect.Show();
            this.ButtonDisconnectEvent?.Invoke(this, e);
        }


        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.ButtonExitEvent?.Invoke(this, e);
        }

        #endregion


        private void Init(DashboardConfiguration config) {
            foreach (var dataModel in config.InputsBool) {
                this.InitInput(new UC_BoolToggle(dataModel), this.grdInputsBool);
            }

            foreach (var dataModel in config.InputsNumericHorizontal) {
                this.InitInput(new UC_HorizontalSlider(dataModel), this.grdInputsNumHorizontal);
            }

            // Outputs
            foreach (var dataModel in config.OutputsBool) {
                this.InitOutput(new UC_BoolProgress(dataModel), this.grdOutputsBool);
            }

            foreach (var dataModel in config.OutputsNumericHorizontal) {
                this.InitOutput(new UC_HorizontalProgressBar(dataModel), this.grdOutputsNumHorizontal);
            }
        }


        private void InitInput(UC_InputBase input, Grid grid) {
            input.SetSendAction(this.sendAction);
            Grid.SetRow(input, input.Row);
            Grid.SetColumn(input, input.Column);
            grid.Children.Add(input);
        }


        private void InitOutput(UC_OutputBase output, Grid grid) {
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
            // TODO Can do some validation here of range? should already be set in the config
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
    }
}
