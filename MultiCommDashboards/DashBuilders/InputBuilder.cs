using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.UserControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfHelperClasses.Core;

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

        #region Properties

        //public List<InputControlDataModel> DataModels {
        //    get {
        //        List<InputControlDataModel> list = new List<InputControlDataModel>();
        //        foreach (var control in Controls) {
        //            InputControlDataModel dm = control.StorageInfo;
        //            dm.Column -= COLUMN_OFFSET;
        //            list.Add(dm);
        //        }
        //        return list;
        //    }
        //}

        #endregion

        #region Constructors

        public InputBuilder() {
            this.SetType();
        }


        public InputBuilder(Button triggerControl, Grid grid) : this() {
            this.Init(triggerControl, grid);
        }

        #endregion

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


        public void Init(Button triggerControl, Grid grid) {
            //this.Reset();
            this.triggerControl = triggerControl;
            this.triggerControl.Click += triggerControl_Click;
            this.grid = grid;
            this.max = this.grid.ColumnDefinitions.Count - COLUMN_OFFSET;

            //this.grid.Drop += Grid_Drop;
            //this.grid.AllowDrop = true;
        }


        //public void Reset() {
        //    if (this.triggerControl != null) {
        //        // Need to disconnect?
        //        this.triggerControl.Click += triggerControl_Click;
        //    }

        //    //Would need to add the trigger event also
        //    foreach (T control in this.Controls) {
        //        control.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
        //    }
        //    this.Controls.Clear();
        //    this.nextColumn = COLUMN_OFFSET;
        //}


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


        #region Drag drop experiments

#if EVERFIGUREOUT

        private void Bt_MouseMove(object sender, MouseEventArgs e) {
            //https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/walkthrough-enabling-drag-and-drop-on-a-user-control?view=netframeworkdesktop-4.8
            //throw new NotImplementedException();
            if (e.LeftButton == MouseButtonState.Pressed) {
                UC_BoolToggle bt = sender as UC_BoolToggle;
                if (bt != null) {
                    //int i = 10;
                    var data = new DataObject(typeof(UC_BoolToggle), bt);
                    DragDrop.DoDragDrop(bt, data, DragDropEffects.Copy | DragDropEffects.Move);
                    //https://stackoverflow.com/questions/5727391/wpf-drag-and-drop-and-data-types
                }
            }
        }



        private void Grid_Drop(object sender, DragEventArgs e) {
            var obj = e.Data as DataObject;
            UC_BoolToggle bt = obj.GetData(typeof(UC_BoolToggle)) as UC_BoolToggle;
            if (bt != null) {
                this.grid.Children.Remove(bt);
                Grid g = sender as Grid;
                // TODO how to determine the location of the drop
                // Position relative to the grid where dropped
                Point p = e.GetPosition(g);

                // Would need to remove it from previous and set in in new area
                //Grid.SetColumn(bt, 4);
                //Grid.SetRow(bt, 1);

                g.InvalidateArrange();
                int col = Grid.GetColumn(bt);
                int row = Grid.GetRow(bt);

                //App.ShowMsgTitle("location", string.Format("X:{0} Y:{1}", p.X, p.Y));
                App.ShowMsgTitle("location", string.Format("Col:{0} Row:{1}", col, row));

                //Grid.SetColumn(bt, );
                //Grid.SetRow(bt, 1);
                // Cant get the drop location to complete work

                g.Children.Add(bt);

                //this.grid.Children.Add(bt);
                this.grid.InvalidateVisual();
            }
        }

#endif

        #endregion

    }
}
