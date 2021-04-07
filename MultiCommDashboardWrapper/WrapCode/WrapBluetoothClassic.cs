using BluetoothCommon.Net;
using BluetoothCommon.Net.interfaces;
using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.interfaces;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        public event EventHandler<BTDeviceInfo> BT_Discovered;
        //public event EventHandler<BTDeviceInfo> BT_InfoGathered;
        public event EventHandler<bool> BT_DiscoveryComplete;
        public event EventHandler<bool> BT_Connected;

        #region Just in time construction

        private IBTInterface __classicBluetooth = null;
        private ICommStackLevel0 __btClassicStack = null;

        private ICommStackLevel0 BTStack {
            get {
                if (this.__btClassicStack == null) {
                    this.__btClassicStack = this.container.GetObjInstance<ICommStackLevel0>();
                }
                return this.__btClassicStack;
            }
        }


        private IBTInterface BT {
            get {
                if (this.__classicBluetooth == null) {
                    this.__classicBluetooth = this.container.GetObjSingleton<IBTInterface>();

                    // Connect comm channel and its stack - Property to ensure just in time creation
                    this.BTStack.SetCommChannel(this.__classicBluetooth);
                    this.BTStack.InTerminators = BinaryMsgDefines.StartDelimiters; ;
                    this.BTStack.OutTerminators = BinaryMsgDefines.EndDelimiters;
                    this.BTStack.MsgReceived += this.BTStack_BytesReceivedHandler;
                    // BT event handlers connected
                    this.__classicBluetooth.DiscoveredBTDevice += this.BT_DiscoveredHandler;
                    this.__classicBluetooth.DiscoveryComplete += this.BT_DiscoveryCompleteHandler;
                    //this.__classicBluetooth.BT_DeviceInfoGathered += this.BT_InfoGatheredHandler;
                    this.__classicBluetooth.ConnectionCompleted += this.BT_ConnectedHander;
                }
                return this.__classicBluetooth;
            }
        }

        #endregion

        #region Public

        public void BTDiscoverAsync(bool paired) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on BTDiscoverAsync", () => {
                this.BT.DiscoverDevicesAsync(paired);
            });
            this.RaiseIfException(report);
        }

        public void BTConnectAsync(BTDeviceInfo device) {
            this.log.InfoEntry("BTConnectAsync");
            ErrReport report;
            WrapErr.ToErrReport(out report, 20003, "Failure on BTConnectAsync", () => {
                this.BT.ConnectAsync(device);
            });
            if (report.Code != 0) {
                WrapErr.SafeAction(() => BT_DiscoveryComplete?.Invoke(this, false));
            }
        }

        public void BTDisconnect() {
            ErrReport report;
            WrapErr.ToErrReport(out report, 9999, "Failure on BTDisconnect", () => {
                this.BT.Disconnect();
            });
            this.RaiseIfException(report);
        }

        public void BTSend(byte[] msg) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 20007, "Failure on BTClassicSend", () => {
                this.BTStack.SendToComm(msg);
            });
            this.RaiseIfException(report);
        }

        #endregion


        #region Init and teardown

        private void BTTeardown() {
            this.BT.Disconnect();
            this.BT.DiscoveredBTDevice -= this.BT_DiscoveredHandler;
            this.BT.DiscoveryComplete -= this.BT_DiscoveryCompleteHandler;
            //this.BT.BT_DeviceInfoGathered -= BT_InfoGatheredHandler;
            this.BT.ConnectionCompleted -= this.BT_ConnectedHander;

            this.BTStack.MsgReceived -= this.BTStack_BytesReceivedHandler;
        }

        #endregion

        #region Event handlers

        private void BTStack_BytesReceivedHandler(object sender, byte[] data) {
            this.ParseBinaryMsgData(data);
        }

        private void BT_ConnectedHander(object sender, bool e) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 20011, "Failure on BT_ConnectionCompletedHander", () => {
                this.BT_Connected?.Invoke(this, e);
            });
            this.RaiseIfException(report);
        }


        private void BT_DiscoveryCompleteHandler(object sender, bool e) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 20012, "Failure on BT_DiscoveryCompleteHandler", () => {
                this.BT_DiscoveryComplete?.Invoke(this, e);
            });
            this.RaiseIfException(report);
        }


        private void BT_DiscoveredHandler(object sender, BTDeviceInfo e) {
            ErrReport report;
            WrapErr.ToErrReport(out report, 20013, "Failure on BT_DiscoveredDeviceHandler", () => {
                this.BT_Discovered?.Invoke(this, e);
            });
            this.RaiseIfException(report);
        }


        //private void BT_InfoGatheredHandler(object sender, BTDeviceInfo e) {
        //    ErrReport report;
        //    WrapErr.ToErrReport(out report, 20014, "Failure on BTClassic_DeviceInfoGathered", () => {
        //        this.BT_InfoGathered?.Invoke(this, e);
        //    });
        //    this.RaiseIfException(report);
        //}

        #endregion


    }
}
