using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.data;
using LanguageFactory.Net.interfaces;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardData.Storage;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        #region Public

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
            if (this._languages == null) {
                this._languages = this.container.GetObjSingleton<ILangFactory>();
                this.Load(
                    this.Settings, 
                    dm => { this._languages.SetCurrentLanguage(dm.CurrentLanguage); }, 
                    err => { });
                this._languages.LanguageChanged += this.languageChanged;
            }
            return this._languages;
        }


        private void LanguageTeardown() {
            this._languages.LanguageChanged -= this.languageChanged;
        }

        #endregion

    }
}
