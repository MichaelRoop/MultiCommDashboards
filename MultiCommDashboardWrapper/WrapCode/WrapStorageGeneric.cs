using ChkUtils.Net;
using ChkUtils.Net.ErrObjects;
using LanguageFactory.Net.data;
using MultiCommDashboardData.Interfaces;
using MultiCommDashboardWrapper.Interfaces;
using StorageFactory.Net.interfaces;
using StorageFactory.Net.StorageManagers;
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

        #region Indexed

        #region Generic Create

        private void Create<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            string display,
            TSToreObject data,
            Action<IIndexItem<TExtraInfo>> onSuccess,
            OnErr onError, TExtraInfo extraInfo = null)
            where TSToreObject : class, IDisplayable, IIndexible where TExtraInfo : class {

            this.Create(manager, display, data, onSuccess, (d) => { }, onError, extraInfo);
        }


        private void Create<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            string display,
            TSToreObject data,
            Action<IIndexItem<TExtraInfo>> onSuccess,
            Action<TSToreObject> onChange,
            OnErr onError, TExtraInfo extraInfo = null)
            where TSToreObject : class, IDisplayable, IIndexible where TExtraInfo : class {

            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    if (display.Length == 0) {
                        onError.Invoke(this.GetText(MsgCode.EmptyName));
                    }
                    else {
                        IIndexItem<TExtraInfo> idx = (extraInfo == null)
                            ? new IndexItem<TExtraInfo>(data.UId)
                            : new IndexItem<TExtraInfo>(data.UId, extraInfo);
                        idx.Display = display;
                        this.Save(manager, idx, data, (obj, idx) => { }, () => onSuccess(idx), onChange, onError);
                    }
                });
                if (report.Code != 0) {
                    onError.Invoke(this.GetText(MsgCode.UnknownError));
                }
            });
        }

        #endregion

        #region Generic Save

        private void Save<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            IIndexItem<TExtraInfo> idx,
            TSToreObject data,
            Action<TSToreObject, IIndexItem<TExtraInfo>> preSaveIndexUpdate,
            Action onSuccess,
            OnErr onError)
            where TSToreObject : class, IDisplayable where TExtraInfo : class {

            this.Save(manager, idx, data, preSaveIndexUpdate, onSuccess, (d) => { }, onError);
        }


        private void Save<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            IIndexItem<TExtraInfo> idx,
            TSToreObject data,
            Action<TSToreObject, IIndexItem<TExtraInfo>> preSaveIndexUpdate,
            Action onSuccess,
            Action<TSToreObject> onChange,
            OnErr onError)
            where TSToreObject : class, IDisplayable where TExtraInfo : class {

            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    if (idx == null) {
                        onError.Invoke(this.GetText(MsgCode.NothingSelected));
                    }
                    else if (string.IsNullOrWhiteSpace(data.Display)) {
                        onError.Invoke(this.GetText(MsgCode.EmptyName));
                    }
                    else {
                        // Transfer display name
                        idx.Display = data.Display;
                        // update index
                        preSaveIndexUpdate(data, idx);
                        manager.Store(data, idx);
                        onSuccess.Invoke();
                        onChange.Invoke(data);
                    }
                });
                if (report.Code != 0) {
                    onError.Invoke(this.GetText(MsgCode.SaveFailed));
                }
            });
        }

        #endregion

        #region Generic Save or Create

        private void SaveOrCreate<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            string display,
            TSToreObject data,
            Action<TSToreObject, IIndexItem<TExtraInfo>> preSaveIndexUpdate,
            Action<IIndexItem<TExtraInfo>> onSuccess,
            OnErr onError,
            TExtraInfo extraInfo = null)
            where TSToreObject : class, IDisplayable, IIndexible where TExtraInfo : class {

            this.SaveOrCreate(manager, display, data, preSaveIndexUpdate, onSuccess, (d) => { }, onError, extraInfo);
        }


        private void SaveOrCreate<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            string display,
            TSToreObject data,
            Action<TSToreObject, IIndexItem<TExtraInfo>> preSaveIndexUpdate,
            Action<IIndexItem<TExtraInfo>> onSuccess,
            Action<TSToreObject> onChange,
            OnErr onError,
            TExtraInfo extraInfo = null)
            where TSToreObject : class, IDisplayable, IIndexible where TExtraInfo : class {

            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    this.RetrievelIndexedItem(manager, data,
                        (idx) => {
                            // Found. Save
                            this.Save(manager, idx, data, preSaveIndexUpdate, () => onSuccess(idx), onChange, onError);
                        },
                        () => {
                            // Not found. Create
                            this.Create(manager, display, data, onSuccess, onChange, onError, extraInfo);
                        }, onError);
                });
                if (report.Code != 0) {
                    onError.Invoke(this.GetText(MsgCode.SaveFailed));
                }
            });
        }

        #endregion

        #region Generic Retrieve Index

        private void RetrieveIndex<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            Action<List<IIndexItem<TExtraInfo>>> onSuccess, OnErr onError)
            where TSToreObject : class where TExtraInfo : class {

            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    onSuccess.Invoke(manager.IndexedItems);
                });
                if (report.Code != 0) {
                    onError.Invoke(this.GetText(MsgCode.LoadFailed));
                }
            });
        }


        private void RetrieveItem<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            IIndexItem<TExtraInfo> index,
            Action<TSToreObject> onSuccess,
            OnErr onError)
            where TSToreObject : class where TExtraInfo : class {

            WrapErr.ToErrReport(9999, () => {
                ErrReport report;
                WrapErr.ToErrReport(out report, 9999, () => {
                    if (index == null) {
                        onError(this.GetText(MsgCode.NothingSelected));
                    }
                    else {
                        if (manager.FileExists(index)) {
                            TSToreObject item = manager.Retrieve(index);
                            if (item != null) {
                                onSuccess.Invoke(item);
                            }
                            else {
                                onError.Invoke(this.GetText(MsgCode.NotFound));
                            }
                        }
                        else {
                            onError.Invoke(this.GetText(MsgCode.NotFound));
                        }
                    }
                });
                if (report.Code != 0) {
                    onError.Invoke(this.GetText(MsgCode.LoadFailed));
                }
            });
        }

        #endregion

        #region RetrieveIndexedItem

        private void RetrievelIndexedItem<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            TSToreObject inObject,
            Action<IIndexItem<TExtraInfo>> found,
            Action notFound,
            OnErr onError)
            where TSToreObject : class, IDisplayable, IIndexible where TExtraInfo : class {

            this.RetrieveIndex(manager,
                (idx) => {
                    foreach (IIndexItem<TExtraInfo> item in idx) {
                        if (item.UId_Object == inObject.UId) {
                            found.Invoke(item);
                            this.log.Info("RetrievelIndexedItem", "Found the object index");
                            return;
                        }
                    }
                    notFound.Invoke();
                }, onError);
        }


        /// <summary>Get the index with no event if not found</summary>
        /// <typeparam name="TSToreObject"></typeparam>
        /// <typeparam name="TExtraInfo"></typeparam>
        /// <param name="manager"></param>
        /// <param name="inObject"></param>
        /// <param name="found"></param>
        /// <param name="onError"></param>
        private void RetrievelIndexedItem<TSToreObject, TExtraInfo>(
            IIndexedStorageManager<TSToreObject, TExtraInfo> manager,
            TSToreObject inObject,
            Action<IIndexItem<TExtraInfo>> found,
            OnErr onError)
            where TSToreObject : class, IDisplayable, IIndexible where TExtraInfo : class {
            this.RetrievelIndexedItem(manager, inObject, found, () => { }, onError);
        }

        #endregion

        #endregion

    }

}
