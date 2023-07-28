export function bulReCaptchaRenderV2(
    instance, 
    elementId, 
    siteKey, 
    theme,
    size,
    completeCallback,
    expiredCallback
) {
    setTimeout(function () {
        if (typeof window.grecaptcha === 'undefined' || typeof window.grecaptcha.render ==='undefined') {
            this.bulReCaptchaRenderV2(instance, elementId, siteKey, theme, size, completeCallback, expiredCallback);
        } else {
            window.grecaptcha.render(elementId, {
                'sitekey' : siteKey,
                'theme' : theme,
                'size' : size,
                'callback' : function(response) {
                    instance.invokeMethodAsync(completeCallback, response);
                },
                'expired-callback' : function() {
                    instance.invokeMethodAsync(expiredCallback);
                }
            });
        }
    }.bind(this), 100);
};

export function bulReCaptchaResetV2 () {
    grecaptcha.reset();
};

// store list of what scripts we've loaded
var loadedScripts = [];
// loadScript: returns a promise that completes when the script loads
export function loadScript(scriptPath) {
    // check list - if already loaded we can ignore
    if (loadedScripts[scriptPath]) {
        // return 'empty' promise
        return Promise.resolve();
    }

    return new Promise(function (resolve, reject) {
        // create JS library script element
        var script = document.createElement("script");
        script.src = scriptPath;
        script.async = 'async';
        script.defer = true;
        script.type = "text/javascript";

        // flag as loading/loaded
        loadedScripts[scriptPath] = true;

        // if the script returns okay, return resolve
        script.onload = function () {
            resolve(scriptPath);
        };

        // if it fails, return reject
        script.onerror = function () {
            reject(scriptPath);
        }
        
        document.head.appendChild(script);
    });
};
