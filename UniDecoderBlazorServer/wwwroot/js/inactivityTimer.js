export function initializeInactivityTimer() {
    var timer;

    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;
    document.onscroll = resetTimer; // scrolling by mouse-wheel doesn't count as mousemove

    function resetTimer() {
        clearTimeout(timer);
        timer = setTimeout(logout, 10 * 60 * 1000);
    }

    function logout() {
        document.location = "./loggedout.html"
    }
}