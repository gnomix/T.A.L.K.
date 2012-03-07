<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Widget.aspx.cs" Inherits="Wybecom.TalkPortal.Widget" %>
<%@ Register Assembly="Wybecom.TalkPortal.CTI.Controls" Namespace="Wybecom.TalkPortal.CTI.Controls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="smTalkPortal" runat="server">
        
    </asp:ScriptManager>
    <fieldset title="CTIClient">
                <legend>CTIClient</legend>
                    <table cellpadding="0" cellspacing="10">
                        <tr style="border-bottom: solid 1px #333333">
                            <th>
                                UI
                            </th>
                            <th>
                                Telephony, presence
                            </th>
                            <th>
                                Features
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="5">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbDisplay" runat="server" Text="Display" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbDisplayCopyright" runat="server" Checked="false" Text="Display copyright" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbDisplayInput" runat="server" Checked="false" Text="Display input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbDisplayPhoneControl" runat="server" Checked="true" Text="Display phone control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbDisplayStatus" runat="server" Checked="false" Text="Display status" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellspacing="5">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblExtension" runat="server" Text="Extension"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbExtension" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblAuthenticationMode" runat="server" Text="Authentication mode"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAuthenticationMode" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblUser" runat="server" Text="User"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbUser" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblCtiService" runat="server" Text="Proxy Web Services, CTI Server url "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbCTIService" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblStateService" runat="server" Text="Proxy Web Services, Line Control Server url "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbStateService" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblDMDService" runat="server" Text="DMD Web Services, DMDWS url "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbDMD" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellspacing="5">
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbVM" runat="server" Checked="true" Text="Enable voice mail access"/>
                                            
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblVM" runat="server" Text="Voice mail pilot "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbVM" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbTransfer" runat="server" Checked="false" Text="Enable Transfer"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbConsultTransfer" runat="server" Checked="true" Text="Enable Consult Transfer"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbHold" runat="server" Checked="true" Text="Enable Hold"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbDND" runat="server" Checked="true" Text="Enable DND"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbDirectory" runat="server" Checked="true" Text="Enable Directory"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbCallLogs" runat="server" Checked="true" Text="Enable CallLogs"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbPopup" runat="server" Checked="true" Text="Enable Transfer popup"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbLookup" runat="server" Checked="true" Text="Enable Transfer lookup"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:CheckBox ID="cbAlert" runat="server" Checked="true" Text="Enable Alert"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
        <asp:Button ID="BtnApply" runat="server" Text="Apply CTIClient Settings" onclick="BtnApply_Click" />
                </fieldset>
                <fieldset>
                    <legend>
                        Directory Control
                    </legend>
                    <table cellpadding="0" cellspacing="10">
                        <tr style="border-bottom: 1px solid #333333">
                            <td align="center" style="font-size:12px;font-weight:bold;">
                                UI
                            </td>
                            <td align="center" style="font-size:12px;font-weight:bold;">
                                Directory
                            </td>
                            <td align="center" style="font-size:12px;font-weight:bold;">
                                Features
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="5">
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="cbDMDDisplay" runat="server" Text="Display"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="cbheader" runat="server" Text="Show header"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%">
                                            <asp:Label ID="lclCss" runat="server" Text="Css"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbCss" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%">
                                            <asp:Label ID="lblSearchHeader" runat="server" Text="Search Header"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbsheader" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%">
                                            <asp:Label ID="lblResultHeader" runat="server" Text="Results header"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbrheader" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%">
                                            <asp:Label ID="lblTarget" runat="server" Text="Target Control ID"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbtarget" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellspacing="5">
                                    <tr>
                                        <td align="right" width="40%">
                                            <asp:Label ID="lblcticlient" runat="server" Text="CTIClient ID"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbcticlient" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%">
                                            <asp:Label ID="lblDDmdService" runat="server" Text="DMD Web Service url"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbDdmd" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lbFilters" runat="server" Rows="4">
                                            </asp:ListBox>
                                            <br />
                                            <asp:Button ID="btnDeleteFilter" runat="server" Text="Delete" />
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFilterName" runat="server" Text="Filter label:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbFilterName" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblfiltervalue" runat="server" Text="Filter:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbfiltervalue" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="btnAddFilter" runat="server" Text="Add" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellspacing="5">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbSort" runat="server" Text="Sorting"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbPage" runat="server" Text="Paging"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbdTransfer" runat="server" Text="Enable Transfer"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbdconsulttransfer" runat="server" Text="Enable Consult Transfer"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbdgsmtransfer" runat="server" Text="Enable GSM Transfer"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbdgsmconsulttransfer" runat="server" Text="Enable GSM Consult Transfer"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbdtransferlookup" runat="server" Text="Transfer Lookup"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnApplyDMD" runat="server" Text="Apply DMD Settings" />
                </fieldset>
    </form>
</body>
</html>
