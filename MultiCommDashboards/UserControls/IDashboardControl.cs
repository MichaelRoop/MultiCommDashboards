using CommunicationStack.Net.Enumerations;
using MultiCommDashboardData.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MultiCommDashboards.UserControls {

    public interface IDashboardControl {

        /// <summary>The id that maps the control to the device input</summary>
        byte Id { get; set; }

        string IOName { get; set; }

        /// <summary>The data type represented by the control</summary>
        BinaryMsgDataType DataType { get; set; }

        /// <summary>Minimum value for this control</summary>
        double Minimum { get; set; }

        /// <summary>Maximum value for this control</summary>
        double Maximum { get; set; }

        /// <summary>At what tick step interval is data to be sent</summary>
        double SendAtStep { get; set; }

        /// <summary>Row location in grid</summary>
        int Row { get; set; }

        /// <summary>Column location in grid</summary>
        int Column { get; set; }

        DashboardControlDataModel StorageInfo { get; }

        /// <summary>Update the fields with the new data</summary>
        /// <param name="dataModel">The data model with new values</param>
        void Update(DashboardControlDataModel dataModel);

        /// <summary>Bring up overlay allowing delete and edit in the editor</summary>
        /// <param name="onOff"></param>
        void SetEditState(bool onOff);


        event EventHandler DeleteRequest;
        event EventHandler EditRequest;


    }
}
