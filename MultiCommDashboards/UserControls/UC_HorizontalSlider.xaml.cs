namespace MultiCommDashboards.UserControls {

    /// <summary>UC_HorizontalSlider.xaml</summary>
    public partial class UC_HorizontalSlider : UC_InputBase {

        public UC_HorizontalSlider() : base() {
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
