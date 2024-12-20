<%@ Control Language="VB"  AutoEventWireup="false" CodeFile="address_1.ascx.vb" Inherits="address_1"  %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

    <script type="text/javascript">
			/* Set the focus to the first input line */
			function init()
			{
				document.FlatPrompt.<%: com.qas.prowebintegration.Constants.FIELD_INPUT_LINES %>[0].focus();
			}
			
			/* Ensure at least one address line has been entered */
			function validate()
			{
				var aUserInput = document.FlatPrompt.<%: com.qas.prowebintegration.Constants.FIELD_INPUT_LINES %>;
				for (var i=0; i < aUserInput.length; i++)
				{
					if (aUserInput[i].value != "")
					{
						return true;
					}
				}
				document.FlatPrompt.<%: com.qas.prowebintegration.Constants.FIELD_INPUT_LINES %>[0].focus();
				alert("Please enter some address details.");
				return false;
			}
		</script>
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
    <div class="panel-heading">Enter Your Address (<cc1:CurrentAddressTypeText ID="CurrentAddressTypeText1" runat="server" />)</div>

     <p>
                    Please enter your house number and post code below (Then click continue to search
                    for your full address)</p>
    <div class="row">

        <div class="col-sm-12">
            <table>
                        <%        dim asInputLines As String()
                            asInputLines = GetInputLines()

                            Dim i As Integer
                            For i = 0 To m_atPromptLines.Length - 1

                                Dim sValue As String = ""
                                If i < asInputLines.Length Then
                                    sValue = asInputLines(i)
                                End If%>
                        <tr>
                            <td>
                                <%: m_atPromptLines(i).Prompt %>
                            </td>
                            <td>
                            </td>
                            <td>
                                <input name="<%: com.qas.prowebintegration.Constants.FIELD_INPUT_LINES %>" size="<%: m_atPromptLines(i).SuggestedInputLength %>"
                                    type="text" value="<%: sValue %>" />
                                &nbsp; <i>(e.g.<%: m_atPromptLines(i).Example %>)</i>
                            </td>
                        </tr>
                        <%
							next i%>
                        <tr>
                            <td colspan="3">
                                <p>
                                    <asp:LinkButton ID="HyperlinkAlternate" runat="server" EnableViewState="False" Font-Size="X-Small">If you do not know the postal/ZIP code then click here.</asp:LinkButton>&nbsp;</p>
                            </td>
                        </tr>
                    </table>
                    <%
                                                 RenderRequestString(com.qas.prowebintegration.Constants.FIELD_DATA_ID)
                                                 RenderHiddenField(com.qas.prowebintegration.Constants.FIELD_COUNTRY_NAME, "United Kingdom")
                        %>
        </div>
    </div>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
</div>
 <input id="HiddenPromptSet" runat="server" name="HiddenPromptSet" type="hidden" />
                    <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" CausesValidation="false" />
                    <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" />
        <script type="text/javascript">
           init();
        </script>



