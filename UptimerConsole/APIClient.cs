
/// <summary>
/// class object to handle the Consuming/Communication of an APIClient
/// </summary>
public class APIClient
{
    private string _APIURL;
    private string _URL;
    private bool _IsActive;
    private int _URLID;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="apiurl">API Response URL</param>
    public APIClient(string apiurl, string url)
    {
        _APIURL = apiurl;
        _URL = url;
        //Initialise these two parameters by default
        _URLID = 0;
        _IsActive = true;
        TestClient();
    }
}
