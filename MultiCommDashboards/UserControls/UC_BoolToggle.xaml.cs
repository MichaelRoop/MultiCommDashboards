using CommunicationStack.Net.Enumerations;
using LogUtils.Net;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction logic for UC_BoolToggle.xaml</summary>
    public partial class UC_BoolToggle : UserControl {

        private ClassLog log = new ClassLog("UC_BoolToggle");
        private byte id = 0;
        private BinaryMsgDataType dataType = BinaryMsgDataType.typeBool;
        private Action<byte, BinaryMsgDataType, double> sendAction = null;
        private Func<bool, string> translateTrueFalseFunc = null;


        public UC_BoolToggle() {
            this.translateTrueFalseFunc = this.DefaultTrueFalseTranlator;
            InitializeComponent();
            this.lblValue.Content = "";
        }


        public void SetSendAction(Action<byte, BinaryMsgDataType, double> sendAction) {
            this.sendAction = sendAction;
        }


        public void SetTrueFalseTranslators(Func<bool, string> func) {
            this.translateTrueFalseFunc = func;
        }


        public void Init(byte id, string name) {
            this.id = id;
            this.lbIdTxt.Content = id.ToString();
            this.lbIdNameTxt.Content = name;
            this.dataType = BinaryMsgDataType.typeBool;
            this.lblValue.Content = this.translateTrueFalseFunc(false);
        }


        private void booSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args) {
            try {
                this.lblValue.Content = this.translateTrueFalseFunc(args.NewValue != 0);
                this.sendAction?.Invoke(this.id, this.dataType, args.NewValue);
            }
            catch (Exception ex) {
                this.log.Exception(9999, "OnSliderActionChanged", "", ex);
            }
        }


        private string DefaultTrueFalseTranlator(bool trueFalse) {
            return trueFalse.ToString();
        }

    }
}
