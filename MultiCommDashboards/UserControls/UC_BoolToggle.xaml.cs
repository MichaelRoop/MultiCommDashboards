using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using System;
using System.Windows;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction logic for UC_BoolToggle.xaml</summary>
    public partial class UC_BoolToggle : UC_InputBase {

        public UC_BoolToggle() : base() {
            InitializeComponent();
            this.boolSlider.ValueChanged += this.controlsValueChangedHandler;
            this.btnDelete.Click += this.deleteClick;
            this.btnEdit.Click += this.editClick;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }

        public UC_BoolToggle(DashboardControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize there
        }


        public override void SetEditState(bool onOff) {
            this.gridEdit.Visibility = onOff ? Visibility.Visible : Visibility.Collapsed;
        }


        protected override void DoInit() {
            InitializeComponent();
            this.txtName.Text = this.IOName;
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
