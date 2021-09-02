using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.data;
using MultiCommDashboardData.DataModels;
using MultiCommDashboardData.Enumerations;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VariousUtils.Net;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {


        public void GetCodeList(Action<List<CodeSelectDisplayDataModel>> onSuccess, OnErr onError) {
            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    List<CodeSelectDisplayDataModel> list = new List<CodeSelectDisplayDataModel>();  
                    foreach (CodeFileCode code in EnumHelpers.GetEnumList<CodeFileCode>()) {
                        list.Add(new CodeSelectDisplayDataModel(code));
                    }
                    onSuccess.Invoke(list);
                });
                if (report.Code != 0) {
                    WrapErr.SafeAction(() => onError(report.Msg));
                }
            });
        }


        public void RetrieveCodeFile(CodeSelectDisplayDataModel dataModel, Action<string> onSuccess, OnErr onError) {
            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    if (dataModel == null) {
                        onError(this.GetText(MsgCode.NothingSelected));
                    }
                    else {
                        string filename = this.CodeFileName(dataModel.Code);
                        if (File.Exists(filename)) {
                            onSuccess.Invoke(File.ReadAllText(filename));
                        }
                        else {
                            onError.Invoke(this.GetText(MsgCode.NotFound));
                        }
                    }
                });
                if (report.Code != 0) {
                    WrapErr.SafeAction(() => onError(report.Msg));
                }
            });
        }


        public void HasCodeFile(CodeSelectDisplayDataModel dataModel, Action<bool> onSuccess, OnErr onError) {
            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    if (dataModel == null) {
                        onError(this.GetText(MsgCode.NothingSelected));
                    }
                    else {
                        onSuccess.Invoke(File.Exists(this.CodeFileName(dataModel.Code)));
                    }
                });
                if (report.Code != 0) {
                    WrapErr.SafeAction(() => onError(report.Msg));
                }
            });
        }



        private string CodeFileName(CodeFileCode code) {
            return string.Format("{0}/Samples/{1}", AppDomain.CurrentDomain.BaseDirectory, code.ToFileName());
        }

    }

}
