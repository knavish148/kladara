// Write your JavaScript code.

function btnClick(btnId) {
    var btn = document.getElementById(btnId);
    var btnClr = getStyle(btn, 'backgroundColor');

    // Button could be previously selected
    if (btnClr == "rgb(255, 255, 255)") {
        // Unselect button
        //TODO notify server

        // Select only selected button in a row, deselect others if previously selected
        var n = btnId.indexOf('_');
        var reg = "button[id^=" + btnId.substr(0, n) + "]";
        $(reg).each(function (i, obj) {
            obj.style.backgroundColor = "#ffffff";
        });
        btn.style.backgroundColor = "#ff0000";
    } else {
        // Unselect button
        //TODO notify server
        btn.style.backgroundColor = "#ffffff";
    }
}

// Read style property
function getStyle(el, styleProp) {
    if (el.currentStyle)
        return el.currentStyle[styleProp];

    return document.defaultView.getComputedStyle(el, null)[styleProp];
}

// Handle entering of wager amount
$("#wager").focusout(function () {
    // Need to recalculate possible gain
});