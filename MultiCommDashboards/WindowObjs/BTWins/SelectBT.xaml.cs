using BluetoothCommon.Net;
using ChkUtils.Net;
using LanguageFactory.Net.data;
using LogUtils.Net;
using MultiCommDashboards.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfCustomControlLib.Core.Helpers;
using WpfHelperClasses.Core;

namespace MultiCommDashboards.WindowObjs.BTWins {

    /// <summary>Interaction logic for SelectBT.xaml</summary>
    public partial class SelectBT : Window {

        private Window parent = null;
        private List<BTDeviceInfo> items = new List<BTDeviceInfo>();
        private ClassLog log = new ClassLog("SelectBT");

        public BTDeviceInfo SelectedBT { get; private set; } = null;

        public static BTDeviceInfo ShowBox(Window parent) {
            SelectBT win = new SelectBT(parent);
            win.ShowDialog();
            return win.SelectedBT;
        }


        public SelectBT(Window parent) {
            this.parent = parent;
            InitializeComponent();
        }


        public override void OnApplyTemplate() {
            this.BindMouseDownToCustomTitleBar();
            base.OnApplyTemplate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e) {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.CenterToParent(this.parent);
            DI.W.BT_Discovered += this.deviceDiscovered;
            DI.W.BT_DiscoveryComplete += this.discoveryComplete;
            this.gridWait.Show();
            DI.W.BTDiscoverAsync(false);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            DI.W.BT_Discovered -= this.deviceDiscovered;
            DI.W.BT_DiscoveryComplete -= this.discoveryComplete;
            this.lbBluetooth.SelectionChanged -= this.selectionChanged;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();

        }


        #region Event handlers

        private void selectionChanged(object sender, SelectionChangedEventArgs e) {
            BTDeviceInfo info = this.lbBluetooth.SelectedItem as BTDeviceInfo;
            if (info != null) {
                this.SelectedBT = info;
                this.Close();
            }
        }

        private void deviceDiscovered(object sender, BTDeviceInfo device) {
            this.log.Info("deviceDiscovered", () => string.Format("Found {0}", device.Name));
            this.Dispatcher.Invoke(() => {
                WrapErr.ToErrReport(9999, () => { 
                    this.lbBluetooth.Add(this.items, device); 
                });
            });
        }


        private void discoveryComplete(object sender, bool e) {
            this.Dispatcher.Invoke(() => {
                WrapErr.ToErrReport(9999, () => {
                    this.gridWait.Collapse();
                    if (this.items.Count == 0) {
                        App.ShowErrMsg(DI.W.GetText(MsgCode.NotFound));
                        this.Close();
                    }
                    else {
                        this.lbBluetooth.SelectionChanged += this.selectionChanged;
                    }
                });
            });
        }

        #endregion

    }
}
