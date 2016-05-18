﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_JC_ProjectTenderCityCompany.aspx.cs"
    Inherits="Workflow_ApprovePage_A_JC_ProjectTenderCityCompany" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="AS" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="ASI" %>
<%@ Register Src="../../Modules/ChangeSign/ChangeSign.ascx" TagName="ChangeSign"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">审批流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 1000px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    招标需求申请
                    <br />
                </div>
                <fieldset class="wf_fieldset">
                    <table class="wf_table" cellpadding="0" cellspacing="1">
                        <tr>
                            <th>
                                编号：
                            </th>
                            <td>
                                <asp:TextBox ID="tbReportCode" MaxLength="50" runat="server" contentEditable="false" />
                            </td>
                            <th>
                                保密等级：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">绝密</asp:ListItem>
                                    <asp:ListItem Value="1">机密</asp:ListItem>
                                    <asp:ListItem Value="2">秘密</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th>
                                紧急程度：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblUrgenLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">加急</asp:ListItem>
                                    <asp:ListItem Value="1">紧急</asp:ListItem>
                                    <asp:ListItem Value="2">一般</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tr>
                            <th>
                                经办部门：
                            </th>
                            <td>
                                <asp:TextBox ID="ddlDepartName" runat="server" CssClass="txt" ReadOnly="true" Width="300px">
                                </asp:TextBox>
                            </td>
                            <th>
                                日期：
                            </th>
                            <td>
                                <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                经办人：
                            </th>
                            <td>
                                <asp:TextBox ID="tbUserName" runat="server" class="txtshort" ReadOnly="true"></asp:TextBox>
                            </td>
                            <th>
                                电话：
                            </th>
                            <td>
                                <asp:TextBox ID="tbMobile" runat="server" class="txtshort" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                集团授权项目：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblIsImpowerProject" runat="server" RepeatDirection="Horizontal"
                                    Enabled="false">
                                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="cblFirstLevel" runat="server" RepeatDirection="Horizontal"
                                    Visible="false">
                                    <asp:ListItem Value="0">一级开发</asp:ListItem>
                                    <asp:ListItem Value="1">二级开发</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                主题：
                            </th>
                            <td colspan='3'>
                                <asp:TextBox ID="tbTitle" runat="server" CssClass="longtxt" Width="700" ReadOnly="true"
                                    BackColor="White"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                主要内容：
                            </th>
                            <td colspan="3">
                                <asp:Label ID="lbcontent" runat="server" Text="招标项目情况描述、招标范围、预计合同金额对投标人的基本要求等；"></asp:Label>
                                <br />
                                <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                    Width="700" Height="200" ReadOnly="true" BackColor="White"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                备注：
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="tbRemark" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                    Width="700" Height="50" ReadOnly="true" BackColor="White"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                关联流程：
                            </th>
                            <td colspan="3">
                                <uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                上传附件：
                            </th>
                            <td colspan="3">
                                <uc1:UploadAttachments ID="UploadAttachments1" runat="server" AppId="10113" IsCanEdit="true" />
                                说明：最大可上传50M的文件，请不要上传同名文件，否则会覆盖之前的文件，所有文件请先解密再上传！
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody id="tbCompany" runat="server">
                            <tr>
                                <td colspan="3">
                                    <a id="ViewUrl2" target="_blank">点此查看相关流程</a>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办部门意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionOperateDeptleader" CurrentNode="false" CurrentNodeName="经办部门意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合约审算部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionContractReviewDeptleaader" CurrentNode="false" CurrentNodeName="合约审算部意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <div style="line-height: 25px;">
                                        分管业务/成本领导意见：
                                    </div>
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionBranchDeptLeader" CurrentNode="false" CurrentNodeName="分管领导意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionRealateDept" CurrentNode="false" CurrentNodeName="相关部门意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbCompanyCommittee" runat="server">
                            <tr>
                                <th>
                                    招标委员会意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionTenderCommitteeLeader" CurrentNode="false" CurrentNodeName="招标委员会意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionTenderCommitteeChairman" CurrentNode="false" CurrentNodeName="招标委员会主任意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbURL" runat="server" visible="false">
                            <tr>
                                <td colspan="3">
                                    <a id="ViewUrl" target="_blank">点此查看相关流程</a>
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbGroup" runat="server" visible="false">
                            <tr>
                                <th>
                                    <div style="line-height: 25px;">
                                        集团业务/成本相关部门领导意见：
                                    </div>
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupCompetentDeptLeader" CurrentNode="false" CurrentNodeName="集团主管部门意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupPurchasadministrationDeptLeader" CurrentNode="false"
                                        CurrentNodeName="集团采购管理部意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbPresident" runat="server" visible="false">
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupTenderCommitteeLeader" CurrentNode="false"
                                        CurrentNodeName="集团招标委员会意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbGroupCommittee" runat="server" visible="false">
                            <tr>
                                <th>
                                    CPO意见:
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupTenderCommitteeChairman" CurrentNode="false"
                                        CurrentNodeName="集团招标委员会主任意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="sn" runat="server" />
                    <asp:HiddenField ID="nodeID" runat="server" />
                    <asp:HiddenField ID="nodeName" runat="server" />
                    <asp:HiddenField ID="taskID" runat="server" />
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                    <asp:HiddenField ID="hfViewUrl" runat="server" />
                </fieldset>
            </div>
        </div>
    </div>
    <!--快捷菜单-->
    <div id="scroll">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <div id="Options" runat="server">
                    <li>
                        <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                    <li id="UnOptions" runat="server">
                        <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                </div>
                <li id="ASOptions" runat="server">
                <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <AS:AddSign ID="AddSign1" runat="server" />
                <ASI:AddSignDeptInner ID="AddSignDeptInner1" runat="server" />
                <uc3:ChangeSign ID="ChangeSign1" runat="server" />
                <li><a href='#' onclick='Close_Win();'>关闭</a></li>
            </ul>
        </div>
        <asp:HiddenField ID="hf_OpId" runat="server" />
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }

    $(function () {
        var url = document.getElementById("hfViewUrl").value;
        $("#ViewUrl").attr("href", url);
        $("#ViewUrl2").attr("href", url + "&type=1");
    });
</script>
