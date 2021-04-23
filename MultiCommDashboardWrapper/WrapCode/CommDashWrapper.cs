using ChkUtils.Net;
using CommunicationStack.Net.DataModels;
using CommunicationStack.Net.Enumerations;
using DependencyInjectorFactory.Net.interfaces;
using LanguageFactory.Net.data;
using LogUtils.Net;
using MultiCommDashboardWrapper.DataModels;
using MultiCommDashboardWrapper.Interfaces;
using System;
using VariousUtils.Net;

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


        public void GetRange(BinaryMsgDataTypeDisplay dataType, Action<NumericRange> onSuccess, OnErr onError) {
            try {
                if (dataType == null) {
                    onError(this.GetText(MsgCode.NothingSelected));
                }
                else if (dataType.DataType == BinaryMsgDataType.tyepUndefined || dataType.DataType == BinaryMsgDataType.typeInvalid) {
                    onError(this.GetText(MsgCode.UnhandledError));
                    
                }
                else {
                    onSuccess(dataType.DataType.Range());
                }
            }
            catch(Exception ex) {
                this.log.Exception(9999, "GetRange", "", ex);
                WrapErr.SafeAction(() => onError(this.GetText(MsgCode.UnknownError)));
            }
        }


        public void ValidateEditValues(RawConfigValues values, Action<ValidatedConfigValues> onSuccess,  OnErr onError) {
            try {
                if (string.IsNullOrEmpty(values.Name)) {
                    onError(this.GetText(MsgCode.EmptyName));
                }
                else {
                    this.Validate(BinaryMsgDataType.typeUInt8, values.Id, () => {
                        this.Validate(values.DataType, values.Min, () => {
                            this.Validate(values.DataType, values.Max, () => {
                                this.Validate(values.DataType, values.Step, () => {
                                    double step = double.Parse(values.Step);                                    
                                    if (step < 1) {
                                        step *= -1;
                                    }

                                    onSuccess(new ValidatedConfigValues() {
                                        Id = values.Id.ToUInt8(),
                                        IOName = values.Name,
                                        Minimum = double.Parse(values.Min),
                                        Maximum = double.Parse(values.Max),
                                        SendAtStep = step,
                                    });
                                }, onError);
                            }, onError);
                        }, onError);
                    }, onError);
                }
            }
            catch (Exception ex) {
                this.log.Exception(9999, "GetRange", "", ex);
                WrapErr.SafeAction(() => onError(this.GetText(MsgCode.UnknownError)));
            }
        }

    }
}
