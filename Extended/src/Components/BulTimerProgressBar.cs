using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Extended
{
    public class BulTimerProgressBar : BulComponentBase, IDisposable
    {
        [Parameter]
        public TimeSpan Value { get; set; } = TimeSpan.Zero;

        [Parameter]
        public TimeSpan Max { get; set; } = TimeSpan.FromSeconds(30);

        [Parameter]
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(1);

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter] 
        public EventCallback<TimeSpan> OnElapsed { get; set; }

        [Parameter]
        public EventCallback<TimeSpan> ValueChanged { get; set; }

        private bool RunTimer { get; set; }
        
        private double Progress { get; set; }

        protected override void OnInitialized()
        {
            if(RunTimer == false)
            {
                StartTimerAsync();
            }
        }

        public void Dispose()
        {
            RunTimer = false;
        }

        protected override void BuildBulma()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            if(Interval > Max)
                Interval = Max;
            
            if(Interval.TotalMilliseconds < 1)
                Interval = TimeSpan.FromMilliseconds(1);

            builder.OpenComponent<BulProgressBar>(0);
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "Value", Progress);
            builder.AddAttribute(3, "Color", Color);
            builder.AddAttribute(4, "Size", Size);
            
            builder.CloseComponent();
        }

        private async void StartTimerAsync()
        {
            RunTimer = true;

            while (RunTimer)
            {
                await ProgressTickHandlerAsync();
                await Task.Delay(Interval);
            }
        }

        private async Task ProgressTickHandlerAsync()
        {
            if(Value >= Max)
            {
                Progress = 100;
                return;
            }

            Progress = (100 * Value.TotalMilliseconds) / Max.TotalMilliseconds;
            if(Progress >= 100)
                Progress = 100;

            Value += Interval;

            if(Value >= Max)
            {
                Value = Max;
                Progress = 100;
            }

            await ValueChanged.InvokeAsync(Value);

            if(Value == Max)
                await OnElapsed.InvokeAsync(Value);
        }
    }
}