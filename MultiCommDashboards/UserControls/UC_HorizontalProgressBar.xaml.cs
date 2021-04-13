using MultiCommDashboardData.Storage;

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_HorizontalProgressBar.xaml</summary>
    public partial class UC_HorizontalProgressBar : UC_OutputBase {

        public UC_HorizontalProgressBar() {
            InitializeComponent();
            this.DoDisplay(0);
        }


        public UC_HorizontalProgressBar(OutputControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize there
        }


        protected override void DoInit() {
            InitializeComponent();
            this.lbIdTxt.Content = this.Id.ToString();
            this.lbIdNameTxt.Content = this.IOName;
            this.sbProgress.Minimum = this.Minimum;
            this.sbProgress.Maximum = this.Maximum;
            this.DoDisplay(0);
        }


        protected override void DoDisplay(double value) {
            this.sbProgress.Value = value;
        }

    }
}
