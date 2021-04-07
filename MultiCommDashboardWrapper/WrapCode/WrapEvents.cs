using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.Enumerations;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        public event EventHandler<ErrReport> UnexpectedExceptionEvent;

        public event EventHandler<BinaryMsgBool> MsgEventBool;
        public event EventHandler<BinaryMsgInt8> MsgEventInt8;
        public event EventHandler<BinaryMsgInt16> MsgEventInt16;
        public event EventHandler<BinaryMsgInt32> MsgEventInt32;
        public event EventHandler<BinaryMsgUInt8> MsgEventUInt8;
        public event EventHandler<BinaryMsgUInt16> MsgEventUInt16;
        public event EventHandler<BinaryMsgUInt32> MsgEventUInt32;
        public event EventHandler<BinaryMsgFloat32> MsgEventFloat32;


        private void RaiseIfException(ErrReport report) {
            if (report.Code != 0) {
                WrapErr.ToErrReport(9999, "Error raising unexpected exception event", () => {
                    this.UnexpectedExceptionEvent?.Invoke(this, report);
                });
            }
        }



        private void ParseBinaryMsgData(byte[] data) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure parse binary msg packet", () => {
                // Comm Binary stack just sends back packet with start and end delimiters. Validate here
                if (data.IsValidMsg()) {
                    switch (data.GetDataType()) {
                        case BinaryMsgDataType.typeBool:
                            this.RaiseMsgBool(data.ToBoolMsg());
                            break;
                        case BinaryMsgDataType.typeInt8:
                            this.RaiseMsgInt8(data.ToInt8Msg());
                            break;
                        case BinaryMsgDataType.typeUInt8:
                            this.RaiseMsgUInt8(data.ToUInt8Msg());
                            break;
                        case BinaryMsgDataType.typeInt16:
                            this.RaiseMsgUInt16(data.ToUInt16Msg());
                            break;
                        case BinaryMsgDataType.typeUInt16:
                            this.RaiseMsgUInt16(data.ToUInt16Msg());
                            break;
                        case BinaryMsgDataType.typeInt32:
                            this.RaiseMsgUInt32(data.ToUInt32Msg());
                            break;
                        case BinaryMsgDataType.typeUInt32:
                            this.RaiseMsgUInt32(data.ToUInt32Msg());
                            break;
                        case BinaryMsgDataType.typeFloat32:
                            this.RaiseMsgFloat32(data.ToFloat32Msg());
                            break;
                        case BinaryMsgDataType.tyepUndefined:
                        case BinaryMsgDataType.typeInvalid:
                            // Already checked with IsValidMsg
                            break;
                    }
                }
                else {
                    // TODO Raise error
                }
            });
            this.RaiseIfException(report);
        }


        private void RaiseMsgBool(BinaryMsgBool msg) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on RaiseMsg", () => {
                this.MsgEventBool?.Invoke(this, msg);
            });
            this.RaiseIfException(report);
        }

        private void RaiseMsgInt8(BinaryMsgInt8 msg) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on RaiseMsgInt8", () => {
                this.MsgEventInt8?.Invoke(this, msg);
            });
            this.RaiseIfException(report);
        }

        private void RaiseMsgInt16(BinaryMsgInt16 msg) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on RaiseMsgInt16", () => {
                this.MsgEventInt16?.Invoke(this, msg);
            });
            this.RaiseIfException(report);
        }

        private void RaiseMsgInt32(BinaryMsgInt32 msg) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on RaiseMsgInt32", () => {
                this.MsgEventInt32?.Invoke(this, msg);
            });
            this.RaiseIfException(report);
        }

        private void RaiseMsgUInt8(BinaryMsgUInt8 msg) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on RaiseMsgUInt8", () => {
                this.MsgEventUInt8?.Invoke(this, msg);
            });
            this.RaiseIfException(report);
        }

        private void RaiseMsgUInt16(BinaryMsgUInt16 msg) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on RaiseMsgUInt16", () => {
                this.MsgEventUInt16?.Invoke(this, msg);
            });
            this.RaiseIfException(report);
        }

        private void RaiseMsgUInt32(BinaryMsgUInt32 msg) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on RaiseMsgUInt32", () => {
                this.MsgEventUInt32?.Invoke(this, msg);
            });
            this.RaiseIfException(report);
        }

        private void RaiseMsgFloat32(BinaryMsgFloat32 msg) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on RaiseMsgFloat32", () => {
                this.MsgEventFloat32?.Invoke(this, msg);
            });
            this.RaiseIfException(report);
        }


    }

}
