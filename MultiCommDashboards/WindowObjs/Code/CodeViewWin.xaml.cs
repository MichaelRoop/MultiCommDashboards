using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using MultiCommDashboardData.DataModels;
using MultiCommDashboards.DependencyInjection;
using System.Windows;
using WpfCustomControlLib.Core.Helpers;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.WindowObjs.Code {

    /// <summary>Interaction logic for CodeViewWin.xaml</summary>
    public partial class CodeViewWin : Window {

        private Window parent = null;
        private CodeSelectDisplayDataModel codeDataModel = null;
        private ButtonGroupSizeSyncManager buttonWidthManager = null;


        public static void ShowBox(Window parent, CodeSelectDisplayDataModel codeDataModel) {
            CodeViewWin win = new CodeViewWin(parent, codeDataModel);
            win.ShowDialog();
        }


        private CodeViewWin(Window parent, CodeSelectDisplayDataModel codeDataModel) {
            this.parent = parent;
            this.codeDataModel = codeDataModel;
            InitializeComponent();
            this.Title = this.codeDataModel.Name;
            this.buttonWidthManager = new ButtonGroupSizeSyncManager(this.btnCopy, this.btnExit);
            this.buttonWidthManager.PrepForChange();
        }


        /// <summary>Bind Mouse drag to Template style</summary>
        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DI.W.RetrieveCodeFile(this.codeDataModel, this.OnSampleLoad, App.ShowErrMsg);
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.CenterToParent(this.parent);
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.buttonWidthManager.Teardown();
        }


        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }


        private void btnCopy_Click(object sender, RoutedEventArgs e) {
            this.codeBox.SelectAll();
            this.codeBox.Copy();
            this.codeBox.Select(0, 0);
        }


        private void OnSampleLoad(string sample) {
            this.Dispatcher.Invoke(() => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    this.codeBox.Text = sample;
                });
                if (report.Code != 0) {
                    OnError("", report.Msg);
                }
            });
        }


        private void OnError(string title, string msg) {
            App.ShowMsgTitle(title, msg);
            this.Close();
        }

    }

}
