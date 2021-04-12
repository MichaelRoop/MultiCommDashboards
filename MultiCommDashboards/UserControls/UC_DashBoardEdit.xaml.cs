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

        //InputBuilder<UC_BoolToggle> inputB0 = new InputBuilder<UC_BoolToggle>();
        //InputBuilder<UC_BoolToggle> inputB1 = new InputBuilder<UC_BoolToggle>();
        //InputBuilder<UC_BoolToggle> inputB2 = new InputBuilder<UC_BoolToggle>();
        //InputBuilder<UC_BoolToggle> inputB3 = new InputBuilder<UC_BoolToggle>();

        //InputBuilder<UC_HorizontalSlider> inputH0 = new InputBuilder<UC_HorizontalSlider>();
        //InputBuilder<UC_HorizontalSlider> inputH1 = new InputBuilder<UC_HorizontalSlider>();
        //InputBuilder<UC_HorizontalSlider> inputH2 = new InputBuilder<UC_HorizontalSlider>();
        //InputBuilder<UC_HorizontalSlider> inputH3 = new InputBuilder<UC_HorizontalSlider>();
        //InputBuilder<UC_HorizontalSlider> inputH4 = new InputBuilder<UC_HorizontalSlider>();
        //InputBuilder<UC_HorizontalSlider> inputH5 = new InputBuilder<UC_HorizontalSlider>();

        //InputBuilder<UC_VerticalSlider> inputV0 = new InputBuilder<UC_VerticalSlider>();
        //InputBuilder<UC_VerticalSlider> inputV1 = new InputBuilder<UC_VerticalSlider>();
        //InputBuilder<UC_VerticalSlider> inputV2 = new InputBuilder<UC_VerticalSlider>();

        List<InputBuilder<UC_BoolToggle>> inputsBool = new List<InputBuilder<UC_BoolToggle>>();
        List<InputBuilder<UC_HorizontalSlider>> inputsHNum = new List<InputBuilder<UC_HorizontalSlider>>();
        List<InputBuilder<UC_VerticalSlider>> inputsVNum = new List<InputBuilder<UC_VerticalSlider>>();

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

            //this.inputB0.Init(this.grdInputsBool, 0, 10);
            //this.inputB1.Init(this.grdInputsBool, 1, 10);
            //this.inputB2.Init(this.grdInputsBool, 2, 10);
            //this.inputB3.Init(this.grdInputsBool, 3, 10);

            //this.inputH0.Init(this.grdHorizontalInputs, 0, 4);
            //this.inputH1.Init(this.grdHorizontalInputs, 1, 4);
            //this.inputH2.Init(this.grdHorizontalInputs, 2, 4);
            //this.inputH3.Init(this.grdHorizontalInputs, 3, 4);
            //this.inputH4.Init(this.grdHorizontalInputs, 4, 4);
            //this.inputH5.Init(this.grdHorizontalInputs, 5, 4);

            //this.inputV0.Init(this.grdInputVNumeric, 0, 10);
            //this.inputV1.Init(this.grdInputVNumeric, 1, 10);
            //this.inputV2.Init(this.grdInputVNumeric, 2, 10);

        }


        public void Init() {


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

    }
}
