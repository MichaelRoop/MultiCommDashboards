using MultiCommDashboardData.Storage;
using MultiCommDashboardData.StorageIndex;
using MultiCommDashboardWrapper.Interfaces;
using StorageFactory.Net.interfaces;
using StorageFactory.Net.Serializers;
using StorageFactory.Net.StorageManagers;

namespace MultiCommDashboards.DependencyInjection {

    public class StorageManagerSet : IStorageManagerSet {

        #region Data

        private IStorageManager<SettingsDataModel> settings =
            new SimpleStorageManger<SettingsDataModel>(new JsonReadWriteSerializerIndented<SettingsDataModel>());


        /// <summary>Singleton controls configuration indexed storage</summary>
        private IIndexedStorageManager<DashboardConfiguration, DashboardConfigIndexExtraInfo> configurationsStorage =
            new IndexedStorageManager<DashboardConfiguration, DashboardConfigIndexExtraInfo>(
                new JsonReadWriteSerializerIndented<DashboardConfiguration>(),
                new JsonReadWriteSerializerIndented<IIndexGroup<DashboardConfigIndexExtraInfo>>());

        #endregion

        // The properties from the interface will load the variable

        public IStorageManager<SettingsDataModel> Settings { get { return this.settings; } }

        public IIndexedStorageManager<DashboardConfiguration, DashboardConfigIndexExtraInfo> Configurations { get { return this.configurationsStorage; } }

    }

}
