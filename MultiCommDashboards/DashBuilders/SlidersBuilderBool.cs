using MultiCommDashboards.UserControls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Windows;

namespace MultiCommDashboards.DashBuilders {


    public class SlidersBuilderBool {

        private class SliderHolder {
            public UC_BoolToggle Toggle { get; set; }
            public int Column { get; set; }
            public SliderHolder(int column, UC_BoolToggle toggle) {
                this.Column = column;
                this.Toggle = toggle;
            }
        }


        private List<SliderHolder> toggles = new List<SliderHolder>();
        // Always start at column 1. 0 reserved for Add
        private int nextColumn = 1;
        private int max = 1;
        private Grid grid;
        private int row = 0;

        // bogus test id
        private byte testId = 0;

        public bool Add() {
            if (this.nextColumn <= max) {

                // TODO Here open the Dialog for the ID and name
                this.testId++;
                UC_BoolToggle bt = new UC_BoolToggle();
                bt.Init(this.testId, string.Format("DigiIO_{0}", this.testId));
                Grid.SetRow(bt, this.row);
                Grid.SetColumn(bt, nextColumn);
                this.grid.Children.Add(bt);
                bt.MouseLeftButtonUp += Bt_MouseLeftButtonUp;
                this.toggles.Add(new SliderHolder(this.nextColumn, bt)); 
                nextColumn++;

                // TODO - turn on if I can ever figure out where it is dropped
                //bt.MouseMove += Bt_MouseMove;
                //bt.AllowDrop = true;
                return true;



            }
            return false;
        }


        public void Init(Grid grid, int row, int maxColumns) {
            this.Reset();
            this.grid = grid;
            this.row = row;
            this.max = maxColumns;

            //this.grid.Drop += Grid_Drop;
            //this.grid.AllowDrop = true;
        }


        public void Reset() {
            foreach (SliderHolder holder in this.toggles) {
                holder.Toggle.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            }
            this.toggles.Clear();
            this.nextColumn = 1;
        }


        private void Bt_MouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            UC_BoolToggle toggle = sender as UC_BoolToggle;
            toggle.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            this.toggles.RemoveAll(x => x.Toggle == toggle);
            this.grid.Children.Remove(toggle);

            // change the column in each of the list and reset in grid
            for (int i = 0; i < this.toggles.Count; i++) {
                SliderHolder h = this.toggles[i];
                h.Column = i + 1;
                Grid.SetColumn(h.Toggle, h.Column);
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
