export function initializeInactivityTimer() {
    const minutes = 10;
    let isActivityDetected = false;
    let minuteCounter = 0;

    document.onmousemove = activityDetected;
    document.onkeydown = activityDetected;
    document.onscroll = activityDetected; // scrolling by mouse-wheel doesn't count as mousemove, so catch separately

    // check every minute for recent activity
    let timer = setInterval(checkActivity, 1 * 60 * 1000); // 1 minute
    console.log("InactivityTimer: Will log out after " + minutes + " minutes of inactivity");

    // just quickly set a flag on detected activity
    function activityDetected() {
        isActivityDetected = true;
    }

    function checkActivity() {
        if (isActivityDetected) {
            // activity seen, so reset all
            console.log("InactivityTimer: activity was seen in previous minute")
            isActivityDetected = false;
            minuteCounter = 0;
        } else {
            // no activity in *at least* the last minute
            minuteCounter++;
            console.log(`InactivityTimer: NO activity seen for ${minuteCounter} minute${minuteCounter == 1 ? '' : 's'} out of ${minutes}.`);
            if (minuteCounter >= minutes) {
                logout();
            }
        }
    }

    function logout() {
        clearTimeout(timer);
        console.log("timeout happened - exiting app");
        let backlen = history.length;
        console.log(`clearing ${backlen - 1} history items`);
        history.go(-(backlen - 1)); // keep the last page, which will be replaced
        setTimeout(() => { history.replaceState(null, '', './loggedout.html'); history.go(0); }, 500);
    }
}