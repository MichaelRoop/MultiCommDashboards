using CommunicationStack.Net.Enumerations;
using LogUtils.Net;
using MultiCommDashboardData.Storage;
using MultiCommDashboards.WindowObjs.Configs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MultiCommDashboards.UserControls {

    public abstract class UC_InputBase : UserControl, IDashboardControl {

        #region Data

        private ClassLog log = new ClassLog("UC_InputBase");
        protected Action<byte, BinaryMsgDataType, double> sendAction = null;

        #endregion

        #region Properties

        /// <summary>The id that maps the control to the device input</summary>
        public byte Id { get; set; } = 0;

        public string IOName { get; set; } = string.Empty;

        /// <summary>The data type represented by the control</summary>
        public BinaryMsgDataType DataType { get; set; } = BinaryMsgDataType.tyepUndefined;

        /// <summary>Minimum value for this control</summary>
        public double Minimum { get; set; } = 0;

        /// <summary>Maximum value for this control</summary>
        public double Maximum { get; set; } = 0;

        /// <summary>At what tick step interval is data to be sent</summary>
        public double SendAtStep { get; set; } = 0;

        /// <summary>Row location in grid</summary>
        public int Row { get; set; } = 0;

        /// <summary>Column location in grid</summary>
        public int Column { get; set; } = 0;

        public DashboardControlDataModel StorageInfo {
            get {
                return new DashboardControlDataModel() {
                    Id = this.Id,
                    IOName = this.IOName,
                    DataType = this.DataType,
                    Minimum = this.Minimum,
                    Maximum = this.Maximum,
                    SendAtStep = this.SendAtStep,
                    Row = this.Row,
                    Column = this.Column,
                };
            }
        }

        #endregion

        #region IDashboardControl events

        public event EventHandler DeleteRequest;

        #endregion

        #region Constructors

        public UC_InputBase() {
        }


        public UC_InputBase(DashboardControlDataModel data) {
            this.Update(data);
        }

        #endregion

        #region Public

        // Make abstract
        public abstract void SetEditState(bool onOff);


        /// <summary>Sets the action to raise when the value changes</summary>
        /// <param name="sendAction">The send action</param>
        public void SetSendAction(Action<byte, BinaryMsgDataType, double> sendAction) {
            this.sendAction = sendAction;
        }


        public void Update(DashboardControlDataModel dataModel) {
            this.Id = dataModel.Id;
            this.IOName = dataModel.IOName;
            this.DataType = dataModel.DataType;
            this.SendAtStep = dataModel.SendAtStep;
            this.Minimum = dataModel.Minimum;
            this.Maximum = dataModel.Maximum;
            this.Row = dataModel.Row;
            this.Column = dataModel.Column;
            this.DoInit();
        }


        #endregion

        #region Abstract 

        protected abstract void DoInit();
        protected abstract void OnValueChanged(double newValue);

        #endregion

        #region Protected

        protected void controlsValueChangedHandler(object sender, RoutedPropertyChangedEventArgs<double> args) {
            try {
                this.OnValueChanged(args.NewValue);
                this.sendAction?.Invoke(this.Id, this.DataType, args.NewValue);
            }
            catch (Exception ex) {
                this.log.Exception(9999, "OnSliderActionChanged", "", ex);
            }
        }


        protected void deleteClick(object sender, RoutedEventArgs e) {
            try {
                this.DeleteRequest?.Invoke(this, new EventArgs());
            }
            catch (Exception ex) {
                this.log.Exception(9999, "deleteClick", "", ex);
            }
        }


        protected void editClick(object sender, RoutedEventArgs e) {
            try {
                // TODO Sub this with the edit box eventually
                DashboardControlView.ShowBox(this, this.StorageInfo);
            }
            catch (Exception ex) {
                this.log.Exception(9999, "editClick", "", ex);
            }
        }

        #endregion

    }
}
