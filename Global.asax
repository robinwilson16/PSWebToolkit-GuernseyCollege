<%@ Application Language="VB" %>
<%@ Import Namespace="CompassCC.CCCSystem.CCCCommon" %>
<%@ Import Namespace="CompassCC.ProSolution.PSWebEnrolmentKit" %>

<script RunAt="server">

    Dim m_blnInitialised As Boolean


    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup

        Initialise()

        m_blnInitialised = True

    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        Try
            'Get the last error that occured and save it in a session
            Dim exceptionContainer As Exception = Server.GetLastError

            'Write the error to the session if it is available
            If Not System.Web.HttpContext.Current.Session Is Nothing Then
                Session("LastError") = exceptionContainer

                'Need to do this to not destroy the session
                'Server.ClearError()

                'Response.Redirect("~/GenericError.aspx")
            End If

        Catch ex As Exception
            Diagnostics.Debug.WriteLine(ex.Message)
        End Try
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started

        If Not m_blnInitialised Then
            'In case of errors during startup (E.g. Database server not running)
            Initialise()
            m_blnInitialised = True
        End If

        Session.LCID = 2057 'Set Locale to UK (Ignoring regional settings of computer)
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub

    Private Sub Initialise()



        CCCDataCache.DisableAllCaching = True
        CCCConfigLibrary.ReadConfigFromConfigFile(IO.Path.Combine(HttpRuntime.AppDomainAppPath, "web.config"))
        CCCConfigLibrary.ReadConfigFromConfigFile(IO.Path.Combine(HttpRuntime.AppDomainAppPath, "web.dbconfig"))
        CCCConfigLibrary.ReadConfigFromMyAppDBConfigFile()
        CCCDatabaseConnectInfoList.LoadDatabaseInfoFromConfig()


        'Set timeout values from config
        CCCConfigLibrary.SetAllFrameworkValuesFromConfig()

        CCCObjectFactory.InitialiseForAssembly(GetType(CompassCC.ProSolution.PSWebEnrolmentKit.EnrolmentRequestDataTable).Assembly, AssemblyType.Business)

        'Configure/check connections
        Dim rules As CCCBrokenRulesList = CCCDatabaseConnectionLibrary.CheckDBConnectionInfos(CCCDatabaseConnectInfoList.GetDatabaseConnectInfos(), False)
        If Not rules.IsAllValid Then
            Throw New CCCTechnicalException("One or more database connections is not valid: " & rules.GetBrokenRulesText())
        End If

        If CCCDatabaseConnectInfo.DefaultDatabaseDBInfo IsNot Nothing Then
            'Load metadata from default database
            CCCMetaData.InitialiseForDatabase(CCCDatabaseConnectInfo.DefaultDatabaseDBInfo)
        End If

        CCCEnvironmentLibrary.ApplicationInfo_ProductName = "ProSolution Web Toolkit"
        'Custom Control Settings

        CCCWebField.ShouldCreateRequiredFieldIcon = False
        CCCWebField.UseProSolutionMetadata = False

        CheckoutBaseControl.ShouldCheckBasketNotEmpty = False

        WorkingData.Provider = New SessionWorkingDataProvider

        PaymentSubmitter.ShouldSendEmailConfirmation = False
        PaymentSubmitter.AllowEmptyBasket = False
        PaymentSubmitter.ShouldValidateAge = False
        PaymentSubmitter.MinimumAge = 16
        PaymentSubmitter.ShouldMakeNewApplicationForEachOffering = True
        Toastr.UseToastr = True


        'System Settings
        SystemSettings.AcademicYearID = "21/22"

        SystemSettings.CollegeOrganisationID = 0

        'DB Timeouts
        SystemSettings.DefaultTimeout = 60
        SystemSettings.DefaultLongTimeout = 1200

        'Proxy Server for web requests to pass through
        SystemSettings.ProxyDomain = ""
        SystemSettings.ProxyPassword = ""
        SystemSettings.ProxyURL = ""
        SystemSettings.ProxyUserName = ""

        'QAS Pro Web Settings
        SystemSettings.QAS_Layout = "Database Layout"
        SystemSettings.QAS_ServerURL = ""

        'KIS Widget URL Settings. For example. "https://discoveruni.org.uk/widget/" If not set, default will be "https://widget.unistats.ac.uk/Widget/"
        SystemSettings.KISWidgetURL = ""

        'QAS On Demand Settings
        SystemSettings.QASOnDemand_Password = ""
        SystemSettings.QASOnDemand_URL = ""
        SystemSettings.QASOnDemand_Username = ""

        'AFD Postcode Evolution Settings
        SystemSettings.AFDEvolution_Password = ""
        SystemSettings.AFDEvolution_SerialNumber = ""
        SystemSettings.AFDEvolution_Server = ""
        SystemSettings.AFDEvolution_UserID = ""

        'Capscan On Demand Settings
        SystemSettings.Capscan_AccessCode = ""
        SystemSettings.Capscan_ClientID = ""

        'Matchcode360 Settings
        SystemSettings.UseMatchcode360 = False
        SystemSettings.Matchcode360Username = ""
        SystemSettings.Matchcode360Password = ""
        SystemSettings.Matchcode360API = "https://idmp.gb.co.uk/idm-powersearch-rest/powersearch/names?address="

        'GetAddress SEttings
        SystemSettings.UseGetAddress = False
        SystemSettings.GetAddressIOAPIKey = ""

        'Generic Payment Settings 'used as Bu for x-pay
        SystemSettings.Payment_InstitutionID = ""
        SystemSettings.Payment_Secret = ""
        SystemSettings.Payment_SubmitURL = "https://pay.wcg.ac.uk/prosolutiondev"
        'Live : https://payments.coventry.gov.uk/public/XPay.aspx?
        'Test : https://testpay.coventry.gov.uk/public/XPay.aspx?
        'WPM : https://pay.wcg.ac.uk/prosolutiondev

        'WorldPay-specific Settings
        SystemSettings.WorldPay_TestMode = "100"
        'See info in http://support.worldpay.com/support/kb/bg/pdf/181450-basic-integration-f.pdf

        'RealEx-specific Settings
        SystemSettings.RealEx_Account = ""


        'X-Pay Specific settings
        SystemSettings.PaymentType = ""
        SystemSettings.CcEmail = ""

        'Verify Email for new acccounts
        SystemSettings.ShouldVerifyEmail = True

        'Address provider to use (one only)
        SystemSettings.UseAFD = False
        SystemSettings.UseAFDEvolution = False
        SystemSettings.UseQASProOnDemand = False

        'Payment Provider to use (one only)
        SystemSettings.UseCapita = False
        SystemSettings.UseCivica = False
        SystemSettings.UsePayPal = False
        SystemSettings.UseRealEx = False
        SystemSettings.UseWorldPay = False
        SystemSettings.UseBucksNet = False
        SystemSettings.UseXPay = False
        SystemSettings.UseWPM = False
        SystemSettings.UsePay360 = False


        'CCCPS-81189 - Image resizer settings. To reduce the size of image.
        SystemSettings.UseImageResizeFeature = False
        'Note that achieving an exact file size can be challenging due to the nature of image compression algorithms And variations in image content. 
        'The resulting file size may still have slight variations. So for such images size can be little bigger than mentioned below.
        SystemSettings.MaxReducedImageSize = 500000  'In Bytes only. i.e. given is 500KB

        ''Pay360 settings
        SystemSettings.Pay360_HMACKeyID = "456"
        SystemSettings.Pay360_SiteID = "998"
        SystemSettings.Pay360_SCPID = "174064579"

        'CCCPS-79462 WPM specific settings
        SystemSettings.WPM_clientid = 0
        SystemSettings.WPM_pathwayid = 0
        SystemSettings.WPM_emailfrom = ""
        SystemSettings.WPM_payoption = ""
        SystemSettings.WPM_DepartmentID = ""

        'Civica-specific settings
        SystemSettings.Civica_Account = ""
        SystemSettings.Civica_FundCode = ""
        SystemSettings.Civica_AccountReference = ""

        'Capita-specific settings
        SystemSettings.CapitaPaymentsV2 = False

        ''CCCPS-75837: CCCFileUpload control Porperties
        SystemSettings.MaxAllowedFileSize = 0 ''No file size restriction on control (KB)
        SystemSettings.SupportedFileTypes = ".jpg, .jpeg, .png, .pdf" ''Change this according to your need

        ''CCCPS-75553: Following properties need to be set to enable User Locking mechanism.
        SystemSettings.AllowedInvalidPasswordAttempts = 3 ''Number of attempts after which user will be locked out.
        SystemSettings.AutoUnlockUserAfter = 360 ''User will be unlocked after specified minutes here.

        SystemSettings.SaveForLater = False ''CCCPS-80182: New setting to control Save for Later feature.

        ''CCCPS-78454: New password policy settings to guide user to set strong password. Set EnableNewPolicy to False if you wish to disable this policy.
        ''Set particular configuration to 0 to bypass the check (for example, set MinUpperCaseRequired = 0 to let user set password without any upper case character(s). 
        PasswordValidation.EnableNewPolicy = False
        PasswordValidation.MinLength = 7
        PasswordValidation.MinUpperCaseRequired = 1
        PasswordValidation.MinLowerCaseRequired = 1
        PasswordValidation.MinNumericRequired = 1
        PasswordValidation.MinNonAlphaNumRequired = 1

        SystemSettings.PreloadStudentInfo = False ''CCCPS-81074: New setting to control Pre-loading of student info via PS student record.
    End Sub
    ''' <summary>
    ''' CCCPS-78455 add samesite to cookies (same was already done in PS Web - CCCPS-78449)
    ''' </summary>
    Protected Sub Application_PreSendRequestHeaders()
        Dim IsSameSiteEnabled As String = ConfigurationManager.AppSettings("SameSiteToCookie")
        If Context IsNot Nothing AndAlso IsSameSiteEnabled = "1" Then
            Dim cookieValueSuffix = "; Secure; SameSite=none"
            Dim cookies = Context.Response.Cookies

            For i = 0 To cookies.Count - 1
                Dim cookie = cookies(i)
                cookie.Value += cookieValueSuffix
            Next
        End If
    End Sub

</script>
