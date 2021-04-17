using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.UserControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiCommDashboards.DashBuilders {


    public class InputBuilder<T>  where T : UC_InputBase, new() {

        #region Data

        private enum InputType {
            Undefined,
            Bool,
            Horizontal,
            Vertical
        };


        // Always start at column 2, 0 for Digital label, 1 for Add button
        private const int COLUMN_OFFSET = 2;
        private int nextColumn = COLUMN_OFFSET;
        private int max = 1;
        private Grid grid;
        private Button triggerControl = null;
        private InputType inType = InputType.Undefined;
        private List<UC_InputBase> Controls = new List<UC_InputBase>();

        // bogus test id
        private byte testId = 0;

        #endregion

        #region Constructors

        public InputBuilder() {
            this.SetType();
        }


        public InputBuilder(Button triggerControl, Grid grid) : this() {
            this.Init(triggerControl, grid);
        }

        #endregion

        #region Public

        public void BuildConfig(DashboardConfiguration config) {
            foreach (var control in Controls) {
                InputControlDataModel dm = control.StorageInfo;
                dm.Column -= COLUMN_OFFSET;
                switch (this.inType) {
                    case InputType.Undefined:
                        break;
                    case InputType.Bool:
                        config.InputsBool.Add(dm);
                        break;
                    case InputType.Horizontal:
                        config.InputsNumericHorizontal.Add(dm);
                        break;
                    case InputType.Vertical:
                        config.InputsNumericVertical.Add(dm);
                        break;
                }
            }
        }


        public void Init(Button triggerControl, Grid grid) {
            this.triggerControl = triggerControl;
            this.triggerControl.Click += triggerControl_Click;
            this.grid = grid;
            this.max = this.grid.ColumnDefinitions.Count - COLUMN_OFFSET;
        }

        #endregion

        #region Private

        private bool Add(int row) {
            if (this.nextColumn <= this.max) {
                this.testId++;
                UC_InputBase bt = new T() {
                    Column = nextColumn,
                    Row = row,
                };
                bt.SetSliderEnabled(false);

                //// TODO Here open the Dialog for the ID and name, and data type if not bool
                if (typeof(T) == typeof(UC_BoolToggle)) {
                    // TODO - Dialog for bool
                    bt.Init(this.testId, string.Format("DigiIO_{0}", this.testId), BinaryMsgDataType.typeBool, 1, 0, 1);
                }
                else {
                    // TODO - Dialog for others
                    bt.Init(this.testId, string.Format("DigiIO_{0}", this.testId), BinaryMsgDataType.typeUInt8, 1, 0, 255);
                }

                Grid.SetRow(bt, row);
                Grid.SetColumn(bt, nextColumn);
                this.grid.Children.Add(bt);
                bt.MouseLeftButtonUp += Bt_MouseLeftButtonUp;
                this.Controls.Add(bt);
                this.grid.InvalidateVisual();
                nextColumn++;

                // TODO - turn on if I can ever figure out where it is dropped
                //bt.MouseMove += Bt_MouseMove;
                //bt.AllowDrop = true;
                return true;

            }
            return false;
        }


        private void triggerControl_Click(object sender, RoutedEventArgs e) {
            if (sender is Button) {
                Button btn = (Button)sender;
                this.Add(Grid.GetRow(btn));
            }
        }


        private void Bt_MouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            UC_InputBase control = sender as UC_InputBase;
            control.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            this.Controls.Remove(control);
            this.grid.Children.Remove(control);

            // change the column in each of the list and reset in grid
            for (int i = 0; i < this.Controls.Count; i++) {
                control = this.Controls[i];
                control.Column = i + COLUMN_OFFSET;
                Grid.SetColumn(control, control.Column);
            }

            this.grid.InvalidateVisual();
            this.nextColumn--;
            if (this.nextColumn < COLUMN_OFFSET) {
                this.nextColumn = COLUMN_OFFSET;
            }
        }


        private void SetType() {
            if (typeof(T) == typeof(UC_BoolToggle)) {
                this.inType = InputType.Bool;
            }
            else if (typeof(T) == typeof(UC_HorizontalSlider)) {
                this.inType = InputType.Horizontal;
            }
            else if (typeof(T) == typeof(UC_VerticalSlider)) {
                this.inType = InputType.Vertical;
            }
        }

        #endregion
    }
}
