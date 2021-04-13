using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardData.Storage {

    public class DashboardConfiguration {

        public string Name { get; set; } = string.Empty;
        public List<InputControlDataModel> InputsBool { get; set; } = new List<InputControlDataModel>();
        public List<InputControlDataModel> InputsNumericHorizontal { get; set; } = new List<InputControlDataModel>();
        public List<InputControlDataModel> InputsNumericVertical { get; set; } = new List<InputControlDataModel>();


    }
}
