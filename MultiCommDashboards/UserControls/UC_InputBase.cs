using CommunicationStack.Net.Enumerations;
using LogUtils.Net;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MultiCommDashboards.UserControls {

    public abstract class UC_InputBase : UserControl {

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

        #endregion

        #region Constructors

        public UC_InputBase() {
        }

        #endregion

        #region Public

        /// <summary>Sets the action to raise when the value changes</summary>
        /// <param name="sendAction">The send action</param>
        public void SetSendAction(Action<byte, BinaryMsgDataType, double> sendAction) {
            this.sendAction = sendAction;
        }

        public void Init(byte id, string name, BinaryMsgDataType dataType, double step, double min, double max) {
            this.Id = id;
            this.IOName = name;
            this.DataType = dataType;
            this.SendAtStep = step;
            this.Minimum = min;
            this.Maximum = max;
            this.DoInit();
        }

        #endregion

        #region Abstract 

        protected abstract void DoInit();
        protected abstract void OnValueChanged(double newValue);

        public virtual void SetTrueFalseTranslators(Func<bool, string> func) {
            // Only used for bool data values
        }

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

        #endregion

    }
}
