using MultiCommDashboardData.Storage;
using MultiCommDashboards.DashBuilders;
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

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction for UC_DashBoardEdit.xaml</summary>
    public partial class UC_DashBoardEdit : UserControl {

        #region Data

        List<InputBuilder<UC_BoolToggle>> inputsBool = new List<InputBuilder<UC_BoolToggle>>();
        List<InputBuilder<UC_HorizontalSlider>> inputsHNum = new List<InputBuilder<UC_HorizontalSlider>>();
        List<InputBuilder<UC_VerticalSlider>> inputsVNum = new List<InputBuilder<UC_VerticalSlider>>();

        List<OutputBuilder<UC_BoolProgress>> outputsBool = new List<OutputBuilder<UC_BoolProgress>>();
        List<OutputBuilder<UC_HorizontalProgressBar>> outputsHNum = new List<OutputBuilder<UC_HorizontalProgressBar>>();
        List<OutputBuilder<UC_VerticalProgressBar>> outputsVNum = new List<OutputBuilder<UC_VerticalProgressBar>>();

        private static int INPUTS_ROWS_BOOL = 4;
        private static int INPUTS_COLS_BOOL = 10;
        private static int INPUTS_ROWS_NUM_H = 10;
        private static int INPUTS_COLS_NUM_H = 4;
        private static int INPUTS_ROWS_NUM_V = 3;
        private static int INPUTS_COLS_NUM_V = 10;

        #endregion

        public UC_DashBoardEdit() {
            InitializeComponent();
            // Initialize the builders
            for (int i = 0; i < INPUTS_ROWS_BOOL; i++) {
                this.inputsBool.Add(new InputBuilder<UC_BoolToggle>(this.grdInputsBool, i, INPUTS_COLS_BOOL));
            }

            for (int i = 0; i < INPUTS_ROWS_NUM_H; i++) {
                this.inputsHNum.Add(new InputBuilder<UC_HorizontalSlider>(this.grdInputsNumHorizontal, i, INPUTS_COLS_NUM_H));
            }

            for (int i = 0; i < INPUTS_ROWS_NUM_V; i++) {
                this.inputsVNum.Add(new InputBuilder<UC_VerticalSlider>(this.grdInputsNumVertical, i, INPUTS_COLS_NUM_V));
            }

            // Using same defines for output column and row within grid
            for (int i = 0; i < INPUTS_ROWS_BOOL; i++) {
                this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(this.grdOutputsBool, i, INPUTS_COLS_BOOL));
            }

            for (int i = 0; i < INPUTS_ROWS_NUM_H; i++) {
                this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(this.grdOutputsNumHorizontal, i, INPUTS_COLS_NUM_H));
            }

            for (int i = 0; i < INPUTS_ROWS_NUM_V; i++) {
                this.outputsVNum.Add(new OutputBuilder<UC_VerticalProgressBar>(this.grdOutputsNumVertical, i, INPUTS_COLS_NUM_V));
            }
        }


        public void Init() {
            // TODO Remove

        }


        public DashboardConfiguration GetConfig() {
            DashboardConfiguration config = new DashboardConfiguration() {
                Name = "Test config name"
            };
            // NOTE: they are always 1 column over since dummy is in 0. Need decrement
            foreach (var ib in this.inputsBool) {
                foreach (InputControlDataModel data in ib.DataModels) {
                    config.InputsBool.Add(data);
                }
            }

            foreach (var inH in this.inputsHNum) {
                foreach(InputControlDataModel data in inH.DataModels) {
                    config.InputsNumericHorizontal.Add(data);
                }
            }

            foreach (var inV in this.inputsVNum) {
                foreach(InputControlDataModel data in inV.DataModels) {
                    config.InputsNumericVertical.Add(data);
                }
            }

            // Outputs
            foreach (var ib in this.outputsBool) {
                foreach (OutputControlDataModel data in ib.DataModels) {
                    config.OutputsBool.Add(data);
                }
            }

            foreach (var inH in this.outputsHNum) {
                foreach (OutputControlDataModel data in inH.DataModels) {
                    config.OutputsNumericHorizontal.Add(data);
                }
            }

            foreach (var inV in this.outputsVNum) {
                foreach (OutputControlDataModel data in inV.DataModels) {
                    config.OutputsNumericVertical.Add(data);
                }
            }

            return config;
        }




        private void mouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if (sender is Border) {
                switch ((sender as Border).Name) {
                    // Boolean Input rows
                    case "brdBool_0":
                        this.inputsBool[0].Add();
                        break;
                    case "brdBool_1":
                        this.inputsBool[1].Add();
                        break;
                    case "brdBool_2":
                        this.inputsBool[2].Add();
                        break;
                    case "brdBool_3":
                        this.inputsBool[3].Add();
                        break;

                    // Horizontal input rows
                    case "brdHNum_0":
                        this.inputsHNum[0].Add();
                        break;
                    case "brdHNum_1":
                        this.inputsHNum[1].Add();
                        break;
                    case "brdHNum_2":
                        this.inputsHNum[2].Add();
                        break;
                    case "brdHNum_3":
                        this.inputsHNum[3].Add();
                        break;
                    case "brdHNum_4":
                        this.inputsHNum[4].Add();
                        break;
                    case "brdHNum_5":
                        this.inputsHNum[5].Add();
                        break;

                    // Vertical input rows
                    case "brdVNum_0":
                        this.inputsVNum[0].Add();
                        break;
                    case "brdVNum_1":
                        this.inputsVNum[1].Add();
                        break;
                    case "brdVNum_2":
                        this.inputsVNum[2].Add();
                        break;
                }
            }
        }




        private void mouseLeftButtonUpOutput(object sender, MouseButtonEventArgs e) {
            if (sender is Border) {
                switch ((sender as Border).Name) {
                    // Boolean Output rows
                    case "brdOutputBool_0":
                        this.outputsBool[0].Add();
                        break;
                    case "brdOutputBool_1":
                        this.outputsBool[1].Add();
                        break;
                    case "brdOutputBool_2":
                        this.outputsBool[2].Add();
                        break;
                    case "brdOutputBool_3":
                        this.outputsBool[3].Add();
                        break;

                    // Horizontal output rows
                    case "brdOutputHNum_0":
                        this.outputsHNum[0].Add();
                        break;
                    case "brdOutputHNum_1":
                        this.outputsHNum[1].Add();
                        break;
                    case "brdOutputHNum_2":
                        this.outputsHNum[2].Add();
                        break;
                    case "brdOutputHNum_3":
                        this.outputsHNum[3].Add();
                        break;
                    case "brdOutputHNum_4":
                        this.outputsHNum[4].Add();
                        break;
                    case "brdOutputHNum_5":
                        this.outputsHNum[5].Add();
                        break;

                    // Vertical input rows
                    case "brdOutputVNum_0":
                        this.outputsVNum[0].Add();
                        break;
                    case "brdOutputVNum_1":
                        this.outputsVNum[1].Add();
                        break;
                    case "brdOutputVNum_2":
                        this.outputsVNum[2].Add();
                        break;
                }
            }
        }


    }
}
