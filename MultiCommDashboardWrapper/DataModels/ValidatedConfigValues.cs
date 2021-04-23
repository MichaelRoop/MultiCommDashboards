namespace MultiCommDashboardWrapper.DataModels {

    public class ValidatedConfigValues {

        /// <summary>The id that maps the control to the device input</summary>
        public byte Id { get; set; } = 0;

        /// <summary>User defined name for the control</summary>
        public string IOName { get; set; } = string.Empty;

        /// <summary>Minimum value for this control</summary>
        public double Minimum { get; set; } = 0;

        /// <summary>Maximum value for this control</summary>
        public double Maximum { get; set; } = 0;

        /// <summary>At what tick step interval is data to be sent</summary>
        public double SendAtStep { get; set; } = 0;

    }

}
