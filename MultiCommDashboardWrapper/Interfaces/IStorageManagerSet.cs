using MultiCommDashboardData.Storage;
using MultiCommDashboardData.StorageIndex;
using StorageFactory.Net.interfaces;

namespace MultiCommDashboardWrapper.Interfaces {

    public interface IStorageManagerSet {

        /// <summary>Storage for current settings</summary>
        IStorageManager<SettingsDataModel> Settings { get; }

        /// <summary>Storage for Dashboard configurations</summary>
        IIndexedStorageManager<DashboardConfiguration, DashboardConfigIndexExtraInfo> Configurations { get; }

    }
}
