using LanguageFactory.Net.data;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardData.DataModels;
using MultiCommDashboardData.Enumerations;
using MultiCommDashboards.DependencyInjection;
using MultiCommDashboards.WindowObjs.BTWins;
using MultiCommDashboards.WindowObjs.Configs;
using MultiCommDashboards.WpfHelpers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfCustomControlLib.Core.UtilWindows;

namespace MultiCommDashboards.WindowObjs {

    /// <summary>Interaction logic for MenuWin.xaml</summary>
    public partial class MenuWin : Window {

        #region Data

        private Window mainWindow = null;
        private List<MenuItemDataModel> items = new List<MenuItemDataModel>();

        #endregion

        public MenuWin(Window mainWindow) {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }


        private void Window_ContentRendered(object sender, EventArgs e) {
            this.LoadList();
            DI.W.LanguageChanged += this.languageChanged;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.lvMenuItems.SelectionChanged -= this.menuItemsSelectionChanged;
            DI.W.LanguageChanged -= this.languageChanged;
        }


        private void menuItemsSelectionChanged(object sender, SelectionChangedEventArgs args) {
            MenuItemDataModel item = this.lvMenuItems.SelectedItem as MenuItemDataModel;
            if (item != null) {
                this.Hide();
                switch (item.Code) {
                    case MenuCode.Language:
                        LanguageSelector.ShowBox(this.mainWindow, DI.W.Languages);
                        break;
                    case MenuCode.Dashboards:
                        // TODO - need to open an intermediate window to select edit or create
                        DashboardConfigSelect.ShowBox(this);

                        //DashboardEditor.ShowBox(this);



                        break;
                    case MenuCode.Ethernet:
                        //this.runPageManager.Open(typeof(EthernetRun));
                        break;
                    case MenuCode.Usb:
                        //this.runPageManager.Open(typeof(SerialRun));
                        break;
                    case MenuCode.Wifi:
                        //this.runPageManager.Open(typeof(WifiRun));
                        break;
                    case MenuCode.Bluetooth:
                        //this.runPageManager.Open(typeof(BTRun));
                        RunBT win = new RunBT();
                        win.ShowDialog();
                        break;
                    case MenuCode.BLE:
                        //this.runPageManager.Open(typeof(BLE_Full));
                        break;
                    case MenuCode.CodeSamples:
                        //Help_CommunicationMediums cm = new Help_CommunicationMediums(this.mainWindow);
                        //cm.ShowDialog();
                        break;
                    case MenuCode.Settings:
                        //MainSettings.ShowBox(this.mainWindow);
                        break;
                    default:
                        // Not supported
                        break;
                }

                this.lvMenuItems.SelectionChanged -= this.menuItemsSelectionChanged;
                this.Hide();
                this.lvMenuItems.UnselectAll();
                this.lvMenuItems.SelectionChanged += this.menuItemsSelectionChanged;
            }
        }


        private void languageChanged(object sender, SupportedLanguage e) {
            this.Dispatcher.Invoke(this.LoadList);
        }


        private void LoadList() {
            this.Dispatcher.Invoke(() => {
                this.lvMenuItems.SelectionChanged -= this.menuItemsSelectionChanged;
                this.lvMenuItems.ItemsSource = null;
                this.items.Clear();
                this.AddItem(MenuCode.Bluetooth, "Bluetooth", IconBinder.BLUETOOTH);
                this.AddItem(MenuCode.Dashboards, MsgCode.Edit, IconBinder.EDIT);

                //this.AddItem(MenuCode.BLE, "BLE", UIIcon.BluetoothLE, "0");
                //this.AddItem(MenuCode.Wifi, "WIFI", UIIcon.Wifi, "0");
                //this.AddItem(MenuCode.Usb, "USB", UIIcon.Usb, "0");
                //this.AddItem(MenuCode.Ethernet, MsgCode.Ethernet, UIIcon.Ethernet, "0");
                this.AddItem(MenuCode.Language, MsgCode.language, IconBinder.LANGUAGE);
                //this.AddItem(MenuCode.CodeSamples, MsgCode.CodeSamples, UIIcon.Code, "0");
                //this.AddItem(MenuCode.Settings, MsgCode.Settings, UIIcon.Settings, "0");
                this.lvMenuItems.ItemsSource = this.items;
                this.lvMenuItems.SelectionChanged += this.menuItemsSelectionChanged;
            });
        }


        private void AddItem(MenuCode menuCode, MsgCode msgCode, string uIIcon) {
            this.AddItem(menuCode, DI.W.GetText(msgCode), uIIcon, "0");
        }


        private void AddItem(MenuCode menuCode, MsgCode msgCode, string uIIcon, string padding) {
            this.AddItem(menuCode, DI.W.GetText(msgCode), uIIcon, padding);
        }


        private void AddItem(MenuCode menuCode, string display, string uIIcon) {
            this.items.Add(new MenuItemDataModel(menuCode, display, uIIcon));
        }

        private void AddItem(MenuCode menuCode, string display, string uIIcon, string padding) {
            this.items.Add(new MenuItemDataModel(menuCode, display, uIIcon, padding));
        }

    }
}
