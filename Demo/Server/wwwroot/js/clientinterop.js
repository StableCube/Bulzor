(function () {
    window.readCurrentTheme = () => {
        const element = document.documentElement;

        return element.getAttribute("data-theme");
    };

    window.setCurrentTheme = (themeName) => {
        const element = document.documentElement;

        return element.setAttribute("data-theme", themeName);
    };
})();