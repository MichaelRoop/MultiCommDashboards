using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.UserControls;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiCommDashboards.DashBuilders {


    public class InputBuilder<T>  where T : UC_InputBase, new() {

        private List<UC_InputBase> Controls { get; set; } = new List<UC_InputBase>();
        private UC_InputBase triggerControl = null;

        public List<InputControlDataModel> DataModels {
            get {
                List<InputControlDataModel> list = new List<InputControlDataModel>();
                foreach (var control in Controls) {
                    // Note. All the columns are over by 1 since 0 is occupied by event dummy
                    InputControlDataModel dm = control.StorageInfo;
                    dm.Column -= 1;
                    list.Add(dm);
                }
                return list;
            }
        }


        // Always start at column 1. 0 reserved for Add
        private int nextColumn = 1;
        private int max = 1;
        private Grid grid;

        // bogus test id
        private byte testId = 0;


        public InputBuilder() {
        }


        public InputBuilder(int row, UC_InputBase triggerControl, Grid grid) {
            this.Init(row, triggerControl, grid);
        }



        private bool Add() {
            if (this.nextColumn <= max) {

                this.testId++;
                UC_InputBase bt = new T() {
                    Column = nextColumn,
                    Row = this.triggerControl.Row,
                };

                // TODO Here open the Dialog for the ID and name, and data type if not bool
                if (typeof(T) == typeof(UC_BoolToggle)) {
                    // TODO - Dialog for bool
                    bt.Init(this.testId, string.Format("DigiIO_{0}", this.testId), BinaryMsgDataType.typeBool, 1, 0, 1);
                }else {
                    // TODO - Dialog for others
                    bt.Init(this.testId, string.Format("DigiIO_{0}", this.testId), BinaryMsgDataType.typeUInt8, 1, 0, 255);
                }

                Grid.SetRow(bt, this.triggerControl.Row);
                Grid.SetColumn(bt, nextColumn);
                this.grid.Children.Add(bt);
                bt.MouseLeftButtonUp += Bt_MouseLeftButtonUp;
                this.Controls.Add(bt);
                nextColumn++;

                // TODO - turn on if I can ever figure out where it is dropped
                //bt.MouseMove += Bt_MouseMove;
                //bt.AllowDrop = true;
                return true;



            }
            return false;
        }


        public void Init(int row, UC_InputBase triggerControl, Grid grid) {
            //this.Reset();
            this.triggerControl = triggerControl;
            this.triggerControl.Row = row;
            this.triggerControl.Column = 0;
            this.triggerControl.SetAsAddDummy();
            this.triggerControl.MouseLeftButtonUp += this.dummyMouseLeftButtonUp;
            this.grid = grid;
            // The 0 column is reserved for the trigger Control
            this.max = this.grid.ColumnDefinitions.Count - 1;

            //this.grid.Drop += Grid_Drop;
            //this.grid.AllowDrop = true;
        }


        public void Reset() {
            if (this.triggerControl != null) {
                // Need to disconnect?
                this.triggerControl.MouseLeftButtonUp -= this.dummyMouseLeftButtonUp;
            }

            //Would need to add the trigger event also
            foreach (T control in this.Controls) {
                control.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            }
            this.Controls.Clear();
            this.nextColumn = 1;
        }


        private void dummyMouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            this.Add();
        }


        private void Bt_MouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            UC_InputBase control = sender as UC_InputBase;
            control.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            this.Controls.Remove(control);
            this.grid.Children.Remove(control);

            // change the column in each of the list and reset in grid
            for (int i = 0; i < this.Controls.Count; i++) {
                control = this.Controls[i];
                control.Column = i + 1; // Skip column where dummy is
                Grid.SetColumn(control, control.Column);
            }

            this.grid.InvalidateVisual();
            this.nextColumn--;
            if (this.nextColumn < 1) {
                this.nextColumn = 1;
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
