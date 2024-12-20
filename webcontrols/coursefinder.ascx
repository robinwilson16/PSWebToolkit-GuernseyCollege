<%@ Control Language="VB" AutoEventWireup="false" CodeFile="coursefinder.ascx.vb" Inherits="webcontrols_coursefinder" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>


<br /><br />
                <ol class="breadcrumb">
                  <li><a href="default.aspx">Home</a></li>
                  <li class="active">Course Picker</li>
                </ol>


  
<div class="panel panel-success">
    <div class="panel-heading">Choose your course(s)</div>
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


 <cc1:CCCButton id="btnBack" runat="server" Text="Home" ImageResource="btnBack" LinkResource="search_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="checkout_aspx" CausesValidation="true" />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />

