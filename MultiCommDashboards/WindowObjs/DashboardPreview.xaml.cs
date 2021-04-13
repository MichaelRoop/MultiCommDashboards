using MultiCommDashboardData.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfCustomControlLib.Core.Helpers;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.WindowObjs {
    /// <summary>
    /// Interaction logic for DashboardPreview.xaml
    /// </summary>
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
        }

        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.ucDashboard.Init(this.config);
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.CenterToParent(this.parent);
        }

        private void Window_Closing(object sender, CancelEventArgs e) {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
