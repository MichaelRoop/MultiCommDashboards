using MultiCommDashboardData.Storage;
using MultiCommDashboardWrapper.Interfaces;
using StorageFactory.Net.interfaces;
using StorageFactory.Net.Serializers;
using StorageFactory.Net.StorageManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboards.DependencyInjection {

    public class StorageManagerSet : IStorageManagerSet {

        #region Data

        private IStorageManager<SettingsDataModel> settings =
            new SimpleStorageManger<SettingsDataModel>(new JsonReadWriteSerializerIndented<SettingsDataModel>());


        ///// <summary>Singleton terminator indexed storage</summary>
        //private IIndexedStorageManager<TerminatorDataModel, DefaultFileExtraInfo> terminatorStorage =
        //    new IndexedStorageManager<TerminatorDataModel, DefaultFileExtraInfo>(
        //        new JsonReadWriteSerializerIndented<TerminatorDataModel>(),
        //        new JsonReadWriteSerializerIndented<IIndexGroup<DefaultFileExtraInfo>>());

        //private IIndexedStorageManager<ScriptDataModel, DefaultFileExtraInfo> scriptStorage =
        //    new IndexedStorageManager<ScriptDataModel, DefaultFileExtraInfo>(
        //        new JsonReadWriteSerializerIndented<ScriptDataModel>(),
        //        new JsonReadWriteSerializerIndented<IIndexGroup<DefaultFileExtraInfo>>());

        //private IIndexedStorageManager<BLECommandSetDataModel, BLECmdIndexExtraInfo> bleCommandsStorage =
        //    new IndexedStorageManager<BLECommandSetDataModel, BLECmdIndexExtraInfo>(
        //        new JsonReadWriteSerializerIndented<BLECommandSetDataModel>(),
        //        new JsonReadWriteSerializerIndented<IIndexGroup<BLECmdIndexExtraInfo>>());


        ///// <summary>Encrypted storage for the WIFI credentials</summary>
        //private IIndexedStorageManager<WifiCredentialsDataModel, DefaultFileExtraInfo> wifiCredStorage =
        //    new IndexedStorageManager<WifiCredentialsDataModel, DefaultFileExtraInfo>(
        //        new EncryptingReadWriteSerializer<WifiCredentialsDataModel>(),
        //        new EncryptingReadWriteSerializer<IIndexGroup<DefaultFileExtraInfo>>());


        //private IIndexedStorageManager<SerialDeviceInfo, SerialIndexExtraInfo> serialStorage =
        //    new IndexedStorageManager<SerialDeviceInfo, SerialIndexExtraInfo>(
        //        new JsonReadWriteSerializerIndented<SerialDeviceInfo>(),
        //        new JsonReadWriteSerializerIndented<IIndexGroup<SerialIndexExtraInfo>>());

        //private IIndexedStorageManager<EthernetParams, EthernetExtraInfo> ethernetStorage =
        //    new IndexedStorageManager<EthernetParams, EthernetExtraInfo>(
        //        new JsonReadWriteSerializerIndented<EthernetParams>(),
        //        new JsonReadWriteSerializerIndented<IIndexGroup<EthernetExtraInfo>>());


        #endregion

        // The properties from the interface will load the variable

        // Example
        public IStorageManager<SettingsDataModel> Settings { get { return this.settings; } }

    }

}
