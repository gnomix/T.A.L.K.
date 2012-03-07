<%@ Page Title="" Language="C#" MasterPageFile="~/Talk_Pro.Master" AutoEventWireup="true" CodeBehind="CodifMgt.aspx.cs" Inherits="Wybecom.TalkPortal.CodifMgt" Theme="Wybecom" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderToolBar" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderDisplay" runat="server">
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" 
        ConnectionString="name=CodifEntities" DefaultContainerName="CodifEntities" 
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" 
        EntitySetName="Codif" >
    </asp:EntityDataSource>
    <div style="text-align:center; margin: 0 25% 0 25%;">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="codifid" DataSourceID="EntityDataSource1" ForeColor="#333333" 
        GridLines="None" Width="100%">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Image" 
                CancelImageUrl="~/img/1302720979_refresh.png" 
                DeleteImageUrl="~/img/1302721031_edit-trash.png" 
                EditImageUrl="~/img/1302721125_27-Edit Text.png" 
                InsertImageUrl="~/img/1302721178_insert-link.png" 
                NewImageUrl="~/img/1302721246_edit_add.png" 
                SelectImageUrl="~/img/1302721334_edit-select-all.png" 
                UpdateImageUrl="~/img/1302721388_db_update.png" />
            <asp:BoundField DataField="codif1" HeaderText="Codification" 
                SortExpression="codif1" >
            <ControlStyle ForeColor="DimGray" />
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                ForeColor="DimGray"  />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="active" HeaderText="Activée" 
                SortExpression="active" >
            <ControlStyle ForeColor="DimGray" />
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                ForeColor="DimGray" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:CheckBoxField>
            <asp:BoundField DataField="codifid" HeaderText="codifid" ReadOnly="True" 
                SortExpression="codifid" Visible="False" />
        </Columns>
        <FooterStyle BackColor="#F2B5BE" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#F2B5BE" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#F2B5BE" Font-Bold="True" ForeColor="#333333" Font-Underline="false" Font-Size="Large" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="#F8DADE" ForeColor="#284775" />
    </asp:GridView>
    <hr style="margin: 5px 0 5px 0; height:2px; color:#F2B5BE; background-color:#F2B5BE" />
    <asp:FormView ID="FormView1" runat="server" CellPadding="4" 
        DataSourceID="EntityDataSource1" ForeColor="#333333" 
        onitemcommand="FormView1_ItemCommand" ondatabound="FormView1_DataBound" >
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <InsertItemTemplate>
            <div style="color:#ffffff; font-weight:bold; padding: 5px 5px 5px 5px;" ><asp:Literal runat="server" Text="<%$Resources:Localization, AddCodif %>" /></div>
            <div style="padding: 5px 5px 5px 5px;"><asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox></div>
            <div style="padding: 5px 5px 5px 5px;"><asp:ImageButton ID="ImageButton1" runat="server" CommandName="InsertCode" ImageUrl="~/img/1302721246_edit_add.png"  /></div>
        </InsertItemTemplate>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
    </asp:FormView>
    </div>
</asp:Content>
