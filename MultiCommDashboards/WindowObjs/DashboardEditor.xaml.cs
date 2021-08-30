using MultiCommDashboardData.Storage;
using MultiCommDashboardData.StorageIndex;
using MultiCommDashboards.DependencyInjection;
using StorageFactory.Net.interfaces;
using System.Windows;
using WpfCustomControlLib.Core.Helpers;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.WindowObjs {

    /// <summary>DashboardEditor.xaml</summary>
    public partial class DashboardEditor : Window {


        private Window parent = null;
        // TODO group and size buttons

        public static void ShowBox(Window parent) {
            DashboardEditor win = new DashboardEditor(parent);
            win.ShowDialog();
        }


        public static void ShowBox(Window parent, IIndexItem<DashboardConfigIndexExtraInfo> index = null) {
            DashboardEditor win = new DashboardEditor(parent, index);
            win.ShowDialog();
        }


        private DashboardEditor(Window parent) {
            this.parent = parent;
            InitializeComponent();
        }


        private DashboardEditor(Window parent, IIndexItem<DashboardConfigIndexExtraInfo> index) {
            this.parent = parent;
            InitializeComponent();
            DI.W.RetrieveConfig(index, this.OnRetrieveSuccess, this.OnRetrieveFail);
        }


        private void RetrieveConfig() {

        }


        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.CenterToParent(this.parent);
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

        }


        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }


        private void btnPreview_Click(object sender, RoutedEventArgs e) {
            // Get data model from UC and pass to preview window
            DashboardPreview.ShowBox(this, this.ucDashboardEdit.GetConfig());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            // TODO - validate
            // Does not create the extra info

            var cfg = this.ucDashboardEdit.GetConfig();


            DI.W.CreateOrSaveConfiguration(this.ucDashboardEdit.GetConfig(), this.Close, App.ShowErrMsg);


            // TODO - doing create each time for now as if there was no index existing
            //DI.W.CreateConfiguration(this.ucDashboardEdit.GetConfig(), idx => { this.Close(); }, App.ShowErrMsg);

        }

        private void ucDashboardEdit_Loaded(object sender, RoutedEventArgs e) {

        }

        #region Delegates

        private void OnRetrieveSuccess(DashboardConfiguration cfg) {
            this.ucDashboardEdit.Init(cfg);
        }

        private void OnRetrieveFail(string err) {
            App.ShowErrMsg(err);
            //this.Close();
            // TODO cannot close since the upcomming Show will crash
        }

        #endregion

    }
}
