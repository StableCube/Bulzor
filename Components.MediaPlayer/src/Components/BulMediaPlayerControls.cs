using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using StableCube.Bulzor.Components;

namespace StableCube.Bulzor.Components.MediaPlayer
{
    public class BulMediaPlayerControls : BulComponentBase
    {
        [Parameter]
        public BulMediaPlayerStateModel PlayerState { get; set; }

        [Parameter]
        public BulSize? Size { get; set; }

        [Parameter]
        public BulSchemeColor? Color { get; set; }

        [Parameter]
        public Dictionary<string, string> IconClassMap { get; set; } = new Dictionary<string, string>()
        {
            { "play", "fas fa-play" },
            { "pause", "fas fa-pause" },
            { "volume-muted", "fas fa-volume-mute" },
            { "volume-high", "fas fa-volume-up" },
            { "volume-low", "fas fa-volume-down" },
            { "volume-off", "fas fa-volume-off" },
            { "additional-controls", "fas fa-cog" },
            { "fullscreen-in", "fas fa-expand" },
            { "fullscreen-out", "fas fa-compress" },
        };

        [Parameter]
        public EventCallback<MouseEventArgs> OnPlayPauseClick { get; set; }

        [Parameter]
        public EventCallback<double> OnVolumeChange { get; set; }

        [Parameter]
        public EventCallback<TimeSpan> OnScrubberTimeChange { get; set; }

        [Parameter]
        public EventCallback<TimeSpan> OnScrubberTimeInput { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnMuteClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnFullscreenClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnLoopClick { get; set; }

        [Parameter]
        public EventCallback<double> OnPlaybackRateChange { get; set; }

        protected BulmaClassBuilder RootClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player-input");
        protected BulmaClassBuilder ScrubberRootClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player-scrubber");
        protected BulmaClassBuilder ScrubberSliderClassBuilder { get; set; } = new BulmaClassBuilder("slider");
        protected BulmaClassBuilder VolumeClassBuilder { get; set; } = new BulmaClassBuilder("slider");
        protected BulmaClassBuilder PlayProgressClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player-play-progress");
        protected BulmaClassBuilder LoadProgressClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player-load-progress");
        protected BulmaClassBuilder ControlsGroupClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player-control-group");
        protected BulmaClassBuilder TimeDisplayClassBuilder { get; set; } = new BulmaClassBuilder("bul-media-player-control-time");

        private double _volumePos;
        private TimeSpan _scrubberPos;
        private bool _seekLock;
        private bool _seekStateCache;
        private bool _isScrubberMouseDown;
        private bool _isMouseOverPlayer;
        private bool _isAdditionalControlsMenuActive;
        private bool _isRateMenuExpanded;
        private DateTime _pointerLastMoved;
        private TimeSpan _lastCurrentTime = TimeSpan.Zero;
        private StringBuilder _currentTimeStrBuilder = new StringBuilder();
        private TimeSpan _lastDuration = TimeSpan.Zero;
        private StringBuilder _durationStrBuilder = new StringBuilder();

        protected override void BuildBulma()
        {
            if(PlayerState.PlayState != BulMediaPlayState.Playing)
            {
                RootClassBuilder.IsCursorHidden = false;
                ControlsGroupClassBuilder.IsHidden = false;
            }
            else
            {
                TimeSpan pointerIdleTime = DateTime.UtcNow - _pointerLastMoved;

                if(!_isMouseOverPlayer)
                {
                    ControlsGroupClassBuilder.IsHidden = true;
                }
                else if(pointerIdleTime.TotalSeconds > 3)
                {
                    RootClassBuilder.IsCursorHidden = true;
                    ControlsGroupClassBuilder.IsHidden = true;
                }
                else
                {
                    RootClassBuilder.IsCursorHidden = false;
                    ControlsGroupClassBuilder.IsHidden = false;
                }
            }

            ScrubberSliderClassBuilder.Size = Size;
            ScrubberSliderClassBuilder.IsFullWidth = true;
            
            VolumeClassBuilder.Size = Size;
            VolumeClassBuilder.SchemeColor = Color;
            VolumeClassBuilder.IsCircle = true;

            ScrubberRootClassBuilder.Size = Size;

            TimeDisplayClassBuilder.Size = Size;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            BuildBulma();

            // Prevent rubber banding of the progress bar while seeking
            if(PlayerState.Seeking != _seekStateCache)
            {
                _seekLock = PlayerState.Seeking;
                _seekStateCache = PlayerState.Seeking;
            }

            if(PlayerState.CurrentTime != _lastCurrentTime || _durationStrBuilder.Length == 0 || PlayerState.Duration != _lastDuration)
            {
                _lastCurrentTime = PlayerState.CurrentTime;
                _currentTimeStrBuilder.Clear();
                _currentTimeStrBuilder.Append(GetTimeSpanString(PlayerState.CurrentTime, (PlayerState.Duration.Hours > 0), PlayerState.Duration.Minutes > 0));
            }
            
            if(PlayerState.Duration != _lastDuration || _durationStrBuilder.Length == 0)
            {
                _lastDuration = PlayerState.Duration;
                _durationStrBuilder.Clear();
                _durationStrBuilder.Append(GetTimeSpanString(PlayerState.Duration, (PlayerState.Duration.Hours > 0), PlayerState.Duration.Minutes > 0));
            }

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", RootClassBuilder.ClassString);
            builder.AddAttribute(2, "onmouseover", EventCallback.Factory.Create<MouseEventArgs>(this, OnPlayerMouseOverHandler));
            builder.AddAttribute(3, "onmouseout", EventCallback.Factory.Create<MouseEventArgs>(this, OnPlayerMouseOutHandler));
            builder.AddAttribute(4, "onpointermove", EventCallback.Factory.Create<PointerEventArgs>(this, OnPointerMoveHandler));

            BuildControlGroup(builder, 5);

            builder.CloseElement();
        }

        private void BuildControlGroup(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", ControlsGroupClassBuilder.ClassString);
            builder.AddContent(2, (RenderFragment)((builder2) => {
                BuildScrubberRow(builder2, 3);
                BuildControlsRow(builder2, 4);
            }));

            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildScrubberRow(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "bul-media-scrubber-row");
            builder.AddContent(2, (RenderFragment)((builder3) => {
                BuildScrubber(builder3, 3);
            }));
            
            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildControlsRow(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "bul-media-controls-row");
            builder.AddContent(2, (RenderFragment)((builder2) => {

                builder2.OpenElement(3, "div");
                builder2.AddAttribute(4, "class", "bul-media-controls-left");
                builder2.AddContent(5, (RenderFragment)((builder3) => {
                    BuildPlayButton(builder3, 6);
                    BuildMuteButton(builder3, 7);
                    BuildVolumeSlider(builder3, 8);
                    BuildTimeDisplay(builder3, 9);
                }));
                builder2.CloseElement();

                builder2.OpenElement(10, "div");
                builder2.AddAttribute(11, "class", "bul-media-controls-right");
                builder2.AddContent(12, (RenderFragment)((builder4) => {
                    BuildAdditionalControls(builder4, 13);
                    BuildFullscreenButton(builder4, 14);
                }));
                builder2.CloseElement();
            }));
            builder.CloseElement();
            builder.CloseRegion();
        }

        private string GetTimeSpanString(TimeSpan time, bool showHours, bool showMinutes)
        {
            string result = string.Format("{0}{1}{2}",
                showHours ? string.Format("{0:0}", time.TotalHours) : string.Empty,
                showMinutes ? string.Format("{1}{0:0}", time.Minutes, showHours ? ":" : string.Empty) : string.Empty,
                string.Format("{1}{0:D2}", time.Seconds, showMinutes ? ":" : string.Empty));

            if(result == "00")
                return "0:00";
            
            return result;
        }

        private void BuildPlayButton(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenComponent<BulButton>(0);
            builder.AddAttribute(1, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnPlayPauseClick));
            builder.AddAttribute(2, "Loading", (!PlayerState.CanPlay || PlayerState.Seeking || PlayerState.Duration == TimeSpan.Zero));
            builder.AddAttribute(3, "Size", Size);
            builder.AddAttribute(4, "ChildContent", (RenderFragment)((builder2) => {
                builder2.OpenComponent<BulIcon>(5);
                builder2.AddAttribute(6, "Size", Size);

                string btnClass = IconClassMap["play"];
                if(PlayerState.PlayState == BulMediaPlayState.Playing)
                    btnClass = IconClassMap["pause"];

                builder2.AddAttribute(7, "class", btnClass);
                
                builder2.CloseComponent();
            }));
            builder.CloseComponent();
            builder.CloseRegion();
        }

        private void BuildMuteButton(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenComponent<BulButton>(0);
            builder.AddAttribute(1, "Size", Size);
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnMuteClick));
            builder.AddAttribute(3, "ChildContent", (RenderFragment)((builder2) => {
                builder2.OpenComponent<BulIcon>(4);
                builder2.AddAttribute(5, "Size", Size);

                string muteClass = IconClassMap["volume-high"];
                if(PlayerState.Muted)
                {
                    muteClass = IconClassMap["volume-muted"];
                }
                else
                {
                    if(PlayerState.Volume <= 0.5 && PlayerState.Volume > 0)
                    {
                        muteClass = IconClassMap["volume-low"];
                    }
                    else if(PlayerState.Volume == 0)
                    {
                        muteClass = IconClassMap["volume-off"];
                    }
                }

                builder2.AddAttribute(6, "class", muteClass);
                builder2.CloseComponent();
            }));
            builder.CloseComponent();
            builder.CloseRegion();
        }

        private void BuildAdditionalControls(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "bul-media-additional-controls");
            builder.AddContent(2, (RenderFragment)((builder2) => {

                builder2.OpenComponent<BulDropdown>(3);
                builder2.AddAttribute(4, "Active", _isAdditionalControlsMenuActive);
                builder2.AddAttribute(5, "Up", true);
                builder2.AddAttribute(6, "Right", true);
                builder2.AddAttribute(7, "OnClickOut", EventCallback.Factory.Create<MouseEventArgs>(this, OnAdditionalControlsClickOutHandler));
                builder2.AddAttribute(8, "BulDropdownTrigger", (RenderFragment)((builder3) => {

                    builder3.OpenRegion(9);
                    builder3.OpenComponent<BulButton>(0);
                    builder3.AddAttribute(1, "Size", Size);
                    builder3.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnAdditionalControlsToggleClickHandler));
                    builder3.AddAttribute(3, "ChildContent", (RenderFragment)((builder4) => {
                        builder4.OpenComponent<BulIcon>(4);
                        builder4.AddAttribute(5, "Size", Size);
                        builder4.AddAttribute(6, "class", IconClassMap["additional-controls"]);
                        builder4.CloseComponent();
                    }));
                    builder3.CloseComponent();
                    builder3.CloseRegion();

                }));

                builder2.AddAttribute(10, "BulDropdownContent", (RenderFragment)((builder3) => {
                    BuildLoopAdditionalControl(builder3, 11);
                    BuildRateSelectAdditionalControl(builder3, 12);
                }));

                builder2.CloseComponent();

            }));

            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildLoopAdditionalControl(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenComponent<BulDropdownItemLink>(0);
            builder.AddAttribute(1, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnLoopToggleClickHandler));
            builder.AddAttribute(2, "ChildContent", (RenderFragment)((builder2) => {
                builder2.OpenElement(3, "span");
                builder2.AddContent(4, "Loop");
                builder2.CloseElement();
            }));
            builder.CloseComponent();
            builder.CloseRegion();
        }

        private void BuildRateSelectAdditionalControl(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenComponent<BulDropdownItemDiv>(0);
            builder.AddAttribute(1, "ChildContent", (RenderFragment)((builder2) => {

                builder2.OpenComponent<BulMenu>(2);
                builder2.AddAttribute(3, "ChildContent", (RenderFragment)((builder3) => {
                    builder3.OpenComponent<BulMenuLabel>(4);
                    builder3.AddAttribute(5, "ChildContent", (RenderFragment)((builder4) => {
                        builder4.OpenElement(6, "a");
                        builder4.AddAttribute(7, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnRateMenuToggleClickHandler));
                        builder4.AddContent(8, "Speed");
                        builder4.CloseElement();
                    }));
                    builder3.CloseComponent();

                    builder3.OpenComponent<BulMenuList>(9);
                    builder3.AddAttribute(10, "ChildContent", (RenderFragment)((builder4) => {
                        builder4.OpenComponent<BulMenuListItem>(11);
                        builder4.AddAttribute(12, "ChildContent", (RenderFragment)((builder5) => {
                            if(_isRateMenuExpanded)
                            {
                                for (int i = 1; i <= 8; i++)
                                    BuildRateSelectMenuItem(builder5, 12 + i, i);
                            }
                        }));
                        builder4.CloseComponent();
                    }));
                    builder3.CloseComponent();
                }));
                builder2.CloseComponent();
            }));
            builder.CloseComponent();
            builder.CloseRegion();
        }

        private void BuildRateSelectMenuItem(RenderTreeBuilder builder, int index, int menuIndex)
        {
            double rate = 0.25 * menuIndex;

            builder.OpenRegion(index);
            builder.OpenElement(0, "a");
            builder.AddAttribute(1, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, async (args) => await OnChangePlaybackRateClickHandler(rate)));
            builder.AddContent(2, (rate == 1) ? "Normal" : rate);
            builder.CloseElement();
            builder.CloseRegion();
        }

        private async Task OnChangePlaybackRateClickHandler(double value)
        {
            _isAdditionalControlsMenuActive = false;
            await OnPlaybackRateChange.InvokeAsync(value);
        }

        private void OnRateMenuToggleClickHandler(MouseEventArgs args)
        {
            _isRateMenuExpanded = !_isRateMenuExpanded;
        }

        private async Task OnLoopToggleClickHandler(MouseEventArgs args)
        {
            _isAdditionalControlsMenuActive = false;
            await OnLoopClick.InvokeAsync(args);
        }

        private void OnAdditionalControlsToggleClickHandler(MouseEventArgs args)
        {
            _isAdditionalControlsMenuActive = !_isAdditionalControlsMenuActive;
        }

        private void OnAdditionalControlsClickOutHandler(MouseEventArgs args)
        {
            _isAdditionalControlsMenuActive = false;
        }

        private void BuildFullscreenButton(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenComponent<BulButton>(0);
            builder.AddAttribute(1, "Size", Size);
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnFullscreenClick));
            builder.AddAttribute(3, "ChildContent", (RenderFragment)((builder2) => {
                builder2.OpenComponent<BulIcon>(4);
                builder2.AddAttribute(5, "Size", Size);

                string btnClass = IconClassMap["fullscreen-out"];
                if(!PlayerState.Fullscreen)
                    btnClass = IconClassMap["fullscreen-in"];

                builder2.AddAttribute(6, "class", btnClass);
                builder2.CloseComponent();
            }));
            builder.CloseComponent();
            builder.CloseRegion();
        }

        private void BuildVolumeSlider(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", "range");
            builder.AddAttribute(2, "class", VolumeClassBuilder.ClassString);
            builder.AddAttribute(3, "min", 0);
            builder.AddAttribute(4, "max", 1);
            builder.AddAttribute(5, "step", 0.01);
            builder.AddAttribute(6, "value", PlayerState.Volume);
            builder.AddAttribute(7, "onchange", EventCallback.Factory.Create<EventArgs>(this, OnVolumeChangeHandler));
            builder.AddAttribute(8, "oninput", EventCallback.Factory.Create<ChangeEventArgs>(this, OnVolumeInputHandler));
            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildTimeDisplay(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", TimeDisplayClassBuilder.ClassString);
            builder.AddContent(2, String.Format("{0} / {1}", _currentTimeStrBuilder, _durationStrBuilder));
            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildScrubber(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", ScrubberRootClassBuilder.ClassString);

            builder.AddContent(2, (RenderFragment)((builder2) => {
                BuildScrubberSlider(builder2, 3);
            }));

            BuildScrubberLoadProgress(builder, 4);
            BuildScrubberPlayProgress(builder, 5);
            
            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildScrubberSlider(RenderTreeBuilder builder, int index)
        {
            builder.OpenRegion(index);
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", "range");
            builder.AddAttribute(2, "disabled", (!PlayerState.CanPlay || PlayerState.Duration == TimeSpan.Zero));
            builder.AddAttribute(3, "class", ScrubberSliderClassBuilder.ClassString);
            builder.AddAttribute(4, "min", 0);
            builder.AddAttribute(5, "max", Math.Ceiling(PlayerState.Duration.TotalSeconds));
            builder.AddAttribute(6, "step", 1);

            if(!_isScrubberMouseDown)
                builder.AddAttribute(7, "value", PlayerState.CurrentTime.TotalSeconds);
            
            builder.AddAttribute(8, "onchange", EventCallback.Factory.Create<EventArgs>(this, OnScrubberChangeHandler));
            builder.AddAttribute(9, "oninput", EventCallback.Factory.Create<ChangeEventArgs>(this, OnScrubberInputHandler));
            builder.AddAttribute(10, "onmousedown", EventCallback.Factory.Create<MouseEventArgs>(this, OnScrubberMouseDownHandler));
            builder.AddAttribute(11, "onmouseup", EventCallback.Factory.Create<MouseEventArgs>(this, OnScrubberMouseUpHandler));
            builder.CloseElement();
            builder.CloseRegion();
        }

        private void BuildScrubberPlayProgress(RenderTreeBuilder builder, int index)
        {
            double b = (PlayerState.CurrentTime.TotalSeconds / PlayerState.Duration.TotalSeconds);
            if(_isScrubberMouseDown || _seekLock)
                b = (_scrubberPos.TotalSeconds / PlayerState.Duration.TotalSeconds);

            if(double.IsNaN(b))
                b = 0;

            builder.OpenRegion(index);
            builder.OpenComponent<BulProgressBar>(0);
            builder.AddAttribute(1, "class", PlayProgressClassBuilder.ClassString);
            builder.AddAttribute(2, "Size", Size);
            builder.AddAttribute(3, "Color", Color);
            builder.AddAttribute(4, "Value", b * 100);
            
            builder.CloseComponent();
            builder.CloseRegion();
        }

        private void BuildScrubberLoadProgress(RenderTreeBuilder builder, int index)
        {
            double b = 0;
            for (int i = 0; i < PlayerState.BufferProgress.Count; i++)
            {
                var segment = PlayerState.BufferProgress[i];
                var b2 = (segment.End.TotalSeconds / PlayerState.Duration.TotalSeconds * 100);
                if(b2 > b)
                    b = b2;
            }

            if(double.IsNaN(b))
                b = 0;

            builder.OpenRegion(index);
            builder.OpenComponent<BulProgressBar>(0);
            builder.AddAttribute(1, "class", LoadProgressClassBuilder.ClassString);
            builder.AddAttribute(2, "Size", Size);
            builder.AddAttribute(3, "Color", Color);
            builder.AddAttribute(4, "Value", b * 100);
            
            builder.CloseComponent();
            builder.CloseRegion();
        }

        private void OnPlayerMouseOverHandler(MouseEventArgs args)
        {
            _isMouseOverPlayer = true;
        }

        private void OnPlayerMouseOutHandler(MouseEventArgs args)
        {
            _isMouseOverPlayer = false;
        }

        private void OnPointerMoveHandler(PointerEventArgs args)
        {
            _pointerLastMoved = DateTime.UtcNow;
        }

        private void OnVolumeInputHandler(ChangeEventArgs args)
        {
            _volumePos = double.Parse(args.Value as string);
        }

        private async Task OnVolumeChangeHandler(EventArgs args)
        {
            await OnVolumeChange.InvokeAsync(_volumePos);
        }

        private async Task OnScrubberInputHandler(ChangeEventArgs args)
        {
            _seekLock = true;
            _seekStateCache = PlayerState.Seeking;
            _scrubberPos = TimeSpan.FromSeconds(double.Parse(args.Value as string));
            await OnScrubberTimeInput.InvokeAsync(_scrubberPos);
        }

        private async Task OnScrubberChangeHandler(EventArgs args)
        {
            await OnScrubberTimeChange.InvokeAsync(_scrubberPos);
        }

        private void OnScrubberMouseDownHandler(MouseEventArgs args)
        {
            _isScrubberMouseDown = true;
        }

        private void OnScrubberMouseUpHandler(MouseEventArgs args)
        {
            _isScrubberMouseDown = false;
        }
    }
}