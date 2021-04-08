using CommunicationStack.Net.Enumerations;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {



    /// <summary>
    /// Interaction logic for UC_BoolToggle.xaml
    /// </summary>
    public partial class UC_BoolToggle : UserControl {

        public class StateChange {
            public byte Id { get; set; } = 0;
            public bool Value { get; set; } = false;
            public StateChange(byte id, double state) {
                this.Id = id;
                this.Value = state == 0 ? false : true;
            }
        }


        // Have data type so we know how to handle the state changed event. Always comes back as double

        public event EventHandler<StateChange> OnStateChange;

        public byte Id { get; set; } = 10;

        BinaryMsgDataType dataType = BinaryMsgDataType.typeBool;


        public void InitAsBool() {
            this.dataType = BinaryMsgDataType.typeBool;
            this.sliderNumeric.Collapse();
            this.boolSlider.Show();
        }


        public void InitAsNumeric(BinaryMsgDataType dataType,  double step, double min, double max) {
            this.dataType = dataType;
            this.sliderNumeric.TickFrequency = step;
            this.sliderNumeric.Minimum = min;
            this.sliderNumeric.Maximum = max;
        }

        public UC_BoolToggle() {
            InitializeComponent();
            this.sliderNumeric.IsSnapToTickEnabled = true;
        }

        private void booSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args) {
            this.lbIdTxt.Content = args.NewValue.ToString();
            this.lblValue.Content = ((uint)args.NewValue).ToString();
            this.OnStateChange?.Invoke(this.Id, new StateChange(this.Id, args.NewValue));
        }

        private void sliderNumeric_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args) {

            this.lblValue.Content = args.NewValue.ToString();

        }
    }
}
