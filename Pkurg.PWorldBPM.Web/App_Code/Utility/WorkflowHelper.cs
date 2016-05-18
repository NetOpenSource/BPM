﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using SourceCode.Workflow.Client;

/// <summary>
///WorkflowHelper 的摘要说明
/// </summary>
public class WorkflowHelper
{
    #region k2 server info
    /// <summary>
    /// k2 server name
    /// </summary>
    static readonly string K2ServerName = ConfigurationManager.AppSettings["K2ServerName"];
    /// <summary>
    /// k2 server port
    /// </summary>
    static readonly string K2ServerPort = ConfigurationManager.AppSettings["K2ServerPort"];
    /// <summary>
    /// k2 host port
    /// </summary>
    static readonly string K2HostPort = ConfigurationManager.AppSettings["K2HostPort"];
    /// <summary>
    /// Windows Domain Name
    /// </summary>
    static readonly string DomainName = ConfigurationManager.AppSettings["DomainName"];
    /// <summary>
    /// k2 service user account
    /// </summary>
    static readonly string KSUserName = ConfigurationManager.AppSettings["K2ServiceUser"];
    /// <summary>
    /// k2 service user password
    /// </summary>
    static readonly string KSUserPwd = ConfigurationManager.AppSettings["K2ServicePwd"];

    //FBA to AD
    private static string _currentUser;
    public static string CurrentUser
    {
        get
        {
            ///增加对流程操作的用户处理
            ///modify by star
            if (string.IsNullOrEmpty(_currentUser))
            {
                if (System.Web.HttpContext.Current != null)
                {
                    //object userInfo = System.Web.HttpContext.Current.Session["BPM_User"];
                    //if (userInfo == null)
                    //{
                       object userInfo = new IdentityUser().GetEmployee();
                    //    System.Web.HttpContext.Current.Session["BPM_User"] = userInfo;
                    //}
                    Pkurg.PWorldBPM.Common.Info.UserInfo currentUserInfo = userInfo as Pkurg.PWorldBPM.Common.Info.UserInfo;
                    _currentUser = currentUserInfo.LoginId;
                }
                else
                {
                    _currentUser = System.Environment.UserName;
                }
            }
            return _currentUser;
        }
        set { _currentUser = value; }
    }
    #endregion

    #region k2 connection string
    /// <summary>
    /// 根据实例ID更新DataField
    /// </summary>
    /// <param name="procInst"></param>
    /// <param name="nvcDataFields"></param>
    /// <returns></returns>
    public static bool UpdataField(ProcessInstance procInst, NameValueCollection nvcDataFields)
    {
        if (nvcDataFields != null)
        {
            for (int i = 0; i < nvcDataFields.Count; i++)
            {
                procInst.DataFields[nvcDataFields.GetKey(i)].Value = nvcDataFields[i];
            }
            procInst.Update();
        }
        return true;
    }
    /// <summary>
    /// get k2 connection string.(Integrated=True)
    /// </summary>
    /// <returns>k2 connection string</returns>
    public static string GetConnString()
    {
        //FBA to AD
        //return "Integrated=False;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2HostPort + ";SecurityLabelName=K2SQL;UserID=" + userName + ";Password=" + password + ";";
        return "Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2HostPort + ";WindowsDomain=" + DomainName + ";SecurityLabelName=K2;UserID=" + KSUserName + ";Password=" + KSUserPwd + ";";
    }

    /// <summary>
    /// 服务端
    /// </summary>
    /// <returns></returns>
    public static string GetConnString4Management()
    {
        //FBA to AD
        //return "Integrated=False;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2HostPort + ";SecurityLabelName=K2SQL;UserID=" + userName + ";Password=" + password + ";";
        return "Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2ServerPort + ";WindowsDomain=" + DomainName + ";SecurityLabelName=K2;UserID=" + KSUserName + ";Password=" + KSUserPwd + ";";
    }

    public static string GetConnStringDel()
    {
        //FBA to AD
        //return "Integrated=False;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2HostPort + ";SecurityLabelName=K2SQL;UserID=" + userName + ";Password=" + password + ";";
        return "Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=" + K2ServerName + ";Port=" + K2ServerPort + ";WindowsDomain=" + DomainName + ";SecurityLabelName=K2;UserID=" + KSUserName + ";Password=" + KSUserPwd + ";";
    }

    #endregion

    #region start process instance
    /// <summary>
    /// strat process instance
    /// </summary>
    /// <param name="processName">process full name</param>
    /// <param name="folio">process folio</param>
    /// <param name="nvcDataFields">process data fields</param>
    /// <returns>start process instance successfully or not</returns>
    public static bool StartProcess(string processName, string folio, NameValueCollection nvcDataFields)
    {
        using (Connection conn = new Connection())
        {
            bool result = false;
            try
            {
                conn.Open(K2ServerName, GetConnString());

                conn.RevertUser();
                conn.ImpersonateUser(CurrentUser);

                ProcessInstance procInst = conn.CreateProcessInstance(processName);
                if (nvcDataFields != null)
                {
                    for (int i = 0; i < nvcDataFields.Count; i++)
                    {
                        procInst.DataFields[nvcDataFields.GetKey(i)].Value = nvcDataFields[i];
                    }
                }
                procInst.Folio = folio;
                conn.StartProcessInstance(procInst);
                result = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }

    /// <summary>
    /// strat process instance,and return ProcInstID
    /// </summary>
    /// <param name="processName">process full name</param>
    /// <param name="folio">process folio</param>
    /// <param name="nvcDataFields">process data fields</param>
    /// <returns>start process instance successfully or not</returns>
    public static bool StartProcess(string processName, string folio, NameValueCollection nvcDataFields, ref int ProcInstID)
    {
        using (Connection conn = new Connection())
        {
            bool result = false;
            try
            {
                conn.Open(K2ServerName, GetConnString());

                conn.RevertUser();
                conn.ImpersonateUser(CurrentUser);




                ProcessInstance procInst = conn.CreateProcessInstance(processName);
                if (nvcDataFields != null)
                {
                    for (int i = 0; i < nvcDataFields.Count; i++)
                    {
                        procInst.DataFields[nvcDataFields.GetKey(i)].Value = nvcDataFields[i];
                    }
                }
                procInst.Folio = folio;

                conn.StartProcessInstance(procInst);

                ProcInstID = procInst.ID;//added by pccai

                result = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }

    #endregion
    #region
    /// <summary>
    /// strat process instance,and return ProcInstID
    /// </summary>
    /// <param name="processName">process full name</param>
    /// <param name="folio">process folio</param>
    /// <param name="nvcDataFields">process data fields</param>
    /// <param name="CurrentUser">CurrentUser</param>
    /// <returns>start process instance successfully or not</returns>
    public static bool StartProcess(string processName, string folio, NameValueCollection nvcDataFields, ref int ProcInstID, string CurrentUser)
    {
        using (Connection conn = new Connection())
        {
            bool result = false;
            try
            {
                conn.Open(K2ServerName, GetConnString());

                conn.RevertUser();
                conn.ImpersonateUser(CurrentUser);




                ProcessInstance procInst = conn.CreateProcessInstance(processName);
                if (nvcDataFields != null)
                {
                    for (int i = 0; i < nvcDataFields.Count; i++)
                    {
                        procInst.DataFields[nvcDataFields.GetKey(i)].Value = nvcDataFields[i];
                    }
                }
                procInst.Folio = folio;

                conn.StartProcessInstance(procInst);

                ProcInstID = procInst.ID;//added by pccai

                result = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }

    #endregion


    #region approve process instance
    /// <summary>
    /// approve process instance
    /// </summary>
    /// <param name="sn">serial number</param>
    /// <param name="action">approval action</param>
    /// <returns>approve process instance successfully or not</returns>
    public static bool ApproveProcess(string sn, string action)
    {
        using (Connection conn = new Connection())
        {
            bool result = false;
            try
            {
                conn.Open(K2ServerName, GetConnString());

                conn.RevertUser();
                conn.ImpersonateUser(CurrentUser);

                WorklistItem wlItem = conn.OpenWorklistItem(sn);


                wlItem.Actions[action].Execute();
                result = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("24411"))
                {
                    return false;
                }
                else if (ex.Message.StartsWith("26030"))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }


    public static bool ApproveProcess(string sn, string action, string currentUser)
    {
        using (Connection conn = new Connection())
        {
            bool result = false;
            try
            {
                conn.Open(K2ServerName, GetConnString());

                conn.RevertUser();
                conn.ImpersonateUser(currentUser);
                WorklistItem wlItem = conn.OpenWorklistItem(sn);
                wlItem.Actions[action].Execute();
                result = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }



    #endregion

    #region update data field of the current process
    /// <summary>
    /// update some data field of the current process
    /// </summary>
    /// <param name="sm">update some data field</param>
    /// <param name="list">new data field value</param>
    /// <returns>is success</returns>
    public static bool UpdateDataFields(string sn, NameValueCollection nvcDataFields)
    {
        using (Connection conn = new Connection())
        {
            bool result = false;
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(@"Founder\zybpmadmin");

                //WorklistItem wlItem = wlItem = conn.OpenWorklistItem(sn);
                //for (int i = 0; i < nvcDataFields.Count; i++)
                //{
                //    wlItem.ProcessInstance.DataFields[nvcDataFields.GetKey(i)].Value = nvcDataFields[i];
                //}
                //wlItem.ProcessInstance.Update();

                ProcessInstance inst = conn.OpenProcessInstance(Convert.ToInt32(sn.Split('_')[0]));
                for (int i = 0; i < nvcDataFields.Count; i++)
                {
                    inst.DataFields[nvcDataFields.GetKey(i)].Value = nvcDataFields[i];
                }
                inst.Update();

                result = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
    #endregion

    #region update data field of the current process
    /// <summary>
    /// update some data field of the current process
    /// </summary>
    /// <param name="sm">update some data field</param>
    /// <param name="list">new data field value</param>
    /// <returns>is success</returns>
    public static bool UpdateDataFields(string sn, NameValueCollection nvcDataFields, string currentUser)
    {
        using (Connection conn = new Connection())
        {
            bool result = false;
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                if (currentUser == "")
                {
                    conn.ImpersonateUser(CurrentUser);
                }
                else
                {
                    conn.ImpersonateUser(currentUser);
                }


                //WorklistItem wlItem = wlItem = conn.OpenWorklistItem(sn);
                //for (int i = 0; i < nvcDataFields.Count; i++)
                //{
                //    wlItem.ProcessInstance.DataFields[nvcDataFields.GetKey(i)].Value = nvcDataFields[i];
                //}
                //wlItem.ProcessInstance.Update();

                ProcessInstance inst = conn.OpenProcessInstance(Convert.ToInt32(sn.Split('_')[0]));
                for (int i = 0; i < nvcDataFields.Count; i++)
                {
                    inst.DataFields[nvcDataFields.GetKey(i)].Value = nvcDataFields[i];
                }
                inst.Update();

                result = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
    #endregion

    #region get process info.
    /// <summary>
    /// get process info.
    /// </summary>
    /// <param name="sn">serial number</param>
    /// <param name="processID">process id//edit by lee 2011-06-15</param>
    /// <param name="activityName">process activity name</param>
    /// <returns>get process info.</returns>
    public static void GetProcessInfo(string sn, ref string FormID, ref string ProcessID, ref string activityName, ref string approveXML)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(CurrentUser);
                WorklistItem wlItem = conn.OpenWorklistItem(sn);
                //edit by lee 2011-06-15
                FormID = wlItem.ProcessInstance.DataFields["FormID"].Value.ToString();
                ProcessID = wlItem.ProcessInstance.DataFields["ProcessID"].Value.ToString();
                //if (wlItem.ProcessInstance.DataFields["ApprovalXML"] != null)
                //    approveXML = wlItem.ProcessInstance.DataFields["ApprovalXML"].Value.ToString();
                activityName = wlItem.ActivityInstanceDestination.Name;

                //添加获取自定义流程XML方法
                foreach (DataField dataField in wlItem.ProcessInstance.DataFields)
                {
                    if (dataField.Name == "ApprovalXML")
                    {
                        approveXML = dataField.Value.ToString();
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion

    #region get scf process info
    public static void GetSCFProcessInfo(string sn, ref string FormID, ref string ProcessID, ref string activityName, ref string approveXML)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(CurrentUser);
                WorklistItem wlItem = conn.OpenWorklistItem(sn);
                //edit by lee 2011-06-15
                FormID = wlItem.ProcessInstance.Folio;
                ProcessID = wlItem.ProcessInstance.ID.ToString();
                //ProcessID = wlItem.ProcessInstance.DataFields["ProcessID"].Value.ToString();
                //if (wlItem.ProcessInstance.DataFields["ApprovalXML"] != null)
                //    approveXML = wlItem.ProcessInstance.DataFields["ApprovalXML"].Value.ToString();
                activityName = wlItem.ActivityInstanceDestination.Name;

                //添加获取自定义流程XML方法
                //foreach (DataField dataField in wlItem.ProcessInstance.DataFields)
                //{
                //    if (dataField.Name == "ApprovalXML")
                //    {
                //        approveXML = dataField.Value.ToString();
                //        break;
                //    }
                //}
                approveXML = wlItem.ProcessInstance.DataFields[0].Value.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion


    #region get value of data field of current process
    /// <summary>
    /// get value of data field of current process
    /// </summary>
    /// <param name="sn">serial number</param>
    /// <param name="FieldName">name of the data field</param>
    /// <returns>value of the specified data field</returns>
    public static string GetDataField(string sn, string FieldName)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(CurrentUser);
                WorklistItem wlItem = wlItem = conn.OpenWorklistItem(sn);
                return wlItem.ProcessInstance.DataFields[FieldName].Value.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion


    #region get value of data field of current process
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="FieldNames"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetDataField(string sn, string[] FieldNames)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(CurrentUser);
                WorklistItem wlItem = wlItem = conn.OpenWorklistItem(sn);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (string fieldName in FieldNames)
                {
                    dictionary.Add(fieldName, wlItem.ProcessInstance.DataFields[fieldName].Value.ToString());
                }
                return dictionary;

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion

    #region get value of data field of current process
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="FieldNames"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetDataField(string sn, string[] FieldNames, string UserAccount)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(UserAccount);
                WorklistItem wlItem = wlItem = conn.OpenWorklistItem(sn);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (string fieldName in FieldNames)
                {
                    dictionary.Add(fieldName, wlItem.ProcessInstance.DataFields[fieldName].Value.ToString());
                }
                return dictionary;

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion


    /// <summary>
    /// 获得流程实例
    /// </summary>
    /// <param name="processInstID"></param>
    /// <returns></returns>
    public static ProcessInstance GetProcessInstance(int processInstID)
    {
        using (Connection conn = new Connection())
        {
            ProcessInstance inst = null;
            try
            {
                conn.Open(K2ServerName, GetConnString());

                inst = conn.OpenProcessInstance(processInstID);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return inst;
        }
    }

    public static NameValueCollection GetProcessInstanceAllDataFields(int processInstID)
    {
        NameValueCollection dataFields = new NameValueCollection();
        using (Connection conn = new Connection())
        {
            ProcessInstance inst = null;
            try
            {
                conn.Open(K2ServerName, GetConnString());

                inst = conn.OpenProcessInstance(processInstID);

                foreach (DataField item in inst.DataFields)
                {
                    dataFields.Add(item.Name, item.Value.ToString());
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dataFields;
        }
    }

    /// <summary>
    /// 获取实例的datafield
    /// </summary>
    /// <param name="processInstID"></param>
    /// <param name="dfName"></param>
    /// <returns></returns>
    public static string GetProcessInstanceDataField(int processInstID,string dfName)
    {
        using (Connection conn = new Connection())
        {
            ProcessInstance inst = null;
            try
            {
                conn.Open(K2ServerName, GetConnString());

                inst = conn.OpenProcessInstance(processInstID);

                return inst.DataFields["dfName"].Value.ToString();
            }
            catch
            {
                
            }
            finally
            {
                conn.Close();
            }
            return "";
        }
    }

    public static WorklistItem GetWorklistItem(int processInstanceId)
    {
        using (Connection conn = new Connection())
        {
            WorklistItem retitem = null;
            try
            {
                conn.Open(K2ServerName, GetConnString());
                Worklist list = conn.OpenWorklist();
                foreach (WorklistItem item in list)
                {
                    //item.ProcessInstance.Originator发起人
                    if (item.ProcessInstance.ID == processInstanceId)
                    {
                        retitem = item;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return retitem;
        }
    }

    /// <summary>
    /// 根据流水号返回WorklistItem
    /// </summary>
    /// <param name="sn"></param>
    /// <returns></returns>
    public static WorklistItem GetWorklistItemWithSN(string sn)
    {
        using (Connection conn = new Connection())
        {
            WorklistItem retitem = null;
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(CurrentUser);
                retitem = conn.OpenWorklistItem(sn);

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return retitem;
        }
    }

    public static Worklist GetWorklistItem(string account)
    {
        using (Connection conn = new Connection())
        {
            Worklist wlt = null;
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(account);
                wlt = conn.OpenWorklist();

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return wlt;
        }
    }
    public static Worklist GetAllWorkItem(string Account)
    {
        using (Connection conn = new Connection())
        {
            Worklist retitem = null;
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(Account);
                retitem = conn.OpenWorklist();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return retitem;
        }
    }
    #region
    /// <summary>
    /// 根据流水号  和 员工AD账号返回WorklistItem
    /// </summary>
    /// <param name="sn"></param>
    /// <returns></returns>
    public static WorklistItem GetWorklistItemWithSN(string sn, string Account)
    {
        using (Connection conn = new Connection())
        {
            WorklistItem retitem = null;
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(Account);
                retitem = conn.OpenWorklistItem(sn);

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return retitem;
        }
    }
    #endregion


    /// <summary>
    /// 加签
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="Account"></param>
    /// <param name="nextUser"></param>
    public static void ForwardToNextApprover(string sn, string Account, string nextUser)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(Account);

                WorklistItem wli = conn.OpenWorklistItem(sn);

                string addApproversBy = wli.ActivityInstanceDestination.DataFields["AddApproversBy"].Value.ToString();

                addApproversBy += Account + ";";

                wli.ActivityInstanceDestination.DataFields["AddApproversBy"].Value = addApproversBy;
                wli.Actions["更新"].Execute();
                wli.Redirect(nextUser);

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    /// <summary>
    /// 转签
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="Account"></param>
    /// <param name="nextUser"></param>
    public static void ForwardToNextApprover_Change(string sn, string Account, string nextUser)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(Account);

                WorklistItem wli = conn.OpenWorklistItem(sn);
                wli.Actions["更新"].Execute();
                wli.Redirect(nextUser);

            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    /// <summary>
    /// 加签返回
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="Account"></param>
    public static bool BackToPreApprover(string sn, string Account)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(Account);

                WorklistItem wli = conn.OpenWorklistItem(sn);

                string addApproversBy = wli.ActivityInstanceDestination.DataFields["AddApproversBy"].Value.ToString();

                string[] arrPreApprovers = addApproversBy.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                string lastPreApprover = arrPreApprovers[arrPreApprovers.Length - 1];

                addApproversBy = addApproversBy.Substring(0, addApproversBy.Length - lastPreApprover.Length - 1);

                wli.ActivityInstanceDestination.DataFields["AddApproversBy"].Value = addApproversBy;
                wli.Actions["更新"].Execute();
                wli.Redirect(lastPreApprover);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    /// <summary>
    /// 获取反加签处理人
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="Account"></param>
    public static string GetBackToPreApproverUser(string sn, string Account)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(Account);

                WorklistItem wli = conn.OpenWorklistItem(sn);

                string addApproversBy = wli.ActivityInstanceDestination.DataFields["AddApproversBy"].Value.ToString();

                string[] arrPreApprovers = addApproversBy.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                string lastPreApprover = arrPreApprovers[arrPreApprovers.Length - 1];

                return lastPreApprover;
            }
            catch
            {
                return "";
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public static bool GetActivityDataField(string sn, string fieldName, string UserAccount, out string value)
    {
        using (Connection conn = new Connection())
        {
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(UserAccount);
                WorklistItem wlItem = conn.OpenWorklistItem(sn);
                value = wlItem.ActivityInstanceDestination.DataFields[fieldName].Value.ToString();

                return true;
            }
            catch
            {
                value = "";
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public static bool UpdateDataFieldsByWorkflowId(int wfId, NameValueCollection nvcDataFields)
    {
        using (Connection conn = new Connection())
        {
            bool result = false;
            try
            {
                conn.Open(K2ServerName, GetConnString());
                conn.RevertUser();
                conn.ImpersonateUser(@"Founder\zybpmadmin");

                ProcessInstance inst = conn.OpenProcessInstance(wfId);
                for (int i = 0; i < nvcDataFields.Count; i++)
                {
                    inst.DataFields[nvcDataFields.GetKey(i)].Value = nvcDataFields[i];
                }
                inst.Update();

                result = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }

    public static bool BackToNextApprover(string p, string p_2)
    {
        throw new NotImplementedException();
    }

}