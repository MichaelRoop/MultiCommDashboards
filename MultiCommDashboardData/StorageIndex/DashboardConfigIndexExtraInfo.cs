using MultiCommDashboardData.Storage;

namespace MultiCommDashboardData.StorageIndex {

    /// <summary>Hold any extra display info for an item when index is loaded</summary>
    public class DashboardConfigIndexExtraInfo {
        // Demo only for now
        public int DigitalInputs { get; set; } = 0;
        public int AnalogInputs { get; set; } = 0;
        public int DigitalOutputs { get; set; } = 0;
        public int AnalogOutputs { get; set; } = 0;


        /// <summary>Default constructor required for deserialization</summary>
        public DashboardConfigIndexExtraInfo() { 
        }


        public DashboardConfigIndexExtraInfo(DashboardConfiguration config) {
            this.Update(config);
        }

        public void Update(DashboardConfiguration config) {
            // This is where we could harvest some extra info to put in the index extra info
            this.DigitalInputs = config.InputsBool.Count;
            this.AnalogInputs = config.InputsNumericHorizontal.Count;
            this.DigitalOutputs = config.OutputsBool.Count;
            this.AnalogOutputs = config.OutputsNumericHorizontal.Count;
        }

    }
}
