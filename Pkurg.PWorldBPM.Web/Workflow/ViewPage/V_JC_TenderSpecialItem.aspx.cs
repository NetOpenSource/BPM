﻿using System;
using System.Collections.Generic;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;

public partial class Workflow_ViewPage_V_JC_TenderSpecialItem : System.Web.UI.Page
{
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string instId = Request.QueryString["ID"];
            if (!string.IsNullOrEmpty(instId))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                ViewState["InstanceID"] = Request.QueryString["ID"];
                FormId = info.FormId;
                InitFormData();
                InitApproveOpinion();
            }
            else
            {
                ExceptionHander.GoToErrorPage();
            }

            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                tbCompany.Visible = false;
                tbCompanyCommittee.Visible = false;
                tbURL.Visible = false;
                tbGroup.Visible = true;
                tbGroupCommittee.Visible = cblIsImpowerProject.SelectedValue == "1" ? false : true;
            }
            else
            {
                tbCompanyCommittee.Visible = cblIsImpowerProject.SelectedValue == "1" ? true : false;
            }
        }
    }

    /// <summary>
    /// 初始化意见框
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    private void InitApproveOpinion()
    {
        List<UserControls_ApproveOpinionUC> options = GetOptions();
        foreach (var item in options)
        {
            item.InstanceId = ViewState["InstanceID"].ToString();
        }
    }

    /// <summary>
    /// 得到意见列表
    /// </summary>
    /// <returns></returns>
    private List<UserControls_ApproveOpinionUC> GetOptions()
    {
        List<UserControls_ApproveOpinionUC> options = new List<UserControls_ApproveOpinionUC>();
        //将各个领导意见添加到list中
        options.Add(OpinionOperateDeptleader);
        options.Add(OpinionRealateDept);
        options.Add(OpinionGroupRealateDept);
        options.Add(OpinionExecutiveDirector);
        options.Add(OpinionTenderCommitteeLeader);
        options.Add(OpinionTenderCommitteeChairman);
        options.Add(OpinionGroupTenderCommitteeLeader);
        options.Add(OpinionGroupTenderCommitteeChairman);
        return options;
    }
    /// <summary>
    /// 加载表单
    /// </summary>
    private void InitFormData()
    {
        try
        {
            JC_TenderSpecialItemInfo info = JC_TenderSpecialItem.GetJC_TenderSpecialItemInfoByFormID(FormId);
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (info != null)
                 //加载业务数据
                cblSecurityLevel.SelectedIndex = int.Parse(info.SecurityLevel);
                cblUrgenLevel.SelectedIndex = int.Parse(info.UrgenLevel);
                ddlDepartName.Text = info.DeptName;
                tbDateTime.Text = info.Date ?? "";
                tbUserName.Text = info.UserName;
                tbMobile.Text = info.Tel;
                tbTitle.Text = info.Title;
                tbContent.Text = info.Substance.Replace(" ", "&nbsp;").Replace("\n", "<br/>");
                tbRemark.Text = info.Remark;
                cblIsImpowerProject.SelectedIndex = int.Parse(info.IsAccreditByGroup);
                tbReportCode.Text = info.FormID;
                if (!string.IsNullOrEmpty(info.IsApproval))
                {
                    lbIsApproval.Text = string.Format("{1}({0})", info.IsApproval == "1" ? "批准" : "拒绝", lbIsApproval.Text);
                    if (info.IsApproval == "1")
                    {
                        this.lbIsApproval.Style.Add("color", "green");
                    }
                    else
                    {
                        this.lbIsApproval.Style.Add("color", "red");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// FormId
    /// </summary>
    public string FormId
    {
        get
        {
            return ViewState["FormID"].ToString();
        }
        set
        {
            ViewState["FormID"] = value;
        }
    }
}