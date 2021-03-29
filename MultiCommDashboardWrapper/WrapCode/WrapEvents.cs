using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        public event EventHandler<SupportedLanguage> LanguageChanged;


        public event EventHandler<ErrReport> UnexpectedExceptionEvent;


        private void RaiseIfException(ErrReport report) {
            if (report.Code != 0) {
                WrapErr.ToErrReport(9999, "Error raising unexpected exception event", () => {
                    this.UnexpectedExceptionEvent?.Invoke(this, report);
                });
            }
        }



        private void languageChanged(object sender, SupportedLanguage language) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 2000201, "Failure on Event_LanguageChanged", () => {
                // TODO Store change in settings

                this.LanguageChanged?.Invoke(sender, language);
            });
            this.RaiseIfException(report);


        }


    }

}
