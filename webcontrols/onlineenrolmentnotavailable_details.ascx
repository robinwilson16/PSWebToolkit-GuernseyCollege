<%@ Control Language="VB" AutoEventWireup="false" CodeFile="onlineenrolmentnotavailable_details.ascx.vb" Inherits="webcontrols_onlineenrolmentnotavailable_details" %>

<div class="alert alert-danger">


    <h3>
        Error Processing Request</h3>
    
    <p>The system has determined that your request is blank. At least one of the following fields needs to be populated but all were found to be blank:</p>

    <ul>
        <li>Surname</li>
        <li>First Name</li>
        <li>Address Line 1</li>
        <li>Email</li>
    </ul>

    <p>If you believe you have filled these details in, there may be a problem with the system. Please retry your request and if the problem continues, <a href="../webenrolment.aspx?page=~/webcontrols/contact.ascx">contact us</a> for assistance.</p>
    </div>
