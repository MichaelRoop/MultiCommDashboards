using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfCustomControlLib.Core.UtilWindows;

namespace MultiCommDashboards.UserControls {
    /// <summary>
    /// Interaction logic for UC_VerticalSlider.xaml
    /// </summary>
    public partial class UC_VerticalSlider : UserControl {

        private bool isSelected = false;
        private bool selectMode = false;

        public bool IsSelected {
            get { return this.isSelected; }
            set { 
                isSelected = value;
                this.Opacity = this.isSelected ? 1 : .4;
            } 
        }

        public UC_VerticalSlider() {
            InitializeComponent();
        }


        public void SetSelectMode(bool on) {
            this.mainBorder.MouseLeftButtonUp -= MainBorder_MouseLeftButtonUp;
            this.selectMode = on;
            if (this.selectMode) {
                // Have a mode for selecting it to set it positive
                this.mainBorder.MouseLeftButtonUp += MainBorder_MouseLeftButtonUp;
                this.vSliderNumeric.IsEnabled = false;
                this.IsSelected = false;
            }
            else {
                this.vSliderNumeric.IsEnabled = true;
            }
        }



        private void MainBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

            Border b = sender as Border;
            UC_VerticalSlider s = b.Parent as UC_VerticalSlider;
            s.IsSelected = MsgBoxYesNo.ShowBox("Do You Want to select this one?") == MsgBoxYesNo.MsgBoxResult.Yes;

            if (this.IsSelected) {
                // You could pass in info for ID, name, data type
                this.lbIdTxt.Content = "33";
                this.lbIdNameTxt.Content = "Volume";
            }
            else {
                this.lbIdTxt.Content = "";
                this.lbIdNameTxt.Content = "";
            }
        }

        private void vSliderNumeric_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args) {
            this.lblValue.Content = args.NewValue.ToString();
        }

        //private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

        //    UC_VerticalSlider s = sender as UC_VerticalSlider;
        //    s.IsSelected = MsgBoxYesNo.ShowBox("Do You Want to select this one?") == MsgBoxYesNo.MsgBoxResult.Yes;

        //    if (this.IsSelected) {
        //        // You could pass in info for ID, name, data type
        //        this.lbIdTxt.Content = "33";
        //        this.lbIdNameTxt.Content = "Volume";
        //    }
        //    else {
        //        this.lbIdTxt.Content = "";
        //        this.lbIdNameTxt.Content = "";
        //    }


        //}
    }
}
