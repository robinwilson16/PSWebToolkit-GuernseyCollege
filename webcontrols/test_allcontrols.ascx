<%@ Control Language="VB" AutoEventWireup="false" CodeFile="test_allcontrols.ascx.vb" Inherits="webcontrols_test_allcontrols" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>
   
<link href="<%= ResolveUrl("~/webcontrols/test_allcontrols.css") %>" rel="stylesheet" />

  <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />


<div runat="server" id="myForm" class="form-inline">


</div>

  
<div class="panel panel-success">
    <div class="panel-heading">Choose your course(s)</div>

    <div class=" form-group">
        <asp:DropDownList ID="fldType" runat="server">
            <asp:ListItem Value="Application"></asp:ListItem>
            <asp:ListItem Value="Enquiry"></asp:ListItem>
            <asp:ListItem Value="Enrolment"></asp:ListItem>
        </asp:DropDownList>
        
        </div>
    <div class=" form-group">
        <asp:DropDownList ID="cboOfferings" runat="server"></asp:DropDownList>
        </div>
    <div class=" form-group">
        <asp:Button ID="btnAdd" runat="server" Text="Add to basket" CssClass="btn btn-success" />
        </div>
    <div class=" form-group">
       <asp:Label runat="server" ID="lblAdded"></asp:Label>
        </div>
</div>

  

