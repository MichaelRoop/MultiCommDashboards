﻿using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using LogUtils.Net;
using WpfCustomControlLib.Core.UtilWindows;
//using System.Windows.Shapes;

namespace MultiCommDashboards.WindowObjs {

    /// <summary>Logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window {


        public MainWindow() {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e) {

            MsgBoxSimple.ShowBox(this, "BLIPO");

        }
    }
}
