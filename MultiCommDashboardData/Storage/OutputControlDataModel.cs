using CommunicationStack.Net.Enumerations;

namespace MultiCommDashboardData.Storage {

    public class OutputControlDataModel {

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

        #endregion


    }

}
