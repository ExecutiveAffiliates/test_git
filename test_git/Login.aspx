<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Executive Affiliates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <asp:Label ID="lbl_Header" runat="server" Text="Executive Affiliates Reports Login Page" CssClass="title" /><br />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
    <asp:Panel ID="pnl_login" runat="server">
    <asp:Table ID="tbl_Login" CssClass="defaulttable" runat="server">
        <asp:TableRow CssClass="defaulttablerow">
            <asp:TableHeaderCell CssClass="defaulttableheadercell">
                <asp:Label ID="lbl_Username" runat="server" Text="Username"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell CssClass="defaulttablecell">
                <asp:TextBox ID="txt_Username" TextMode="SingleLine" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell CssClass="defaulttableheadercell">
                <asp:Label ID="lbl_Password" runat="server" Text="Password"></asp:Label>
            </asp:TableHeaderCell>
            <asp:TableCell CssClass="defaulttablecell">
                <asp:TextBox ID="txt_Password" TextMode="Password" runat="server" Width="150px" MaxLength="255"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="center" ColumnSpan="2">
                <asp:Button ID="btn_Login" runat="server" Text="Login" OnClick="btn_Login_OnClick" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label CssClass="loginfail" ID="lbl_LoginFail" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    </asp:Panel>
    <asp:Panel ID="pnl_sitedown" runat="server">
        <asp:Label ID="lbl_sitedown" runat="server"></asp:Label>
    </asp:Panel>
</asp:Content>

 