using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.UserControls;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiCommDashboards.DashBuilders {

    public class OutputBuilder<T> where T : UC_OutputBase, new() {

        #region Data

        private enum OutputType {
            Undefined,
            Bool,
            Horizontal,
            Vertical
        };

        /// <summary>Column 0 has label, 1 has dummy object</summary>
        private const int COLUMN_OFFSET = 2;
        private int nextColumn = COLUMN_OFFSET;
        private int max = 1;
        private Grid grid;
        private Button triggerControl = null;
        private OutputType outType = OutputType.Undefined;
        private List<UC_OutputBase> Controls = new List<UC_OutputBase>();


        // bogus test id
        private byte testId = 0;

        #endregion

        #region Constructors

        public OutputBuilder() {
            this.SetType();
        }


        public OutputBuilder(Button triggerControl, Grid grid) : this() {
            this.Init(triggerControl, grid);
        }

        #endregion

        #region Public
        public void Init(Button triggerControl, Grid grid) {
            this.triggerControl = triggerControl;
            this.triggerControl.Click += triggerControl_Click;
            this.grid = grid;
            this.max = this.grid.ColumnDefinitions.Count - COLUMN_OFFSET;
        }


        public void BuildConfig(DashboardConfiguration config) {
            foreach (var control in Controls) {
                // Note. All the columns are over by 1 since 0 is occupied by event dummy
                OutputControlDataModel dm = control.StorageInfo;
                dm.Column -= COLUMN_OFFSET;
                switch (this.outType) {
                    case OutputType.Undefined:
                        break;
                    case OutputType.Bool:
                        config.OutputsBool.Add(dm);
                        break;
                    case OutputType.Horizontal:
                        config.OutputsNumericHorizontal.Add(dm);
                        break;
                    case OutputType.Vertical:
                        config.OutputsNumericVertical.Add(dm);
                        break;
                }
            }
        }


        #endregion

        #region Private
        private bool Add(int row) {
            if (this.nextColumn <= this.max) {
                this.testId++;
                UC_OutputBase bt = new T() {
                    Column = nextColumn,
                    Row = row,
                };
                // TODO - disable slider here

                // TODO Here open the Dialog for the ID and name, and data type if not bool
                if (typeof(T) == typeof(UC_BoolProgress)) {
                    // TODO - Dialog for bool
                    bt.Init(this.testId, string.Format("DigiIO_{0}", this.testId), BinaryMsgDataType.typeBool, 0, 1);
                }
                else {
                    // TODO - Dialog for others
                    bt.Init(this.testId, string.Format("DigiIO_{0}", this.testId), BinaryMsgDataType.typeUInt8, 0, 255);
                }

                Grid.SetRow(bt, row);
                Grid.SetColumn(bt, nextColumn);
                this.grid.Children.Add(bt);
                bt.MouseLeftButtonUp += Bt_MouseLeftButtonUp;
                this.Controls.Add(bt);
                nextColumn++;
                return true;
            }
            return false;
        }


        private void triggerControl_Click(object sender, System.Windows.RoutedEventArgs e) {
            if (sender is Button) {
                Button btn = (Button)sender;
                this.Add(Grid.GetRow(btn));
            }
        }


        private void Bt_MouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            UC_OutputBase control = sender as UC_OutputBase;
            control.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            this.Controls.Remove(control);
            this.grid.Children.Remove(control);

            // change the column in each of the list and reset in grid
            for (int i = 0; i < this.Controls.Count; i++) {
                control = this.Controls[i];
                control.Column = i + COLUMN_OFFSET;
                Grid.SetColumn(control, control.Column);
            }

            this.nextColumn--;
            if (this.nextColumn < COLUMN_OFFSET) {
                this.nextColumn = COLUMN_OFFSET;
            }
        }



        private void SetType() {
            if (typeof(T) == typeof(UC_BoolProgress)) {
                this.outType = OutputType.Bool;
            }
            else if (typeof(T) == typeof(UC_HorizontalProgressBar)) {
                this.outType = OutputType.Horizontal;
            }
            else if (typeof(T) == typeof(UC_VerticalProgressBar)) {
                this.outType = OutputType.Vertical;
            }

        }

        #endregion


    }

}
