<%@ control Language="VB"  AutoEventWireup="false" CodeFile="address_2.ascx.vb" Inherits="address_2"  %>

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
  
<div class="panel panel-success">
    <div class="panel-heading"> Select Your Address (<cc1:currentaddresstypetext id="CurrentAddressTypeText1" runat="server"></cc1:currentaddresstypetext>)</div>

     <p>
    Select one of the following addresses that matched your selection.</p>

    <div class="row">
        <div class="col-sm-12">
            <table>
        <%
            Dim atItems As com.qas.proweb.PicklistItem() = m_Picklist.Items
            Dim i As Integer
            For i = 0 To atItems.Length - 1
        %>
        <tr>
            <td>
                <input name="<%: com.qas.prowebintegration.Constants.FIELD_MONIKER %>" type="radio"
                    value="<%: atItems(i).Moniker %>" /><%: atItems(i).Text %>
            </td>
            <td>
            </td>
            <td>
                <%: atItems(i).Postcode %>
            </td>
        </tr>
        <%
							next%>
    </table>
    <%
    ' carry through values from earlier pages
    RenderRequestString(com.qas.prowebintegration.Constants.FIELD_DATA_ID)
    RenderRequestString(com.qas.prowebintegration.Constants.FIELD_COUNTRY_NAME)
    RenderRequestArray(com.qas.prowebintegration.Constants.FIELD_INPUT_LINES)
    RenderRequestString(FIELD_PROMPTSET)
    RenderRequestString(FIELD_PICKLIST_MONIKER)
    ' hidden field to be populated by client JavaScript, picked out from form PrivateData
    RenderHiddenField(FIELD_MUST_REFINE, "")
    %>
        </div>
    </div>
</div>
   
    <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" />
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" />

