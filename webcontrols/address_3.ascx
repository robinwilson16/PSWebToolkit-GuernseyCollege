<%@ control Language="VB"  AutoEventWireup="false" CodeFile="address_3.ascx.vb" Inherits="address_3"  %>

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
    <div class="panel-heading">  Enter Your Address (<cc1:currentaddresstypetext id="CurrentAddressTypeText1" runat="server"></cc1:currentaddresstypetext>)</div>

     <p>
        <asp:Literal ID="LiteralMessage" runat="server" EnableViewState="False"></asp:Literal></p>

    <div class="row">
        <div class="col-sm-12">
            <table>
        <tr>
            <td>
                <p>
                    <strong>Address</strong></p>
            </td>
            <td width="10">
            </td>
            <td>
                <input id="TextRefinement" runat="server" name="TextRefinement" size="10" type="text" /></td>
            <td>
                &nbsp; in&nbsp;</td>
            <td>
                <asp:Literal ID="LiteralRefineLine" runat="server" EnableViewState="False"></asp:Literal></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:Literal ID="LiteralRefineAddress" runat="server" EnableViewState="False"></asp:Literal></td>
        </tr>
    </table>
    <%
        ' carry through values from earlier pages
        RenderRequestString(com.qas.prowebintegration.Constants.FIELD_DATA_ID)
        RenderRequestString(com.qas.prowebintegration.Constants.FIELD_COUNTRY_NAME)
        RenderRequestArray(com.qas.prowebintegration.Constants.FIELD_INPUT_LINES)
        RenderRequestString(FIELD_PROMPTSET)
        RenderRequestString(FIELD_PICKLIST_MONIKER)
        RenderRequestString(FIELD_REFINE_MONIKER)
    %>
        </div>
    </div>
    </div>
   
    
    <input id="ButtonBack" runat="server" type="button" value="Go Back" visible="false" />&nbsp;
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" />
    <br />


