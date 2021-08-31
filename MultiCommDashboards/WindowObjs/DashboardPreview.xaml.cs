using MultiCommDashboardData.Storage;
using System;
using System.ComponentModel;
using System.Windows;
using WpfCustomControlLib.Core.Helpers;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.WindowObjs {

    /// <summary>DashboardPreview.xaml</summary>
    public partial class DashboardPreview : Window {

        private DashboardConfiguration config = new DashboardConfiguration();
        private Window parent = null;

        public static void ShowBox(Window parent, DashboardConfiguration config) {
            DashboardPreview win = new DashboardPreview(parent, config);
            win.ShowDialog();
        }


        public DashboardPreview(Window parent, DashboardConfiguration config) {
            this.parent = parent;
            this.config = config;
            InitializeComponent();
            this.Title = this.config.Display;
        }

        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.ucDashboard.SetAsPreview(this.config);
            this.ucDashboard.ButtonExitEvent += this.dashboardButtonExitEvent;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.CenterToParent(this.parent);
        }


        private void dashboardButtonExitEvent(object sender, EventArgs e) {
            this.Close();
        }


        private void Window_Closing(object sender, CancelEventArgs e) {
            this.ucDashboard.ButtonExitEvent -= this.dashboardButtonExitEvent;
        }

    }
}
