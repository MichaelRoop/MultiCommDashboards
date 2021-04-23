using MultiCommDashboardData.Interfaces;
using System;
using System.Collections.Generic;

namespace MultiCommDashboardData.Storage {

    public class DashboardConfiguration : IDisplayable, IIndexible {

        #region IDisplayable Properties
        public string Display { get; set; } = string.Empty;

        #endregion

        #region IIndexible Properties

        public string UId { get; set; }

        #endregion

        #region Properties

        public List<DashboardControlDataModel> InputsBool { get; set; } = new List<DashboardControlDataModel>();
        public List<DashboardControlDataModel> InputsNumericHorizontal { get; set; } = new List<DashboardControlDataModel>();
        //public List<DashboardControlDataModel> InputsNumericVertical { get; set; } = new List<DashboardControlDataModel>();

        public List<DashboardControlDataModel> OutputsBool { get; set; } = new List<DashboardControlDataModel>();
        public List<DashboardControlDataModel> OutputsNumericHorizontal { get; set; } = new List<DashboardControlDataModel>();
        //public List<DashboardControlDataModel> OutputsNumericVertical { get; set; } = new List<DashboardControlDataModel>();

        #endregion

        public DashboardConfiguration() {
            this.UId = Guid.NewGuid().ToString();
        }

    }
}
