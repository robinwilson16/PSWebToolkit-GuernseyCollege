<%@ control Language="VB"  AutoEventWireup="false" CodeFile="courseapply.ascx.vb" Inherits="courseapply"  %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />
                <ol class="breadcrumb">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/coursedisplay.ascx">Course Details</a></li>
                  <li class="active">Course Added to Basket</li>
                </ol>

    <!--The CourseApplyAction adds the offering to the basket as an application-->
    <cc1:CourseApplyAction ID="CourseApplyAction" runat="server" />
    <h3>The following Application has been added to your list of courses:</h3>
    
    
 <cc1:OfferingFeesDisplayAll runat="server"  />

<p>
        While applying online you will not have to pay, any payment of fees will occur at the college when you enrol.
    </p>
    <p>
        NOTE: We would like to remind you that
        applying to a course does not nesescarily mean you will get a place.</p>

    <br />
    <br />
    <p>
        Total courses selected: &nbsp;<span class="wglyphicon glyphicon-shopping-cart"></span><cc1:ShoppingBasketTotals ID="ShoppingBasket1"
            runat="server" HideIcon="true" />
    </p>
        <cc1:CCCButton id="btnBack" runat="server" Text="Search for more courses" ImageResource="btnBack" LinkResource="search_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue to Checkout" ImageResource="btnContinue" LinkResource="checkout_aspx" CausesValidation="true" />

