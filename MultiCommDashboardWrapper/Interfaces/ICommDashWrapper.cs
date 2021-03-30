using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.data;
using LanguageFactory.Net.interfaces;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardData.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.Interfaces {

    #region Delegates used throughout the wrapper

    public delegate void OnErr(string msg);
    public delegate void OnErrTitle(string title, string msg);

    #endregion

    public interface ICommDashWrapper {

        #region Events

        event EventHandler<SupportedLanguage> LanguageChanged;
        event EventHandler<ErrReport> UnexpectedExceptionEvent;

        #endregion

        #region Properties

        ILangFactory Languages { get; }

        #endregion

        #region Utils methods

        /// <summary>Make sure everyting is shut down before exit</summary>
        void Teardown();

        #endregion

        #region Languages

        /// <summary>Get the message in the currently selected language</summary>
        /// <param name="code">The message code</param>
        /// <returns>The message in the current language</returns>
        string GetText(MsgCode code);


        /// <summary>Set a new current language and save to disk</summary>
        /// <param name="code">Language code</param>
        /// <param name="onError">Invoked on error</param>
        void SetLanguage(LangCode code, OnErr onError);

        #endregion

        #region Settings

        void GetSettings(Action<SettingsDataModel> onSuccess, OnErr onError);
        void SaveSettings(SettingsDataModel settings, Action onSuccess, OnErr onError);

        #endregion

    }
}
