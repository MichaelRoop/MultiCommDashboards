using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        private void LanguageTeardown() {
            this._languages.LanguageChanged -= this.languageChanged;
        }

    }
}
