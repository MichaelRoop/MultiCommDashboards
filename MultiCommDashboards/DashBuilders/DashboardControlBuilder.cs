using MultiCommDashboardData.Storage;
using MultiCommDashboards.UserControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiCommDashboards.DashBuilders {

    /// <summary>Used to add, edit, delete controls in Dashboard Editor</summary>
    /// <typeparam name="T">The Control type it manages</typeparam>
    public class DashboardControlBuilder<T> where T:UIElement, IDashboardControl, new() {

        #region Data

        private enum ControlType {
            Undefined,
            InBool,
            InHorizontal,
            InVertical,
            OutBool,
            OutHorizontal,
            OutVertical,
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
                    case ControlType.OutVertical:
                        config.OutputsNumericVertical.Add(dm);
                        break;
                    case ControlType.InBool:
                        config.InputsBool.Add(dm);
                        break;
                    case ControlType.InHorizontal:
                        config.InputsNumericHorizontal.Add(dm);
                        break;
                    case ControlType.InVertical:
                        config.InputsNumericVertical.Add(dm);
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


        // TODO TEMP - REMOVE
        int count = 1;
        string nameBase = string.Empty;

        private bool Add(int row) {
            if (this.nextColumn <= this.max) {
                T control = new T();
                control.Column = nextColumn;
                control.Row = row;

                //---------------------------------------------------
                // TODO REMOVE - REPLACE WITH DIALOG
                DashboardControlDataModel dm = control.StorageInfo;
                dm.IOName = string.Format("{0}{1}", this.nameBase, count);
                control.Update(dm);
                //---------------------------------------------------

                Grid.SetRow(control, row);
                Grid.SetColumn(control, nextColumn);
                this.grid.Children.Add(control);
                control.MouseLeftButtonUp += this.controlMouseLeftButtonUp;
                this.Controls.Add(control);
                nextColumn++;
                return true;
            }
            return false;
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
            else if (typeof(T) == typeof(UC_VerticalSlider)) {
                this.nameBase = "";
                this.controlType = ControlType.InVertical;
            }
            else if (typeof(T) == typeof(UC_BoolProgress)) {
                this.nameBase = "Dig Out:";
                this.controlType = ControlType.OutBool;
            }
            else if (typeof(T) == typeof(UC_HorizontalProgressBar)) {
                this.nameBase = "Num Out:";
                this.controlType = ControlType.OutHorizontal;
            }
            else if (typeof(T) == typeof(UC_VerticalProgressBar)) {
                this.nameBase = "";
                this.controlType = ControlType.OutVertical;
            }
        }


        /// <summary>When user clicks on one of the objects in the Editor</summary>
        private void controlMouseLeftButtonUp(object sender, MouseButtonEventArgs args) {
            T control = sender as T;
            if (control != null) {
                if (!this.ProcessClick(control)) {
                    control.MouseLeftButtonUp -= this.controlMouseLeftButtonUp;
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
        }


        private bool ProcessClick(T control) {
            //---------------------------------------------------
            // TODO This is where we would launch the edit dialogs with edit or delete options
            
            //---------------------------------------------------
            // False means delete
            return false;
        }

        #endregion


    }
}
