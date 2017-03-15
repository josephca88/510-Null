﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Ralid.OpenCard.OpenCardService.ETC;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.UserControls;

namespace Ralid.OpenCard.UI.ETC
{
    public partial class FrmETCRecordReport : FrmReportBase
    {
        public FrmETCRecordReport()
        {
            InitializeComponent();
        }

        private void FrmETCRecordReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.ucDateTimeInterval1.SelectToday();
        }

        #region 重写基类方法
        protected override void OnItemSearching(EventArgs e)
        {
            GridView.Rows.Clear();
            ETCPaymentRecordSearchCondition con = new ETCPaymentRecordSearchCondition();
            con.AddTime = new DateTimeRange(ucDateTimeInterval1.StartDateTime, ucDateTimeInterval1.EndDateTime);
            if (chkUnuploaded.Checked) con.WaitingUpload = true;
            List<ETCPaymentRecord> items = (new ETCPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect)).GetRecords(con).QueryObjects;
            foreach (var item in items)
            {
                int row = this.GridView.Rows.Add();
                ShowPayOperationLogOnRow(GridView.Rows[row], item);
            }
        }

        private void ShowPayOperationLogOnRow(DataGridViewRow row, ETCPaymentRecord item)
        {
            try
            {
                ETCPaymentList list = JsonConvert.DeserializeObject<ETCPaymentList>(item.Data);
                row.Tag = list;
                row.Cells["colListNo"].Value = list != null ? list.ListNo : null;
                row.Cells["colLaneNo"].Value = item.LaneNo;
                row.Cells["colAddTime"].Value = item.AddTime.ToString("yyyy-MM-dd HH:mm:ss");
                row.Cells["colCardID"].Value = list != null ? list.CardNo : null;
                row.Cells["colCarplate"].Value = list != null ? list.ExVehPlate : null;
                row.Cells["colPayment"].Value = list != null ? (decimal)list.CashMoney / 100 : 0;
                row.Cells["colBalance"].Value = list != null ? (decimal)list.Balance / 100 : 0;
                row.Cells["colUploadTime"].Value = item.UploadTime.HasValue ? item.UploadTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
                row.Visible = (string.IsNullOrEmpty(txtCarplate.Text) || (list != null && list.ExVehPlate.Contains(txtCarplate.Text)));
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        private void txtCarplate_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in customDataGridView1.Rows)
            {
                ETCPaymentList list = row.Tag as ETCPaymentList;
                row.Visible = (string.IsNullOrEmpty(txtCarplate.Text) || (list != null && list.ExVehPlate.Contains(txtCarplate.Text)));
            }
        }
    }
}