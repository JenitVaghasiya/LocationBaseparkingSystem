var BingMapsKey = 'AvlaIw2FacL55KAGT-H-D67wWy3glq22P_uqUCZ7iFfrPtNrpS-NRsq0Xv-zCyUk';
function temp() {
    var x = document.getElementById("demo");
    var geocoder = new google.maps.Geocoder();
    var address = document.getElementById("txtArea").value;

    geocoder.geocode({ 'address': address }, function (results, status) {

        if (status == google.maps.GeocoderStatus.OK) {
            var latitude = results[0].geometry.location.lat();
            var longitude = results[0].geometry.location.lng();
            //alert(latitude + ":" + longitude);
            x.innerHTML = latitude + " : " + longitude;
        }
    });
}


function getLocation() {
    var x = document.getElementById("demo");
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    /*var x = document.getElementById("vendordata");
    x.innerHTML = "Latitude: " + position.coords.latitude +
    "<br>Longitude: " + position.coords.longitude;*/

    GetParksOnvendors(position.coords.latitude, position.coords.longitude);
}

function GetParksOnvendors(latitude, longitude) {
    $.ajax({
        type: "POST",
        url: "/Home/Getparkonvendors",
        data: { "latitude": latitude, "longitude": longitude },
        success: function (data) {
            console.log(data);
            var resulthtml = "";
            $.each(data, function (i, item) {
                resulthtml += '<div class="col-md-3 col-sm-3 col-xs-6">';
                resulthtml += '<div class="dashboard-div-wrapper bk-clr-two">';
                resulthtml += '<i id="AvailableParkingCount" class="fa dashboard-div-icon" >' + item.NoOfRemainingParking + '</i>';
                resulthtml += '<h4>' + item.Name + '</h4>';
                resulthtml += '<h5>' + item.Address + '</h5>';
                resulthtml += '</div>';
                resulthtml += '</div>';
            });

            if (data.length == 0) {
                resulthtml = "No results found on your search location.";
            }

            $("#vendordata").html(resulthtml);

            RenderBingMap(data, latitude, longitude);
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}

function showError(error) {
    var x = document.getElementById("demo");
    switch (error.code) {
        case error.PERMISSION_DENIED:
            x.innerHTML = "User denied the request for Geolocation."
            break;
        case error.POSITION_UNAVAILABLE:
            x.innerHTML = "Location information is unavailable."
            break;
        case error.TIMEOUT:
            x.innerHTML = "The request to get user location timed out."
            break;
        case error.UNKNOWN_ERROR:
            x.innerHTML = "An unknown error occurred."
            break;
    }
}

// Bing Map Api Code

function geocode() {
    var query = document.getElementById('txtArea').value;
    var geocodeRequest = "http://dev.virtualearth.net/REST/v1/Locations?query=" + encodeURIComponent(query) + "&jsonp=GeocodeCallback&key=" + BingMapsKey;
    CallRestService(geocodeRequest, GeocodeCallback);
}
function GeocodeCallback(response) {
    var output = document.getElementById('map');

    if (response &&
        response.resourceSets &&
        response.resourceSets.length > 0 &&
        response.resourceSets[0].resources) {

        var results = response.resourceSets[0].resources;

        GetParksOnvendors(results[0].point.coordinates[0], results[0].point.coordinates[1]);

    } else {
        output.innerHTML = "No results found.";
    }
}

function CallRestService(request) {
    var script = document.createElement("script");
    script.setAttribute("type", "text/javascript");
    script.setAttribute("src", request);
    document.body.appendChild(script);
}

// Multi markers
var randomLocations;
var map, infobox;
function RenderBingMap(resultsdata, curentlat, currentlong) {
    map = new Microsoft.Maps.Map('#map', {});
    //Create an infobox at the center of the map but don't show it.
    infobox = new Microsoft.Maps.Infobox(map.getCenter(), {
        visible: false
    });
    //Assign the infobox to a map instance.
    infobox.setMap(map);
    //Create random locations in the map bounds.
    //randomLocations = Microsoft.Maps.TestDataGenerator.getLocations(5, map.getBounds());

    var pin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(curentlat, currentlong), {
        color: 'blue'
    });
    //Store some metadata with the pushpin.
    pin.metadata = {
        title: "It's Me!"
        //description: item.Address
    };
    //Add a click event handler to the pushpin.
    Microsoft.Maps.Events.addHandler(pin, 'click', pushpinClicked);
    //Add pushpin to the map.
    map.entities.push(pin);

    //for (var i = 0; i < randomLocations.length; i++) {
    $.each(resultsdata, function (i, item) {
        //var pin = new Microsoft.Maps.Pushpin(randomLocations[i]);
        var pin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(item.Latitude, item.Longitude));
        //Store some metadata with the pushpin.
        pin.metadata = {
            title: item.Name,
            description: item.Address
        };
        //Add a click event handler to the pushpin.
        Microsoft.Maps.Events.addHandler(pin, 'click', pushpinClicked);
        //Add pushpin to the map.
        map.entities.push(pin);
    });
}
function pushpinClicked(e) {
    //Make sure the infobox has metadata to display.
    if (e.target.metadata) {
        //Set the infobox options with the metadata of the pushpin.
        infobox.setOptions({
            location: e.target.getLocation(),
            title: e.target.metadata.title,
            description: e.target.metadata.description,
            visible: true
        });
    }
}