using System;
using System.Threading;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace StableCube.Bulzor.Components.Extended;

public class BulTimerProgressBar : BulComponentBase, IDisposable
{
    [Parameter]
    public TimeSpan Value { get; set; } = TimeSpan.Zero;

    [Parameter]
    public TimeSpan Max { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Time interval between UI refreshes
    /// </summary>
    [Parameter]
    public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(1);

    [Parameter]
    public BulSchemeColor? Color { get; set; }

    [Parameter]
    public BulSize? Size { get; set; }

    [Parameter] 
    public EventCallback<BulTimeElapsedEventArgs> OnElapsed { get; set; }

    [Parameter]
    public EventCallback<TimeSpan> ValueChanged { get; set; }

    /// <summary>
    /// To reset the timer or offset start you can change this value
    /// </summary>
    [Parameter]
    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    private double Progress { get; set; }

    private Timer Timer { get; set; }

    protected override void OnInitialized()
    {
        StartTimer();
    }

    public void Dispose()
    {
        Timer.Change(Timeout.Infinite, Timeout.Infinite);
        Timer.Dispose();
    }

    protected override void OnParametersSet()
    {
        BuildBulma();

        base.OnParametersSet();
    }

    protected override void BuildBulma()
    {
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if(Interval > Max)
            Interval = Max;
        
        if(Interval.TotalMilliseconds < 1)
            Interval = TimeSpan.FromMilliseconds(1);

        builder.OpenComponent<BulProgressBar>(0);
        builder.AddMultipleAttributes(1, CombinedAdditionalAttributes);
        builder.AddAttribute(2, "Value", Progress);
        builder.AddAttribute(3, "Color", Color);
        builder.AddAttribute(4, "Size", Size);
        
        builder.CloseComponent();
    }

    private void StartTimer()
    {
        Timer = new Timer(_ =>
        {
            InvokeAsync(ProgressTickHandlerAsync);
        }, null, TimeSpan.Zero, Interval);
    }

    private async void ProgressTickHandlerAsync()
    {
        if(Value >= Max)
        {
            Progress = 100;
            return;
        }

        Timer.Change(Interval, Interval);

        Value = (DateTime.UtcNow - StartDate);

        Progress = (100 * Value.TotalMilliseconds) / Max.TotalMilliseconds;
        if(Progress >= 100)
            Progress = 100;

        if(Value >= Max)
        {
            Value = Max;
            Progress = 100;
        }

        await ValueChanged.InvokeAsync(Value);

        if(Value == Max)
        {
            await OnElapsed.InvokeAsync(new BulTimeElapsedEventArgs(Max, Interval));
        }
    }
}
