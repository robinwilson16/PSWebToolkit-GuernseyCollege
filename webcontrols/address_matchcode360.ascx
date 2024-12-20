<%@ Control Language="VB" AutoEventWireup="false"  CodeFile="address_matchcode360.ascx.vb" Inherits="webcontrols_address_matchcode360" EnableViewState="true" %>
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
    <table>
    <tr>
    <td style="width: 600px" >
        <div class="alert alert-info" id="alert1" runat="server">
        <h4>
        Confirm Your Address (<cc1:currentaddresstypetext id="CurrentAddressTypeText1" runat="server"></cc1:currentaddresstypetext>
        )
    </h4>
       
                        <div id="noCapture" runat="server" visible="false">Automatic address capture not available.</div>
                        <div id="pnlAmb" runat="server" Visible="false" >
                        <strong>Please select from the list below:</strong>
                        <br />
                        <div>
                        
                        </div>
                            <asp:Table ID="AmbTable" runat="server"  />
                            
                        </div>
        </div>


        <div id="Panel1" class="panel panel-info" runat="server">
            <div class="panel-heading">Address Search</div>
        <table class="DemoTable">
        <tbody>
        

        <tr runat="server" id="rowInputCountry">
        <th style="text-align:left;">Country</th>
        <td>
        <asp:DropDownList ID="cboCountry" runat="server" />
        
        </td>
        </tr>

        <tr runat="server" id="rowInputHouseNumber">
        <th style="text-align:left;">
        <span  id="lblHouseNo" style="text-align: left">House No</span>
        </th>
        <td>
        <asp:TextBox id="txtHouseNo" runat="server" style="width:180px;"/>
            <img src="_images/requiredfieldicon.png" alt="Field is Required"/> 
        </td>
        </tr>
        <tr runat="server" id="rowInputPostalCode">
        <th style="text-align:left;">
        <span id="lblPostalCode">Postcode</span>
        </th>
        <td>
        <asp:TextBox id="txtPostcode" runat="server" style="width:180px;"/>
             <img src="_images/requiredfieldicon.png" alt="Field is Required"/> 
        </td>
        </tr>

        <tr runat="server" id="rowInputStreet">
        <th style="text-align:left;">
        <span  id="lblStreet">Street</span>
        </th>
        <td>
        <asp:TextBox id="txtStreet" runat="server" style="width:180px;"/>
             <img src="_images/requiredfieldicon.png" alt="Field is Required"/> 
        </td>
        </tr>

        <tr runat="server" id="rowInputTown">
        <th style="text-align:left;">
        <span id="lblTown2">Town</span>
        </th>
        <td>
        <asp:TextBox id="txtTown" runat="server" style="width:180px;"/>
             <img src="_images/requiredfieldicon.png" alt="Field is Required"/> 
        </td>
        </tr>

        <tr runat="server" id="rowOutputAddress1">
        <th style="text-align:left;">
        <span id="lblAddress1">Address</span>
        </th>
        <td>
        <asp:TextBox id="txtAddress1" runat="server" style="width:180px;"/>
        </td>
        </tr>

        <tr runat="server" id="rowOutputAddress2">
        <th style="text-align:left;">
        <span id="lblAddress2"></span>
        </th>
        <td>
        <asp:TextBox id="txtAddress2" runat="server" style="width:180px;"/>
        </td>
        </tr>

        <tr runat="server" id="rowOutputAddress3">
        <th style="text-align:left;">
        <span id="lblTown">Town</span>
        </th>
        <td>
        <asp:TextBox id="txtAddress3" runat="server" style="width:180px;"/>
        </td>
        </tr>

        <tr runat="server" id="rowOutputAddress4">
        <th style="text-align:left;">
        <span id="lblCounty">County</span>
        </th>
        <td>
        <asp:TextBox id="txtAddress4" runat="server" style="width:180px;"/>
        </td>
        </tr>

        <tr runat="server" id="rowOutputPostcode">
        <th style="text-align:left;">
        <span id="lblPostcode">Postcode</span>
        </th>
        <td>
        <asp:TextBox id="txtOutputPostcode" runat="server" style="width:90px;"/>
        </td>
        </tr>









        <tr>
        <th id="ButtonCol">&nbsp;</th>
        <td style="padding-top:8px;">
        
        <Button class="btn btn-primary" ID="btnSearch" runat="server" type="button">Search</Button>
        <Button class="btn btn-info" ID="btnClear" runat="server" type="button" >Clear</Button>
        <br />
        <br />
        </td>
        </tr>
        <tr id="lblNoResults" runat="server" visible="false">
									<td></td>
									<td class="alert alert-danger">No results found</td>

								</tr>
        <tr id="lblNoPostcode" runat="server"><td></td>
        <td ><asp:LinkButton ID="lnkNoPostCode"
            runat="server">I don't know the postcode</asp:LinkButton></td></tr>
        <tr id="lblKnowPostcode"  runat="server" visible="False"><td></td>
        <td ><asp:LinkButton ID="lnkPostCode"
            runat="server">I know the postcode</asp:LinkButton></td></tr>
        <tr id="lblManualAddress"  runat="server" visible="true"><td></td>
        <td >
            <asp:LinkButton ID="lnkManualAddress"
            runat="server" >Enter address manually / My address is not listed</asp:LinkButton></td></tr>
        


        

        </tbody>
        </table>
        </div>
        <br />
        
        <asp:Panel ID="pnlTermTimeAddress" runat="server" Width="100%">
        <p> Do you have a different term-time address:</p>
        <asp:RadioButtonList ID="optAnotherAddress" runat="server" ToolTip="Do you have a different term-time address:">
            <asp:ListItem  Value="0" Selected="True">My term-time Address is the same as the main 
            address</asp:ListItem>
            <asp:ListItem Value="1">I have a different term-time Address</asp:ListItem>
        </asp:RadioButtonList>
                        </asp:Panel>
        <br/>

        

      
    </td>
    </tr>
    
    </table>


           
         <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />


