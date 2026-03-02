// Dark/Light theme toggle with localStorage persistence
window.themeManager = {
    init: function () {
        const saved = localStorage.getItem('cmc-theme') || 'light';
        document.documentElement.setAttribute('data-bs-theme', saved);
    },
    toggle: function () {
        const current = document.documentElement.getAttribute('data-bs-theme') || 'light';
        const next = current === 'dark' ? 'light' : 'dark';
        document.documentElement.setAttribute('data-bs-theme', next);
        localStorage.setItem('cmc-theme', next);
        return next;
    },
    get: function () {
        return document.documentElement.getAttribute('data-bs-theme') || 'light';
    }
};

// Apply saved theme immediately to prevent flash
window.themeManager.init();
