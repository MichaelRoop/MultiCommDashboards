using CommunicationStack.Net.Enumerations;
using MultiCommDashboards.DependencyInjection;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiCommDashboards.UserControls {

    /// <summary>UC_NumericEditBox.xaml</summary>
    public partial class UC_NumericEditBox : UserControl {

        private BinaryMsgDataType dataType = BinaryMsgDataType.typeBool;

        public string Text { 
            get { return this.txtValue.Text; } 
            set { this.txtValue.Text = value; } }


        public UC_NumericEditBox() {
            InitializeComponent();
        }


        public void SetDataType(BinaryMsgDataType dataType) {
            this.dataType = dataType;
        }


        private void txtValue_PreviewKeyDown(object sender, KeyEventArgs e) {
            // Filter out any unwanted that do not show up in the preview Text input
            if (e.Key == Key.Space) {
                e.Handled = true;
                return;
            }
        }


        private void txtValue_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            var textBox = sender as TextBox;
            // Insert new character at proposed index
            string fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            DI.W.Validate(this.dataType, fullText, (err) => {
                e.Handled = true;
                App.ShowErrMsg(err);
            });
        }

    }
}
