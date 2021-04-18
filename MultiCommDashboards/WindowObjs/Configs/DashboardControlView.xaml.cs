using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using System.Windows;
using System.Windows.Controls;
using WpfCustomControlLib.Core.Helpers;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.WindowObjs.Configs {

    /// <summary>Interaction logic for DashboardControlView.xaml</summary>
    public partial class DashboardControlView : Window {

        UserControl callingUserControl = null;


        public static void ShowBox(UserControl parent, DashboardControlDataModel dm) {
            DashboardControlView win = new DashboardControlView(parent, dm);
            win.ShowDialog();
        }


        public DashboardControlView(UserControl parent, DashboardControlDataModel dm) {
            this.callingUserControl = parent;
            InitializeComponent();
            this.SetFieldValue(this.txtId, dm.Id);
            this.SetFieldValue(this.txtName, dm.IOName);
            this.txtDataType.Text = dm.DataType.ToStr();
            this.SetFieldValue(this.txtMin, dm.Minimum);
            this.SetFieldValue(this.txtMax, dm.Maximum);
            this.SetFieldValue(this.txtStep, dm.SendAtStep);
            this.SetFieldValue(this.txtRow, dm.Row);
            this.SetFieldValue(this.txtColum, dm.Column);
            if (dm.DataType == BinaryMsgDataType.typeBool) {
                this.rowMin.Height = new GridLength(0);
                this.rowMax.Height = new GridLength(0);
                this.rowStep.Height = new GridLength(0);
            }
        }


        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.CenterToParent(this.callingUserControl);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }


        private void SetFieldValue(TextBlock txt, object obj) {
            txt.Text = obj.ToString();
        }

    }
}
