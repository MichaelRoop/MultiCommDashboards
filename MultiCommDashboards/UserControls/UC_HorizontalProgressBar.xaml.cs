using MultiCommDashboardData.Storage;
using WpfHelperClasses.Core;

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


        public override void SetAsAddDummy() {
            this.gridAdd.Show();
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
