
public class TimerSettings
{
    public string URL { get; set; }

    public int Frequency { get; set; }

    public TimerSettings(string url = "https://www.google.com", int frequency = 30000)
    {   
        URL = url;
        Frequency = frequency;
    }
}