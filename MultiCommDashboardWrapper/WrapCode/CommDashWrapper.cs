using CommunicationStack.Net.Enumerations;
using DependencyInjectorFactory.Net.interfaces;
using LanguageFactory.Net.data;
using LogUtils.Net;
using MultiCommDashboardWrapper.Interfaces;
using System;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        #region Data

        private ClassLog log = new ClassLog("CommDashWrapper");

        private IObjContainer container = null;

        #endregion


        public CommDashWrapper(IObjContainer container) {
            this.container = container;
        }


        public void Teardown() {
            try {
                this.LanguageTeardown();
                this.BTTeardown();
            }
            catch (Exception ex) {
                this.log.Exception(9999, "", ex);
            }
        }




        public void Validate(BinaryMsgDataType dataType, string value, Action onSuccess, OnErr onError) {
            dataType.Validate(value, onSuccess, (range) => {
                if (dataType == BinaryMsgDataType.tyepUndefined || dataType == BinaryMsgDataType.typeInvalid) {
                    onError(string.Format("'{0}' {1}",
                        this.GetText(MsgCode.DataType), this.GetText(MsgCode.undefined)));
                }
                else {
                    onError(string.Format("'{0}' {1} {2} - {3}",
                        value, this.GetText(MsgCode.OutOfRange), range.Min, range.Max));
                }
            });
        }


        public void Validate(BinaryMsgDataType dataType, string value, OnErr onError) {
            this.Validate(dataType, value, () => { }, onError);
        }


    }
}
