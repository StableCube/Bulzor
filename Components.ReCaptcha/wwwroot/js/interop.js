(function() {
    window.bulReCaptchaRenderV2 = (
        instance, 
        elementId, 
        siteKey, 
        theme,
        size,
        completeCallback,
        expiredCallback
    ) => {
        grecaptcha.render(elementId, {
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
    };
})();