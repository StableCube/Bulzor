(function() {

    window.bulMediaPlayerEvents = (
        instance, 
        elementId,
        onPlayEvent,
        onPlayingEvent,
        onPauseEvent,
        onVolumeChangeEvent,
        onFullscreenChangeEvent,
        onEndedEvent,
        onDurationChangeEvent,
        onProgressChangeEvent,
        onTimeUpdateEvent,
        onSeekingEvent,
        onSeekedEvent,
        onCanPlayEvent,
        onCanPlayThroughEvent,
        onRateChangeEvent,
        onAbortEvent,
        onEmptiedEvent,
        onErrorEvent,
    ) => {
        const rootElm = document.getElementById(elementId);
        const mediaElm = rootElm.getElementsByClassName('bul-media-player-media-root')[0];
        
        mediaElm.onplay = function() {
            instance.invokeMethodAsync(onPlayEvent, mediaElm.currentSrc);
        };

        mediaElm.onplaying = function() {
            instance.invokeMethodAsync(onPlayingEvent, mediaElm.currentSrc);
        };

        mediaElm.onpause = function() {
            instance.invokeMethodAsync(onPauseEvent, mediaElm.currentSrc);
        };

        mediaElm.onvolumechange = function () {
            instance.invokeMethodAsync(onVolumeChangeEvent, `{ "Volume": ${mediaElm.volume}, "Muted": ${mediaElm.muted} }`);
        };
        
        rootElm.addEventListener("fullscreenchange", function () {
            var isFullscreen = (document.fullscreenElement != null);
            if (
                document.fullscreenElement || /* Standard syntax */
                document.webkitFullscreenElement || /* Safari and Opera syntax */
                document.msFullscreenElement /* IE11 syntax */
            ) {
                isFullscreen = true;
            }

            instance.invokeMethodAsync(onFullscreenChangeEvent, isFullscreen);
        });

        mediaElm.onended = function() {
            instance.invokeMethodAsync(onEndedEvent, mediaElm.currentSrc);
        };

        mediaElm.ondurationchange = function() {
            instance.invokeMethodAsync(onDurationChangeEvent, mediaElm.duration);
        };

        mediaElm.onprogress = function () {
            let progress = { "Progress": {} };
            
            for (let index = 0; index < mediaElm.buffered.length; index++) {
                const start = mediaElm.buffered.start(index);
                const end = mediaElm.buffered.end(index);
                progress["Progress"][index] = {
                    "Index": index,
                    "Start": start,
                    "End": end
                };
            }

            instance.invokeMethodAsync(onProgressChangeEvent, JSON.stringify(progress));
        };

        mediaElm.ontimeupdate = function() {
            instance.invokeMethodAsync(onTimeUpdateEvent, mediaElm.currentTime);
        };

        mediaElm.onseeking = function() {
            instance.invokeMethodAsync(onSeekingEvent, null);
        };

        mediaElm.onseeked = function() {
            instance.invokeMethodAsync(onSeekedEvent, null);
        };

        mediaElm.oncanplay = function() {
            instance.invokeMethodAsync(onCanPlayEvent, `{ "CurrentSrc": "${mediaElm.currentSrc}", "Width": ${mediaElm.videoWidth}, "Height": ${mediaElm.videoHeight} }`);
        };

        mediaElm.oncanplaythrough = function() {
            instance.invokeMethodAsync(onCanPlayThroughEvent, mediaElm.currentSrc);
        };

        mediaElm.onratechange = function() {
            instance.invokeMethodAsync(onRateChangeEvent, mediaElm.playbackRate);
        };

        mediaElm.onabort = function() {
            instance.invokeMethodAsync(onAbortEvent, mediaElm.currentSrc);
        };

        mediaElm.onemptied = function() {
            instance.invokeMethodAsync(onEmptiedEvent, null);
        };

        mediaElm.onerror = function() {
            instance.invokeMethodAsync(onErrorEvent, mediaElm.currentSrc);
        };
    };

    window.bulMediaPlayerPlay = (elementId) => {
        const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
        el.play();
    };

    window.bulMediaPlayerPause = (elementId) => {
        const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
        el.pause();
    };

    window.bulMediaPlayerSetVolume = (elementId, volume) => {
        const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
        el.volume = volume;
    };

    window.bulMediaPlayerSetMuted = (elementId, value) => {
        const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
        el.muted = value;
    };

    window.bulMediaPlayerFullscreenToggle = (elementId) => {
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

    window.bulMediaPlayerSetCurrentTime = (elementId, time) => {
        const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
        el.currentTime = time;
    };

    window.bulMediaPlayerSetPlaybackRate = (elementId, value) => {
        const el = document.getElementById(elementId).getElementsByClassName('bul-media-player-media-root')[0];
        el.playbackRate = value;
    };
})();