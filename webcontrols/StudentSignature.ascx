<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StudentSignature.ascx.vb" Inherits="webcontrols_StudentSignature" %>
<link href="<%= ResolveUrl("~/webcontrols/StudentSignature.css") %>" rel="stylesheet" />

<div class="form-group">
       <p>Signature</p>
       <div id="signature"></div>
    <button class="btn btn-danger" id="clear">Clear</button>
   </div>
	
	
<asp:HiddenField runat="server" ID="hidSignature" ClientIDMode="Static" />
 <!--[if lt IE 9]>
	<script type="text/javascript" src="scripts/jSignature/flashcanvas.js"></script>
	<![endif]-->
	<script src="scripts/jSignature/jSignature.min.js"></script>
<script type="text/javascript" src="<%= ResolveUrl("~/webcontrols/StudentSignature.js") %>"></script>