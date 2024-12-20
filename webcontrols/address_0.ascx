<%@ Control Language="VB" AutoEventWireup="false" CodeFile="address_0.ascx.vb" Inherits="address_0" %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>
<asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <script src="<%=ResolveUrl("~/Webcontrols/address_0.js") %>"></script>
    <br />
    <br />
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


    <div class="panel panel-success" id="panel0" runat="server">
        <div class="panel-heading">Address</div>
        <p>Please select your country from the list below.</p>

        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label>Country</label>
                    <select name="<%: com.qas.prowebintegration.Constants.FIELD_DATA_ID %>" onchange="setCountryValue();">
                        <!-- #include virtual="countries.all.inc" -->
                    </select>
                </div>
            </div>
        </div>

    </div>
    <input name="<%: com.qas.prowebintegration.Constants.FIELD_COUNTRY_NAME %>" type="hidden" />

    <input id="hdnDataID" type="hidden" value="<%: GetDataID() %>" />
    <input id="hdnFieldDataID" type="hidden" value="<%: com.qas.prowebintegration.Constants.FIELD_DATA_ID %>" />
    <input id="hdnCountryName" type="hidden" value="<%: com.qas.prowebintegration.Constants.FIELD_COUNTRY_NAME %>" />
    <p>
        <cc1:CCCButton ID="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_enrolments_aspx" />
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" />
        &nbsp;
    </p>
</asp:PlaceHolder>

