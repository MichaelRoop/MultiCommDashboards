using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_BoolProgress.xaml</summary>
    public partial class UC_BoolProgress : UC_OutputBase {

        public UC_BoolProgress() {
            InitializeComponent();
            this.sbProgress.Value = 0;
        }


        public UC_BoolProgress(OutputControlDataModel data) : base(data) {
            // The base initializes variables and calls the DoInit. Initialize() there
        }


        public override void SetAsAddDummy() {
            this.gridAdd.Show();
        }


        protected override void DoInit() {
            this.InitializeComponent();
            this.lbIdTxt.Content = this.Id.ToString();
            this.lbIdNameTxt.Content = this.IOName;
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
