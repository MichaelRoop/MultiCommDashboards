using CommunicationStack.Net.Enumerations;
using LogUtils.Net;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction logic for UC_BoolToggle.xaml</summary>
    public partial class UC_BoolToggle : UserControl {

        private ClassLog log = new ClassLog("UC_BoolToggle");
        private byte id = 0;
        private BinaryMsgDataType dataType = BinaryMsgDataType.typeBool;
        private Action<byte, BinaryMsgDataType, double> sendAction = null;
        private Func<bool, string> translateTrueFalseFunc = null;

        public void SetSendAction(Action<byte, BinaryMsgDataType, double> sendAction) {
            this.sendAction = sendAction;
        }

        public void SetTrueFalseTranslators(Func<bool, string> func) {
            this.translateTrueFalseFunc = func;
        }


        public void InitAsBool(byte id, string name) {
            this.Init(id, name, BinaryMsgDataType.typeBool);
            this.sliderNumeric.Collapse();
            this.boolSlider.Show();
        }


        public void InitAsNumeric(byte id, string name, BinaryMsgDataType dataType,  double step, double min, double max) {
            this.Init(id, name, dataType);
            this.sliderNumeric.TickFrequency = step;
            this.sliderNumeric.Minimum = min;
            this.sliderNumeric.Maximum = max;
        }


        public UC_BoolToggle() {
            this.translateTrueFalseFunc = this.DefaultTrueFalseTranlator;
            InitializeComponent();
            this.sliderNumeric.IsSnapToTickEnabled = true;
            this.lblValue.Content = "";
        }


        private void Init(byte id, string name, BinaryMsgDataType dataType) {
            this.id = id;
            this.lbIdTxt.Content = id.ToString();
            this.lbIdNameTxt.Content = name;
            this.dataType = dataType;
            this.lblValue.Content = (this.dataType == BinaryMsgDataType.typeBool)
                ? this.translateTrueFalseFunc(false)
                : this.sliderNumeric.Value.ToString();
        }


        private void booSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args) {
            this.OnSliderActionChanged(args.NewValue);
        }


        private void sliderNumeric_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args) {
            this.OnSliderActionChanged(args.NewValue);
        }


        private void OnSliderActionChanged(double value) {
            try {
                this.lblValue.Content = (this.dataType == BinaryMsgDataType.typeBool)
                    ? this.translateTrueFalseFunc(value != 0)
                    : value.ToString();
                this.sendAction?.Invoke(this.id, this.dataType, value);
            }
            catch (Exception ex) {
                this.log.Exception(9999, "OnSliderActionChanged", "", ex);
            }
        }


        private string DefaultTrueFalseTranlator(bool trueFalse) {
            return trueFalse.ToString();
        }

    }
}
