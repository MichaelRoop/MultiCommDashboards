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
                bt.InitAsBool(this.testId, string.Format("DigiIO_{0}", this.testId));
                Grid.SetRow(bt, this.row);
                Grid.SetColumn(bt, nextColumn);
                this.grid.Children.Add(bt);
                bt.MouseLeftButtonUp += Bt_MouseLeftButtonUp;
                this.toggles.Add(new SliderHolder(this.nextColumn, bt)); 
                nextColumn++;
                return true;
            }
            return false;
        }


        public void Init(Grid grid, int row, int maxColumns) {
            this.Reset();
            this.grid = grid;
            this.row = row;
            this.max = maxColumns;
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

    }
}
