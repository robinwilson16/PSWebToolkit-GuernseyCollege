<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DoPasswordReset.ascx.vb" Inherits="account_ccc_DoPasswordReset" %>

<br /><br />
<ol class="breadcrumb">
    <li><a href="default.aspx">Home</a></li>
    <li class="active">Reset Password</li>
</ol>


<br /><br />

<div class="panel panel-info">
                        <div class="panel-heading">To change your password, enter your new password below</div>
                                     
                    <div class=" form-group">
                        <label for="passwordnew" class="label2 ">New Password</label>
                        <input type="password" id="passwordnew" name="passwordnew" class="form-control" placeholder="New password here..." autocomplete="off"   />
                        <asp:Label ID="lblPasswordInfo" runat="server" Font-Size="12px"></asp:Label>
                    </div>
                    <div class=" form-group">
                        <label for="password_mirror" class="label2 ">Retype New Password</label>
                        <input type="password" id="password_mirror" name="password_mirror" class="form-control"  placeholder="Retype your new password..." autocomplete="off"   oninput="check(this)"/>
                    </div>
                   
                    <div class=" form-group">
                        <p runat="server" id="errtext"></p>
                    </div>
                    <div class=" form-group">
                        
                       <asp:Button runat="server" ID="btnUpdate" Text="Update" CssClass="btn btn-primary btn-block" />
                        
                    </div>
</div>
 <div class=" form-group">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />
               </div>
<script type="text/javascript" src="<%= ResolveUrl("~/account/ccc/DoPasswordReset.js") %>"></script>