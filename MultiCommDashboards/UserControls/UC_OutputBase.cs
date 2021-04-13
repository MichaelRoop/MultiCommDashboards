using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.Enumerations;
using LogUtils.Net;
using MultiCommDashboardData.Storage;
using System;
using System.Windows.Controls;

namespace MultiCommDashboards.UserControls {

    public abstract class UC_OutputBase : UserControl {

        #region Data

        private ClassLog log = new ClassLog("UC_InputBase");

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

        /// <summary>Row location in grid</summary>
        public int Row { get; set; } = 0;

        /// <summary>Column location in grid</summary>
        public int Column { get; set; } = 0;

        public OutputControlDataModel StorageInfo {
            get {
                return new OutputControlDataModel() {
                    Id = this.Id,
                    IOName = this.IOName,
                    DataType = this.DataType,
                    Minimum = this.Minimum,
                    Maximum = this.Maximum,
                    Row = this.Row,
                    Column = this.Column,
                };
            }
        }

        #endregion

        #region Constructors

        public UC_OutputBase() {
        }


        public UC_OutputBase(OutputControlDataModel data) {
            this.Init(data.Id, data.IOName, data.DataType, data.Minimum, data.Maximum);
            this.Row = data.Row;
            this.Column = data.Column;
        }

        #endregion

        #region Public
        // todo some way to process data in

        public bool IsMine(byte id) {
            return this.Id == id;
        }

        public void Process(BinaryMsgBool msg) {
            if (IsMine(msg.Id)) {
                this.DispatchDisplay(msg.Value == 0 ? 0 : 1);
            }
        }

        public void Process(BinaryMsgInt8 msg) {
            if (IsMine(msg.Id)) {
                this.DispatchDisplay(msg.Value);
            }
        }

        public void Process(BinaryMsgInt16 msg) {
            if (IsMine(msg.Id)) {
                this.DispatchDisplay(msg.Value);
            }
        }

        public void Process(BinaryMsgInt32 msg) {
            if (IsMine(msg.Id)) {
                this.DispatchDisplay(msg.Value);
            }
        }


        public void Process(BinaryMsgUInt8 msg) {
            if (IsMine(msg.Id)) {
                this.DispatchDisplay(msg.Value);
            }
        }

        public void Process(BinaryMsgUInt16 msg) {
            if (IsMine(msg.Id)) {
                this.DispatchDisplay(msg.Value);
            }
        }

        public void Process(BinaryMsgUInt32 msg) {
            if (IsMine(msg.Id)) {
                this.DispatchDisplay(msg.Value);
            }
        }

        public void Process(BinaryMsgFloat32 msg) {
            if (IsMine(msg.Id)) {
                // Check if we need to round precision
                this.DispatchDisplay(msg.Value);
            }
        }


        public void Init(byte id, string name, BinaryMsgDataType dataType, double min, double max) {
            this.Id = id;
            this.IOName = name;
            this.DataType = dataType;
            this.Minimum = min;
            this.Maximum = max;
            this.DoInit();
        }

        #endregion

        #region Abstract 

        protected abstract void DoInit();
        protected abstract void DoDisplay(double value);

        #endregion

        #region Private

        private void DispatchDisplay(double value) {
            Dispatcher.Invoke(() => {
                try {
                    this.DoDisplay(value);
                }
                catch (Exception ex) {
                    this.log.Exception(9999, "DispatchDisplay", "", ex);
                }
            });
        }

        #endregion
    }

}
