using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.interfaces;
using LanguageFactory.Net.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.Interfaces {

    public interface ICommDashWrapper {

        #region Events

        event EventHandler<SupportedLanguage> LanguageChanged;
        event EventHandler<ErrReport> UnexpectedExceptionEvent;

        #endregion

        #region Properties

        ILangFactory Languages { get; }

        #endregion



        void Teardown();

    }
}
