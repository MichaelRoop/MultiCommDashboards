using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.data;
using LanguageFactory.Net.interfaces;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardWrapper.Interfaces;
using System;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        // Container creates on demand
        private ILangFactory __languages = null;

        public event EventHandler<SupportedLanguage> LanguageChanged;


        #region Public
        
        public ILangFactory Languages { 
            get { return this.LanguageInit(); } 
        }


        public string GetText(MsgCode code) {
            return this.Languages.CurrentLanguage.GetText(code);
        }


        public void SetLanguage(LangCode code, OnErr onError) {
            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    this.Languages.SetCurrentLanguage(code);
                    this.Load(
                        this.Settings, 
                        settings => {
                            settings.CurrentLanguage = code;
                            this.Save(this.Settings, settings, () => { }, onError);
                        }, 
                        onError);
                });
            });
        }

        #endregion

        #region Private

        /// <summary>Guaranteed load of language factory and set at last saved selection</summary>
        /// <returns>The language factory</returns>
        private ILangFactory LanguageInit() {
            if (this.__languages == null) {
                this.__languages = this.container.GetObjSingleton<ILangFactory>();
                this.Load(
                    this.Settings, 
                    dm => { this.__languages.SetCurrentLanguage(dm.CurrentLanguage); }, 
                    err => { });
                this.__languages.LanguageChanged += this.languageChanged;
            }
            return this.__languages;
        }


        private void LanguageTeardown() {
            if (this.__languages != null){
                this.__languages.LanguageChanged -= this.languageChanged; 
            }
        }

        #endregion

        #region Event handlers

        private void languageChanged(object sender, SupportedLanguage language) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 2000201, "Failure on Event_LanguageChanged", () => {
                // TODO Store change in settings

                this.LanguageChanged?.Invoke(sender, language);
            });
            this.RaiseIfException(report);
        }

        #endregion

    }
}
