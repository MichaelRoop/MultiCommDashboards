using MultiCommDashboardData.Storage;
using MultiCommDashboardData.StorageIndex;
using MultiCommDashboardWrapper.Interfaces;
using StorageFactory.Net.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        #region Data

        private static string PDF_USER_MANUAL_DIR_AND_FILE = "Documents/MultiCommDashboardUserDocRelease.pdf";

        private readonly string APP_DIR = "MultiCommDashboards";
        private readonly string SETTINGS_DIR = "Settings";
        private readonly string SETTINGS_FILE = "MultiCommDashboardSettings.txt";

        private readonly string CONFIGURATIONS_DIR = "DashboardConfigs";
        private readonly string CONFIGURATIONS_INDEX_FILE = "DashboardConfigsIndex.txt";

        // Storage members
        private IStorageManagerFactory _storageFactory = null;
        private IStorageManager<SettingsDataModel> _settings = null;
        private IIndexedStorageManager<DashboardConfiguration, DashboardConfigIndexExtraInfo> __dashboardConfigs = null;

        #endregion

        #region Just in time object creation

        private IStorageManagerFactory StorageFactory {
            get {
                if (this._storageFactory == null) {
                    this._storageFactory = this.container.GetObjSingleton<IStorageManagerFactory>();
                }
                return this._storageFactory;
            }
        }


        private IStorageManager<SettingsDataModel> Settings {
            get {
                if (this._settings == null) {
                    this._settings =
                        this.StorageFactory.GetManager<SettingsDataModel>(this.DirPath(this.SETTINGS_DIR), this.SETTINGS_FILE);
                    this.AssureSettingsDefault(this._settings);
                }
                return this._settings;
            }
        }


        private IIndexedStorageManager<DashboardConfiguration, DashboardConfigIndexExtraInfo> Configurations {
            get {
                if (this.__dashboardConfigs == null) {
                    this.__dashboardConfigs =
                        this.StorageFactory.GetIndexedManager<DashboardConfiguration, DashboardConfigIndexExtraInfo>(
                            this.DirPath(this.CONFIGURATIONS_DIR), this.CONFIGURATIONS_INDEX_FILE);
                }
                return this.__dashboardConfigs;
            }
        }


        #endregion

        #region Assure default methods

        /// <summary>Create default settings if it does not exist</summary>
        private void AssureSettingsDefault(IStorageManager<SettingsDataModel> data) {
            if (!data.DefaultFileExists()) {
                data.WriteObjectToDefaultFile(new SettingsDataModel());
            }
        }

        #endregion

        #region Private

        private string DirPath(string subDir) {
            return Path.Combine(APP_DIR, subDir);
        }

        #endregion

    }

}
