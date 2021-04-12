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

        private List<UC_BoolToggle> toggles = new List<UC_BoolToggle>();
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
                this.toggles.Add(bt);
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
            foreach (UC_BoolToggle toggle in this.toggles) {
                toggle.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            }
            this.toggles.Clear();
            this.nextColumn = 1;
        }


        private void Bt_MouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            UC_BoolToggle bt = sender as UC_BoolToggle;
            bt.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            this.toggles.Remove(bt);
            this.grid.Children.Remove(bt);

            // Have to cast them as UIElement since some contain Border and will crash
            // They are not filtered before the cast is applied
            var children = this.grid.Children.Cast<UIElement>().Where(
                x => Grid.GetRow(x) == this.row && Grid.GetColumn(x) > 0).ToList();

            for (int i = 0; i < children.Count(); i++) {
                UC_BoolToggle b = children[i] as UC_BoolToggle;
                Grid.SetColumn(b, i+1);
            }
            this.grid.InvalidateVisual();
            this.nextColumn--;
            if (this.nextColumn < 1) {
                this.nextColumn = 1;
            }
        }

    }
}
