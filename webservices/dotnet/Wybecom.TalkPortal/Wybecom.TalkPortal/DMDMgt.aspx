<%@ Page Title="<%$Resources:Localization, DMDManagementTitle %>" Language="C#" Culture="auto" UICulture="auto" MasterPageFile="~/Talk_Pro.Master" AutoEventWireup="true" CodeBehind="DMDMgt.aspx.cs" Inherits="Wybecom.TalkPortal.DMDMgt" Theme="Wybecom" %>

<%@ Register Assembly="Wybecom.TalkPortal.CTI.Controls" Namespace="Wybecom.TalkPortal.CTI.Controls"
    TagPrefix="cc2" %>

<%@ Register Assembly="Wybecom.TalkPortal.DMD.Controls" Namespace="Wybecom.TalkPortal.DMD.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="css/black.css" type="text/css" />
    <link rel="Stylesheet" href="css/black_bg.css" type="text/css" />
    <link rel="Stylesheet" href="css/template.css" type="text/css" />
    <link rel="Stylesheet" type="text/css" href="css/dmd/rounded.css" />
    <link rel="Stylesheet" type="text/css" href="css/dmd/system.css" />
    <link rel="Stylesheet" type="text/css" href="css/dmd/template.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderToolBar" runat="server">
    <table class="toolbar">
    <tr>
        <td style="width:50px;" id="toolbar-new" >
        <a href="DMDAdd.aspx" class="toolbar">
        <span class="icon-32-new" >
        </span>
        <asp:Literal runat="server" Text="<%$Resources:Localization, DMDNew %>" />
        </a>
        </td>

        <td style="width:50px;" id="toolbar-popup-Popup" >
        <a class="toolbar" href="#">
        <span class="icon-32-config" 
        >
        </span>
        <asp:Literal runat="server" Text="<%$Resources:Localization, DMDSettings %>" />
        </a>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDisplay" runat="server">
    <asp:ObjectDataSource ID="DirectoryTypeName" runat="server" 
        SelectMethod="GetDirectoriesList" TypeName="Wybecom.TalkPortal.Global" 
        DeleteMethod="DeleteDirectory">
        <DeleteParameters>
            <asp:Parameter Name="name" Type="String" />
        </DeleteParameters>
        
    </asp:ObjectDataSource>
    
    <table width="100%" cellpadding="5" cellspacing="0" border="0">
    <asp:ListView ID="lvListDirectoryType" runat="server" 
        DataSourceID="DirectoryTypeName">
        <ItemTemplate>
            <tr>
                <td class="key" align="right" width="10%" style="padding:5px;">
                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddDirectoryName %>" />
                </td>
                <td width="15%" style="padding:5px;">
                   <a href="DMDEdit.aspx?dir=<%# Eval("name")%>"><%# Eval("name")%></a> 
                </td>
                <td style="padding:5px;">
                    <asp:Button ID="DeleteButton" runat="server" Text="<%$Resources:Localization, DMDDeleteField %>" OnCommand="DeleteButton_Click" CommandName="Delete" CommandArgument=<%# Eval("name")%> />
                </td>
            </tr>
            
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color:#fafad2">
                <td class="key" align="right" width="10%" style="padding:5px;">
                    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDAddDirectoryName %>" />
                </td>
                <td width="15%" style="padding:5px;">
                   <a href="DMDEdit.aspx?dir=<%# Eval("name")%>"><%# Eval("name")%></a> 
                </td>
                <td style="padding:5px;">
                    <asp:Button ID="DeleteButton" runat="server" Text="<%$Resources:Localization, DMDDeleteField %>" OnCommand="DeleteButton_Click" CommandName="Delete" CommandArgument=<%# Eval("name")%> />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <asp:Literal runat="server" Text="<%$Resources:Localization, DMDConfigEmpty %>" />
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <li style="">Item:
                <asp:TextBox ID="ItemTextBox" runat="server" Text='<%# Bind("Item") %>' />
                <br />
                name:
                <asp:TextBox ID="nameTextBox" runat="server" Text='<%# Bind("name") %>' />
                <br />
                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                    Text="Insérer" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                    Text="Désactiver" />
            </li>
        </InsertItemTemplate>
        <LayoutTemplate>
            <ul ID="itemPlaceholderContainer" runat="server" 
                style="font-family: Verdana, Arial, Helvetica, sans-serif;">
                <li ID="itemPlaceholder" runat="server" />
                </ul>
                <div style="text-align: center;background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                </div>
            </LayoutTemplate>
            <EditItemTemplate>
                <li style="background-color: #008A8C; color: #FFFFFF;">Item:
                    <asp:TextBox ID="ItemTextBox" runat="server" Text='<%# Bind("Item") %>' />
                    <br />
                    name:
                    <asp:TextBox ID="nameTextBox" runat="server" Text='<%# Bind("name") %>' />
                    <br />
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                        Text="Mettre à jour" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Annuler" />
                </li>
            </EditItemTemplate>
            <ItemSeparatorTemplate>
                <br />
            </ItemSeparatorTemplate>
            <SelectedItemTemplate>
                <li style="background-color: #008A8C; font-weight: bold;color: #FFFFFF;">Item:
                    <asp:Label ID="ItemLabel" runat="server" Text='<%# Eval("Item") %>' />
                    <br />
                    name:
                    <asp:Label ID="nameLabel" runat="server" Text='<%# Eval("name") %>' />
                    <br />
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" 
                        Text="Supprimer" />
                </li>
            </SelectedItemTemplate>
    </asp:ListView>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <asp:Literal runat="server" Text="<%$Resources:Localization, DMDManagementTitle %>" />
</asp:Content>
