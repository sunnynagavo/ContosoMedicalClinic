// Dark/Light theme toggle with localStorage persistence and cross-tab sync
window.themeManager = {
    apply: function (theme) {
        document.documentElement.setAttribute('data-bs-theme', theme);
        var icon = document.getElementById('themeIcon');
        if (icon) icon.className = theme === 'dark' ? 'bi bi-sun-fill' : 'bi bi-moon-fill';
    },
    init: function () {
        var saved = localStorage.getItem('cmc-theme') || 'light';
        this.apply(saved);
    },
    toggle: function () {
        var current = document.documentElement.getAttribute('data-bs-theme') || 'light';
        var next = current === 'dark' ? 'light' : 'dark';
        localStorage.setItem('cmc-theme', next);
        this.apply(next);
        return next;
    },
    get: function () {
        return document.documentElement.getAttribute('data-bs-theme') || 'light';
    }
};

// Apply saved theme immediately to prevent flash
window.themeManager.init();

// Re-apply theme after Blazor enhanced navigation resets the DOM
document.addEventListener('blazor:enhancedload', function () {
    window.themeManager.init();
});

// Sync theme across tabs when localStorage changes
window.addEventListener('storage', function (e) {
    if (e.key === 'cmc-theme' && e.newValue) {
        window.themeManager.apply(e.newValue);
    }
});
