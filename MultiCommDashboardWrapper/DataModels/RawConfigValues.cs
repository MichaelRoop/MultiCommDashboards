using CommunicationStack.Net.Enumerations;

namespace MultiCommDashboardWrapper.DataModels {

    /// <summary>To send edit configu values as a string group for validation</summary>
    public class RawConfigValues {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Step { get; set; }

        /// <summary>The data type represented by the control</summary>
        public BinaryMsgDataType DataType { get; set; } = BinaryMsgDataType.tyepUndefined;

    }
}
