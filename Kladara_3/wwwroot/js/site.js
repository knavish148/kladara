﻿// Write your JavaScript code.

function btnClick(btnId) {
    var btn = document.getElementById(btnId);
    var btnClr = getStyle(btn, "backgroundColor");

    // Button could be previously selected
    if (btnClr === "rgb(255, 255, 255)") {
        // Deselect all buttons in a row
        var n = btnId.indexOf("_");
        var reg = "button[id^=" + btnId.substr(0, n) + "]";
        $(reg).each(function (i, obj) {
            obj.style.backgroundColor = "#ffffff";
        });
        // Select button which is pressed
        btn.style.backgroundColor = "#ff0000";
    } else {
        // Button was previously selected, unselect it
        btn.style.backgroundColor = "#ffffff";
    }

    //Notify server about selected pair and the bet by passing 
    // - Match Id and Bet type (1/X/2)
    $.post("tickets/updatepairs?matchIdBet=" + btnId,
        function () {
            console.log("Successfully posted new Pair to server: " + btnId);
            RefreshNetTicketData();
        }
    );
}

// Read style property
function getStyle(el, styleProp) {
    if (el.currentStyle)
        return el.currentStyle[styleProp];

    return document.defaultView.getComputedStyle(el, null)[styleProp];
}

// HTTP GET: Update new ticket info client-side
function RefreshNetTicketData() {
    $.get("tickets/RefreshNewTicketDataView", {},
        function (data) {
            console.log("Server response: Successfully obtained newTicketData");
            $("#newTicket").html(data);
        }, "html");
}

function wagerInputChange() {
    $.post("Tickets/WagerUpdate?value=" + $("#wager").val(),
        function () {
            console.log("Server response: wager change success");
            RefreshNetTicketData();
        });
}

// Handle ticket submit
function submitBtnClk() {
    var wager = $("#wager").val();
    if (parseInt(wager) && wager >= 0) {
        $.post("tickets/submit", {},
            function (result) {
                console.log("Server response: Successfully submitted new ticket");
                window.location = result;
            });
    } else {
        alert("Wager must be non-zero.");
    }


}