(function() {
    window.bulSetPageTitle = (value) => {
        document.title = value;
    };

    window.bulSetMetaTag = (name, content) => {
        var el = document.querySelector(`meta[name="${name}"]`);

        if(!el) {
            el = document.createElement('meta');
            document.getElementsByTagName('head')[0].appendChild(el);
            el.name = name;
        }

        el.setAttribute("content", content);
    };
})();