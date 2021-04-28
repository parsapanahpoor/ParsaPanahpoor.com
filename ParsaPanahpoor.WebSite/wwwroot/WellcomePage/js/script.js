$(function() {

    "use strict";


    /*-----------------------------------
     * SIDEBAR & MENU
     *-----------------------------------*/
    var scrollable = $('#fscontent');
    $(window).on('load', function() {
        scrollable.find('.aos-init').removeClass('aos-animate');
    });
    var hamburger = $('.hamburger');
    hamburger.on('click', function() {
        $('body').toggleClass('sidebar-open');
        // start animating elements in vieport once content in open
        setTimeout(function() {
            scrollable.find('.aos-init:not(.aos-animate):in-viewport').addClass('aos-animate');
        }, 1200);

    });

    scrollable.on('scroll', function() {
        scrollable.find('.aos-init:not(.aos-animate):in-viewport').addClass('aos-animate');
    });

    /*-----------------------------------
     * FORM STYLING
     *-----------------------------------*/


    $('input, textarea').keyup(function() {
        if ($(this).val() == '') {
            $(this).siblings('label').removeClass('active');
        } else {
            $(this).siblings('label').addClass('active');
        }
    });

    /*-----------------------------------
     * Slick Slider
     *-----------------------------------*/

    if ($('#bg-slider').length && $.fn.slick) {

        $('#bg-slider').slick({
            arrows: false,
            autoplay: true,
            fade: true,
            dots: true,
            appendDots: $('.wrapper'),
            speed: 1000,
            autoplaySpeed: 5000
        });


        $(window).on('load', function() {
            $('.slick-slide .img-holder').height($(window).height());
        });

        $(window).on('resize', function() {
            $('.slick-slide .img-holder').height($(window).height());
        });

    }

    /*-----------------------------------
     * COUNTDOWN JS
     *-----------------------------------*/

    var $countdownDiv = $('#countdown');
    if ($countdownDiv.length && $.fn.countdown) {

        /*Fetch Event Date From HTML via data-* attr */
        var get_date = $countdownDiv.data('event-date');
        /*init*/
        if (get_date) {

            $countdownDiv.countdown({
                date: get_date,
                /*Change date and time in HTML data-event-date attribute */
                format: "on"
            });
        }
    }


});
/* ----------------------------End Fn*/

/*-----------------------------------
 * ANIMATE ON SCROLL - AOS
 *-----------------------------------*/

$(window).on('load', function() {
    AOS.init({
        duration: 1200,
    })

    loadGoogleMapsAPI()
});

/*-----------------------------------
 * Google Maps
 *-----------------------------------*/

function loadGoogleMapsAPI() {
    var script = document.createElement("script");
    var mapdiv = document.getElementById('gmaps');
    var mapsapi = mapdiv.getAttribute('data-maps-apikey');
    // This script has a callback function that will run when the script has
    // finished loading.
    script.src = "https://maps.googleapis.com/maps/api/js?callback=loadMap&key=" + mapsapi;
    script.type = "text/javascript";
    document.getElementsByTagName("body")[0].appendChild(script);
}

function loadMap() {

    var mapdiv = $('#gmaps');
    var latitude = mapdiv.data('lat') || '40.6700';
    var longitude = mapdiv.data('lon') || '-73.9400';
    var zoom = mapdiv.data('zoom') || '12';
    var mapOptions = {

        zoom: zoom,
        center: new google.maps.LatLng(latitude, longitude),
         scrollwheel: false,
        // How you would like to style the map. 
        styles: [{ "featureType": "all", "elementType": "labels.text.fill", "stylers": [{ "saturation": 36 }, { "color": "#000000" }, { "lightness": 40 }] }, { "featureType": "all", "elementType": "labels.text.stroke", "stylers": [{ "visibility": "on" }, { "color": "#000000" }, { "lightness": 16 }] }, { "featureType": "all", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "administrative", "elementType": "geometry.fill", "stylers": [{ "color": "#000000" }, { "lightness": 20 }] }, { "featureType": "administrative", "elementType": "geometry.stroke", "stylers": [{ "color": "#000000" }, { "lightness": 17 }, { "weight": 1.2 }] }, { "featureType": "landscape", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 20 }] }, { "featureType": "poi", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 21 }] }, { "featureType": "road.highway", "elementType": "geometry.fill", "stylers": [{ "color": "#000000" }, { "lightness": 17 }] }, { "featureType": "road.highway", "elementType": "geometry.stroke", "stylers": [{ "color": "#000000" }, { "lightness": 29 }, { "weight": 0.2 }] }, { "featureType": "road.arterial", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 18 }] }, { "featureType": "road.local", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 16 }] }, { "featureType": "transit", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 19 }] }, { "featureType": "water", "elementType": "geometry", "stylers": [{ "color": "#000000" }, { "lightness": 17 }] }]
    };

    var mapElement = document.getElementById('gmaps');
    var map = new google.maps.Map(mapElement, mapOptions);

    // Let's also add a marker while we're at it
    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(latitude, longitude),
        map: map,
        title: 'We are here!'
    });
}