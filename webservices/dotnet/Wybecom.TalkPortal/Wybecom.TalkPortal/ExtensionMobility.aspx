<%@ Page Language="C#" Culture="auto" UICulture="auto"  Title="<%$Resources:Localization, EMTitle %>" MasterPageFile="~/Talk_Pro.Master" AutoEventWireup="true" CodeBehind="ExtensionMobility.aspx.cs" Inherits="Wybecom.TalkPortal.ExtensionMobility" Theme="Wybecom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderDisplay" runat="server">
<div>
        
        <asp:Table ID="Table1" runat="server" Height="25px" Width="302px">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Right" VerticalAlign="Middle">
                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:Localization, EMExtension %>"></asp:Label>
</asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" HorizontalAlign="Left" VerticalAlign="Middle">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell3" runat="server" ColumnSpan="2" HorizontalAlign="Center" 
                    VerticalAlign="Middle">
                    <asp:Button ID="Button1" runat="server" Text="<%$Resources:Localization, EMConnect %>" OnClick="Button1_Click"/>
</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
    </div>
</asp:Content>