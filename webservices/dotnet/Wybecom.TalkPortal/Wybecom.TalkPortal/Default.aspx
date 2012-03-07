<%@ Page Title="Login" Language="C#" Culture="auto" UICulture="auto" MasterPageFile="~/Talk_Pro.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Wybecom.TalkPortal.Default1" Theme="Wybecom" %>
<%@ Register Assembly="Wybecom.TalkPortal.DMD.Controls" Namespace="Wybecom.TalkPortal.DMD.Controls"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDisplay" runat="server">
   <asp:Login runat="server" ID="Login1" onauthenticate="Login1_Authenticate">
    
   </asp:Login>
</asp:Content>
