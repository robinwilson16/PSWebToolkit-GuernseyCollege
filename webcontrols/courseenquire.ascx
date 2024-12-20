<%@ control Language="VB"  AutoEventWireup="false" CodeFile="courseenquire.ascx.vb" Inherits="courseenquire"   %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

    <!--The CourseEnquireAction adds the offering to the basket as an application-->
    <cc1:CourseEnquireAction ID="CourseEnquireAction" runat="server" />
    <h3>The following Enquiry has been added to your list of courses:</h3>
    
    
  
 <cc1:OfferingFeesDisplayAll runat="server"  />

<p>
        While enquiring online you will not have to pay, any payment of fees will occur at the college when you enrol.
    </p>
    <br />
    <br />
    <p>
        Total courses selected: &nbsp;<span class="wglyphicon glyphicon-shopping-cart"></span><cc1:ShoppingBasketTotals ID="ShoppingBasket1"
            runat="server" HideIcon="true" />
    </p>
        <cc1:CCCButton id="btnBack" runat="server" Text="Search for more courses" ImageResource="btnBack" LinkResource="search_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue to Checkout" ImageResource="btnContinue" LinkResource="checkout_aspx" CausesValidation="true" />


