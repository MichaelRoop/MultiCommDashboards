using ChkUtils.Net;
using MultiCommDashboardWrapper.Interfaces;
using StorageFactory.Net.interfaces;

namespace MultiCommDashboardWrapper.StorageFactories {

    public class MultiCommDashboardsStorageFactory : IStorageManagerFactory {

        private IStorageManagerSet set = null;


        public MultiCommDashboardsStorageFactory(IStorageManagerSet managers) {
            this.set = managers;
        }


        public IIndexedStorageManager<TData, TIndexExtraInfo> GetIndexedManager<TData, TIndexExtraInfo>()
            where TData : class
            where TIndexExtraInfo : class {
            // TODO
            //if (typeof(TData).Name == typeof(TerminatorDataModel).Name) {
            //    return this.set.Terminators as IIndexedStorageManager<TData, TIndexExtraInfo>;
            //}
            //else if (typeof(TData).Name == typeof(ScriptDataModel).Name) {
            //    return this.set.Scripts as IIndexedStorageManager<TData, TIndexExtraInfo>;
            //}
            //else if (typeof(TData).Name == typeof(WifiCredentialsDataModel).Name) {
            //    return this.set.WifiCred as IIndexedStorageManager<TData, TIndexExtraInfo>;
            //}
            //else if (typeof(TData).Name == typeof(SerialDeviceInfo).Name) {
            //    return this.set.Serial as IIndexedStorageManager<TData, TIndexExtraInfo>;
            //}
            //else if (typeof(TData).Name == typeof(EthernetParams).Name) {
            //    return this.set.Ethernet as IIndexedStorageManager<TData, TIndexExtraInfo>;
            //}
            //else if (typeof(TData).Name == typeof(BLECommandSetDataModel).Name) {
            //    return this.set.BLECommands as IIndexedStorageManager<TData, TIndexExtraInfo>;
            //}
            //// Add others

            return null;
        }


        public IIndexedStorageManager<TData, TIndexExtraInfo> GetIndexedManager<TData, TIndexExtraInfo>(string subDirectory)
            where TData : class
            where TIndexExtraInfo : class {
            IIndexedStorageManager<TData, TIndexExtraInfo> manager = this.GetIndexedManager<TData, TIndexExtraInfo>();
            manager.StorageSubDir = subDirectory;
            return manager;
        }

        public IIndexedStorageManager<TData, TIndexExtraInfo> GetIndexedManager<TData, TIndexExtraInfo>(string subDirectory, string indexName)
            where TData : class
            where TIndexExtraInfo : class {
            IIndexedStorageManager<TData, TIndexExtraInfo> manager = this.GetIndexedManager<TData, TIndexExtraInfo>(subDirectory);
            manager.IndexFileName = indexName;
            return manager;
        }


        public IStorageManager<T> GetManager<T>() where T : class {
            // TODO
            //if (typeof(T).Name == typeof(SettingItems).Name) {
            //    return this.set.Settings as IStorageManager<T>;
            //}
            ////else if(typeof(T).Name == typeof(TerminatorData).Name) {
            ////}
            WrapErr.ChkTrue(false, 9999, () => string.Format("No storage manager for type {0}", typeof(T).Name));
            return null;
        }


        public IStorageManager<T> GetManager<T>(string subDirectory) where T : class {
            IStorageManager<T> manager = this.GetManager<T>();
            manager.StorageSubDir = subDirectory;
            return manager;
        }


        public IStorageManager<T> GetManager<T>(string subDirectory, string defaultFileName) where T : class {
            IStorageManager<T> manager = this.GetManager<T>(subDirectory);
            manager.DefaultFileName = defaultFileName;
            return manager;
        }

    }
}
