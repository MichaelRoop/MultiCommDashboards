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
            //this.InitVerticalInputs();
            
            this.InitBoolOutputs();
            this.InitHorizontalOutputs();
            //this.InitVerticalOutputs();
        }


        public DashboardConfiguration GetConfig() {
            // TODO - have the name saved and loaded
            DashboardConfiguration config = new DashboardConfiguration() {
                Name = "Test config name"
            };

            this.BuildConfig(this.inputsBool, config);
            this.BuildConfig(this.inputsHNum, config);
            this.BuildConfig(this.inputsVNum, config);
            this.BuildConfig(this.outputsBool, config);
            this.BuildConfig(this.outputsHNum, config);
            this.BuildConfig(this.outputsVNum, config);
            return config;
        }


        private void InitBoolInputs() {
            this.inputsBool.Add(new InputBuilder<UC_BoolToggle>(this.btnInBool0, this.grdInputsBool));
            this.inputsBool.Add(new InputBuilder<UC_BoolToggle>(this.btnInBool1, this.grdInputsBool));
            this.inputsBool.Add(new InputBuilder<UC_BoolToggle>(this.btnInBool2, this.grdInputsBool));
        }


        private void InitHorizontalInputs() {
            this.inputsHNum.Add(new InputBuilder<UC_HorizontalSlider>(this.btnInAnalog0, this.grdInputsNumHorizontal));
            this.inputsHNum.Add(new InputBuilder<UC_HorizontalSlider>(this.btnInAnalog1, this.grdInputsNumHorizontal));
            this.inputsHNum.Add(new InputBuilder<UC_HorizontalSlider>(this.btnInAnalog2, this.grdInputsNumHorizontal));

        }


        //private void InitVerticalInputs() {
        //    this.inputsVNum.Add(new InputBuilder<UC_VerticalSlider>(0, this.inVSlider0, this.grdInputsNumVertical));
        //    this.inputsVNum.Add(new InputBuilder<UC_VerticalSlider>(1, this.inVSlider1, this.grdInputsNumVertical));
        //}


        private void InitBoolOutputs() {
            this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(this.btnOutBool0, this.grdOutputsBool));
            this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(this.btnOutBool1, this.grdOutputsBool));
            this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(this.btnOutBool2, this.grdOutputsBool));
            this.outputsBool.Add(new OutputBuilder<UC_BoolProgress>(this.btnOutBool3, this.grdOutputsBool));
        }


        private void InitHorizontalOutputs() {
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH0, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH1, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH2, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH3, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH4, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new OutputBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH5, this.grdOutputsNumHorizontal));
        }


        //private void InitVerticalOutputs() {
        //    this.outputsVNum.Add(new OutputBuilder<UC_VerticalProgressBar>(0, this.outVProgress0, this.grdOutputsNumVertical));
        //    this.outputsVNum.Add(new OutputBuilder<UC_VerticalProgressBar>(1, this.outVProgress1, this.grdOutputsNumVertical));
        //    this.outputsVNum.Add(new OutputBuilder<UC_VerticalProgressBar>(2, this.outVProgress2, this.grdOutputsNumVertical));
        //}


        private void BuildConfig<T>(List< InputBuilder<T> > inputs, DashboardConfiguration config) where T:UC_InputBase, new() {
            foreach (var input in inputs) {
                input.BuildConfig(config);
            }
        }


        private void BuildConfig<T>(List<OutputBuilder<T>> outputs, DashboardConfiguration config) where T : UC_OutputBase, new() {
            foreach (var output in outputs) {
                output.BuildConfig(config);
            }
        }


    }
}
