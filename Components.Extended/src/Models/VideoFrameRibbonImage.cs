using System;

namespace StableCube.Bulzor.Components.Extended;

public class VideoFrameRibbonImage
{
    private Uri _uri;

    private string _uriString;

    private TimeSpan _time;

    private string _timeString;

    public TimeSpan Time
    { 
        get { 
            return _time; 
        } 
        set {
            _time = value;
            _timeString = GetTimeString(); 
        } 
    }

    public string TimeString { get { return _timeString; } }

    public Uri Uri
    { 
        get { 
            return _uri; 
        } 
        set {
            _uri = value;
            _uriString =  value.ToString(); 
        } 
    }

    public string UriString { get { return _uriString; } }
    
    public bool IsFocalImage { get; set; }
    
    private string GetTimeString()
    {
        if(_time.TotalMinutes < 1)
            return _time.ToString(@"ss\.ff");

        if(_time.TotalHours < 1)
            return _time.ToString(@"mm\:ss\.ff");

        return _time.ToString(@"hh\:mm\:ss\.ff");
    }
}
