using BluetoothCommon.Net;
using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.Enumerations;
using LanguageFactory.Net.data;
using MultiCommDashboards.DependencyInjection;
using MultiCommDashboards.UserControls;
using MultiCommDashboards.WindowObjs.BTWins;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiCommDashboards.WindowObjs {

    /// <summary>Logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            DI.W.BT_Connected += W_BT_Connected;
            DI.W.MsgEventFloat32 += W_MsgEventFloat32;
            DI.W.SetLanguage(LangCode.Spanish, App.ShowErrMsg);
            this.InitControls();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }


        private void W_MsgEventFloat32(object sender, BinaryMsgFloat32 e) {
            this.Dispatcher.Invoke(() => {
                this.txtInput.Text = e.Value.ToString();
            });
        }


        private void W_BT_Connected(object sender, bool ok) {
            if (ok) {
                App.ShowMsgTitle("BT", "Connected");
            }
            else {
                App.ShowErrMsg("Failed connection");
            }
        }


        private void btnConnect_Click(object sender, RoutedEventArgs e) {
            BTDeviceInfo device = SelectBT.ShowBox(this);
            if (device != null) {
                DI.W.BTConnectAsync(device);
            }
            else {
                App.ShowErrMsg("No device selected");
            }
        }


        private void btnDisconnect_Click(object sender, RoutedEventArgs e) {
            DI.W.BTDisconnect();
        }


        bool selectMode = false;
        private void btnSend_Click(object sender, RoutedEventArgs e) {
            //BinaryMsgBool mb = new BinaryMsgBool(10, true);
            //DI.W.BTSend(mb.ToByteArray());
            this.selectMode = !this.selectMode;
            this.vSlider1.SetSelectMode(this.selectMode);

        }

        #region Init Controls

        // This will be done later from saved configurations in dashboards

        private void InitControls() {
            this.sliderBool.SetSendAction(this.sendAction);
            this.sliderBool.SetTrueFalseTranslators(this.translateTrueFalseFunc);
            this.sliderBool.InitAsBool(10, "IO 1 LED");

            this.numericSlider.SetSendAction(this.sendAction);
            this.numericSlider.SetTrueFalseTranslators(this.translateTrueFalseFunc);
            this.numericSlider.InitAsNumeric(12, "IO 3 PWM", BinaryMsgDataType.typeUInt8, 1, 0, 254);
        }

        #endregion

        #region Actions to pass to the sliders

        private string translateTrueFalseFunc(bool trueFalse) {
            return DI.W.GetText(trueFalse ? MsgCode.True : MsgCode.False);
        }


        private void sendAction(byte id, BinaryMsgDataType dataType, double value) {
            // TODO Can do some validation here of range
            switch (dataType) {
                case BinaryMsgDataType.typeBool:
                    DI.W.BTSend(new BinaryMsgBool(id, (value != 0)).ToByteArray());
                    break;
                case BinaryMsgDataType.typeInt8:
                    break;
                case BinaryMsgDataType.typeUInt8:
                    DI.W.BTSend(new BinaryMsgUInt8(id, (byte)value).ToByteArray());
                    break;
                case BinaryMsgDataType.typeInt16:
                    break;
                case BinaryMsgDataType.typeUInt16:
                    break;
                case BinaryMsgDataType.typeInt32:
                    break;
                case BinaryMsgDataType.typeUInt32:
                    break;
                case BinaryMsgDataType.typeFloat32:
                    break;
                case BinaryMsgDataType.tyepUndefined:
                case BinaryMsgDataType.typeInvalid:
                    break;
            }
        }



        #endregion

        List<UC_BoolToggle> boolToggles0 = new List<UC_BoolToggle>();
        int boolNext = 1;
        int id = 0;

        private void brdBool_0_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (boolNext <= 10) {
                UC_BoolToggle bt = new UC_BoolToggle();
                id++;
                bt.InitAsBool((byte)id, string.Format("DigiIO_{0}", id));
                Grid.SetRow(bt, 0);
                Grid.SetColumn(bt, boolNext);
                this.grdBool.Children.Add(bt);
                bt.MouseLeftButtonUp += Bt_MouseLeftButtonUp;
                this.boolToggles0.Add(bt);
                boolNext++;
            }
        }

        private void Bt_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            UC_BoolToggle bt = sender as UC_BoolToggle;
            bt.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            this.boolToggles0.Remove(bt);
            this.grdBool.Children.Remove(bt);
            for (int i = 1; i < this.grdBool.Children.Count; i++) {
                UC_BoolToggle b = this.grdBool.Children[i] as UC_BoolToggle;
                Grid.SetColumn(b, i);
            }

            this.grdBool.InvalidateVisual();
            boolNext--;
            if (boolNext < 1) {
                boolNext = 1;
            }
        }

        private void boolRow0_MouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            var point = Mouse.GetPosition(this.grdBool);

            App.ShowMsgTitle("Mouse Pos sender", string.Format("boolRow0_MouseLeftButtonUp   Sender:{0} X:{1} Y:{2}", sender.GetType().Name, point.X, point.Y));

        }

        private void grdBool_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            //UC_BoolToggle bt = sender as UC_BoolToggle;

            //App.ShowMsgTitle("Mouse Pos sender", string.Format("grdBool_PreviewMouseLeftButtonUp Sender:{0}", sender.GetType().Name));



            //if (bt != null) {
            //    int row = (int)bt.GetValue(Grid.RowProperty);
            //    int col = (int)bt.GetValue(Grid.ColumnProperty);
            //    App.ShowMsgTitle("Bool Toggle in Grid", string.Format("Name:{0} X:{1} Y:{2}", bt.Name, row, col));

            //    //DataGrid dg = sender as DataGrid;
            //    //var row = ItemsControl.ContainerFromElement(dg, e.OriginalSource as DependencyObject) as DataGridRow;
            //    //var column = ItemsControl.ContainerFromElement(dg, e.OriginalSource as DependencyObject) as DataGridColumn;
            //    //UC_BoolToggle bt = (UC_BoolToggle)this.grdBool.Item
            //}
            //else {
                Grid dg = sender as Grid;
                if (dg != null) {
                    //App.ShowMsgTitle("Mouse Pos sender", string.Format("Sender:{0}", sender.GetType().Name));

                    if (e.OriginalSource == null) {
                        //e.Handled = false;
                        return;
                    }

                //if (e.OriginalSource is Border) {
                //    var b = e.OriginalSource as Border;
                //    var rrr = b.GetValue(Grid.RowProperty);
                //    var ccc = b.GetValue(Grid.ColumnProperty);
                //    //App.ShowMsgTitle("Bool Toggle in Grid", string.Format("Name:{0} Row:{1} Column:{2}", "Name", rrr, ccc));


                //    return;
                //}

                //if (e.OriginalSource is UC_BoolToggle) {
                //    UC_BoolToggle boolToggle = e.OriginalSource as UC_BoolToggle;
                //    var rr = boolToggle.GetValue(Grid.RowProperty);
                //    var cc = boolToggle.GetValue(Grid.ColumnProperty);
                //    App.ShowMsgTitle("Bool Toggle in Grid", string.Format("Name:{0} Row:{1} Column:{2}", boolToggle.Name, rr, cc));

                //    e.Handled = true;
                //}



                    int col = (int)dg.GetValue(Grid.ColumnProperty);
                    int row = (int)dg.GetValue(Grid.RowProperty);

                    if (col == 0) {

                        //UC_BoolToggle bt = new UC_BoolToggle();
                        //int id = this.boolToggles0.Count + 1;

                        //bt.InitAsBool((byte)id, string.Format("DigiIO+{0}", id));
                        //Grid.SetRow(bt, 0);
                        //Grid.SetColumn(bt, id);
                        //this.grdBool.Children.Add(bt);
                        //this.boolToggles0.Add(bt);



                        //e.Handled = false;
                        return;
                    }

                        //bt = (UC_BoolToggle) 

                        //dg.

                        var c = grdBool.Children.Cast<UC_BoolToggle>();


                        UC_BoolToggle bt2 = this.grdBool.Children.Cast<UC_BoolToggle>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);

                if (bt2 != null) {
                    App.ShowMsgTitle("Bool Toggle in Grid", string.Format("Name:{0} X:{1} Y:{2}", bt2.Name, row, col));

                }


                        //var row = ItemsControl.ContainerFromElement(dg, e.OriginalSource as DependencyObject) as DataGridRow;
                        //var col = ItemsControl.ContainerFromElement(dg, e.OriginalSource as DependencyObject) as DataGridColumn;
                        //var collections = (UC_BoolToggle)dg.Items[row.GetIndex()];

                        ////App.ShowMsgTitle("Bool Toggle in Grid", string.Format("Name:{0} X:{1} Y:{2}", bt.Name, row.GetIndex(), col.));

                        int i = 10;


                }



            //}
        }
    }
}
