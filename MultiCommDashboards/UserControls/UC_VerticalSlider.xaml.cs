﻿using MultiCommDashboardData.Storage;

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction logic for UC_VerticalSlider.xaml</summary>
    public partial class UC_VerticalSlider : UC_InputBase {

        public UC_VerticalSlider() :base() {
            InitializeComponent();
            this.OnConstruction();
        }


        public UC_VerticalSlider(InputControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize there
        }


        protected override void DoInit() {
            InitializeComponent();
            this.lbIdTxt.Content = this.Id.ToString();
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
