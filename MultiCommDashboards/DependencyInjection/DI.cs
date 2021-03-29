using DependencyInjectorFactory.Net.interfaces;
using MultiCommDashboardWrapper.DI;
using MultiCommDashboardWrapper.Interfaces;
using MultiCommDashboardWrapper.WrapCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboards.DependencyInjection {
    
    public static class DI {

        #region Data

        private static IObjContainer container = null;
        private static ICommDashWrapper wrapper = null;

        #endregion


        /// <summary>Get the full container of objects</summary>
        /// <returns>The container</returns>
        private static IObjContainer GetContainer() {
            if (DI.container == null) {
                DI.container = new MultiCommDashCrossPlatformIOC();
                DI.container.Initialise(new ExtraCreators());
            }
            return DI.container;
        }


        /// <summary>Shortcut to get the application code wrapper</summary>
        /// <returns>The multi comm code wrapper</returns>
        public static ICommDashWrapper W {
            get {
                if (DI.wrapper == null) {
                    DI.wrapper = new CommDashWrapper(DI.GetContainer());
                }
                return DI.wrapper;
            }
        }


    }
}
