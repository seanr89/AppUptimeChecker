
public class UptimeModel
{
    public UptimeModel(string url = "https://google.com", string frequency = "10000", bool available = false)
    {
        URL = url;
        Frequency = frequency;
        Available = available;
    }
    /// <summary>
    /// GET:SET: property to handle if the site is currently available
    /// </summary>
    /// <returns></returns>
    public bool Available { get; set; }

    public bool ErrorAlertSent { get; set; }

    public string URL { get; set; }

    public string Frequency { get; set; }

}