using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.data;
using MultiCommDashboardWrapper.Interfaces;
using StorageFactory.Net.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        #region Non Indexed

        private void Load<TStoredObject>(
            IStorageManager<TStoredObject> manager, 
            Action<TStoredObject> onSuccess, 
            OnErr onError) where TStoredObject : class {

            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    onSuccess.Invoke(manager.ReadObjectFromDefaultFile());
                });
                if (report.Code != 0) {
                    onError.Invoke(this.GetText(MsgCode.LoadFailed));
                }
            });
        }


        private void Save<TStoredObject>(
            IStorageManager<TStoredObject> manager, 
            TStoredObject data, 
            Action onSuccess, 
            OnErr onError) where TStoredObject : class {

            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    if (manager.WriteObjectToDefaultFile(data)) {
                        onSuccess.Invoke();
                    }
                    else {
                        onError.Invoke(this.GetText(MsgCode.SaveFailed));
                    }
                });
                if (report.Code != 0) {
                    onError.Invoke(this.GetText(MsgCode.SaveFailed));
                }
            });
        }

        #endregion


    }

}
