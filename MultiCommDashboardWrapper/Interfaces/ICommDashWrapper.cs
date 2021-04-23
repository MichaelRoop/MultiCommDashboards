using BluetoothCommon.Net;
using ChkUtils.Net.ErrObjects;
using CommunicationStack.Net.BinaryMsgs;
using CommunicationStack.Net.DataModels;
using CommunicationStack.Net.Enumerations;
using LanguageFactory.Net.data;
using LanguageFactory.Net.interfaces;
using LanguageFactory.Net.Messaging;
using MultiCommDashboardData.Storage;
using MultiCommDashboardWrapper.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using VariousUtils.Net;

namespace MultiCommDashboardWrapper.Interfaces {

    #region Delegates used throughout the wrapper

    public delegate void OnErr(string msg);
    public delegate void OnErrTitle(string title, string msg);

    #endregion

    public interface ICommDashWrapper {

        #region Events

        event EventHandler<ErrReport> UnexpectedExceptionEvent;

        event EventHandler<BinaryMsgBool> MsgEventBool;
        event EventHandler<BinaryMsgInt8> MsgEventInt8;
        event EventHandler<BinaryMsgInt16> MsgEventInt16;
        event EventHandler<BinaryMsgInt32> MsgEventInt32;
        event EventHandler<BinaryMsgUInt8> MsgEventUInt8;
        event EventHandler<BinaryMsgUInt16> MsgEventUInt16;
        event EventHandler<BinaryMsgUInt32> MsgEventUInt32;
        event EventHandler<BinaryMsgFloat32> MsgEventFloat32;

        #endregion

        #region Properties

        ILangFactory Languages { get; }

        #endregion

        #region Utils methods

        /// <summary>Make sure everyting is shut down before exit</summary>
        void Teardown();

        #endregion

        #region Languages

        event EventHandler<SupportedLanguage> LanguageChanged;

        /// <summary>Get the message in the currently selected language</summary>
        /// <param name="code">The message code</param>
        /// <returns>The message in the current language</returns>
        string GetText(MsgCode code);


        /// <summary>Set a new current language and save to disk</summary>
        /// <param name="code">Language code</param>
        /// <param name="onError">Invoked on error</param>
        void SetLanguage(LangCode code, OnErr onError);

        #endregion

        #region BLuetoothClassic

        event EventHandler<BTDeviceInfo> BT_Discovered;
        //event EventHandler<BTDeviceInfo> BT_InfoGathered;
        event EventHandler<bool> BT_DiscoveryComplete;
        event EventHandler<bool> BT_Connected;

        void BTDiscoverAsync(bool paired);

        void BTConnectAsync(BTDeviceInfo device);

        void BTDisconnect();

        void BTSend(byte[] msg);

        #endregion

        #region Settings

        void GetSettings(Action<SettingsDataModel> onSuccess, OnErr onError);
        void SaveSettings(SettingsDataModel settings, Action onSuccess, OnErr onError);

        #endregion

        #region General

        /// <summary>Validate entry of data for control</summary>
        /// <param name="dataType">The current data type</param>
        /// <param name="value">The proposed value</param>
        /// <param name="onError">Raised if value is invalid for data type</param>
        void Validate(BinaryMsgDataType dataType, string value, OnErr onError);


        /// <summary>Validate entry of data for control</summary>
        /// <param name="dataType">The current data type</param>
        /// <param name="value">The proposed value</param>
        /// <param name="onSuccess">Raised on valid value for data type</param>
        /// <param name="onError">Raised if value is invalid for data type</param>
        void Validate(BinaryMsgDataType dataType, string value, Action onSuccess, OnErr onError);


        /// <summary>Validate the group of values before saving</summary>
        /// <param name="values">The set of values to validate</param>
        /// <param name="onSuccess">Raised on success with completed data model</param>
        /// <param name="onError">Raised on error</param>
        void ValidateEditValues(RawConfigValues values, Action<ValidatedConfigValues> onSuccess, OnErr onError);
        
        
        /// <summary>Get range values as string for display</summary>
        /// <param name="dataType">The message data type</param>
        /// <param name="onSuccess">Raised on success with range</param>
        /// <param name="onError">Raised on error</param>
        void GetRange(BinaryMsgDataTypeDisplay dataType, Action<NumericRange> onSuccess, OnErr onError);

        #endregion

    }
}
