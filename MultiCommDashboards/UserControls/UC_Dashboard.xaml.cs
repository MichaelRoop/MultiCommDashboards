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

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_Dashboard.xaml</summary>
    public partial class UC_Dashboard : UserControl {

        public UC_Dashboard() {
            InitializeComponent();
        }


        public void Init(DashboardConfiguration config) {
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


        private void InitItem(UC_InputBase input, Grid grid) {
            input.SetSendAction(this.sendAction);
            Grid.SetRow(input, input.Row);
            Grid.SetColumn(input, input.Column);
            grid.Children.Add(input);
        }

        private void InitItem(UC_OutputBase input, Grid grid) {
            // TODO - register to receive. subscribe
            Grid.SetRow(input, input.Row);
            Grid.SetColumn(input, input.Column);
            grid.Children.Add(input);
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


    }
}
