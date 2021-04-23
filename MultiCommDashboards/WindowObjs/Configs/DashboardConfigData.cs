using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;

namespace MultiCommDashboards.WindowObjs.Configs {

    /// <summary>Holds the result of edit in the DashboardControlEdit dialog</summary>
    public class DashboardConfigData {

        /// <summary>Indicates if the Dialog save was chosed</summary>
        public bool SaveChanges { get; set; } = false;

        /// <summary>The edit result</summary>
        public DashboardControlDataModel DataModel { get; set; } = new DashboardControlDataModel();


        public DashboardConfigData() {
        }


        /// <summary>Constructor</summary>
        /// <param name="dataModel">Data model passed in with existing values</param>
        public DashboardConfigData(DashboardControlDataModel dataModel) {
            this.DataModel = dataModel;
        }


        /// <summary>Used to create a data model for the appropriate type</summary>
        /// <param name="dataType"></param>
        public DashboardConfigData(BinaryMsgDataType dataType) {
            this.DataModel = new DashboardControlDataModel() {
                DataType = dataType,
            };




        }

    }

}
