﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_ERP_SupplementalAgreement.aspx.cs"
    Inherits="Workflow_EditPage_E_ERP_SupplementalAgreement" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="CSG" %>
<%@ Register Namespace="CustomControl" TagPrefix="CC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发起流程</title>
    <style type="text/css">
        
    </style>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/PreventRepeatSubmit.js" type="text/javascript"></script>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">发起流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 1080px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title" id="wf_form_title" runat="server">
                    <%= FormTitle %>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <iframe id="iframe" style="width: 1050px; height: 700px; border: none;" src='<%= IFrameHelper.GetErpUrl() %>'>
                            </iframe>
                            <!--业务表单-->
                            <tr>
                                <th>
                                    <b>原合同详情</b>：
                                </th>
                                <td>
                                    <div id="detailContract" runat="server">
                                        <a href='<%= SupplementalAgreement_Common.GetPoUrl() %>' target="_blank">点此查看</a>
                                    </div>
                                </td>
                                <th>
                                    上报资源集团：
                                </th>
                                <td>
                                    <CC:CheckBoxForReport ID="cbIsReportResource" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlDepartName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <th>
                                    上报方正集团：
                                </th>
                                <td>
                                    <asp:CheckBox ID="cbIsReportFounder" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <FR:FlowRelated ID="flowRelated" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="2004" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" colspan="1">
                        <tbody>
                            <tr>
                                <th>
                                    部门负责人意见:
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CS:Countersign ID="Countersign1" runat="server" IsCanEdit="true" DisableDepartments='法务部,财务管理部'
                                        DefaultCheckdDepartments="财务管理部" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p style="margin-top: 5px; margin-right: 10px; margin-bottom: 5px; line-height: 20px;">
                                        相关部门<br />
                                        主管领导意见：</p>
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见:：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    董事长意见:：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CSG:Countersign_Group ID="Countersign_Group1" runat="server" IsCanEdit="true" DisableDepartments='法务部,财务管理部'
                                        DefaultCheckdDepartments="财务管理部"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门意见：
                                </th>
                                <td colspan='2'>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p style="margin-top: 5px; margin-right: 10px; margin-bottom: 5px; line-height: 20px;">
                                        集团相关部门<br />
                                        主管领导意见：</p>
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团CEO意见:：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <%--                            <tr>
                                <th>
                                    流程发起人意见:：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    公司法务部负责人意见:：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    印章管理员意见:：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    档案管理员意见:：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>--%>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                </fieldset>
            </div>
        </div>
    </div>
    <!--快捷菜单-->
    <div id="scroll" style="margin-left: 520px;">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li>
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbClose" runat="server" OnClientClick="return Close_Win()">关闭</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click">终止</asp:LinkButton></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //    $(function () {
    //        $("#scroll_button").find("a[href^='javascript:__doPostBack']").each(function () {
    //            // var href = $(this).attr("href");
    //            // alert(href);
    //            //if (href.indexOf("javascript:__doPostBack") > -1) {
    //            //alert($(this).attr("id"));
    //            $(this).click(function () {
    //                if ($(this).hasClass("disable")) {
    //                    alert("系统处理中，请稍后");
    //                    return false;
    //                }
    //                //alert($(this).attr("id"));
    //                $(this).attr("disabled", "disabled");
    //                $("a[disabled]").addClass("disable");
    //                __doPostBack($(this).attr("id"), "");
    //            });
    //            // }
    //        });
    //    });
    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }
</script>
