using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
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

    /// <summary>DashboardControlEdit.xaml</summary>
    public partial class DashboardControlEdit : Window {

        private DashboardControlDataModel copy = new DashboardControlDataModel();
        private UserControl callingUserControl = null;
        //https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private static readonly Regex REGEX_UINT = new Regex("[^0-9]+");
        private static readonly Regex REGEX_INT = new Regex("[^0-9-]+");
        private static readonly Regex REGEX_FLOAT = new Regex("[^0-9.-]+");
        private Regex currentRegex = REGEX_UINT;

        public DashboardControlDataModel DataModel { get; set; } = new DashboardControlDataModel();


        public static DashboardControlDataModel ShowBox(UserControl parent, DashboardControlDataModel dm) {
            DashboardControlEdit win = new DashboardControlEdit(parent, dm);
            win.ShowDialog();
            return win.DataModel;
        }


        public DashboardControlEdit(UserControl parent, DashboardControlDataModel dm) {
            this.callingUserControl = parent;
            this.copy = dm;
            this.DataModel = dm;
            InitializeComponent();

            this.SetFieldValue(this.txtId, dm.Id);
            this.SetFieldValue(this.txtName, dm.IOName);
            // TODO - set as drop box
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

        // TODO - change regex on the data type drop down change.
        // Affects min, max, step

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


        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();        
        }


        private void btnSave_Click(object sender, RoutedEventArgs e) {
            // TODO validate and set the property

            byte b;
            if (Byte.TryParse(this.txtId.Text, out b)) {
                this.DataModel.Id = b;
            }
            else {
                App.ShowErrMsg("Range 0-255");
            }





            this.Close();
        }


        private void SetFieldValue(TextBox txt, object obj) {
            txt.Text = obj.ToString();
        }

        private void txtId_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            var textBox = sender as TextBox;
            // Use SelectionStart property to find the caret position.
            // Insert the previewed text into the existing text in the textbox.
            //if (String.IsNullOrWhiteSpace(e.Text)) {
            //    App.ShowErrMsg("Space");
            //    e.Handled = true;
            //    return;
            //}


            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

            byte b;
            e.Handled = !Byte.TryParse(fullText, out b);
            if (e.Handled) {
                App.ShowErrMsg("Range 0-255");
            }



            //double val;
            //// If parsing is successful, set Handled to false
            //e.Handled = !double.TryParse(fullText,
            //                             NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
            //                             CultureInfo.InvariantCulture,
            //                             out val);
        }

        private void keyDownFilter(object sender, KeyEventArgs e) {
            // Filter out any unwanted that do not show up in the preview Text input
            if (e.Key == Key.Space) {
                e.Handled = true;
                return;
            }
        }

    }
}
