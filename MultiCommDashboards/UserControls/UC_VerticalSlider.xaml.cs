namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction logic for UC_VerticalSlider.xaml</summary>
    public partial class UC_VerticalSlider : UC_InputBase {

        public UC_VerticalSlider() :base() {
            InitializeComponent();
            this.lblValue.Content = "";
            this.sliderNumeric.ValueChanged += this.controlsValueChangedHandler;
        }


        protected override void DoInit() {
            this.lbIdTxt.Content = this.Id.ToString();
            this.lbIdNameTxt.Content = this.IOName;
            this.sliderNumeric.TickFrequency = this.SendAtStep;
            this.sliderNumeric.Minimum = this.Minimum;
            this.sliderNumeric.Maximum = this.Maximum;
            this.OnValueChanged(this.sliderNumeric.Value);
        }


        protected override void OnValueChanged(double newValue) {
            this.lblValue.Content = newValue.ToString();
        }

    }
}
