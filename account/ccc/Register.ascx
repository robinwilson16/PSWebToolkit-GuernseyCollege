<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Register.ascx.vb" Inherits="account_ccc_Register" %>

<br /><br />
<ol class="breadcrumb">
    <li><a href="default.aspx">Home</a></li>
    <li class="active">Register</li>
</ol>

<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/cupertino/jquery-ui.css" />
<script src="//ajax.aspnetcdn.com/ajax/jquery.validate/1.19.1/jquery.validate.js"></script>

<br /><br />


<div class="panel panel-info">
    <div class="panel-heading">Register an account</div>
        <div>         
            
                <%--<form  id="registerform">--%>
                
                    <div class="form-group">
                         <label for="firstname" class="label2 textfieldlabelrequired">First Name</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Your first name here..." autocomplete="off" required autofocus  />
                      </div>
                    <div class="form-group">
                         <label for="surname" class="label2 textfieldlabelrequired">Surname</label>
                        <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" placeholder="Your surname here..." autocomplete="off" required />
                    </div>
                    <div class="form-group">
                         <label for="title" class="label2 textfieldlabelrequired">Title</label>
                        <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control" title="Title" >
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                            <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                            <asp:ListItem Text="Miss" Value="Miss"></asp:ListItem>
                            <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                            <asp:ListItem Text="Dr" Value="Dr"></asp:ListItem>
                            <asp:ListItem Text="Br" Value="Br"></asp:ListItem>
                        </asp:DropDownList>                  
                    </div>
                    <div class="form-group">
                         <label for="sex" class="label2 textfieldlabelrequired">Sex</label>
                        <asp:DropDownList ID="ddlSex" runat="server" CssClass="form-control" title="Sex">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                         <label for="dob" class="label2 textfieldlabelrequired">Date of Birth</label>
                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date" autocomplete="off"  />
                    </div>
                    <div class="form-group">
                         <label for="txtAddress1" class="label2">Address Line 1</label>
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" autocomplete="off" />
                    </div>
                    <div class="form-group">
                         <label for="txtAddress2" class="label2">Address Line 2</label>
                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" autocomplete="off" />
                    </div>
                    <div class="form-group">
                         <label for="txtAddress3" class="label2">Address Line 3</label>
                        <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" autocomplete="off" />
                    </div>
                     <div class="form-group">
                         <label for="txtAddress4" class="label2">Address Line 4</label>
                        <asp:TextBox ID="txtAddress4" runat="server" CssClass="form-control" autocomplete="off" />
                    </div>
                    <div class="form-group">
                         <label for="txtPostcode" class="label2">Postcode</label>
                        <asp:TextBox ID="txtPostcode" runat="server" CssClass="form-control" placeholder="Your postcode here..." autocomplete="off" />
                    </div>
                    <div class="form-group">
                         <label for="txtEmail" class="label2 textfieldlabelrequired">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Your E-mail here..." autocomplete="off" required />
                    </div>                   
                    <div class="form-group">
                        <label for="txtPassword" class="label2 textfieldlabelrequired">Password</label>
                        <asp:TextBox ID="txtPassword" ClientIDMode="Static" runat="server" Cssclass="form-control" TextMode="Password" placeholder="Your password here..." autocomplete="off" required />
                        <asp:Label ID="lblPasswordInfo" runat="server" Font-Size="12px"></asp:Label>
                    </div>                    
                    <div class="form-group">
                        <label for="txtPassword_Mirror" class="label2 textfieldlabelrequired">Retype Password</label>
                        <asp:TextBox ID="txtPassword_Mirror" runat="server" Cssclass="form-control" TextMode="Password" placeholder="Retype your password..." autocomplete="off" required  oninput="check(this)"/>
                    </div>
                   
                    <div class="form-group">
                        
                       <%--<input type="submit" value="Register" id="register" runat="server"/>--%>
                       <asp:Button runat="server" ID="btnRegister" Text="Register" CssClass="btn btn-primary"/>
                        
                    </div>
            <div class="form-group" style="text-align:center;">
            <label id="lblError" runat="server" visible="false" class="text-center text-danger"></label>
               </div>
                <%--</form>--%>
            </div>

       <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />
    </div>
<link href="<%= ResolveUrl("~/account/ccc/Register.css") %>" rel="stylesheet" />
<script type="text/javascript" src="<%= ResolveUrl("~/account/ccc/Register.js") %>"></script>