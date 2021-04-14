using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using System;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction logic for UC_BoolToggle.xaml</summary>
    public partial class UC_BoolToggle : UC_InputBase {

        private Func<bool, string> translateTrueFalseFunc = null;


        public UC_BoolToggle() : base() {
            InitializeComponent();
            this.translateTrueFalseFunc = this.DefaultTrueFalseTranlator;
            this.lblValue.Content = "";
            this.boolSlider.ValueChanged += this.controlsValueChangedHandler;
        }


        public UC_BoolToggle(InputControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize there
        }


        public override void SetAsAddDummy() {
            this.gridAdd.Show();
        }


        public override void SetTrueFalseTranslators(Func<bool, string> func) {
            this.translateTrueFalseFunc = func;
        }


        protected override void DoInit() {
            InitializeComponent();
            this.lbIdTxt.Content = this.Id.ToString();
            this.lbIdNameTxt.Content = this.IOName;
            // Force it bool every time
            this.DataType = BinaryMsgDataType.typeBool;
            this.Minimum = 0;
            this.Maximum = 1;
            this.translateTrueFalseFunc = this.DefaultTrueFalseTranlator;
            this.lblValue.Content = this.translateTrueFalseFunc(false);
            this.boolSlider.ValueChanged += this.controlsValueChangedHandler;
        }


        protected override void OnValueChanged(double newValue) {
            this.lblValue.Content = this.translateTrueFalseFunc(newValue != 0);
        }


        private string DefaultTrueFalseTranlator(bool trueFalse) {
            return trueFalse.ToString();
        }


        private void OnConstruction() {
        }

    }
}
