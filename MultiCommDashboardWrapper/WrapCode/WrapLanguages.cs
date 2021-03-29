using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.interfaces;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {


        /// <summary>Guaranteed load of language factory and set at last saved selection</summary>
        /// <returns>The language factory</returns>
        private ILangFactory LanguageInit() {
            if (this._languages == null) {
                this._languages = this.container.GetObjSingleton<ILangFactory>();
                // TODO - at this point we can load stored language setting and set factory
                this._languages.LanguageChanged += this.languageChanged;
            }
            return this._languages;
        }


        private void LanguageTeardown() {
            this._languages.LanguageChanged -= this.languageChanged;
        }

    }
}
