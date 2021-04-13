using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardData.Storage {

    public class DashboardConfiguration {

        public string Name { get; set; } = string.Empty;
        public List<InputControlDataModel> InputsBool { get; set; } = new List<InputControlDataModel>();
        public List<InputControlDataModel> InputsNumericHorizontal { get; set; } = new List<InputControlDataModel>();
        public List<InputControlDataModel> InputsNumericVertical { get; set; } = new List<InputControlDataModel>();

        public List<OutputControlDataModel> OutputsBool { get; set; } = new List<OutputControlDataModel>();
        public List<OutputControlDataModel> OutputsNumericHorizontal { get; set; } = new List<OutputControlDataModel>();
        public List<OutputControlDataModel> OutputsNumericVertical { get; set; } = new List<OutputControlDataModel>();

    }
}
