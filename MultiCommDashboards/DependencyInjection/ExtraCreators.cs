using BluetoothCommon.Net.interfaces;
using BluetoothRFCommUWP;
using DependencyInjectorFactory.Net;
using DependencyInjectorFactory.Net.interfaces;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboards.DependencyInjection {

    public class ExtraCreators : IObjExtraCreators {

        public Dictionary<Type, ObjCreator> InstanceCreators { get; } = new Dictionary<Type, ObjCreator>();

        public Dictionary<Type, ObjCreator> SingletonCreators { get; } = new Dictionary<Type, ObjCreator>();

        public ExtraCreators() {
            // Add singleton and other creators

            this.SingletonCreators.Add(
                typeof(IStorageManagerSet), new ObjSingletonCreator(() => new StorageManagerSet()));

            this.SingletonCreators.Add(
                typeof(IBTInterface), new ObjSingletonCreator(() => new BTRfCommUwp()));

            // From MultiCommTerminal
            //this.SingletonCreators.Add(
            //    typeof(IIconFactory), new ObjSingletonCreator(() => new WinIconFactory()));


            //this.SingletonCreators.Add(
            //    typeof(IBLETInterface), new ObjSingletonCreator(() => new BluetoothLEImplWin32Core()));

            //this.SingletonCreators.Add(
            //    typeof(IWifiInterface), new ObjSingletonCreator(() => new WifiImpleUwp()));

            //this.SingletonCreators.Add(
            //    typeof(ISerialInterface), new ObjSingletonCreator(() => new SerialImplUwp()));

            //this.SingletonCreators.Add(
            //    typeof(IEthernetInterface), new ObjSingletonCreator(() => new EthernetImplUwp()));

        }



    }
}
