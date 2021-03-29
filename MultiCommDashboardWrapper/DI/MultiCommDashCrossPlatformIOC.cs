using DependencyInjectorFactory.Net;
using LanguageFactory.Net.interfaces;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardWrapper.Interfaces;
using MultiCommDashboardWrapper.StorageFactories;
using StorageFactory.Net.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.DI {

    public class MultiCommDashCrossPlatformIOC : ObjContainer {

        public MultiCommDashCrossPlatformIOC() : base() { }

        protected override void LoadCreators(
            Dictionary<Type, ObjCreator> instanceCreators, 
            Dictionary<Type, ObjCreator> singletonCreators) {

            // Instance creators

            // TODO - binary stack level 0
            //instanceCreators.Add(typeof(ICommStackLevel0), new ObjInstanceCreator(() => new CommStackLevel0()));

            singletonCreators.Add(typeof(ILangFactory), new ObjSingletonCreator(() => new SupportedLanguageFactory()));

            singletonCreators.Add(typeof(IStorageManagerFactory),
                new ObjSingletonCreator(() => new MultiCommDashboardsStorageFactory(this.GetObjSingleton<IStorageManagerSet>())));


        }
    }
}
