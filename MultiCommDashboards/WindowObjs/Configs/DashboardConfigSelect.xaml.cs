using MultiCommDashboardData.StorageIndex;
using MultiCommDashboards.DependencyInjection;
using StorageFactory.Net.interfaces;
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

namespace MultiCommDashboards.WindowObjs.Configs {

    /// <summary>Interaction logic for DashboardConfigSelect.xaml</summary>
    public partial class DashboardConfigSelect : Window {

        private Window parent = null;
        private List<IIndexItem<DashboardConfigIndexExtraInfo>> confgurations = new List<IIndexItem<DashboardConfigIndexExtraInfo>>();


        public static void ShowBox(Window parent) {
            DashboardConfigSelect win = new DashboardConfigSelect(parent);
            win.ShowDialog();
        }


        private DashboardConfigSelect(Window parent) {
            this.parent = parent;
            InitializeComponent();
            DI.W.GetConfigsIndex((configs) => {
                this.confgurations = configs;
                this.lbConfigs.ItemsSource = this.confgurations;
            }, App.ShowErrMsg);
        }


        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
