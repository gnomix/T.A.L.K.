<%@ Page Title="<%$Resources:Localization, CTIControlTitle %>" Culture="auto" UICulture="auto" Language="C#" MasterPageFile="~/Talk_Pro.Master" AutoEventWireup="true" CodeBehind="CTIControl.aspx.cs" Inherits="Wybecom.TalkPortal.CTIControl" Theme="Wybecom" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>

<%@ Register Assembly="Wybecom.TalkPortal.DMD.Controls" Namespace="Wybecom.TalkPortal.DMD.Controls"
    TagPrefix="cc2" %>


<%@ Register Assembly="Wybecom.TalkPortal.CTI.Controls" Namespace="Wybecom.TalkPortal.CTI.Controls"
    TagPrefix="cc1" %>
    
<%@ Register Assembly="Wybecom.TalkPortal.CTI.CallLogs.Controls" Namespace="Wybecom.TalkPortal.CTI.CallLogs.Controls"
    TagPrefix="cc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderDisplay" runat="server">

    <script type="text/javascript" language="javascript">
        function OnCallReceived(sender,eventArgs) {
            alert(eventArgs.get_caller());
        }
        function OnCallEnded(sender, eventArgs) {
            lcs = eventArgs.get_lineControlConnection();
            for (lcCompteur = 0; lcCompteur < lcs.length; lcCompteur = lcCompteur + 1) {
                var lcc = lcs[lcCompteur];
                alert(lcc.callid + ': ' + lcc.contact);
            }
        }

        function OnDirectoryClicked(sender, eventArgs) {

        }

        function OnExceptionHandler(sender, eventArgs) {
            alert("Stack Trace: " + eventArgs.get_stackTrace() + "/r/n" +
          "Error: " + eventArgs.get_message() + "/r/n" +
          "Status Code: " + eventArgs.get_statusCode() + "/r/n" +
          "Exception Type: " + eventArgs.get_exceptionType() + "/r/n" +
          "Timed Out: " + eventArgs.get_timedOut());
        }
        
    </script>

    <cc4:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2">
        <cc4:TabPanel runat="server" HeaderText="<%$Resources:Localization, CTIPanelHeader %>" ID="TabPanel1">
            <ContentTemplate>
            <cc1:CTIClient ID="CTIClient1" runat="server" />
            </ContentTemplate>
        </cc4:TabPanel>
        <cc4:TabPanel ID="TabPanel2" runat="server" HeaderText="<%$Resources:Localization, DMDPanelHeader %>">
            <ContentTemplate>
            <cc2:DMDClient ID="DMDClient1" runat="server" CTIClientID="CTIClient1" showAlert="false" >
			</cc2:DMDClient>
    </ContentTemplate>
    
        </cc4:TabPanel>
        <cc4:TabPanel ID="TabPanel3" runat="server" HeaderText="<%$Resources:Localization, CallLogsPanelHeader %>">
            <HeaderTemplate>
                <asp:Literal runat="server" Text="<%$Resources:Localization, CallLogsPanelHeader %>" />
            </HeaderTemplate>
            <ContentTemplate>
            <cc3:CallLogsControl runat="server" ID="CallLogsControl1" 
                    CssClass="calllogs" CTIClientID="CTIClient1" 
                    dirNumLength="4" 
                    emptyCallLogsText="<%$Resources:Localization, EmptyCallLogsText %>" lookupEnabled="False" 
                    missedTabText="<%$Resources:Localization, CallLogsMissedTabHeader %>" placedTabText="<%$Resources:Localization, CallLogsPlacedTabHeader %>" 
                    presenceEnabled="False" receivedTabText="<%$Resources:Localization, CallLogsReceivedTabHeader %>" showAlert="False">
    </cc3:CallLogsControl>
    </ContentTemplate>
        </cc4:TabPanel>
        <cc4:TabPanel ID="TabPanel4" runat="server" HeaderText="<%$Resources:Localization, SettingsPanelHeader %>">
            <ContentTemplate>
                <fieldset>
                <legend><asp:Literal runat="server" Text="<%$Resources:Localization, CTIClientFieldset %>" /></legend>
                    <table cellspacing="10">
                        <tr style="border-bottom: solid 1px #333333">
                            <td align="center" style="font-size:12px;font-weight:bold;border-bottom: solid 1px #E66C7D;">
                                <asp:Literal runat="server" Text="<%$Resources:Localization, CTIClientSettingsUI %>" />
                            </td>
                            <td align="center" style="font-size:12px;font-weight:bold;border-bottom: solid 1px #E66C7D;">
                                <asp:Literal runat="server" Text="<%$Resources:Localization, CTIClientSettingsTelephony %>" />
                            </td>
                            <td align="center" style="font-size:12px;font-weight:bold;border-bottom: solid 1px #E66C7D">
                                <asp:Literal runat="server" Text="<%$Resources:Localization, CTIClientSettingsFeatures %>" />
                            </td>
                        </tr>
                        <tr>
                            <td style="background: #F8DADE; padding:2px;">
                                <table >
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbDisplay" runat="server" Text="<%$Resources:Localization, CTIClientSettingsCbDisplay %>" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbDisplayCopyright" runat="server" Checked="false" Text="<%$Resources:Localization, CTIClientSettingsCbDisplayCopyright %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbDisplayInput" runat="server" Checked="false" Text="<%$Resources:Localization, CTIClientSettingsCbDisplayInput %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbDisplayPhoneControl" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsCbDisplayPhoneControl %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbDisplayStatus" runat="server" Checked="false" Text="<%$Resources:Localization, CTIClientSettingsCbDisplayStatus %>" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background: #F2B5BE; padding:2px;">
                                <table>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblExtension" runat="server" Text="<%$Resources:Localization, CTIClientSettingsExtension %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbExtension" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblAuthenticationMode" runat="server" Text="<%$Resources:Localization, CTIClientSettingsAuthenticationMode %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:DropDownList ID="ddlAuthenticationMode" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblUser" runat="server" Text="<%$Resources:Localization, CTIClientSettingsUser %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbUser" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblPassword" runat="server" Text="<%$Resources:Localization, CTIClientSettingsPassword %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblCtiService" runat="server" Text="<%$Resources:Localization, CTIClientSettingsPWSCTIServerURL %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbCTIService" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblStateService" runat="server" Text="<%$Resources:Localization, CTIClientSettingsPWSLineControlServerURL %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbStateService" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblDMDService" runat="server" Text="<%$Resources:Localization, CTIClientSettingsDMDWSURL %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbDMD" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblCTICallLogsService" runat="server" Text="<%$Resources:Localization, CTIClientSettingsCallLogsWSURL %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbCTICallLogsService" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background: #F8DADE; padding:2px;">
                                <table >
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbAgent" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsAgentMode %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbVM" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableVoiceMail %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding:2px;">
                                            <asp:Label ID="lblVM" runat="server" Text="<%$Resources:Localization, CTIClientSettingsVoiceMail %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbVM" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbCodif" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableCodif %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding:2px;">
                                            <asp:Label ID="lblCodifMode" runat="server" Text="<%$Resources:Localization, CTIClientSettingsCodifMode %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:DropDownList ID="ddlCodifMode" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbTransfer" runat="server" Checked="false" Text="<%$Resources:Localization, CTIClientSettingsEnableTransfer %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbConsultTransfer" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableConsultTransfer %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbHold" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableHold %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbDND" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableDND %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbDirectory" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableDirectory %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbCallLogs" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableCallLogs %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbPopup" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableTransferPopup %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbLookup" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableTransferLookup %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style="padding:2px;">
                                            <asp:CheckBox ID="cbAlert" runat="server" Checked="true" Text="<%$Resources:Localization, CTIClientSettingsEnableAlert %>"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
						<tr>
							<td colspan="3" align="center" style="border-top: solid 1px #E66C7D;padding:5px;">
								<asp:Button ID="BtnApply" runat="server" Text="<%$Resources:Localization, CTIClientSettingsApply %>" onclick="BtnApply_Click" />
							</td>
						</tr>
                    </table>
        
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Literal runat="server" Text="<%$Resources:Localization, DMDClientFieldset %>" />
                    </legend>
                    <table cellspacing="10">
                        <tr style="border-bottom: 1px solid #E66C7D">
                            <td align="center" style="font-size:12px;font-weight:bold;border-bottom: solid 1px #E66C7D;">
                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDClientSettingsUI %>" />
                            </td>
                            <td align="center" style="font-size:12px;font-weight:bold;border-bottom: solid 1px #E66C7D;">
                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDClientSettingsDirectory %>" />
                            </td>
                            <td align="center" style="font-size:12px;font-weight:bold;border-bottom: solid 1px #E66C7D;">
                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDClientSettingsFeatures %>" />
                            </td>
                        </tr>
                        <tr>
                            <td style="background: #F2B5BE; padding:2px;">
                                <table >
                                    <tr>
                                        <td colspan="2" style="padding:2px;">
                                            <asp:CheckBox ID="cbDMDDisplay" runat="server" Text="<%$Resources:Localization, DMDClientSettingsDisplay %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding:2px;">
                                            <asp:CheckBox ID="cbheader" runat="server" Text="<%$Resources:Localization, DMDClientSettingsShowHeader %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lclCss" runat="server" Text="<%$Resources:Localization, DMDClientSettingsCSS %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbCss" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblSearchHeader" runat="server" Text="<%$Resources:Localization, DMDClientSettingsSearchHeader %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbsheader" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblResultHeader" runat="server" Text="<%$Resources:Localization, DMDClientSettingsResultsHeader %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbrheader" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblTarget" runat="server" Text="<%$Resources:Localization, DMDClientSettingsTargetControl %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbtarget" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background: #F8DADE; padding:2px;">
                                <table >
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblDirectoryName" runat="server" Text="<%$Resources:Localization, DMDClientSettingsDirectory %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbDirectoryName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblcticlient" runat="server" Text="<%$Resources:Localization, DMDClientSettingsCTIClient %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbcticlient" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;"> 
                                            <asp:Label ID="lblDDmdService" runat="server" Text="<%$Resources:Localization, DMDClientSettingsDMDWS %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbDdmd" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                        <table>
                                            <tr>
                                                <td style="padding:2px;">
                                                    <fieldset>
                                                    <legend><asp:Literal runat="server" Text="<%$Resources:Localization, DMDClientSettingsFilterFieldset %>" /></legend>
                                                        <table>
                                                            <tr>
                                                                <td style="padding:2px;">
                                            <asp:ListBox ID="lbFilters" runat="server" Rows="4" OnSelectedIndexChanged="lbFilters_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:ListBox>
                                            <br />
                                            <asp:Button ID="btnDeleteFilter" runat="server" Text="<%$Resources:Localization, DMDClientSettingsDeleteFilter %>" onclick="btnDeleteFilter_Click"/>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="padding:2px;">
                                                        <asp:Label ID="lblFilterName" runat="server" Text="<%$Resources:Localization, DMDClientSettingsFilterLabel %>"></asp:Label>
                                                    </td>
                                                    <td style="padding:2px;">
                                                        <asp:TextBox ID="tbFilterName" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding:2px;">
                                                        <asp:Label ID="lblfiltervalue" runat="server" Text="<%$Resources:Localization, DMDClientSettingsFilterValue %>"></asp:Label>
                                                    </td>
                                                    <td style="padding:2px;">
                                                        <asp:TextBox ID="tbfiltervalue" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" style="padding:2px;">
                                                        <asp:Button ID="btnAddFilter" runat="server" Text="<%$Resources:Localization, DMDClientSettingsAddFilter %>" onclick="btnAddFilter_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background: #F2B5BE; padding:2px;">
                                <table>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbSort" runat="server" Text="<%$Resources:Localization, DMDClientSettingsEnableSorting %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbPage" runat="server" Text="<%$Resources:Localization, DMDClientSettingsEnablePaging %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbdTransfer" runat="server" Text="<%$Resources:Localization, DMDClientSettingsEnableTransfer %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbdconsulttransfer" runat="server" Text="<%$Resources:Localization, DMDClientSettingsEnableConsultTransfer %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbdgsmtransfer" runat="server" Text="<%$Resources:Localization, DMDClientSettingsEnableGSMTransfer %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbdgsmconsulttransfer" runat="server" Text="<%$Resources:Localization, DMDClientSettingsEnableGSMConsultTransfer %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:CheckBox ID="cbdtransferlookup" runat="server" Text="<%$Resources:Localization, DMDClientSettingsEnableTransferLookup %>"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
						<tr>
							<td colspan="3" align="center" style="border-top: solid 1px #E66C7D;padding:5px;">
								<asp:Button ID="btnApplyDMD" runat="server" Text="<%$Resources:Localization, DMDClientSettingsApply %>" onclick="BtnApplyDMD_Click" />
							</td>
						</tr>
                    </table>
                    
                </fieldset>
                <fieldset>
                    <legend><asp:Literal runat="server" Text="<%$Resources:Localization, CallLogsSettingsFieldset %>" /></legend>
                    <table cellspacing="10">
                        <tr style="border-bottom: 1px solid #333333">
                            <td align="center" style="font-size:12px;font-weight:bold;border-bottom: solid 1px #E66C7D;">
                                <asp:Literal runat="server" Text="<%$Resources:Localization, CallLogsSettingsUI %>" />
                            </td>
                            <td align="center" style="font-size:12px;font-weight:bold;border-bottom: solid 1px #E66C7D;">
                                <asp:Literal runat="server" Text="<%$Resources:Localization, CallLogsSettingsCallLogs %>" />
                            </td>
                            <td align="center" style="font-size:12px;font-weight:bold;border-bottom: solid 1px #E66C7D;">
                                <asp:Literal runat="server" Text="<%$Resources:Localization, CallLogsSettingsFeatures %>" />
                            </td>
                        </tr>
                        <tr>
                            <td style="background: #F8DADE; padding:2px;">
                                <table >
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblclcss" runat="server" Text="<%$Resources:Localization, CallLogsSettingsCSS %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbclcss" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblmissed" runat="server" Text="<%$Resources:Localization, CallLogsSettingsMissedTab %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbmissed" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblreceived" runat="server" Text="<%$Resources:Localization, CallLogsSettingsReceivedTab %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbreceived" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblplaced" runat="server" Text="<%$Resources:Localization, CallLogsSettingsPlacedTab %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbplaced" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="blbempty" runat="server" Text="<%$Resources:Localization, CallLogsSettingsEmptyResults %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbempty" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background: #F2B5BE; padding:2px;">
                                <table >
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblclcti" runat="server" Text="<%$Resources:Localization, CallLogsSettingsCTIClient %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbclcti" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblclservice" runat="server" Text="<%$Resources:Localization, CallLogsSettingsCallLogsWSURL %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbclservice" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lblcldmd" runat="server" Text="<%$Resources:Localization, CallLogsSettingsDMDWSURL %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbcldmd" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background: #F8DADE; padding:2px;">
                                <table >
                                    <tr>
                                        <td colspan="2" style="padding:2px;">
                                            <asp:CheckBox ID="cbpresence" runat="server" Text="<%$Resources:Localization, CallLogsSettingsEnablePresence %>"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="40%" style="padding:2px;">
                                            <asp:Label ID="lbldirnumlength" runat="server" Text="<%$Resources:Localization, CallLogsSettingsPresenceLength %>"></asp:Label>
                                        </td>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="tbdnlength" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding:2px;">
                                            <asp:CheckBox ID="cbcllookup" runat="server" Text="<%$Resources:Localization, CallLogsSettingsEnableLookup %>"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
						<tr>
							<td colspan="3" align="center" style="border-top: solid 1px #E66C7D;padding:5px;">
								<asp:Button ID="btncl" runat="server" Text="<%$Resources:Localization, CallLogsSettingsApply %>" onclick="btncl_Click" />
							</td>
						</tr>
                    </table>
                    
                </fieldset>
            </ContentTemplate>
        </cc4:TabPanel>
    </cc4:TabContainer>
    
    
    
</asp:Content>
