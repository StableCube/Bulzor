export class BulMediaPlayerEventListener {
    canPlaySent = false;

    constructor(elementId, dotnetInstance) {
        this.mediaElm = document.querySelector(`[data-player-id="${elementId}"]`);
        this.inst = dotnetInstance;

        // For some reason particularly Firefox will not send the initial canPlay event.
        // So check for the equivalent readyState and send the canPlay event if ready
        if (this.mediaElm.readyState >= this.mediaElm.HAVE_FUTURE_DATA && this.canPlaySent === false) {
            this.OnCanPlayEventHandler(null);
        }

        if(this.mediaElm.paused === false && this.canPlaySent === false) {
            this.OnCurrentlyPlayingHandler(null);
        }

        this.mediaElm.addEventListener("play", (e) => {
            this.OnPlayEventHandler(e);
        });

        this.mediaElm.addEventListener("playing", (e) => {
            this.OnPlayingEventHandler(e);
        });

        this.mediaElm.addEventListener("pause", (e) => {
            this.OnPauseEventHandler(e);
        });

        this.mediaElm.addEventListener("volumechange", (e) => {
            this.OnVolumeChangeEventHandler(e);
        });

        this.mediaElm.addEventListener("fullscreenchange", (e) => {
            this.OnFullscreenChangeEventHandler(e);
        });

        this.mediaElm.addEventListener("ended", (e) => {
            this.OnEndedEventHandler(e);
        });

        this.mediaElm.addEventListener("durationchange", (e) => {
            this.OnDurationChangeEventHandler(e);
        });

        this.mediaElm.addEventListener("progress", (e) => {
            this.OnProgressEventHandler(e);
        });

        this.mediaElm.addEventListener("timeupdate", (e) => {
            this.OnTimeUpdateEventHandler(e);
        });

        this.mediaElm.addEventListener("seeking", (e) => {
            this.OnSeekingEventHandler(e);
        });

        this.mediaElm.addEventListener("seeked", (e) => {
            this.OnSeekedEventHandler(e);
        });

        this.mediaElm.addEventListener("canplay", (e) => {
            this.OnCanPlayEventHandler(e);
            this.canPlaySent = true;
        });

        this.mediaElm.addEventListener("canplaythrough", (e) => {
            this.OnCanPlayThroughEventHandler(e);
        });

        this.mediaElm.addEventListener("ratechange", (e) => {
            this.OnRateChangeEventHandler(e);
        });

        this.mediaElm.addEventListener("abort", (e) => {
            this.OnAbortEventHandler(e);
        });

        this.mediaElm.addEventListener("emptied", (e) => {
            this.OnEmptiedEventHandler(e);
        });

        this.mediaElm.addEventListener("error", (e) => {
            this.OnErrorEventHandler(e);
        });
    }

    OnCurrentlyPlayingHandler() {
        let result = { 
            "CurrentSrc": this.mediaElm.currentSrc, 
            "Width": this.mediaElm.videoWidth, 
            "Height": this.mediaElm.videoHeight, 
            "Duration": this.mediaElm.duration 
        };

        this.inst.invokeMethodAsync('OnCurrentlyPlayingHandler', JSON.stringify(result));
    };

    OnPlayEventHandler(e)
    {
        this.inst.invokeMethodAsync('OnPlayEventHandler', this.mediaElm.currentSrc);
    }

    OnPlayingEventHandler(e) {
        this.inst.invokeMethodAsync('OnPlayingEventHandler', this.mediaElm.currentSrc);
    };

    OnPauseEventHandler(e) {
        this.inst.invokeMethodAsync('OnPauseEventHandler', this.mediaElm.currentSrc);
    };

    OnVolumeChangeEventHandler(e) {
        let result = { 
            "Volume": this.mediaElm.volume, 
            "Muted": this.mediaElm.muted
        };

        this.inst.invokeMethodAsync('OnVolumeChangeEventHandler', JSON.stringify(result));
    };
    
    OnFullscreenChangeEventHandler(e) {
        var isFullscreen = (document.fullscreenElement != null);
        if (
            document.fullscreenElement || /* Standard syntax */
            document.webkitFullscreenElement || /* Safari and Opera syntax */
            document.msFullscreenElement /* IE11 syntax */
        ) {
            isFullscreen = true;
        }

        this.inst.invokeMethodAsync('OnFullscreenChangeEventHandler', isFullscreen);
    };

    OnEndedEventHandler(e) {
        this.inst.invokeMethodAsync('OnEndedEventHandler', this.mediaElm.currentSrc);
    };

    OnDurationChangeEventHandler(e) {
        this.inst.invokeMethodAsync('OnDurationChangeEventHandler', this.mediaElm.duration);
    };

    OnProgressEventHandler(e) {
        let progress = { "Progress": {} };
        
        for (let index = 0; index < this.mediaElm.buffered.length; index++) {
            const start = this.mediaElm.buffered.start(index);
            const end = this.mediaElm.buffered.end(index);
            progress["Progress"][index] = {
                "Index": index,
                "Start": start,
                "End": end
            };
        }

        this.inst.invokeMethodAsync('OnProgressEventHandler', JSON.stringify(progress));
    };

    OnTimeUpdateEventHandler(e) {
        this.inst.invokeMethodAsync('OnTimeUpdateEventHandler', this.mediaElm.currentTime);
    };

    OnSeekingEventHandler(e) {
        this.inst.invokeMethodAsync('OnSeekingEventHandler', null);
    };

    OnSeekedEventHandler(e) {
        this.inst.invokeMethodAsync('OnSeekedEventHandler', null);
    };

    OnCanPlayEventHandler(e) {
        let result = { 
            "CurrentSrc": this.mediaElm.currentSrc, 
            "Width": this.mediaElm.videoWidth, 
            "Height": this.mediaElm.videoHeight, 
            "Duration": this.mediaElm.duration 
        };

        this.inst.invokeMethodAsync('OnCanPlayEventHandler', JSON.stringify(result));
    };

    OnCanPlayThroughEventHandler(e) {
        let result = { 
            "CurrentSrc": this.mediaElm.currentSrc, 
            "Width": this.mediaElm.videoWidth, 
            "Height": this.mediaElm.videoHeight, 
            "Duration": this.mediaElm.duration 
        };

        this.inst.invokeMethodAsync('OnCanPlayThroughEventHandler', JSON.stringify(result));
    };

    OnRateChangeEventHandler(e) {
        this.inst.invokeMethodAsync('OnRateChangeEventHandler', this.mediaElm.playbackRate);
    };

    OnAbortEventHandler(e) {
        this.inst.invokeMethodAsync('OnAbortEventHandler', this.mediaElm.currentSrc);
    };

    OnEmptiedEventHandler() {
        this.inst.invokeMethodAsync('OnEmptiedEventHandler', null);
    };

    OnErrorEventHandler(e) {
        this.inst.invokeMethodAsync('OnErrorEventHandler', this.mediaElm.currentSrc);
    };
}

export class BulMediaPlayerCommands {
    constructor(elementId) {
        this.mediaElm = document.querySelector(`[data-player-id="${elementId}"]`);
    }

    Play () {
        this.mediaElm.play();
    };

    Pause () {
        this.mediaElm.pause();
    };

    SetVolume (volume) {
        this.mediaElm.volume = volume;
    };

    SetMuted (value) {
        this.mediaElm.muted = value;
    };

    FullscreenToggle () {
        const el = this.mediaElm.parentElement;

        var isFullscreen = (document.fullscreenElement != null);
        if (
            document.fullscreenElement || /* Standard syntax */
            document.webkitFullscreenElement || /* Safari and Opera syntax */
            document.msFullscreenElement /* IE11 syntax */
        ) {
            isFullscreen = true;
        }

        if (isFullscreen) {
            if (document.exitFullscreen) {
                document.exitFullscreen();
            } else if (document.webkitExitFullscreen) { /* Safari */
                document.webkitExitFullscreen();
            } else if (document.msExitFullscreen) { /* IE11 */
                document.msExitFullscreen();
            } else if (document.mozExitFullscreen) {
                document.mozExitFullscreen();
            }
        } else {
            if (el.requestFullscreen) {
                el.requestFullscreen();
            } else if (el.webkitRequestFullscreen) { /* Safari */
                el.webkitRequestFullscreen();
            } else if (el.msRequestFullscreen) { /* IE11 */
                el.msRequestFullscreen();
            } else if (el.mozRequestFullScreen) {
                el.mozRequestFullScreen();
            }
        }
    };

    SetCurrentTime (time) {
        this.mediaElm.currentTime = time;
    };

    SetPlaybackRate (value) {
        this.mediaElm.playbackRate = value;
    };
}