using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using System.Windows;

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_BoolProgress.xaml</summary>
    public partial class UC_BoolProgress : UC_OutputBase {

        public UC_BoolProgress() {
            InitializeComponent();
            this.sbProgress.Value = 0;
            this.btnDelete.Click += this.deleteClick;
            this.btnEdit.Click += this.editClick;
        }


        public UC_BoolProgress(DashboardControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize() there
        }


        public override void SetEditState(bool onOff) {
            this.gridEdit.Visibility = onOff ? Visibility.Visible : Visibility.Collapsed;
        }


        protected override void DoInit() {
            this.InitializeComponent();
            this.txtName.Text = this.IOName;
            // Force it bool every time
            this.DataType = BinaryMsgDataType.typeBool;
            this.Minimum = 0;
            this.Maximum = 1;
            this.sbProgress.Minimum = this.Minimum;
            this.sbProgress.Maximum = this.Maximum;
            this.DoDisplay(this.Minimum);
        }


        protected override void DoDisplay(double value) {
            this.sbProgress.Value = value;
        }

    }

}
