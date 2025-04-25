document.addEventListener('DOMContentLoaded', function () {
    const ordenamientoSelect = document.getElementById('ordenamiento');
    if (ordenamientoSelect) {
        ordenamientoSelect.addEventListener('change', function () {
            const urlParams = new URLSearchParams(window.location.search);
            urlParams.set('ordenamiento', this.value);
            window.location.search = urlParams.toString();
        });
    }
});