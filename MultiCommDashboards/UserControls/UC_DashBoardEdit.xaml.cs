﻿using MultiCommDashboardData.Storage;
using MultiCommDashboards.DashBuilders;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MultiCommDashboards.UserControls {

    /// <summary>Interaction for UC_DashBoardEdit.xaml</summary>
    public partial class UC_DashBoardEdit : UserControl {

        #region Data

        List<DashboardControlBuilder<UC_BoolToggle>> inputsBool = new List<DashboardControlBuilder<UC_BoolToggle>>();
        List<DashboardControlBuilder<UC_HorizontalSlider>> inputsHNum = new List<DashboardControlBuilder<UC_HorizontalSlider>>();
        List<DashboardControlBuilder<UC_BoolProgress>> outputsBool = new List<DashboardControlBuilder<UC_BoolProgress>>();
        List<DashboardControlBuilder<UC_HorizontalProgressBar>> outputsHNum = new List<DashboardControlBuilder<UC_HorizontalProgressBar>>();
        private string StorageUId = "";

        #endregion

        public UC_DashBoardEdit() {
            InitializeComponent();
            this.InitBoolInputs();
            this.InitHorizontalInputs();
            this.InitBoolOutputs();
            this.InitHorizontalOutputs();
        }


        /// <summary>Initialize the editor with existing configuration</summary>
        /// <param name="config"></param>
        public void Init(DashboardConfiguration config) {
            foreach (var dataModel in config.InputsBool) {
                this.InitInput(dataModel, this.inputsBool);
            }

            foreach (var dataModel in config.InputsNumericHorizontal) {
                this.InitInput(dataModel, this.inputsHNum);
            }

            // Outputs
            foreach (var dataModel in config.OutputsBool) {
                this.InitOutput(dataModel, this.outputsBool);
            }

            foreach (var dataModel in config.OutputsNumericHorizontal) {
                this.InitOutput(dataModel, this.outputsHNum);
            }
            this.txtName.Text = config.Display;
            this.StorageUId = config.UId;
        }


        public DashboardConfiguration GetConfig() {
            // TODO - Validate the name?
            DashboardConfiguration config = new DashboardConfiguration() {
                Display = this.txtName.Text,
            };
            // For loaded stored configs
            if (!string.IsNullOrWhiteSpace(this.StorageUId)) {
                config.UId = this.StorageUId;
            }

            this.BuildInConfig(this.inputsBool, config);
            this.BuildInConfig(this.inputsHNum, config);
            this.BuildOutConfig(this.outputsBool, config);
            this.BuildOutConfig(this.outputsHNum, config);
            return config;
        }


        private void InitBoolInputs() {
            this.inputsBool.Add(new DashboardControlBuilder<UC_BoolToggle>(this.btnInBool0, this.grdInputsBool));
            this.inputsBool.Add(new DashboardControlBuilder<UC_BoolToggle>(this.btnInBool1, this.grdInputsBool));
        }


        private void InitHorizontalInputs() {
            this.inputsHNum.Add(new DashboardControlBuilder<UC_HorizontalSlider>(this.btnInAnalog0, this.grdInputsNumHorizontal));
            this.inputsHNum.Add(new DashboardControlBuilder<UC_HorizontalSlider>(this.btnInAnalog1, this.grdInputsNumHorizontal));
        }


        private void InitBoolOutputs() {
            this.outputsBool.Add(new DashboardControlBuilder<UC_BoolProgress>(this.btnOutBool0, this.grdOutputsBool));
            this.outputsBool.Add(new DashboardControlBuilder<UC_BoolProgress>(this.btnOutBool1, this.grdOutputsBool));
            this.outputsBool.Add(new DashboardControlBuilder<UC_BoolProgress>(this.btnOutBool2, this.grdOutputsBool));
            this.outputsBool.Add(new DashboardControlBuilder<UC_BoolProgress>(this.btnOutBool3, this.grdOutputsBool));
        }


        private void InitHorizontalOutputs() {
            this.outputsHNum.Add(new DashboardControlBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH0, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new DashboardControlBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH1, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new DashboardControlBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH2, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new DashboardControlBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH3, this.grdOutputsNumHorizontal));
            this.outputsHNum.Add(new DashboardControlBuilder<UC_HorizontalProgressBar>(this.btnOutAnalogH4, this.grdOutputsNumHorizontal));
        }


        private void BuildInConfig<T>(List<DashboardControlBuilder<T>> inputs, DashboardConfiguration config) where T : UC_InputBase, IDashboardControl, new() {
            foreach (var input in inputs) {
                input.BuildConfig(config);
            }
        }


        private void BuildOutConfig<T>(List<DashboardControlBuilder<T>> outputs, DashboardConfiguration config) where T : UC_OutputBase, IDashboardControl, new() {
            foreach (var output in outputs) {
                output.BuildConfig(config);
            }
        }


        private void InitInput<T>(DashboardControlDataModel dm, List<DashboardControlBuilder<T>> ctrls) where T : UC_InputBase, new() {
            foreach (var input in ctrls) {
                if (input.GetRow() == dm.Row) {
                    input.AddExisting(dm.Row);
                }
            }
        }


        private void InitOutput<T>(DashboardControlDataModel dm, List<DashboardControlBuilder<T>> ctrls) where T : UC_OutputBase, new() {
            foreach (var ctrl in ctrls) {
                if (ctrl.GetRow() == dm.Row) {
                    ctrl.AddExisting(dm.Row);
                }
            }
        }

    }
}
