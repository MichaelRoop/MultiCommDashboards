using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardData.Storage {

    public class DashboardConfiguration {

        public string Name { get; set; } = string.Empty;
        public List<DashboardControlDataModel> InputsBool { get; set; } = new List<DashboardControlDataModel>();
        public List<DashboardControlDataModel> InputsNumericHorizontal { get; set; } = new List<DashboardControlDataModel>();
        public List<DashboardControlDataModel> InputsNumericVertical { get; set; } = new List<DashboardControlDataModel>();

        public List<DashboardControlDataModel> OutputsBool { get; set; } = new List<DashboardControlDataModel>();
        public List<DashboardControlDataModel> OutputsNumericHorizontal { get; set; } = new List<DashboardControlDataModel>();
        public List<DashboardControlDataModel> OutputsNumericVertical { get; set; } = new List<DashboardControlDataModel>();

    }
}
