<%@ Control Language="VB" AutoEventWireup="false" CodeFile="address_QASProOnDemand.ascx.vb" Inherits="webcontrols_address_QASProOnDemand" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>
<%@ Register Assembly="QASProOnDemand" Namespace="Experian" TagPrefix="q"  %>
 
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
     
        <script type="text/javascript" src="/Scripts/ondemand/jquery/jquery.tmpl.min.js"></script>
        <script type="text/javascript" src="/Scripts/ondemand/jquery/jquery.cookie.js"></script>
        <script type="text/javascript" src="/Scripts/ondemand/jquery/jquery.showLoading.min.js"></script>
        <script type="text/javascript" src="/Scripts/ondemand/qas-ic.js"></script>
        <script type ="text/javascript" src="/Scripts/ondemand/qas-ic.addresscapture.js"></script>
        <script type="text/javascript" src="<%= ResolveUrl("~/webcontrols/address_QASProOnDemand.js") %>"></script>
 
    <body class="qas-font" style="display:none;" onload="">
        <br /><br />
       <div class="alert alert-info" id="alert1" runat="server">
        <h4>Address</h4>
           </div><br />
<div class="panel panel-info" id="panel1" runat="server">
           <div id="QAS_Container" class="qas-container qas-content">
        	<div style="clear:both;"></div>
                   
               </div></div>
		   
        
       
    </body>