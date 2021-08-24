﻿using IconFactory.Net.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboards.WpfHelpers {


    /// <summary>Provide static functions that XAML can use to bind Image source to Image</summary>
    /// <example>
    /// Declare namespace at start of XAML file window info
    /// xmlns:wpfHelper="clr-namespace:MultiCommTerminal.WPF_Helpers" 
    /// 
    /// In the XAML file you can bind text as the following
    /// 
    ///  <Image ImageSource="{Binding Source={x:Static wpfHelper:IconBinder.Stop}}" />
    /// 
    /// The Stop will correspond to a static function in the static IconBinder class
    /// </example>
    public static class IconBinder {

        ///// <summary>Used in VS XAML design when the DI cannot be loaded</summary>
        //private static IIconFactory designFactory = new WinIconFactory();


        #region Icon paths for UI

        //public static string Save { get { return Source(UIIcon.Save); } }
        //public static string Cancel { get { return Source(UIIcon.Cancel); } }
        //public static string Exit { get { return Source(UIIcon.Exit); } }
        //public static string HamburgMenu_W { get { return Source(UIIcon.HamburgerMenuWhite); } }
        //public static string Settings { get { return Source(UIIcon.Settings); } }
        //public static string Settings_W { get { return Source(UIIcon.SettingsWhite); } }
        //public static string Language { get { return Source(UIIcon.Language); } }
        //public static string Language_W { get { return Source(UIIcon.LanguageWhite); } }
        //public static string Delete { get { return Source(UIIcon.Delete); } }
        //public static string Edit { get { return Source(UIIcon.Edit); } }
        //public static string View { get { return Source(UIIcon.View); } }
        //public static string Add { get { return Source(UIIcon.Add); } }
        //public static string Search { get { return Source(UIIcon.Search); } }
        //public static string Connect { get { return Source(UIIcon.Connect); } }
        //public static string Command { get { return Source(UIIcon.Command); } }
        //public static string Command_W { get { return Source(UIIcon.CommandWhite); } }
        //public static string Send { get { return Source(UIIcon.Run); } }
        //public static string Terminator { get { return Source(UIIcon.Terminator); } }
        //public static string Terminator_W { get { return Source(UIIcon.TerminatorWhite); } }
        //public static string SpinIcon { get { return Source(UIIcon.SpinIcon); } }
        //public static string SerialPort_W { get { return Source(UIIcon.SerialPortWhite); } }
        //public static string USBPort_W { get { return Source(UIIcon.UsbWhite); } }
        //public static string BluetoothClassic { get { return Source(UIIcon.BluetoothClassic); } }
        //public static string BluetoothClassic_W { get { return Source(UIIcon.BluetoothClassicWhite); } }
        //public static string BluetoothLE { get { return Source(UIIcon.BluetoothLE); } }
        //public static string BluetoothLE_W { get { return Source(UIIcon.BluetoothLEWhite); } }
        //public static string Phone { get { return Source(UIIcon.Phone); } }
        //public static string PhoneEmitting { get { return Source(UIIcon.PhoneEmitting); } }
        //public static string Board { get { return Source(UIIcon.Board); } }
        //public static string BoardEmitting { get { return Source(UIIcon.BoardEmitting); } }
        //public static string ArduinoIcon { get { return Source(UIIcon.ArduinoIcon); } }
        //public static string Help { get { return Source(UIIcon.Help); } }
        //public static string HelpWhite { get { return Source(UIIcon.HelpWhite); } }
        //public static string Configure { get { return Source(UIIcon.Configure); } }
        //public static string Pair { get { return Source(UIIcon.Pair); } }
        //public static string Unpair { get { return Source(UIIcon.Unpair); } }
        //public static string Credentials { get { return Source(UIIcon.Credentials); } }
        //public static string Credentials_W { get { return Source(UIIcon.CredentialsWhite); } }
        //public static string About { get { return Source(UIIcon.About); } }
        //public static string AboutWhite { get { return Source(UIIcon.AboutWhite); } }
        //public static string Services { get { return Source(UIIcon.Services); } }
        //public static string ServicesWhite { get { return Source(UIIcon.ServicesWhite); } }
        //public static string Properties { get { return Source(UIIcon.Properties); } }
        //public static string PropertiesWhite { get { return Source(UIIcon.PropertiesWhite); } }
        //public static string LogIcon { get { return Source(UIIcon.LogIcon); } }
        //public static string Ethernet { get { return Source(UIIcon.Ethernet); } }
        //public static string EthernetWhite { get { return Source(UIIcon.EthernetWhite); } }
        //public static string Wifi { get { return Source(UIIcon.Wifi); } }
        //public static string WifiWhite { get { return Source(UIIcon.WifiWhite); } }
        //public static string Code { get { return Source(UIIcon.Code); } }
        //public static string CodeWhite { get { return Source(UIIcon.CodeWhite); } }
        //public static string Read { get { return Source(UIIcon.Read); } }

        #endregion


        ///// <summary>Get the icon corresponding to the Communication Medium Type</summary>
        ///// <param name="medium">The medium type</param>
        ///// <returns>Icon source string</returns>
        //public static string CommMediumSource(this CommMedium medium) {
        //    switch (medium) {
        //        case CommMedium.Bluetooth: return BluetoothClassic;
        //        case CommMedium.BluetoothLE: return BluetoothLE;
        //        case CommMedium.Ethernet: return Source(UIIcon.Ethernet);
        //        case CommMedium.Wifi: return Source(UIIcon.Wifi);
        //        default: return Cancel;
        //    }
        //}


        //public static string CommMediumSourceWhite(this CommMedium helpType) {
        //    switch (helpType) {
        //        case CommMedium.Bluetooth:
        //            return BluetoothClassic_W;
        //        case CommMedium.BluetoothLE:
        //            return BluetoothLE_W;
        //        case CommMedium.Ethernet:
        //            // TODO need white
        //            return Source(UIIcon.EthernetWhite);
        //        case CommMedium.Wifi:
        //            return Source(UIIcon.Wifi);
        //        default:
        //            return Cancel;
        //    }
        //}


        //public static BitmapImage ResourceWhiteBitmap(this CommMedium helpType) {
        //    return new BitmapImage(new Uri(helpType.PacketSourceWhite(), UriKind.Absolute));
        //}

        //public static string PacketSourceWhite(this CommMedium helpType) {
        //    return string.Format("{0}{1}", GetIconPrefix(), IconBinder.CommMediumSourceWhite(helpType));
        //}


        ///// <summary>Get the source from factory directly if in design mode or from DI</summary>
        ///// <param name="code">The icon identifier code </param>
        ///// <returns>String with icon path</returns>
        //public static string Source(UIIcon code) {
        //    if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) {
        //        string result = IconBinder.designFactory.GetIcon(code).IconSource as string;
        //        return result != null ? result : "";
        //    }
        //    return string.Format("{0}{1}", GetIconPrefix(), DI.Wrapper.IconSource(code));
        //}


        //public static string GetIconPrefix() {
        //    // Used to be pack://application:,,,
        //    // Then the file name in ... would add the ... before the path
        //    // The site of origine requires the icons to be defined as Embeded Resource 
        //    // and Copy when new in buld steps
        //    return "pack://siteoforigin:,,,";
        //}


        public static string BLUETOOTH { get { return IconBinder.GetIconSource("icons8-bluetooth-50.png"); } }
        public static string BLUETOOTH_W { get { return IconBinder.GetIconSource("icons8-bluetooth-white-50.png"); } }
        public static string CANCEL { get { return IconBinder.GetIconSource("icons8-close-window-50-noborder.png"); } }
        public static string DELETE { get { return IconBinder.GetIconSource("icons8-trash-can-50.png"); } }
        public static string EDIT { get { return IconBinder.GetIconSource("icons8-edit-50.png"); } }
        public static string EXIT { get { return IconBinder.GetIconSource("icons8-exit-50.png"); } }
        public static string HELP { get { return IconBinder.GetIconSource("icons8-help-50.png"); } }
        public static string LANGUAGE { get { return IconBinder.GetIconSource("icons8-language-50.png"); } }
        public static string LANGUAGE_W { get { return IconBinder.GetIconSource("icons8-language-white-50.png"); } }
        public static string NO { get { return IconBinder.CANCEL; } }
        public static string OK { get { return IconBinder.YES; } }
        public static string SEND { get { return IconBinder.GetIconSource("icons8-running-24.png"); } }
        public static string YES { get { return IconBinder.GetIconSource("icons8-checkmark-50.png"); } }
        public static string ADD { get { return IconBinder.GetIconSource("icons8-add-50-noborder.png"); } }
        public static string MENU { get { return IconBinder.GetIconSource("icons8-menu-white-50.png"); } }


        private static string GetIconSource(string name) {
            // Images must be in the app\Images directory and marked as Resource
            //return string.Format(@"MultiCommDashboards;component\Images\{0}", name);

            // This works in more situations. Gets them from Resource.  Images must be in the
            // app\Images directory and marked as Resource
            return string.Format("pack://application:,,/Images/{0}", name);
        }



    }

}
