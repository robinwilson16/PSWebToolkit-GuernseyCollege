<%@ Page Language="VB"  MasterPageFile="~/UserControlMasterPage.master" AutoEventWireup="false" CodeFile="Register.aspx.vb" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceholder" Runat="Server">
    
    
<br /><br />
<ol class="breadcrumb">
    <li><a href="/default.aspx">Home</a></li>
    <li class="active">Login</li>
</ol>



<br /><br />

    
<div class="panel panel-info">
   
    <div class="panel-heading">Register Page</div>
        <div class="container">         
            
             
    <div class="form-group">

  

        <asp:CreateUserWizard  ID="CreateUserWizard1" runat="server" 
            ContinueDestinationPageUrl="~/Default.aspx" ContinueButtonStyle-CssClass="btn btn-primary btn-block" TextBoxStyle-CssClass="form-control"  LabelStyle-CssClass="label2 textfieldlabelrequired" CreateUserButtonStyle-CssClass="btn btn-primary btn-block">
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server" Title="Create an Account"  />
                <asp:CompleteWizardStep runat="server" />
            </WizardSteps>
        </asp:CreateUserWizard>
    

    </div></div>

   
    </div>

    </asp:Content>