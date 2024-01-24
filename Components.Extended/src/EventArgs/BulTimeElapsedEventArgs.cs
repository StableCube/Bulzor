using System;

namespace StableCube.Bulzor.Components.Extended;

public class BulTimeElapsedEventArgs : EventArgs
{
    public TimeSpan Time { get; set; }

    public TimeSpan Interval { get; set; }

    public BulTimeElapsedEventArgs()
    {
    }

    public BulTimeElapsedEventArgs(TimeSpan time, TimeSpan interval)
    {
        Time = time;
        Interval = interval;
    }
}
