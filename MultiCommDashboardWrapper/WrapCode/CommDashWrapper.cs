using DependencyInjectorFactory.Net.interfaces;
using LanguageFactory.Net.interfaces;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        #region Data

        private IObjContainer container = null;

        // Container creates on demand
        private ILangFactory _languages = null;

        #endregion

        public ILangFactory Languages { get { return this.LanguageInit(); } }


        public CommDashWrapper(IObjContainer container) {
            this.container = container;
        }


        public void Teardown() {
            this.LanguageTeardown();
        }

    }
}
