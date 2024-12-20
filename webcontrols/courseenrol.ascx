<%@ Control Language="VB"  AutoEventWireup="false" CodeFile="courseenrol.ascx.vb" Inherits="courseenrol"  %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<style>
    #FeeGridTotalCell {
    font-weight:700;
}
</style>
        <!--The CourseEnrolAction adds the offering to the basket as an Enrolment-->
    <cc1:CourseEnrolAction ID="CourseEnrolAction" runat="server" />
<h3>The following Enrolment has been added to your list of courses:</h3>
    
   <cc1:OfferingFeesDisplayAll runat="server"  />


<p>
        NOTE: This enrolment is subject to validation by the college.</p>
    <br />
    <br />

    <p>
        Total courses selected: &nbsp;<span class="wglyphicon glyphicon-shopping-cart"></span><cc1:ShoppingBasketTotals ID="ShoppingBasket1"
            runat="server" HideIcon="true" />
    </p>
        <cc1:CCCButton id="btnBack" runat="server" Text="Search for more courses" ImageResource="btnBack" LinkResource="search_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue to Checkout" ImageResource="btnContinue" LinkResource="checkout_aspx" CausesValidation="true" />


