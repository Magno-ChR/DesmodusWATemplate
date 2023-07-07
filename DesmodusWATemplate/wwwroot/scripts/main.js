function showToast() {
    const toastTrigger = document.getElementById('liveToastBtn');
    const toastLiveExample = document.getElementById('liveToast');
    if (toastTrigger) {
        const toast = new bootstrap.Toast(toastLiveExample);
        toast.show();
    }
}