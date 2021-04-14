using MultiCommDashboardData.Storage;
using MultiCommDashboards.DashBuilders;
using System.Collections.Generic;
using System.Windows.Controls;

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

        #endregion

        public UC_DashBoardEdit() {
            InitializeComponent();

            this.InitBoolInputs();
            this.InitHorizontalInputs();
            this.InitVerticalInputs();
            
            this.InitBoolOutputs();
            this.InitHorizontalOutputs();
            this.InitVerticalOutputs();
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
            this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(0, this.outBoolProgress0, this.grdOutputsBool));
            this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(1, this.outBoolProgress1, this.grdOutputsBool));
            this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(2, this.outBoolProgress2, this.grdOutputsBool));
            this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(3, this.outBoolProgress3, this.grdOutputsBool));
        }


        private void InitHorizontalOutputs() {
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(0, this.outHProgress0, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(1, this.outHProgress1, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(2, this.outHProgress2, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(3, this.outHProgress3, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(4, this.outHProgress4, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(5, this.outHProgress5, this.grdOutputsNumHorizontal));
        }


        private void InitVerticalOutputs() {
            this.outputsVNum.Add(new OutputBuilder<UC_VerticalProgressBar>(0, this.outVProgress0, this.grdOutputsNumVertical));
            this.outputsVNum.Add(new OutputBuilder<UC_VerticalProgressBar>(1, this.outVProgress1, this.grdOutputsNumVertical));
            this.outputsVNum.Add(new OutputBuilder<UC_VerticalProgressBar>(2, this.outVProgress2, this.grdOutputsNumVertical));
        }


        public DashboardConfiguration GetConfig() {
            DashboardConfiguration config = new DashboardConfiguration() {
                Name = "Test config name"
            };

            // Inputs
            foreach (var inputs in this.inputsBool) {
                inputs.BuildConfig(config);
            }
            foreach (var inputs in this.inputsHNum) {
                inputs.BuildConfig(config);
            }
            foreach (var inputs in this.inputsVNum) {
                inputs.BuildConfig(config);
            }


            // Outputs
            foreach (var outputs in this.outputsBool) {
                outputs.BuildConfig(config);
            }

            foreach (var outputs in this.outputsHNum) {
                outputs.BuildConfig(config);
            }

            foreach (var outputs in this.outputsVNum) {
                outputs.BuildConfig(config);
            }

            return config;
        }

    }
}
