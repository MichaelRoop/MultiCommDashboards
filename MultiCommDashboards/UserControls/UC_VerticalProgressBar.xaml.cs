using MultiCommDashboardData.Storage;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_VerticalProgressBar.xaml</summary>
    public partial class UC_VerticalProgressBar : UC_OutputBase {

        public UC_VerticalProgressBar() {
            InitializeComponent();
        }


        public UC_VerticalProgressBar(DashboardControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize there
        }


        protected override void DoInit() {
            InitializeComponent();
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
