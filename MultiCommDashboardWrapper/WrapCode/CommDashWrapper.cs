using DependencyInjectorFactory.Net.interfaces;
using LanguageFactory.Net.interfaces;
using LogUtils.Net;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        #region Data

        private ClassLog log = new ClassLog("CommDashWrapper");

        private IObjContainer container = null;

        #endregion


        public CommDashWrapper(IObjContainer container) {
            this.container = container;
        }


        public void Teardown() {
            try {
                this.LanguageTeardown();
                this.BTTeardown();
            }
            catch(Exception ex) {
                this.log.Exception(9999, "", ex);
            }
        }

    }
}
