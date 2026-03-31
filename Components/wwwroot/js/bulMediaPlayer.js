export class BulMediaPlayerEventListener {
    constructor(elementId, dotnetInstance) {
        this.rootElm = document.getElementById(elementId);
        this.mediaElm = this.rootElm.getElementsByClassName('bul-media-player-media-root')[0];
        this.inst = dotnetInstance;

        this.mediaElm.addEventListener("play", () => {
            this.OnPlayEventHandler();
        });

        this.mediaElm.addEventListener("playing", () => {
            this.OnPlayingEventHandler();
        });

        this.mediaElm.addEventListener("pause", () => {
            this.OnPauseEventHandler();
        });

        this.mediaElm.addEventListener("volumechange", () => {
            this.OnVolumeChangeEventHandler();
        });

        this.mediaElm.addEventListener("fullscreenchange", () => {
            this.OnFullscreenChangeEventHandler();
        });

        this.mediaElm.addEventListener("ended", () => {
            this.OnEndedEventHandler();
        });

        this.mediaElm.addEventListener("durationchange", () => {
            this.OnDurationChangeEventHandler();
        });

        this.mediaElm.addEventListener("progress", () => {
            this.OnProgressEventHandler();
        });

        this.mediaElm.addEventListener("timeupdate", () => {
            this.OnTimeUpdateEventHandler();
        });

        this.mediaElm.addEventListener("seeking", () => {
            this.OnSeekingEventHandler();
        });

        this.mediaElm.addEventListener("seeked", () => {
            this.OnSeekedEventHandler();
        });

        this.mediaElm.addEventListener("canplay", () => {
            this.OnCanPlayEventHandler();
        });

        this.mediaElm.addEventListener("canplaythrough", () => {
            this.OnCanPlayThroughEventHandler();
        });

        this.mediaElm.addEventListener("ratechange", () => {
            this.OnRateChangeEventHandler();
        });

        this.mediaElm.addEventListener("abort", () => {
            this.OnAbortEventHandler();
        });

        this.mediaElm.addEventListener("emptied", () => {
            this.OnEmptiedEventHandler();
        });

        this.mediaElm.addEventListener("error", () => {
            this.OnErrorEventHandler();
        });
    }

    OnPlayEventHandler()
    {
        this.inst.invokeMethodAsync('OnPlayEventHandler', this.mediaElm.currentSrc);
    }

    OnPlayingEventHandler() {
        this.inst.invokeMethodAsync('OnPlayingEventHandler', this.mediaElm.currentSrc);
    };

    OnPauseEventHandler() {
        this.inst.invokeMethodAsync('OnPauseEventHandler', this.mediaElm.currentSrc);
    };

    OnVolumeChangeEventHandler() {
        this.inst.invokeMethodAsync('OnVolumeChangeEventHandler', `{ "Volume": ${this.mediaElm.volume}, "Muted": ${this.mediaElm.muted} }`);
    };
    
    OnFullscreenChangeEventHandler() {
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

    OnEndedEventHandler() {
        this.inst.invokeMethodAsync('OnEndedEventHandler', this.mediaElm.currentSrc);
    };

    OnDurationChangeEventHandler() {
        this.inst.invokeMethodAsync('OnDurationChangeEventHandler', this.mediaElm.duration);
    };

    OnProgressEventHandler() {
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

    OnTimeUpdateEventHandler() {
        this.inst.invokeMethodAsync('OnTimeUpdateEventHandler', this.mediaElm.currentTime);
    };

    OnSeekingEventHandler() {
        this.inst.invokeMethodAsync('OnSeekingEventHandler', null);
    };

    OnSeekedEventHandler() {
        this.inst.invokeMethodAsync('OnSeekedEventHandler', null);
    };

    OnCanPlayEventHandler() {
        this.inst.invokeMethodAsync('OnCanPlayEventHandler', `{ "CurrentSrc": "${this.mediaElm.currentSrc}", "Width": ${this.mediaElm.videoWidth}, "Height": ${this.mediaElm.videoHeight}, "Duration": ${this.mediaElm.duration} }`);
    };

    OnCanPlayThroughEventHandler() {
        this.inst.invokeMethodAsync('OnCanPlayThroughEventHandler', this.mediaElm.currentSrc);
    };

    OnRateChangeEventHandler() {
        this.inst.invokeMethodAsync('OnRateChangeEventHandler', this.mediaElm.playbackRate);
    };

    OnAbortEventHandler() {
        this.inst.invokeMethodAsync('OnAbortEventHandler', this.mediaElm.currentSrc);
    };

    OnEmptiedEventHandler() {
        this.inst.invokeMethodAsync('OnEmptiedEventHandler', null);
    };

    OnErrorEventHandler() {
        this.inst.invokeMethodAsync('OnErrorEventHandler', this.mediaElm.currentSrc);
    };
}

export function BulMediaPlayerPlay (elementId) {
    const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
    el.play();
};

export function BulMediaPlayerPause (elementId) {
    const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
    el.pause();
};

export function BulMediaPlayerSetVolume (elementId, volume) {
    const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
    el.volume = volume;
};

export function BulMediaPlayerSetMuted (elementId, value) {
    const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
    el.muted = value;
};

export function BulMediaPlayerFullscreenToggle (elementId) {
    const el = document.getElementById(elementId);
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

export function BulMediaPlayerSetCurrentTime (elementId, time) {
    const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
    el.currentTime = time;
};

export function BulMediaPlayerSetPlaybackRate (elementId, value) {
    const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
    el.playbackRate = value;
};