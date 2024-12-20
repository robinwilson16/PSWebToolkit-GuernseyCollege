<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AccessPending.ascx.vb" Inherits="account_ccc_AccessPending" %>


<br /><br />
<ol class="breadcrumb">
    <li><a href="default.aspx">Home</a></li>
    <li class="active">Access Pending</li>
</ol>



<br /><br />

<div class="panel panel-info">
    <div class="panel-heading">Access Pending</div>
        <div>         
            
               

<h4 class="text-center">We have sent you an email. Please click the link in this email to verify your account. To request a new verification code use this form:</h4>
 <div class=" form-group">
                         <label for="email" class="label2 textfieldlabelrequired">Email</label>
                        <input type="email" id="email" class="form-control" name="pre[email]" placeholder="Your E-mail here..." autocomplete="off" required />
 </div> 
<div class=" form-group">
 <asp:Button runat="server" ID="btnResendCode" Text="Resend Verification Code" CssClass="btn btn-primary btn-block"/>
    </div>
            <div class="alert alert-success alert-dismissable" runat="server" visible="false" id="altSuccess">
  <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
  <strong>Success!</strong>We've sent an email to your inbox, check it for the link to verify your account
</div>

 <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />
             </div>

</div>