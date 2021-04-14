﻿using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.UserControls;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiCommDashboards.DashBuilders {

    public class OutputBuilder<T> where T : UC_OutputBase, new() {

        #region Data

        // Always start at column 1. 0 reserved for Add
        private int nextColumn = 1;
        private int max = 1;
        private Grid grid;
        UC_OutputBase triggerControl = null;

        // bogus test id
        private byte testId = 0;

        #endregion

        #region Properties

        private List<UC_OutputBase> Controls { get; set; } = new List<UC_OutputBase>();


        public List<OutputControlDataModel> DataModels {
            get {
                List<OutputControlDataModel> list = new List<OutputControlDataModel>();
                foreach (var control in Controls) {
                    // Note. All the columns are over by 1 since 0 is occupied by event dummy
                    OutputControlDataModel dm = control.StorageInfo;
                    dm.Column -= 1;
                    list.Add(dm);
                }
                return list;
            }
        }

        #endregion

        #region Constructors

        public OutputBuilder() {
        }


        public OutputBuilder(int row, UC_OutputBase triggerControl, Grid grid) {
            this.Init(row, triggerControl, grid);
        }

        #endregion

        #region Public

        private bool Add() {
            if (this.nextColumn <= this.max) {
                this.testId++;
                UC_OutputBase bt = new T() {
                    Column = nextColumn,
                    Row = this.triggerControl.Row,
                };

                // TODO Here open the Dialog for the ID and name, and data type if not bool
                if (typeof(T) == typeof(UC_BoolProgress)) {
                    // TODO - Dialog for bool
                    bt.Init(this.testId, string.Format("DigiIO_{0}", this.testId), BinaryMsgDataType.typeBool, 0, 1);
                }
                else {
                    // TODO - Dialog for others
                    bt.Init(this.testId, string.Format("DigiIO_{0}", this.testId), BinaryMsgDataType.typeUInt8, 0, 255);
                }

                Grid.SetRow(bt, this.triggerControl.Row);
                Grid.SetColumn(bt, nextColumn);
                this.grid.Children.Add(bt);
                bt.MouseLeftButtonUp += Bt_MouseLeftButtonUp;
                this.Controls.Add(bt);
                nextColumn++;
                return true;
            }
            return false;
        }


        public void Init(int row, UC_OutputBase triggerControl, Grid grid) {
            //this.Reset();
            this.triggerControl = triggerControl;
            this.triggerControl.Row = row;
            this.triggerControl.Column = 0;
            this.triggerControl.SetAsAddDummy();
            this.triggerControl.MouseLeftButtonUp += this.dummyMouseLeftButtonUp;
            this.grid = grid;
            // The 0 column is reserved for the trigger Control
            this.max = this.grid.ColumnDefinitions.Count - 1;
        }


        public void Reset() {
            if (this.triggerControl != null) {
                // Need to disconnect?
                this.triggerControl.MouseLeftButtonUp -= this.dummyMouseLeftButtonUp;
            }

            foreach (T control in this.Controls) {
                control.MouseLeftButtonUp -= this.Bt_MouseLeftButtonUp;
            }
            this.Controls.Clear();
            this.nextColumn = 1;
        }

        #endregion

        #region Private

        private void Bt_MouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            UC_OutputBase control = sender as UC_OutputBase;
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


        private void dummyMouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            this.Add();
        }

        #endregion


    }

}
