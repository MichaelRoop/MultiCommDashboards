using MultiCommDashboardData.Storage;
using System;
using System.Windows;

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_HorizontalProgressBar.xaml</summary>
    public partial class UC_HorizontalProgressBar : UC_OutputBase {

        public UC_HorizontalProgressBar() {
            InitializeComponent();
            this.DoDisplay(0);
            this.btnDelete.Click += this.deleteClick;
            this.btnEdit.Click += this.editClick;
        }


        public UC_HorizontalProgressBar(DashboardControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize there
        }


        public override void SetEditState(bool onOff) {
            this.gridEdit.Visibility = onOff ? Visibility.Visible : Visibility.Collapsed;
        }


        protected override void DoInit() {
            InitializeComponent();
            this.txtName.Text = this.IOName;
            this.sbProgress.Minimum = this.Minimum;
            this.sbProgress.Maximum = this.Maximum;
            this.DoDisplay(0);
        }


        protected override void DoDisplay(double value) {
            this.sbProgress.Value = value;
            if (this.Precision > 0) {
                this.txtProgress.Text = Math.Round(this.sbProgress.Value, this.Precision).ToString();
            }
        }

    }
}
