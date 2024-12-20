<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RecoverPassword.aspx.vb" Inherits="account_aspnetdb_RecoverPassword" MasterPageFile="~/UserControlMasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceholder" Runat="Server">

    <asp:PasswordRecovery ID="PasswordRecovery1" SuccessPageUrl="Login.aspx" Runat="server" CssClass="formtext">
</asp:PasswordRecovery>
 </asp:Content>

