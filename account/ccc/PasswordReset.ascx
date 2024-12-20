<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PasswordReset.ascx.vb" Inherits="account_ccc_PasswordReset" %>

<br /><br />
<ol class="breadcrumb">
    <li><a href="default.aspx">Home</a></li>
    <li class="active">Password Reset</li>
</ol>



<br /><br />

<div class="panel panel-info">
    <div class="panel-heading">Reset Password</div>
        <div>   
<h4 class="text-center">Please enter the email address that you used to register and we will send you a link to reset your password:</h4>
 <div class=" form-group">
                         <label for="email" class="label2 textfieldlabelrequired">Email</label>
                        <input type="email" id="email" class="form-control" name="pre[email]" placeholder="Your E-mail here..." autocomplete="off" required />
 </div> 
<div class=" form-group">
 <asp:Button runat="server" ID="btnResetPassword" Text="Reset Password" CssClass="btn btn-primary btn-block"/>
    </div>
<div class="alert alert-success alert-dismissable" runat="server" visible="false" id="altSuccess">
  <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
  <strong>Success!</strong>We've sent an email to your inbox, check it for the link to reset your password
</div>
            </div>
    </div>
 <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />