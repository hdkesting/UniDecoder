export function initializeInactivityTimer() {
    var timer;

    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;
    document.onscroll = resetTimer; // scrolling by mouse-wheel doesn't count as mousemove

    resetTimer(); // SET the timer initially, so that it even works if you start the app and do nothing

    function resetTimer() {
        clearTimeout(timer);
        timer = setTimeout(logout, 10 * 60 * 1000);
        //timer = setTimeout(logout, 3000);
    }

    function logout() {
        document.location = "./loggedout.html"
    }
}