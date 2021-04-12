using CommunicationStack.Net.Enumerations;
using System;

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction logic for UC_BoolToggle.xaml</summary>
    public partial class UC_BoolToggle : UC_InputBase {

        private Func<bool, string> translateTrueFalseFunc = null;


        public UC_BoolToggle() : base() {
            this.translateTrueFalseFunc = this.DefaultTrueFalseTranlator;
            InitializeComponent();
            this.lblValue.Content = "";
            this.boolSlider.ValueChanged += this.controlsValueChangedHandler;
        }


        public override void SetTrueFalseTranslators(Func<bool, string> func) {
            this.translateTrueFalseFunc = func;
        }


        protected override void DoInit() {
            this.lbIdTxt.Content = this.Id.ToString();
            this.lbIdNameTxt.Content = this.IOName;
            // Force it bool every time
            this.DataType = BinaryMsgDataType.typeBool;
            this.Minimum = 0;
            this.Maximum = 1;
            this.lblValue.Content = this.translateTrueFalseFunc(false);
        }


        protected override void OnValueChanged(double newValue) {
            this.lblValue.Content = this.translateTrueFalseFunc(newValue != 0);
        }


        private string DefaultTrueFalseTranlator(bool trueFalse) {
            return trueFalse.ToString();
        }

    }
}
