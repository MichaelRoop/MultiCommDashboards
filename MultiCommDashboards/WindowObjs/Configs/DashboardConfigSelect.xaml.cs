using MultiCommDashboardData.StorageIndex;
using MultiCommDashboards.DependencyInjection;
using StorageFactory.Net.interfaces;
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
            this.btnEdit.Hide();
            this.btnDelete.Hide();
            this.LoadIndexes();
        }


        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;

        }

        private void Window_Closing(object sender, CancelEventArgs e) {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            DashboardEditor.ShowBox(this.parent);
            this.LoadIndexes();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e) {
            DashboardEditor.ShowBox(this.parent, this.lbConfigs.SelectedItem as IIndexItem<DashboardConfigIndexExtraInfo>);
            this.LoadIndexes();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void lbConfigs_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            this.btnEdit.Show();
            this.btnDelete.Show();
        }


        private void LoadIndexes() {
            this.lbConfigs.SelectionChanged -= this.lbConfigs_SelectionChanged;
            this.lbConfigs.ItemsSource = null;
            DI.W.GetConfigsIndex((configs) => {
                this.confgurations = configs;
                this.lbConfigs.ItemsSource = this.confgurations;
            }, App.ShowErrMsg);
            this.lbConfigs.SelectionChanged += this.lbConfigs_SelectionChanged;
        }


    }
}
