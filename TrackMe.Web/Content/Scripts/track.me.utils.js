if (!Array.prototype.findByDeviceId) {
    Array.prototype.findByDeviceId = function (searchElement /*, fromIndex */) {
        if (this === void 0 || this === null)
            throw new TypeError();

        var t = Object(this);
        var len = t.length >>> 0;
        if (len === 0) return -1;

        for (var k = 0; k < len; k++) {
            if (k in t && t[k].LastTrack.DeviceId === parseInt(searchElement)) return k;
        }

        return -1;
    }; 
}

if (!Array.prototype.findByTrackId) {
    Array.prototype.findByTrackId = function (searchElement, isOrigin) {
        if (this === void 0 || this === null)
            throw new TypeError();

        var t = Object(this);
        var len = t.length >>> 0;
        if (len === 0) return -1;

        var element;
        for (var k = 0; k < len; k++) {
            if (isOrigin == 'true')
                element = t[k].Origin
            else
                element = t[k].Destination

            if (k in t && element.TrackId === parseInt(searchElement)) return k;
        }

        return -1;
    };
} 

function convertSecondsToDays(sec) {
    var hrs = Math.floor(sec / 3600);
    var min = Math.floor((sec % 3600) / 60);
    var days = Math.floor(hrs / 24);

    hrs = hrs % 24;
    sec = sec % 60;

    var result = "";

    if (days > 0) {
        if (days < 10) days = "0" + days;
        result = days + " día(s) ";
    }

    if (hrs > 0) {
        if (hrs < 10) hrs = "0" + hrs;
        result = result + hrs + " hr(s) ";
    }

    if (min > 0) {
        if (min < 10) min = "0" + min;
        result = result + min + " min(s) ";
    }

    if (sec > 0) {
        if (sec < 10) sec = "0" + sec;
        result = result + sec + " seg(s)";
    }
    else {
        if (result == "")
            result = "0 seg";
    }

    return result;
};


var pendingRequests = 0;
function start(requests) {
    pendingRequests = requests.length;
    for (var i = 0; i < requests.length; i++)
        requests[i](i);
};

//Called when async responses complete. 
function done(completedEvents) {
    pendingRequests--;
    if (pendingRequests == 0) {
        for (var i = 0; i < completedEvents.length; i++) {
            completedEvents[i](i);
        }
    }
}


function ShowPopUp(title, content) {
    var popUp = $("#popupDiv");
    popUp.draggable({ handle: "span[id='title']" });
    popUp.find("span[id='title']").html(title);
    popUp.find("div[id='contentPopupDiv']").html(content);

    popUp.show();

    setHeight(popUp.find("div[id='contentPopupDiv']"), 450);
    CenterOverlay("popupDiv");
}

function CenterOverlay(divName) {
    var $overlayWindow;
    $overlayWindow = jQuery('#' + divName);

    var viewportWidth = $(window).width();
    var viewportHeight = $(window).height();

    var overlayWindowWidth = $overlayWindow.width();
    var overlayWindowHeight = $overlayWindow.height();

    $overlayWindow.css('left', (viewportWidth / 2) - (overlayWindowWidth / 2));
    $overlayWindow.css('top', (viewportHeight / 2) - (overlayWindowHeight / 2));
    $overlayWindow.css('z-index', 2000);
    $overlayWindow.css('float', 'left');
    $overlayWindow.css('position', 'fixed');
}

function setHeight(div, height) {
    div.css("height", "auto");
    if (div.height() > height) {
        div.css("height", height + "px");
    }
}

function allowNumbers(e) {
    var key = window.event ? e.keyCode : e.which;
    var keychar = String.fromCharCode(key);
    var reg = new RegExp("[0-9.]")
    if (key == 8) {
        keychar = String.fromCharCode(key);
    }
    if (key == 13) {
        key = 8;
        keychar = String.fromCharCode(key);
    }
    return reg.test(keychar);
} 