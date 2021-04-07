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

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args) {
            this.slider.Foreground = (args.NewValue == 0)
                ? new SolidColorBrush(Colors.Red) 
                : new SolidColorBrush(Colors.YellowGreen);
            this.OnStateChange?.Invoke( this.Id, new StateChange(this.Id, args.NewValue));

        }
    }
}
