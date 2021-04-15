﻿using MultiCommDashboardData.Storage;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_HorizontalSlider.xaml</summary>
    public partial class UC_HorizontalSlider : UC_InputBase {

        public UC_HorizontalSlider() : base() {
            InitializeComponent();
            this.OnConstruction();
        }


        public UC_HorizontalSlider(InputControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize there
        }


        public override void SetAsAddDummy() {
            this.gridAdd.Show();
        }


        public override void SetSliderEnabled(bool tf) {
            this.sliderNumeric.IsEnabled = tf;
        }


        protected override void DoInit() {
            InitializeComponent();
            this.lbIdNameTxt.Content = this.IOName;
            this.sliderNumeric.TickFrequency = this.SendAtStep;
            this.sliderNumeric.Minimum = this.Minimum;
            this.sliderNumeric.Maximum = this.Maximum;
            this.OnValueChanged(this.sliderNumeric.Value);
            this.OnConstruction();
        }


        protected override void OnValueChanged(double newValue) {
            this.lblValue.Content = newValue.ToString();
        }


        private void OnConstruction() {
            this.lblValue.Content = "";
            this.sliderNumeric.ValueChanged += this.controlsValueChangedHandler;
        }

    }
}
