using CommunicationStack.Net.DataModels;
using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.DependencyInjection;
using MultiCommDashboards.UserControls;
using MultiCommDashboardWrapper.DataModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfCustomControlLib.Core.Helpers;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.WindowObjs.Configs {

    /// <summary>DashboardControlEdit.xaml</summary>
    public partial class DashboardControlEdit : Window {

        private UserControl callingUserControl = null;
        ////https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        //private static readonly Regex REGEX_UINT = new Regex("[^0-9]+");
        //private static readonly Regex REGEX_INT = new Regex("[^0-9-]+");
        //private static readonly Regex REGEX_FLOAT = new Regex("[^0-9.-]+");
        //private Regex currentRegex = REGEX_UINT;

        private List<BinaryMsgDataTypeDisplay> typeList = BinaryMsgDataTypeDisplay.TypeList;
        private BinaryMsgDataType currentDataType = BinaryMsgDataType.tyepUndefined;

        public DashboardControlDataModel DataModel { get; set; } = new DashboardControlDataModel();


        public static DashboardControlDataModel ShowBox(UserControl parent, DashboardControlDataModel dm) {
            DashboardControlEdit win = new DashboardControlEdit(parent, dm);
            win.ShowDialog();
            return win.DataModel;
        }


        public DashboardControlEdit(UserControl parent, DashboardControlDataModel dm) {
            this.callingUserControl = parent;
            this.DataModel = dm;
            InitializeComponent();

            this.SetFieldValue(this.txtId, this.DataModel.Id, BinaryMsgDataType.typeUInt8);
            this.txtName.Text = this.DataModel.IOName;
            this.cbDataType.ItemsSource = this.typeList;
            this.cbDataType.SelectedItem = this.typeList.Find(x => x.DataType == this.DataModel.DataType);
            this.currentDataType = this.DataModel.DataType;

            this.SetFieldValue(this.txtMin, this.DataModel.Minimum, this.DataModel.DataType);
            this.SetFieldValue(this.txtMax, this.DataModel.Maximum, this.DataModel.DataType);
            this.SetFieldValue(this.txtStep, this.DataModel.SendAtStep, this.DataModel.DataType);
            this.lblRow.Content = this.DataModel.Row;
            this.lblColum.Content = this.DataModel.Column;

            if (this.DataModel.DataType == BinaryMsgDataType.typeBool) {
                this.rowMin.Height = new GridLength(0);
                this.rowMax.Height = new GridLength(0);
                this.rowStep.Height = new GridLength(0);
                this.cbDataType.IsEnabled = false;
            }
            else {
                this.cbDataType.SelectionChanged += this.dataTypeChanged;
            }
        }


        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.CenterToParent(this.callingUserControl);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (this.DataModel.DataType != BinaryMsgDataType.typeBool) {
                this.cbDataType.SelectionChanged -= this.dataTypeChanged;
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();        
        }


        private void btnSave_Click(object sender, RoutedEventArgs e) {
            DI.W.ValidateEditValues(new RawConfigValues() {
                Id = this.txtId.Text,
                Name = this.txtName.Text,
                DataType = this.currentDataType,
                Min = this.txtMin.Text,
                Max = this.txtMax.Text,
                Step = this.txtStep.Text,
            }, 
            (validated) => {
                this.DataModel.Id = validated.Id;
                this.DataModel.IOName = validated.IOName;
                this.DataModel.DataType = this.currentDataType;
                this.DataModel.Minimum = validated.Minimum;
                this.DataModel.Maximum = validated.Maximum;
                this.DataModel.SendAtStep = validated.SendAtStep;
                this.Close();
            }, App.ShowErrMsg);
        }


        private void SetFieldValue(TextBox txt, object obj) {
            txt.Text = obj.ToString();
        }


        private void SetFieldValue(UC_NumericEditBox txt, object obj, BinaryMsgDataType dataType) {
            txt.Text = obj.ToString();
            txt.SetDataType(dataType);
        }


        private void dataTypeChanged(object sender, SelectionChangedEventArgs e) {
            DI.W.GetRange(
                this.cbDataType.SelectedItem as BinaryMsgDataTypeDisplay, 
                range => {
                    this.txtStep.Text = "1";
                    this.txtMin.Text = range.Min;
                    this.txtMax.Text = range.Max;
                    this.currentDataType = (this.cbDataType.SelectedItem as BinaryMsgDataTypeDisplay).DataType;
                }, App.ShowErrMsg);
        }


    }
}
