<%@ Page Title="Team Management" Language="C#" MasterPageFile="~/Talk_Pro.Master" AutoEventWireup="true" CodeBehind="TeamMgt.aspx.cs" Inherits="Wybecom.TalkPortal.TeamMgt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function buttonQuery_onclick() {
            var ds = new Sys.Data.DataService("./TeamService.svc");
            ds.query("Team", cbSuccess, cbFailure);
        }

        function cbSuccess(result, context, operation) {
            var rList = $get("resultList");
            for (var i in result) {
                var thisCat = result[i];
                var li = document.createElement("li");
                li.appendChild(document.createTextNode(thisCat.teamname);
                rList.appendChild(li);
            }
        }

        function cbFailure(error, context, operation) {
            alert(error);
        }
// ]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderToolBar" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderDisplay" runat="server">
    <input id="buttonQuery" type="button" value="Perform Query" onclick="return buttonQuery_onclick()" />
    <ul id="resultList"></ul>
</asp:Content>
