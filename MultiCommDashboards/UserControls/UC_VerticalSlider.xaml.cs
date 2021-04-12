using CommunicationStack.Net.Enumerations;
using LogUtils.Net;
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
using WpfCustomControlLib.Core.UtilWindows;

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction logic for UC_VerticalSlider.xaml</summary>
    public partial class UC_VerticalSlider : UserControl {

        private ClassLog log = new ClassLog("UC_VerticalSlider");
        private byte id = 0;
        private BinaryMsgDataType dataType = BinaryMsgDataType.typeUInt8;
        private Action<byte, BinaryMsgDataType, double> sendAction = null;

        public UC_VerticalSlider() {
            InitializeComponent();
        }


        public void SetSendAction(Action<byte, BinaryMsgDataType, double> sendAction) {
            this.sendAction = sendAction;
        }


        public void Init(byte id, string name, BinaryMsgDataType dataType, double step, double min, double max) {
            this.id = id;
            this.lbIdTxt.Content = id.ToString();
            this.lbIdNameTxt.Content = name;
            this.dataType = dataType;
            this.lblValue.Content = this.sliderNumeric.Value.ToString();
            this.sliderNumeric.TickFrequency = step;
            this.sliderNumeric.Minimum = min;
            this.sliderNumeric.Maximum = max;
        }


        private void vSliderNumeric_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args) {
            try {
                this.lblValue.Content = args.NewValue.ToString();
                this.sendAction?.Invoke(this.id, this.dataType, args.NewValue);
            }
            catch (Exception ex) {
                this.log.Exception(9999, "OnSliderActionChanged", "", ex);
            }
        }

    }
}
