﻿using MultiCommDashboardData.Storage;
using MultiCommDashboardData.StorageIndex;
using MultiCommDashboardWrapper.Interfaces;
using StorageFactory.Net.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCommDashboardWrapper.WrapCode {

    public partial class CommDashWrapper : ICommDashWrapper {


        public void CreateConfiguration(DashboardConfiguration data, Action<IIndexItem<DashboardConfigIndexExtraInfo>> onSuccess, OnErr onError) {
            this.Create(this.Configurations, data.Display, data, onSuccess, onError, new DashboardConfigIndexExtraInfo(data));
        }


        public void CreateOrSaveConfiguration(DashboardConfiguration data, Action onSuccess, OnErr onError) {
            this.SaveOrCreate(this.Configurations, data.Display, data, this.PreSaveOps, (idx) => onSuccess(), onError);
        }



        private void PreSaveOps(DashboardConfiguration data, IIndexItem<DashboardConfigIndexExtraInfo> idx) {
            idx.ExtraInfoObj.Update(data);
        }


    }
}
