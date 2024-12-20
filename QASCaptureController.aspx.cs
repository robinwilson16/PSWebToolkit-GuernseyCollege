using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web;
using Experian.Qas.Capture.IntegrationCode;

public partial class QASCaptureController : System.Web.UI.Page
{
    // rpcerror id
    private static readonly string _errorCodeMethodNotFound = "-32601";

    // rpc param keys
    private static readonly string _paramKeyMethod = "method";
    private static readonly string _paramKeyCountryId = "countryId";
    private static readonly string _paramKeyEngine = "engine";
    private static readonly string _paramKeyFlatten = "flatten";
    private static readonly string _paramKeyIntensity = "intensity";
    private static readonly string _paramKeyPromptset = "promptset";
    private static readonly string _paramKeyThreshold = "threshold";
    private static readonly string _paramKeyTimeout = "timeout";
    private static readonly string _paramKeyLayout = "layout";
    private static readonly string _paramKeySearch = "search";
    private static readonly string _paramKeyIsFormattedAddressInPicklist = "formattedAddressInPicklist";
    private static readonly string _paramKeyRequestTag = "requestTag";
    private static readonly string _paramKeyLocalisation = "localisation";
    private static readonly string _paramKeyMoniker = "moniker";
    private static readonly string _paramKeyRefinement = "refinement";

    // rpc method names
    private static readonly string _methodCanSearch = "CanSearch";
    private static readonly string _methodSearchMethod = "Search";
    private static readonly string _methodRefine = "Refine";
    private static readonly string _methodGetAddress = "GetAddress";
    private static readonly string _methodGetData = "GetData";
    private static readonly string _methodGetDatasetMap = "GetDataMapDetail";
    private static readonly string _methodGetLayout = "GetLayouts";
    private static readonly string _methodGetExampleAddresses = "GetExampleAddresses";
    private static readonly string _methodGetLicenseInfo = "GetLicenseInfo";
    private static readonly string _methodGetPromptSet = "GetPromptSet";
    private static readonly string _methodGetSystemInfo = "GetSystemInfo";

    // rpc params
    private static readonly string[] _paramCanSearch = { "countryId", "layout", "promptset" };
    private static readonly string[] _paramSearch = { "countryId", "search", "promptset", "layout", "requestTag" };
    private static readonly string[] _paramRefine = { "moniker", "refinement", "requestTag" };
    private static readonly string[] _paramGetAddress = { "moniker", "layout", "requestTag" };
    private static readonly string[] _paramGetData = { };
    private static readonly string[] _paramGetDatasetmap = { "countryId" };
    private static readonly string[] ParamGetLayouts = { "countryId" };
    private static readonly string[] _paramGetExampleAddresses = { "countryId", "layout", "requestTag" };
    private static readonly string[] _paramGetLicenseInfo = { };
    private static readonly string[] _paramGetPromptSet = { "countryId", "promptset" };
    private static readonly string[] _paramGetSystemInfo = { };

    // web.config keys - ondemand account
    private static readonly string _keyConfigOnDemandUrl = "Experian.Qas.Capture.OnDemandUrl";
    private static readonly string _keyConfigOnDemandUsername = "Experian.Qas.Capture.OnDemandUsername";
    private static readonly string _keyConfigOnDemandPassword = "Experian.Qas.Capture.OnDemandPassword";

    // web.config keys - proxy settings
    private static readonly string _keyConfigOnDemandProxyUrl = "Experian.Qas.Capture.ProxyUrl";
    private static readonly string _keyConfigOnDemandProxyUsername = "Experian.Qas.Capture.ProxyUsername";
    private static readonly string _keyConfigOnDemandProxyPassword = "Experian.Qas.Capture.ProxyPassword";

    private static IQASCapture _qasCapture;
    private static IDictionary<string, string[]> _paramList = null;

    public static void InitRpcParams()
    {
        if (_paramList != null)
        {
            return;
        }

        _paramList = new Dictionary<string, string[]>();
        _paramList[_methodCanSearch] = _paramCanSearch;
        _paramList[_methodGetData] = _paramGetData;
        _paramList[_methodGetLayout] = ParamGetLayouts;
        _paramList[_methodGetDatasetMap] = _paramGetDatasetmap;
        _paramList[_methodGetExampleAddresses] = _paramGetExampleAddresses;
        _paramList[_methodGetAddress] = _paramGetAddress;
        _paramList[_methodGetLicenseInfo] = _paramGetLicenseInfo;
        _paramList[_methodGetPromptSet] = _paramGetPromptSet;
        _paramList[_methodGetSystemInfo] = _paramGetSystemInfo;
        _paramList[_methodRefine] = _paramRefine;
        _paramList[_methodSearchMethod] = _paramSearch;
    }

    internal static IQASCapture CreateQasCaptureFromConfig()
    {
        // Retrieve server URL from web.config
        string ondemandUrl = CompassCC.ProSolution.PSWebEnrolmentKit.SystemSettings.QASOnDemand_URL ?? string.Empty;

        // Retrieve Username from web.config
        string username = CompassCC.ProSolution.PSWebEnrolmentKit.SystemSettings.QASOnDemand_Username ?? string.Empty;

        // Retrieve Password from web.config
        string password = CompassCC.ProSolution.PSWebEnrolmentKit.SystemSettings.QASOnDemand_Password ?? string.Empty;

        // Retrieve proxy address Value from web.config
        string proxyUrl = ConfigurationManager.AppSettings[_keyConfigOnDemandProxyUrl] ?? string.Empty;

        // Retrieve proxy username Value from web.config
        string proxyUsername = ConfigurationManager.AppSettings[_keyConfigOnDemandProxyUsername] ?? string.Empty;

        // Retrieve proxy password Value from web.config
        string proxyPassword = ConfigurationManager.AppSettings[_keyConfigOnDemandProxyPassword] ?? string.Empty;

        //Add TLS 1.2 support for .NET 4.0
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        // Create QuickAddress search object
        if (string.IsNullOrEmpty(proxyUrl) != true)
        {
            IWebProxy proxy = new WebProxy(proxyUrl, true);

            NetworkCredential credentials = new NetworkCredential(proxyUsername, proxyPassword);
            proxy.Credentials = credentials;

            // Create QuickAddress search object with proxy server
            return new Experian.Qas.Capture.IntegrationCode.V3.QASCapture(ondemandUrl, username, password, proxy);
        }

        return new Experian.Qas.Capture.IntegrationCode.V3.QASCapture(ondemandUrl, username, password);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // initialize params
        InitRpcParams();

        try
        {
            _qasCapture = CreateQasCaptureFromConfig();
        }
        catch
        {
            // TODO: handle exception
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Clear out the buffer
        Response.ClearHeaders();
        Response.ClearContent();
        Response.Clear();

        // Do not cache response
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // Set the content type and encoding for JSON
        Response.ContentType = "application/json";
        Response.ContentEncoding = Encoding.UTF8;

        // invoke api and get json result
        string results = InvokeJsonRpc(Request);

        // Write and Flush the response buffer
        Response.Write(results);
        Response.Flush();

        // Complete the request.  NOTE: Do not use Response.End() here,
        // because it throws a ThreadAbortException, which cannot be caught!
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    #region Methods to QasCapture methods and returns JSON string.

    private string InvokeJsonRpc(HttpRequest req)
    {
        IJsonSerializable rpcResult = null;
        string methodName = req[_paramKeyMethod];

        // error code is default empty.
        string errorCode = string.Empty;
        try
        {
            object result = null;

            // validate method name is not null or empty
            if (string.IsNullOrEmpty(methodName))
            {
                errorCode = _errorCodeMethodNotFound;
                throw new Exception(methodName + " method not found");
            }

            // match to method name and invoke API
            if (methodName.Equals(_methodCanSearch, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeCanSearch(req);
            }
            else if (methodName.Equals(_methodSearchMethod, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeSearch(req);
            }
            else if (methodName.Equals(_methodGetAddress, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeGetAddress(req);
            }
            else if (methodName.Equals(_methodGetData, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeGetData(req);
            }
            else if (methodName.Equals(_methodGetDatasetMap, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeGetDataMapDetail(req);
            }
            else if (methodName.Equals(_methodGetExampleAddresses, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeGetExampleAddress(req);
            }
            else if (methodName.Equals(_methodGetLayout, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeGetLayouts(req);
            }
            else if (methodName.Equals(_methodGetLicenseInfo, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeGetLicenseInfo(req);
            }
            else if (methodName.Equals(_methodGetPromptSet, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeGetPromptset(req);
            }
            else if (methodName.Equals(_methodGetSystemInfo, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeGetSystemInfo(req);
            }
            else if (methodName.Equals(_methodRefine, StringComparison.OrdinalIgnoreCase))
            {
                result = InvokeRefine(req);
            }
            else
            {
                errorCode = _errorCodeMethodNotFound;
                throw new Exception(methodName + " method not found");
            }

            rpcResult = new RpcResult(result) as IJsonSerializable;
        }
        catch (Exception e)
        {
            RpcError rpcError = new RpcError(errorCode, e.Message);
            rpcResult = new RpcResult(rpcError) as IJsonSerializable;
			throw new Exception(e.Message);
        }

        return rpcResult.ToJson();
    }

    private object InvokeCanSearch(HttpRequest req)
    {
        // prepare parameters
        string countryId = GetCountryFromReq(req);
        string engine = GetEngineFromReq(req);
        bool? flatten = GetFlattenFromRequest(req);
        string intensity = GetIntensityFromReq(req);
        string promptset = GetPromptSetFromReq(req);
        int? threshold = GetThresholdFromReq(req);
        int? timeout = GetTimeoutFromReq(req);
        string layout = GetLayoutFromReq(req);
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.CanSearch(countryId, engine, flatten, intensity, promptset, threshold, timeout, layout, localisation);
    }

    private object InvokeSearch(HttpRequest req)
    {
        // prepare parameters
        string countryId = GetCountryFromReq(req);
        string engine = GetEngineFromReq(req);
        bool? flatten = GetFlattenFromRequest(req);
        string intensity = GetIntensityFromReq(req);
        string promptset = GetPromptSetFromReq(req);
        int? threshold = GetThresholdFromReq(req);
        int? timeout = GetTimeoutFromReq(req);
        string layout = GetLayoutFromReq(req);
        string search = GetSearchFromReq(req);
        bool? formattedAddressInPicklist = GetFormattedAddressInPicklistFromReq(req);
        string requestTag = GetRequestTagFromReq(req);
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.Search(countryId, engine, flatten, intensity, promptset, threshold, timeout, layout, search, formattedAddressInPicklist, requestTag, localisation);
    }

    private object InvokeGetAddress(HttpRequest req)
    {
        // prepare parameters
        string moniker = GetMonikerFromReq(req);
        string layout = GetLayoutFromReq(req);
        string requestTag = GetRequestTagFromReq(req);
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.GetAddress(moniker, layout, requestTag, localisation);
    }

    private object InvokeGetData(HttpRequest req)
    {
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.GetData(localisation);
    }

    private object InvokeGetDataMapDetail(HttpRequest req)
    {
        // prepare parameters
        string countryId = GetCountryFromReq(req);
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.GetDataMapDetail(countryId, localisation);
    }

    private object InvokeGetExampleAddress(HttpRequest req)
    {
        // prepare parameters
        string countryId = GetCountryFromReq(req);
        string layout = GetLayoutFromReq(req);
        string requestTag = GetRequestTagFromReq(req);
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.GetExampleAddresses(countryId, layout, requestTag, localisation);
    }

    private object InvokeGetLayouts(HttpRequest req)
    {
        // prepare parameters
        string countryId = GetCountryFromReq(req);
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.GetLayouts(countryId, localisation);
    }

    private object InvokeGetLicenseInfo(HttpRequest req)
    {
        // prepare parameters
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.GetLicenceInfo(localisation);
    }

    private object InvokeGetPromptset(HttpRequest req)
    {
        // prepare parameters
        string countryId = GetCountryFromReq(req);
        string engine = GetEngineFromReq(req);
        bool? flatten = GetFlattenFromRequest(req);
        string promptset = GetPromptSetFromReq(req);
        int? threshold = GetThresholdFromReq(req);
        int? timeout = GetTimeoutFromReq(req);
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.GetPromptSet(countryId, engine, flatten, promptset, threshold, timeout, localisation);
    }

    private object InvokeGetSystemInfo(HttpRequest req)
    {
        // prepare parameters
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.GetSystemInfo(localisation);
    }

    private object InvokeRefine(HttpRequest req)
    {
        // prepare parameters
        string moniker = GetMonikerFromReq(req);
        string refinement = GetRefinementFromReq(req);
        string layout = GetLayoutFromReq(req);
        bool? formattedAddressInPicklist = GetFormattedAddressInPicklistFromReq(req);
        int? threshold = GetThresholdFromReq(req);
        int? timeout = GetTimeoutFromReq(req);
        string requestTag = GetRequestTagFromReq(req);
        string localisation = GetLocalisationFromReq(req);

        // invoke api and return result as object
        return _qasCapture.Refine(moniker, refinement, layout, formattedAddressInPicklist, threshold, timeout, requestTag, localisation);
    }

    #endregion

    #region Helper methods to get params from request

    private int? GetThresholdFromReq(HttpRequest req)
    {
        string thresholdStr = req[_paramKeyThreshold];
        int? threshold = null;
        int tempThreshold = 0;
        if (int.TryParse(thresholdStr, out tempThreshold))
        {
            threshold = tempThreshold;
        }

        return threshold;
    }

    private int? GetTimeoutFromReq(HttpRequest req)
    {
        string timeoutStr = req[_paramKeyTimeout];
        int? timeout = null;
        int tempTimeout = 0;
        if (int.TryParse(timeoutStr, out tempTimeout))
        {
            timeout = tempTimeout;
        }

        return timeout;
    }

    private bool? GetFlattenFromRequest(HttpRequest req)
    {
        string flattenStr = req[_paramKeyFlatten];
        bool? flatten = null;
        bool tempFlatten;
        if (bool.TryParse(flattenStr, out tempFlatten))
        {
            flatten = tempFlatten;
        }

        return flatten;
    }

    private bool? GetFormattedAddressInPicklistFromReq(HttpRequest req)
    {
        string isFormattedInPickFlagStr = req[_paramKeyIsFormattedAddressInPicklist];
        bool? isFormattedInPickFlag = null;
        bool tempFlatten;
        if (bool.TryParse(isFormattedInPickFlagStr, out tempFlatten))
        {
            isFormattedInPickFlag = tempFlatten;
        }

        return isFormattedInPickFlag;
    }

    private string GetLocalisationFromReq(HttpRequest req)
    {
        return req[_paramKeyLocalisation];
    }

    private string GetRequestTagFromReq(HttpRequest req)
    {
        return req[_paramKeyRequestTag];
    }

    private string GetSearchFromReq(HttpRequest req)
    {
        return req[_paramKeySearch];
    }

    private string GetLayoutFromReq(HttpRequest req)
    {
        return req[_paramKeyLayout];
    }

    private string GetPromptSetFromReq(HttpRequest req)
    {
        return req[_paramKeyPromptset];
    }

    private string GetIntensityFromReq(HttpRequest req)
    {
        return req[_paramKeyIntensity];
    }

    private string GetEngineFromReq(HttpRequest req)
    {
        return req[_paramKeyEngine];
    }

    private string GetCountryFromReq(HttpRequest req)
    {
        return req[_paramKeyCountryId];
    }

    private string GetMonikerFromReq(HttpRequest req)
    {
        return req[_paramKeyMoniker];
    }

    private string GetRefinementFromReq(HttpRequest req)
    {
        return req[_paramKeyRefinement];
    }

    #endregion
}