using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.data;
using MultiCommDashboardData.Storage;
using MultiCommDashboardWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {

        public void GetSettings(Action<SettingsDataModel> onSuccess, OnErr onError) {
            this.Load(this.Settings, onSuccess, onError);
        }


        public void SaveSettings(SettingsDataModel settings, Action onSuccess, OnErr onError) {
            this.Save(this.Settings, settings, onSuccess, onError);
        }


    }
}
