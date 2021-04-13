using System;
using System.Collections.Generic;
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
    /// Interaction logic for DashboardEditor.xaml
    /// </summary>
    public partial class DashboardEditor : Window {


        private Window parent = null;

        public static void ShowBox(Window parent) {
            DashboardEditor win = new DashboardEditor(parent);
            win.ShowDialog();
        }


        public DashboardEditor(Window parent) {
            this.parent = parent;
            InitializeComponent();
        }

        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            //this.CenterToParent(this.parent);
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
    }
}
