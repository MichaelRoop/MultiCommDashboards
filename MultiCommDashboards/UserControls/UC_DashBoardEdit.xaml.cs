using MultiCommDashboardData.Storage;
using MultiCommDashboards.DashBuilders;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

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


        private static int OUTPUTS_ROWS_BOOL = 4;
        private static int OUTPUTS_COLS_BOOL = 10;
        private static int OUTPUTS_ROWS_NUM_H = 6;
        private static int OUTPUTS_COLS_NUM_H = 4;
        private static int OUTPUTS_ROWS_NUM_V = 3;
        private static int OUTPUTS_COLS_NUM_V = 10;

        #endregion

        public UC_DashBoardEdit() {
            InitializeComponent();
            this.InitBoolInputs();
            this.InitHorizontalInputs();
            this.InitVerticalInputs();
            
            this.InitBoolOutputs();
            this.InitHorizontalOutputs();
            this.InitVerticalOutputs();

            // Set the dummy objects as Add
            // Outputs
            this.outBoolProgress0.SetAsAddDummy();
            this.outBoolProgress1.SetAsAddDummy();
            this.outBoolProgress2.SetAsAddDummy();
            this.outBoolProgress3.SetAsAddDummy();

            this.outHProgress0.SetAsAddDummy();
            this.outHProgress1.SetAsAddDummy();
            this.outHProgress2.SetAsAddDummy();
            this.outHProgress3.SetAsAddDummy();
            this.outHProgress4.SetAsAddDummy();
            this.outHProgress5.SetAsAddDummy();

            this.outVProgress0.SetAsAddDummy();
            this.outVProgress1.SetAsAddDummy();
            this.outVProgress2.SetAsAddDummy();

        }


        private void InitBoolInputs() {
            this.inputsBool.Add(new InputBuilder<UC_BoolToggle>(0, this.inBool0, this.grdInputsBool));
            this.inputsBool.Add(new InputBuilder<UC_BoolToggle>(1, this.inBool1, this.grdInputsBool));
            this.inputsBool.Add(new InputBuilder<UC_BoolToggle>(2, this.inBool2, this.grdInputsBool));
        }


        private void InitHorizontalInputs() {
            this.inputsHNum.Add(new InputBuilder<UC_HorizontalSlider>(0, this.inHSlider0, this.grdInputsNumHorizontal));
            this.inputsHNum.Add(new InputBuilder<UC_HorizontalSlider>(1, this.inHSlider1, this.grdInputsNumHorizontal));
            this.inputsHNum.Add(new InputBuilder<UC_HorizontalSlider>(2, this.inHSlider2, this.grdInputsNumHorizontal));
        }


        private void InitVerticalInputs() {
            this.inputsVNum.Add(new InputBuilder<UC_VerticalSlider>(0, this.inVSlider0, this.grdInputsNumVertical));
            this.inputsVNum.Add(new InputBuilder<UC_VerticalSlider>(1, this.inVSlider1, this.grdInputsNumVertical));
        }


        private void InitBoolOutputs() {
            for (int i = 0; i < OUTPUTS_ROWS_BOOL; i++) {
                this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(this.grdOutputsBool, i, OUTPUTS_COLS_BOOL));
            }
        }


        private void InitHorizontalOutputs() {
            for (int i = 0; i < OUTPUTS_ROWS_NUM_H; i++) {
                this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(this.grdOutputsNumHorizontal, i, OUTPUTS_COLS_NUM_H));
            }
        }


        private void InitVerticalOutputs() {
            for (int i = 0; i < OUTPUTS_ROWS_NUM_V; i++) {
                this.outputsVNum.Add(new OutputBuilder<UC_VerticalProgressBar>(this.grdOutputsNumVertical, i, OUTPUTS_COLS_NUM_V));
            }
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
