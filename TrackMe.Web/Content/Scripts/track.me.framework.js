/* Variables */
var infowindow;
var map;
var markersArray = [];
var directionsDisplay;
var routes = [];
var responses = [];
var directionsDisplays = [];
var functions = [];
var totalDistance = 0;
var totalDuration = 0;
var calculateDistanceCallBack;
var distances = [];
var directionsPanel;
var zoomChanged = false;

function callWebMethod(webMethodName, params, callBackFunction) {
    $.ajax({
        type: 'POST',
        url: serverHost + 'Site/WebMethods.aspx/' + webMethodName,
        cache: false,
        data: params,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            callBackFunction(data);
        },
        error: function (result) {
            alert("Error! Try again...");
        }
    });
}

function initializeMap(divMapId) {
    var div = $("#" + divMapId).get(0);
    var latlng = new google.maps.LatLng(-37.3243576, -59.1345161);
    var myOptions =
        {
            zoom: 12,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(div, myOptions);
}

function clearMarkers() {
    for (var i = 0; i < markersArray.length; i++) {
        var item = markersArray[i];
        if (item != undefined)
            item[0].setMap(null);
    } 
}

function clearMarker(deviceId) {
    var item = markersArray[deviceId];
    if (item != undefined)
        item[0].setMap(null);
} 

function markMap(deviceId, latitude, longitude, divMapId, infoWindowContent, isCenter) {
   
    var ltlng = new google.maps.LatLng(latitude, longitude);
    
    if (isCenter == 'true')
        map.setCenter(ltlng);

    var marker = new google.maps.Marker({ position: ltlng, map: map, title: 'Ver detalles'});
    markersArray[deviceId] = [];
    markersArray[deviceId].push(marker);

    google.maps.event.addListener(marker, 'click', function () {
        showMarkerInformation(marker, infoWindowContent);
    });
}

function markTracksInMap(trajectories, divMapId) {
    removeRoute();
    var isCenter = 'true'
    for (var i = 0; i < trajectories.length; i++) {
        if (i > 0) isCenter = 'false';
        markMap(trajectories[i].LastTrack.DeviceId, trajectories[i].LastTrack.Latitude, trajectories[i].LastTrack.Longitude, divMapId, trajectories[i].LastTrack.Info, isCenter);
    }
}

function markTracksFromAndToInMap(trackFrom, trackTo, divMapId) {

    removeRoute();
    markMap(trackFrom.DeviceId, trackFrom.Latitude, trackFrom.Longitude, divMapId, trackFrom.Info, 'false');
    markMap(trackTo.DeviceId, trackTo.Latitude, trackTo.Longitude, divMapId, trackTo.Info, 'false');
}

function removeRoute() {

    for (var i = 0; i < directionsDisplays.length; i++){
        if (directionsDisplays[i] != undefined) {
            directionsDisplays[i].setMap(null);
            directionsDisplays[i] = undefined;
        }
    }
}

function calculateRoute(route, directionsPanelId) {
    clearMarker(route.DeviceId);

    directionsPanel = directionsPanelId;
    routes = route;

    functions = [];
    responses = [];

    for (var index = 0; routes.length > index; index++) {
        functions[index] = getRoutes
    }

    start(functions);
}

function showMarkerInformation(marker, infoWindowContent) {

    var latlng = marker.getPosition();
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        'location': latlng
    },
        function (results, status) {
            infowindow = new google.maps.InfoWindow({ content: getInformation(results[0].address_components, infoWindowContent) });
            infowindow.open(map, marker);
        }
    );
}

function getDirection(latitude, longitude, callBackFunction) {
    var ltlng = new google.maps.LatLng(latitude, longitude);

    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        'location': ltlng
    },
        function (results, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                var address = "";
                var addresses = results[0].address_components;

                if (addresses.length > 5) {
                    address = addresses[1].short_name + " " + addresses[0].short_name;
                    address = address + "<br />" + addresses[2].short_name;
                    address = address + "<br />" + addresses[4].long_name;
                    address = address + "<br />" + addresses[5].long_name;
                }
                else {
                    address = addresses[0].short_name;
                    address = address + "<br />" + addresses[1].short_name;
                    address = address + "<br />" + addresses[2].long_name;
                    address = address + "<br />" + addresses[3].long_name;
                }

                callBackFunction(address);
            }
        }
    );
}

function getInformation(address, content) {

    if (address.length > 5) {
        content = content.replace("{Direccion}", address[1].short_name + " " + address[0].short_name);
        content = content.replace("{Localidad}", address[2].short_name);
        content = content.replace("{Provincia}", address[4].long_name);
        content = content.replace("{Pais}", address[5].long_name);
    }
    else {
        content = content.replace("{Direccion}", address[0].short_name);
        content = content.replace("{Localidad}", address[1].short_name);
        content = content.replace("{Provincia}", address[2].long_name);
        content = content.replace("{Pais}", address[3].long_name);
    }

    return content;
}

function getWayponts(route) {
    var waypoints = [];

    for (var i = 0; i < route.WayPoints.length; i++) {
        var location = new google.maps.LatLng(route.WayPoints[i].Latitude, route.WayPoints[i].Longitude);
        var waypoint = { location: location, stopover: true };

        waypoints.push(waypoint);
    }

    return waypoints;
}


function getRoutes(i) {
    var origin = new google.maps.LatLng(routes[i].Origin.Latitude, routes[i].Origin.Longitude);
    var destination = new google.maps.LatLng(routes[i].Destination.Latitude, routes[i].Destination.Longitude);

    
    var directionsService = new google.maps.DirectionsService();
    var request = { language:'Spanish', origin: origin, destination: destination, waypoints: getWayponts(routes[i]), travelMode: routes[i].DeviceType, optimizeWaypoints: true };
//    setTimeout(function () {
//        directionsService.route(request, getRoutesSuccess);
    //    }, 1000);
    
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            responses.push(response);
            done(new Array(getRoutesComplete));
        }
        else if (status === google.maps.DirectionsStatus.OVER_QUERY_LIMIT) {
            setTimeout(function () {
                getRoutes(i);
            }, 200);
        }
    });
}

//function getRoutesSuccess(response, status) {
//    if (status == google.maps.DistanceMatrixStatus.OK) {
//        
//        responses.push(response);
//        done(new Array(getRoutesComplete));
//    }
//}

function getRoutesComplete(i) {
    directionsDisplays = [];
    var direcctions = [];
    for (var i = 0; i < responses.length; i++) {
        directionsDisplay = new google.maps.DirectionsRenderer({ suppressMarkers: true });
        directionsDisplays.push(directionsDisplay);
        directionsDisplays[i].setMap(map);
        directionsDisplays[i].setDirections(responses[i]);
        directionsDisplays[i].setPanel(document.getElementById(directionsPanel));
    }
}

function getDistancesComplete() {
    totalDistance = 0;
    totalDuration = 0;
    distances = [];
    for (var i = 0; i < responses.length; i++) {
        computeTotalDistance(responses[i]);
    }
 
    calculateDistanceCallBack(distances);
    
}

function computeTotalDistance(result) {
    var myroute = result.routes[0];

    for (i = 0; i < myroute.legs.length; i++) {
        distances.push(myroute.legs[i].distance.value);
    }
}

function getDistances(i) {
    var origin = new google.maps.LatLng(routes[i].Origin.Latitude, routes[i].Origin.Longitude);
    var destination = new google.maps.LatLng(routes[i].Destination.Latitude, routes[i].Destination.Longitude);

    var directionsService = new google.maps.DirectionsService();
    var request = { origin: origin, destination: destination, waypoints: getWayponts(routes[i]), travelMode: routes[i].DeviceType, optimizeWaypoints: false };
//    directionsService.route(request, getDistancesSuccess);

    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            responses.push(response);
            done(new Array(getDistancesComplete));
        }
        else if (status === google.maps.DirectionsStatus.OVER_QUERY_LIMIT) {
            setTimeout(function () {
                getDistances(i);
            }, 200);
        }
    });
    
}

//function getDistancesSuccess(response, status) {
//    if (status == google.maps.DirectionsStatus.OK) {
//        responses.push(response);
//        done(new Array(getDistancesComplete));
//    }
//    else if (status === google.maps.DirectionsStatus.OVER_QUERY_LIMIT) {
//        setTimeout(function () {
//            getDistancesSuccess(response, status);
//        }, 200);
//    }
//}

function calculateDistance(route, callBackFunction) {

    calculateDistanceCallBack = callBackFunction;
    routes = route;

    functions = [];
    responses = [];

    for (var index = 0; routes.length > index; index++) {
        functions[index] = getDistances
    }

    start(functions);
}

function getLocations(locations) {

    var latLngs = [];

    for (var c = 0; c < locations.length; c++) {
        var location = new google.maps.LatLng(locations[c].Latitude, locations[c].Longitude);
        latLngs.push(location);
    }

    return latLngs;
}

function fitZoom(trajectories) {
    var latlngbounds = new google.maps.LatLngBounds();

    for (var i = 0; i < trajectories.length; i++) {
        var ltlng = new google.maps.LatLng(trajectories[i].LastTrack.Latitude, trajectories[i].LastTrack.Longitude);
        latlngbounds.extend(ltlng);
    }
    map.fitBounds(latlngbounds);
}

function setZoom(zoomLevel) {
    map.setZoom(zoomLevel);
}