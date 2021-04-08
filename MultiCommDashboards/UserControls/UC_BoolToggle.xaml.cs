using System;
using System.Windows;
using System.Windows.Controls;

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

        public event EventHandler<StateChange> OnStateChange;

        public byte Id { get; set; } = 10;


        public UC_BoolToggle() {
            InitializeComponent();
        }

        private void sliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args) {
            this.lbIdTxt.Content = args.NewValue.ToString();
            this.OnStateChange?.Invoke(this.Id, new StateChange(this.Id, args.NewValue));
        }

    }
}
