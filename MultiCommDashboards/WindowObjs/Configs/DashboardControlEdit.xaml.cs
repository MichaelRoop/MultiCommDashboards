using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.UserControls;
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
using VariousUtils.Net;
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

            this.SetFieldValue(this.txtId, dm.Id, BinaryMsgDataType.typeUInt8);
            this.txtName.Text = dm.IOName;
            // TODO - set as drop box. If bool freeze drop box
            this.txtDataType.Text = dm.DataType.ToStr();

            this.SetFieldValue(this.txtMin, dm.Minimum, dm.DataType);
            this.SetFieldValue(this.txtMax, dm.Maximum, dm.DataType);
            this.SetFieldValue(this.txtStep, dm.SendAtStep, dm.DataType);
            this.lblRow.Content = dm.Row;
            this.lblColum.Content = dm.Column;

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

            // less to check for bool?


            //byte b;
            //if (Byte.TryParse(this.txtId.Text, out b)) {
            //    this.DataModel.Id = b;
            //}
            //else {
            //    App.ShowErrMsg("Range 0-255");
            //}





            this.Close();
        }


        private void SetFieldValue(TextBox txt, object obj) {
            txt.Text = obj.ToString();
        }


        private void SetFieldValue(UC_NumericEditBox txt, object obj, BinaryMsgDataType dataType) {
            txt.Text = obj.ToString();
            txt.SetDataType(dataType);
        }

    }
}
