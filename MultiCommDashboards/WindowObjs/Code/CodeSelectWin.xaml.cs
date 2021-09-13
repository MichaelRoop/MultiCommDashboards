using MultiCommDashboardData.DataModels;
using MultiCommDashboards.DependencyInjection;
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

namespace MultiCommDashboards.WindowObjs.Code {
    /// <summary>
    /// Interaction logic for CodeSelectWin.xaml
    /// </summary>
    public partial class CodeSelectWin : Window {

        Window parent = null;

        public static void ShowBox(Window parent) {
            CodeSelectWin win = new CodeSelectWin(parent);
            win.ShowDialog();
        }

        private CodeSelectWin(Window parent) {
            this.parent = parent;
            InitializeComponent();
        }


        /// <summary>Bind Mouse drag to Template style</summary>
        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DI.W.GetCodeList(this.OnLoadSuccess, App.ShowErrMsg);
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.CenterToParent(this.parent);
            this.listBoxCode.SelectionChanged += this.listBoxCode_SelectionChanged;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.listBoxCode.SelectionChanged -= this.listBoxCode_SelectionChanged;
        }


        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }


        private void listBoxCode_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            CodeSelectDisplayDataModel dm = this.listBoxCode.SelectedItem as CodeSelectDisplayDataModel;
            DI.W.HasCodeFile(dm, (tf) => CodeViewWin.ShowBox(this.parent, dm), App.ShowErrMsg);
        }


        private void OnLoadSuccess(List<CodeSelectDisplayDataModel> list) {
            this.listBoxCode.ItemsSource = list;
        }



    }
}
