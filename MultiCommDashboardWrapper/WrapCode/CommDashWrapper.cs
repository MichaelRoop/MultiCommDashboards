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


        public ILangFactory Languages { 
            get {
                if (this._languages == null) {
                    this._languages = this.container.GetObjSingleton<ILangFactory>();
                    this._languages.LanguageChanged += this.languageChanged;
                }
                return this._languages;
            }
        }


        public CommDashWrapper(IObjContainer container) {
            this.container = container;
        }


        public void Teardown() {
            this.LanguageTeardown();
        }

    }
}
