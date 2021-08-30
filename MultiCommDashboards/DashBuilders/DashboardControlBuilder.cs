﻿using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.UserControls;
using MultiCommDashboards.WindowObjs.Configs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MultiCommDashboards.DashBuilders {

    /// <summary>Used to add, edit, delete controls in Dashboard Editor</summary>
    /// <typeparam name="T">The Control type it manages</typeparam>
    public class DashboardControlBuilder<T> where T:UIElement, IDashboardControl, new() {

        #region Data

        private enum ControlType {
            Undefined,
            InBool,
            InHorizontal,
            OutBool,
            OutHorizontal,
        };

        // Always start at column 2, 0 for Digital label, 1 for Add button
        private const int COLUMN_OFFSET = 2;
        private int nextColumn = COLUMN_OFFSET;
        private int max = 1;
        private Grid grid;
        private Button addButton = null;
        private ControlType controlType = ControlType.Undefined;
        private List<T> Controls = new List<T>();

        #endregion

        #region Constructors

        public DashboardControlBuilder() {
            this.SetType();
        }


        public DashboardControlBuilder(Button addButton, Grid grid) : this() {
            this.Init(addButton, grid);
        }

        #endregion

        #region Public

        /// <summary>Add managed control config data to the general config</summary>
        /// <param name="config">The general config object</param>
        public void BuildConfig(DashboardConfiguration config) {
            foreach (var control in Controls) {
                // Note. All the columns are over by 1 since 0 is occupied by event dummy
                DashboardControlDataModel dm = control.StorageInfo;
                dm.Column -= COLUMN_OFFSET;
                switch (this.controlType) {
                    case ControlType.Undefined:
                        break;
                    case ControlType.OutBool:
                        config.OutputsBool.Add(dm);
                        break;
                    case  ControlType.OutHorizontal:
                        config.OutputsNumericHorizontal.Add(dm);
                        break;
                    case ControlType.InBool:
                        config.InputsBool.Add(dm);
                        break;
                    case ControlType.InHorizontal:
                        config.InputsNumericHorizontal.Add(dm);
                        break;
                }
            }
        }

        #endregion

        #region Private

        private void Init(Button addButton, Grid grid) {
            this.addButton = addButton;
            this.addButton.Click += this.addButtonClick;
            this.grid = grid;
            this.max = this.grid.ColumnDefinitions.Count - COLUMN_OFFSET;
        }


        private void addButtonClick(object sender, System.Windows.RoutedEventArgs e) {
            if (sender is Button) {
                Button btn = (Button)sender;
                this.Add(Grid.GetRow(btn));
            }
        }

        public int GetRow() {
            return Grid.GetRow(this.addButton);
        }


        public void AddExisting(DashboardControlDataModel dm) {
            T control = this.CreateControl(dm.Row);
            if (control != null) {
                this.InsertControl(control, dm, dm.Row);
            }
        }


        // TODO TEMP - REMOVE
        string nameBase = string.Empty;

        private bool Add(int row) {
            T control = this.CreateControl(row);
            if (control != null) {
                var dm = DashboardControlEdit.ShowBox(null, control.StorageInfo);
                if (dm == null) {
                    return false;
                }
                this.InsertControl(control, dm, dm.Row);
                return true;
            }
            return false;
        }


        private T CreateControl(int row) {
            if (this.nextColumn <= this.max) {
                T control = new T();
                control.Column = this.nextColumn;
                control.Row = row;
                control.SetEditState(true);
                this.SetCtrlType(ref control);
                return control;
            }
            return null;
        }


        private void SetCtrlType(ref T control) {
            if (this.controlType == ControlType.InBool || this.controlType == ControlType.OutBool) {
                control.StorageInfo.DataType = BinaryMsgDataType.typeBool;
                control.DataType = BinaryMsgDataType.typeBool;
                // TODO - determine why set both
            }
        }


        private void InsertControl(T control, DashboardControlDataModel dm, int row) {
            control.Update(dm);
            Grid.SetRow(control, row);
            Grid.SetColumn(control, this.nextColumn);
            this.grid.Children.Add(control);
            control.DeleteRequest += this.deleteRequest;
            this.Controls.Add(control);
            this.nextColumn++;
        }


        private void SetType() {
            if (typeof(T) == typeof(UC_BoolToggle)) {
                this.nameBase = "Dig In:";
                this.controlType = ControlType.InBool;
            }
            else if (typeof(T) == typeof(UC_HorizontalSlider)) {
                this.nameBase = "Num In:";
                this.controlType = ControlType.InHorizontal;
            }
            else if (typeof(T) == typeof(UC_BoolProgress)) {
                this.nameBase = "Dig Out:";
                this.controlType = ControlType.OutBool;
            }
            else if (typeof(T) == typeof(UC_HorizontalProgressBar)) {
                this.nameBase = "Num Out:";
                this.controlType = ControlType.OutHorizontal;
            }
        }


        private void deleteRequest(object sender, EventArgs e) {
            T control = sender as T;
            if (control != null) {
                control.DeleteRequest -= this.deleteRequest;
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
        }


        #endregion


    }
}
