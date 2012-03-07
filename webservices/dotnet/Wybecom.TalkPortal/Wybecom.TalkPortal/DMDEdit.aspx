<%@ Page Title="<%$Resources:Localization, DMDEditTitle %>" Language="C#" Culture="auto" UICulture="auto" MasterPageFile="~/Talk_Pro.Master" AutoEventWireup="true" CodeBehind="DMDEdit.aspx.cs" Inherits="Wybecom.TalkPortal.DMDEdit" Theme="Wybecom" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
        var styleToSelect;

        // Add click handlers for buttons to show and hide modal popup on pageLoad
        function pageLoad() {
            $addHandler($get("sbox-btn-close"), 'click', hideModalPopupViaClient);
        }

        function hideModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
        }
    </script>
    <link rel="Stylesheet" type="text/css" href="css/dmd/modal.css" />
    <link rel="Stylesheet" href="css/black.css" type="text/css" />
    <link rel="Stylesheet" href="css/black_bg.css" type="text/css" />
    <link rel="Stylesheet" href="css/template.css" type="text/css" />
    <link rel="Stylesheet" type="text/css" href="css/dmd/rounded.css" />
    <link rel="Stylesheet" type="text/css" href="css/dmd/system.css" />
    <link rel="Stylesheet" type="text/css" href="css/dmd/template.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderToolBar" runat="server">
<table class="toolbar" width="100%">
        <tr>
            <td style="width:50px;" id="toolbar-save">
                <asp:LinkButton ID="lbtnSave" runat="server" CssClass="toolbar" 
                    onclick="lbtnSave_Click" >
                    <span class="icon-32-save" >
                    </span>
                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDSave %>" />
                </asp:LinkButton>
            </td>

            <td style="width:50px;" id="toolbar-apply">
                <asp:LinkButton ID="lbtnApply" runat="server" CssClass="toolbar" 
                    onclick="lbtnApply_Click">
                    <span class="icon-32-apply" >
                    </span>
                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDApply %>" />        
                </asp:LinkButton>
            </td>

            <td style="width:50px;" id="toolbar-cancel">
                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="toolbar" 
                    onclick="lbtnCancel_Click" >
                    <span class="icon-32-cancel" >
                    </span>
                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDClose %>" />
                </asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDEditTitle %>" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderDisplay" runat="server">
<div style="width:100%">
    <asp:UpdatePanel ID="upDisplay" runat="server">
        <ContentTemplate>
            <table class="admintable" width="100%">
                <tr valign="top">  
                    <td colspan="2">
                        <fieldset>
                            <legend><asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddDirectoryName %>" /></legend>
                            <table width="100%">
			                    <tr>
			                        <td class="key" width="20%" align="right">
						                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddDirectoryNameLabel %>" />
					                </td>
					                <td width="80%">
                                        <asp:TextBox ID="tbDirectoryName" runat="server" Enabled="false"></asp:TextBox>
					                </td>
				                </tr>
				            </table>
                        </fieldset>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr valign="top">
			        <td width="20%">
				        <fieldset>
					        <legend>
						        <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddDirectoryType %>" />				
					        </legend>
                            <asp:RadioButtonList ID="rblDirectoryType" Enabled="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblDirectoryType_SelectedIndexChanged" EnableViewState="true">
                                <asp:ListItem Text="SQL" Value="SQL" />
                                <asp:ListItem Text="LDAP" Value="LDAP" />
                                <asp:ListItem Text="CISCO" Value="CISCO" />
                            </asp:RadioButtonList>
                        </fieldset>
			        </td>
			        <td width="30%">
			            &nbsp;
			        </td>
			        <td>
			            &nbsp;
			        </td>
	            </tr>
	            <tr valign="top">
	                <td colspan="2">
                        <asp:Panel ID="pnlSqlSettings" runat="server">
                                <fieldset id="sqlSettings">
	                            <legend>
	                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddSQLFieldset %>" />
	                            </legend>
	                            <table width="100%">
					                <tr>
							            <td class="key" width="20%" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddSQLDSN %>" />
							            </td>
							            <td width="80%">
                                            <asp:TextBox ID="tbSqlDsn" runat="server"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddSQLUID %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbSqlUid" runat="server"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddSQLPassword %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbSqlPwd" runat="server" TextMode="Password"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddSQLCommand %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbSqlCommand" runat="server" TextMode="MultiLine" Width="250"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddSQLFilter %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbSqlFilter" runat="server" TextMode="MultiLine" Width="250"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
						                <td colspan="2">
                                            <asp:Button ID="btnTestSql" runat="server" Text="<%$Resources:Localization, DMDTest %>" 
                                                onclick="btnTestSql_Click" />
						                </td>
						            </tr>
						            <tr>
							            <td colspan="2">
							                <fieldset>
							                    <legend>
							                        <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldFormatterFieldset %>" />
							                    </legend>
							                    <table width="100%">
							                        <tr>
							                            <td class="key" width="15%" align="right">
								                            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldName %>" />
							                            </td>
							                            <td width="25%">
                                                            <asp:TextBox ID="tbFieldFormatterFieldName" runat="server"></asp:TextBox>
							                            </td>
							                            <td width="25%" align="right">
							                                <asp:Button ID="tbAddSQLFieldFormatter" runat="server" Text="<%$Resources:Localization, DMDAddField %>" Width="75" 
                                                                onclick="tbAddSQLFieldFormatter_Click"/>
							                            </td>
							                            <td width="35%" rowspan="3">
                                                            <asp:ListBox ID="lbSQLFieldFormatters" runat="server" AutoPostBack="true" 
                                                                Width="250" SelectionMode="Single" Rows="7" 
                                                                onselectedindexchanged="lbSQLFieldFormatters_SelectedIndexChanged"></asp:ListBox>
							                            </td>
							                        </tr>
							                        <tr>
							                            <td class="key" align="right">
							                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldPattern %>" />
							                            </td>
							                            <td>
                                                            <asp:TextBox ID="tbFieldFormatterValue" runat="server"></asp:TextBox>
							                            </td>
							                            <td align="right">
                                                            <asp:Button ID="btnDeleteSQLFieldFormatter" runat="server" Text="<%$Resources:Localization, DMDDeleteField %>" 
                                                                Width="75" onclick="btnDeleteSQLFieldFormatter_Click" />
							                            </td>
							                        </tr>
							                        <tr>
							                            <td class="key" align="right">
							                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldType %>" />
							                            </td>
							                            <td>
							                                <asp:DropDownList ID="ddlSQLFieldType" runat="server">
							                                    <asp:ListItem Selected="True" Text="<%$Resources:Localization, DMDFieldTypeOther %>" Value="Other" />
					                                            <asp:ListItem Value="Identity" Text="<%$Resources:Localization, DMDFieldTypeIdentity %>"></asp:ListItem>
					                                            <asp:ListItem Value="Mail" Text="<%$Resources:Localization, DMDFieldTypeMail %>" />
					                                            <asp:ListItem Value="Telephone" Text="<%$Resources:Localization, DMDFieldTypeTelephone %>" />
					                                            <asp:ListItem Value="GSM" Text="<%$Resources:Localization, DMDFieldTypeGSM %>" />
							                                </asp:DropDownList>
							                            </td>
							                        </tr>
							                    </table>
							                </fieldset>
							            </td>
						            </tr>
						            <tr>
						        <td colspan="2">
						            <fieldset>
						                <legend><asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoFilters %>" /></legend>
						                <table width="100%">
						                    <tr>
							                    <td class="key" width="65%" align="right">
								                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoFirstNameField %>" />
							                    </td>
							                    <td width="35%">
                                                    <asp:TextBox ID="tbSQLFirstNameFilterMap" runat="server"></asp:TextBox>
							                    </td>
							                </tr>
							                <tr>
							                    <td class="key" width="65%" align="right">
								                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoLastNameField %>" />
							                    </td>
							                    <td width="35%">
                                                    <asp:TextBox ID="tbSQLLastNameFilterMap" runat="server"></asp:TextBox>
							                    </td>
							                </tr>
							                <tr>
					                            <td class="key" width="65%" align="right">
								                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoTelephoneField %>" />
							                    </td>
							                    <td width="35%">
                                                    <asp:TextBox ID="tbSQLTelephoneNumberFilterMap" runat="server"></asp:TextBox>
							                    </td>
					                        </tr>
							            </table>
						            </fieldset>
						        </td>
						    </tr>
					            </table>
	                        </fieldset></asp:Panel>
                        <asp:Panel ID="pnlLdapSettings" runat="server" Visible="false">
                                <fieldset id="ldapSettings">
	                            <legend>
	                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPFieldset %>" />
	                            </legend>
	                            <table width="100%">
					                <tr>
							            <td class="key" width="20%" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPServer %>" />
							            </td>
							            <td width="80%">
                                            <asp:TextBox ID="tbLdapServer" runat="server"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPAuthentication %>" />
							            </td>
							            <td>
                                            <asp:DropDownList ID="ddlLdapAuthentication" runat="server">
                                                <asp:ListItem Text="Anonymous" Value="Anonymous" />
                                                <asp:ListItem Text="Basic" Value="Basic" />
                                                <asp:ListItem Text="Digest" Value="Digest" />
                                                <asp:ListItem Text="Dpa" Value="Dpa" />
                                                <asp:ListItem Text="External" Value="External" />
                                                <asp:ListItem Text="Kerberos" Value="Kerberos" />
                                                <asp:ListItem Text="Msn" Value="Msn" />
                                                <asp:ListItem Text="Negociate" Value="Negociate" />
                                                <asp:ListItem Text="Ntlm" Value="Ntlm" />
                                                <asp:ListItem Text="Sicily" Value="Sicily" />
                                            </asp:DropDownList>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPUser %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbLdapUser" runat="server"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPUserPassword %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbLdapUserPassword" runat="server" TextMode="Password"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPTargetOU %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbLdapTargetOu" runat="server" Width="250"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPFilter %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbLdapFilter" runat="server" Width="250"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPPageSize %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbLdapPageSize" runat="server"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
							            <td class="key" align="right">
								            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPNumberPage %>" />
							            </td>
							            <td>
                                            <asp:TextBox ID="tbLdapNbPages" runat="server"></asp:TextBox>
							            </td>
						            </tr>
						            <tr>
						                <td colspan="2">
						                    <fieldset>  
						                        <legend><asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPAttributeFieldset %>" /></legend>
						                        <table width="100%">
							                        <tr>
							                            <td class="key" width="15%" align="right">
								                            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddLDAPAttribute %>" />
							                            </td>
							                            <td width="25%">
                                                            <asp:TextBox ID="tbLdapAttribute" runat="server"></asp:TextBox>
							                            </td>
							                            <td width="25%" align="right">
                                                            <asp:Button ID="btnAddLdapAttribute" runat="server" Text="<%$Resources:Localization, DMDAddAttribute %>" Width="75" 
                                                                onclick="btnAddLdapAttribute_Click" />
                                                         </td>
							                            <td width="35%" rowspan="2">
                                                            <asp:ListBox ID="lbLdapAttribute" runat="server" AutoPostBack="true" Width="250" SelectionMode="Single" Rows="7"></asp:ListBox>
							                            </td>
							                        </tr>
							                        <tr>
							                            <td class="key" align="right">
							                                &nbsp;
							                            </td>
							                            <td>
                                                            &nbsp;
							                            </td>
							                            <td align="right">
                                                            <asp:Button ID="btnDeleteLdapAttribute" runat="server" Text="<%$Resources:Localization, DMDDeleteAttribute %>" Width="75" 
                                                                onclick="btnDeleteLdapAttribute_Click" />
							                             </td>
							                        </tr>
							                    </table>
						                    </fieldset>
						                </td>
						            </tr>
						            <tr>
						                <td>
                                            <asp:Button ID="btnTestLdap" runat="server" Text="<%$Resources:Localization, DMDTest %>" 
                                                onclick="btnTestLdap_Click" />
						                </td>
						            </tr>
						            <tr>
							            <td colspan="2">
							                <fieldset>
							                    <legend>
							                        <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldFormatterFieldset %>" />
							                    </legend>
							                    <table width="100%">
							                        <tr>
							                            <td class="key" width="15%" align="right">
								                            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldName %>" />
							                            </td>
							                            <td width="25%">
                                                            <asp:TextBox ID="tbLdapFieldFormatterFieldName" runat="server"></asp:TextBox>
							                            </td>
							                            <td width="25%" align="right">
							                                <asp:Button ID="btnAddLdapFieldFormatter" runat="server" Text="<%$Resources:Localization, DMDAddField %>" Width="75" 
                                                                onclick="btnAddLdapFieldFormatter_Click"/>
							                            </td>
							                            <td width="35%" rowspan="3">
                                                            <asp:ListBox ID="lbLdapFieldFormatters" runat="server" AutoPostBack="true" 
                                                                Width="250" SelectionMode="Single" Rows="7" 
                                                                onselectedindexchanged="lbLdapFieldFormatters_SelectedIndexChanged"></asp:ListBox>
							                            </td>
							                        </tr>
							                        <tr>
							                            <td class="key" align="right">
							                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldPattern %>" />
							                            </td>
							                            <td>
                                                            <asp:TextBox ID="tbLdapFieldFormatterValue" runat="server"></asp:TextBox>
							                            </td>
							                            <td align="right">
                                                            <asp:Button ID="btnDeleteLdapFieldFormatter" runat="server" Text="<%$Resources:Localization, DMDDeleteField %>" 
                                                                Width="75" onclick="btnDeleteLdapFieldFormatter_Click" />
							                            </td>
							                        </tr>
							                        <tr>
							                            <td class="key" align="right">
							                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldType %>" />
							                            </td>
							                            <td>
							                                <asp:DropDownList ID="ddlLdapFieldType" runat="server">
							                                    <asp:ListItem Selected="True" Text="<%$Resources:Localization, DMDFieldTypeOther %>" Value="Other" />
					                                            <asp:ListItem Value="Identity" Text="<%$Resources:Localization, DMDFieldTypeIdentity %>"></asp:ListItem>
					                                            <asp:ListItem Value="Mail" Text="<%$Resources:Localization, DMDFieldTypeMail %>" />
					                                            <asp:ListItem Value="Telephone" Text="<%$Resources:Localization, DMDFieldTypeTelephone %>" />
					                                            <asp:ListItem Value="GSM" Text="<%$Resources:Localization, DMDFieldTypeGSM %>" />
							                                </asp:DropDownList>
							                            </td>
							                        </tr>
							                    </table>
							                </fieldset>
							            </td>
						            </tr>
						            <tr>
						        <td colspan="2">
						            <fieldset>
						                <legend><asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoFilters %>" /></legend>
						                <table width="100%">
						                    <tr>
							                    <td class="key" width="65%" align="right">
								                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoFirstNameField %>" />
							                    </td>
							                    <td width="35%">
                                                    <asp:TextBox ID="tbLdapFirstNameFilterMap" runat="server"></asp:TextBox>
							                    </td>
							                </tr>
							                <tr>
							                    <td class="key" width="65%" align="right">
								                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoLastNameField %>" />
							                    </td>
							                    <td width="35%">
                                                    <asp:TextBox ID="tbLdapLastNameFilterMap" runat="server"></asp:TextBox>
							                    </td>
							                </tr>
							                <tr>
					                            <td class="key" width="65%" align="right">
								                     <asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoTelephoneField %>" />
							                    </td>
							                    <td width="35%">
                                                    <asp:TextBox ID="tbLdapTelephoneNumberFilterMap" runat="server"></asp:TextBox>
							                    </td>
					                        </tr>
							            </table>
						            </fieldset>
						        </td>
						    </tr>
					            </table>
	                        </fieldset></asp:Panel>
	                        <asp:Panel ID="pnlCiscoSettings" runat="server" Visible="false">
                        <fieldset id="ciscoSettings">
	                    <legend>
	                        <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddCiscoDirectoryFieldset %>" />
	                    </legend>
	                    <table width="100%">
					        <tr>
							    <td class="key" width="20%" align="right">
								    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddCiscoDirectoryServer %>" />
							    </td>
							    <td width="80%">
                                    <asp:TextBox ID="tbCiscoServer" runat="server"></asp:TextBox>
							    </td>
						    </tr>
						    <tr>
							    <td class="key" align="right">
								    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddCiscoDirectoryAXLUser %>" />
							    </td>
							    <td>
                                    <asp:TextBox ID="tbAXLUser" runat="server"></asp:TextBox>
							    </td>
						    </tr>
						    <tr>
							    <td class="key" align="right">
								    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddCiscoDirectoryAXLUserPassword %>" />
							    </td>
							    <td>
                                    <asp:TextBox ID="tbAXLUserPassword" runat="server" TextMode="Password" EnableViewState="true"></asp:TextBox>
							    </td>
						    </tr>
						    <tr>
						        <td colspan="2">
                                    <asp:Button ID="btnTestCisco" runat="server" Text="<%$Resources:Localization, DMDTest %>" 
                                        onclick="btnTestCisco_Click" />
						        </td>
						    </tr>
						    <tr>
							    <td colspan="2">
							        <fieldset>
							            <legend>
							                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldFormatterFieldset %>" />
							            </legend>
							            <table width="100%">
							                <tr>
							                    <td class="key" width="15%" align="right">
								                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldName %>" />
							                    </td>
							                    <td width="25%">
                                                    <asp:TextBox ID="tbCiscoFieldFormatterFieldName" runat="server"></asp:TextBox>
							                    </td>
							                    <td width="25%" align="right">
							                        <asp:Button ID="btnAddCiscoFieldFormatter" runat="server" Text="<%$Resources:Localization, DMDAddField %>" Width="75" 
                                                        onclick="btnAddCiscoFieldFormatter_Click"/>
							                    </td>
							                    <td width="35%" rowspan="3">
                                                    <asp:ListBox ID="lbCiscoFieldFormatters" runat="server" AutoPostBack="true" 
                                                        Width="250" SelectionMode="Single" Rows="7" 
                                                        onselectedindexchanged="lbCiscoFieldFormatters_SelectedIndexChanged"></asp:ListBox>
							                    </td>
							                </tr>
							                <tr>
							                    <td class="key" align="right">
							                        <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldPattern %>" />
							                    </td>
							                    <td>
                                                    <asp:TextBox ID="tbCiscoFieldFormatterValue" runat="server"></asp:TextBox>
							                    </td>
							                    <td align="right">
                                                    <asp:Button ID="btnDeleteCiscoFieldFormatter" runat="server" Text="<%$Resources:Localization, DMDDeleteField %>" 
                                                        Width="75" onclick="btnDeleteCiscoFieldFormatter_Click" />
							                    </td>
							                </tr>
							                <tr>
					                            <td class="key" align="right">
					                                <asp:Literal runat="server" Text="<%$Resources:Localization, DMDFieldType %>" />
					                            </td>
					                            <td>
					                                <asp:DropDownList ID="ddlCiscoFieldType" runat="server">
					                                    <asp:ListItem Selected="True" Text="<%$Resources:Localization, DMDFieldTypeOther %>" Value="Other" />
					                                    <asp:ListItem Value="Identity" Text="<%$Resources:Localization, DMDFieldTypeIdentity %>"></asp:ListItem>
					                                    <asp:ListItem Value="Mail" Text="<%$Resources:Localization, DMDFieldTypeMail %>" />
					                                    <asp:ListItem Value="Telephone" Text="<%$Resources:Localization, DMDFieldTypeTelephone %>" />
					                                    <asp:ListItem Value="GSM" Text="<%$Resources:Localization, DMDFieldTypeGSM %>" />
					                                </asp:DropDownList>
					                            </td>
					                        </tr>
							            </table>
							        </fieldset>
							    </td>
						    </tr>
						    <tr>
						        <td colspan="2">
						            <fieldset>
						                <legend><asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoFilters %>" /></legend>
						                <table width="100%">
						                    <tr>
							                    <td class="key" width="65%" align="right">
								                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoFirstNameField %>" />
							                    </td>
							                    <td width="35%">
                                                    <asp:TextBox ID="tbFirstNameFilterMap" runat="server" Enabled="false" Text="firstname"></asp:TextBox>
							                    </td>
							                </tr>
							                <tr>
							                    <td class="key" width="65%" align="right">
								                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoLastNameField %>" />
							                    </td>
							                    <td width="35%">
                                                    <asp:TextBox ID="tbLastNameFilterMap" runat="server" Enabled="false" Text="lastname"></asp:TextBox>
							                    </td>
							                </tr>
							                <tr>
					                            <td class="key" width="65%" align="right">
								                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDCiscoTelephoneField %>" />
							                    </td>
							                    <td width="35%">
                                                    <asp:TextBox ID="tbTelephoneNumberFilterMap" runat="server" Enabled="false" Text="telephonenumber"></asp:TextBox>
							                    </td>
					                        </tr>
							            </table>
						            </fieldset>
						        </td>
						    </tr>
					    </table>
	                </fieldset>
	                </asp:Panel>
                    </td>
	                <td>
                        <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" style="display:none"/>
	                    <asp:Panel ID="Panel1" runat="server" style="display:none;width:350px;height:100px;">
                            <div id="sbox-overlay">
                                
                                
                                <div id="sbox-window">
                                    <div id="sbox-btn-close" style="cursor:pointer;">
                                    </div>
                                    <div id="sbox-content" style="height:100px;width:350px;overflow:hidden;">
                                    <br />
                                    <center>
                                        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BehaviorID="programmaticModalPopupBehavior" TargetControlID="hiddenTargetControlForModalPopup" BackgroundCssClass="body-overlayed" PopupControlID="Panel1" DropShadow="true" >
                        </cc2:ModalPopupExtender>
			        </td>
	            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
	</div>
</asp:Content>
