﻿using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using System;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction logic for UC_BoolToggle.xaml</summary>
    public partial class UC_BoolToggle : UC_InputBase {

        public UC_BoolToggle() : base() {
            InitializeComponent();
            this.boolSlider.ValueChanged += this.controlsValueChangedHandler;
        }


        public UC_BoolToggle(DashboardControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize there
        }


        public override void SetSliderEnabled(bool tf) {
            this.boolSlider.IsEnabled = tf;
        }


        protected override void DoInit() {
            InitializeComponent();
            this.lbIdNameTxt.Content = this.IOName;
            // Force it bool every time
            this.DataType = BinaryMsgDataType.typeBool;
            this.Minimum = 0;
            this.Maximum = 1;
            this.boolSlider.ValueChanged += this.controlsValueChangedHandler;
        }


        protected override void OnValueChanged(double newValue) {
        }
    }
}
