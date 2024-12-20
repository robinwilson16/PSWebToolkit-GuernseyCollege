<%@ Control Language="VB" AutoEventWireup="false" CodeFile="address_AFDEvolution.ascx.vb" Inherits="webcontrols_address_AFDEvolution" %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>
       <br /><br />
                <ol class="breadcrumb" runat="server" id="breadcrumbapps">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>
                  <li class="active">Address</li>
                </ol>
                <ol class="breadcrumb" runat="server" id="breadcrumbenrols">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>
                  <li class="active">Address</li>
                </ol>
<div class="alert alert-info" id="alert1" runat="server">
<h4>Please enter your postcode or partial address below:</h4>
<p>Search: <asp:TextBox ID="txtLookup" runat="server" CssClass="formtext" ></asp:TextBox>
<asp:Button ID="btnFind" runat="server" Text="Find" CausesValidation="False" UseSubmitBehavior="false" />    
<asp:Button ID="btnCSA" runat="server" Text="" AccessKey="S" CausesValidation="False" UseSubmitBehavior="false" style="display:none;" /></p>
</div>
<br>


<div class="panel panel-info" id="panel1" runat="server">
    <div class="panel-heading">Search Results</div>
    <div class=" form-group">
    <asp:ListBox ID="lstresult" runat="server" Width="75%"></asp:ListBox>
    <br />
        <asp:Button ID="btnFetchAddress" runat="server" Text="Fetch Selected Address" CausesValidation="False" UseSubmitBehavior="false" />

</div>

    <div class=" form-group">
        <cc1:StudentEnrolmentField ID="txtAddress1" runat="server" CustomCaption="Property/Street"
                    IsRequired="true" StudentEnrolmentFieldType="Address1" LabelWidth="200" />
</div>

    <div class=" form-group">
        <cc1:StudentEnrolmentField ID="txtAddress2" runat="server" CustomCaption="Locality"
                    IsRequired="false" StudentEnrolmentFieldType="Address2" LabelWidth="200" />
</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField ID="txtAddress3" runat="server" CustomCaption="Town"
                    IsRequired="false" StudentEnrolmentFieldType="Address3" LabelWidth="200" />
</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField ID="txtAddress4" runat="server" CustomCaption="County"
                    IsRequired="false" StudentEnrolmentFieldType="Address4" LabelWidth="200" />
</div>
    

        <div class=" form-group">
            <label for="postcode" style="width:200px">Postcode</label><input type="text" runat="server"  id="postcode" title="Postcode" placeholder="Enter Postcode" />

    </div>
    </div>





<%-- <asp:Panel ID="pnlTermTimeAddress" runat="server" Width="100%">
        <p> Do you have a different term-time address:</p>
        <asp:RadioButtonList ID="optAnotherAddress" runat="server" ToolTip="Do you have a different term-time address:">
            <asp:ListItem  Value="0" Selected="True">My term-time Address is the same as the main 
            address</asp:ListItem>
            <asp:ListItem Value="1">I have a different term-time Address</asp:ListItem>
        </asp:RadioButtonList>
                        </asp:Panel>--%>


 <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />