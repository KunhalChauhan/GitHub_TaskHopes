﻿
function checkAuth() {
    if (getCookie("clientid") == null) {
        window.location.href = "../Sign-in.html";
    }
    else {
        LoadFormReport();
    }
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function Loadsubmissiondetails() {
    var urlParams;
    (window.onpopstate = function () {
        var match,
        pl = /\+/g,  // Regex for replacing addition symbol with a space
        search = /([^&=]+)=?([^&]*)/g,
        decode = function (s) { return decodeURIComponent(s.replace(pl, " ")); },
        query = window.location.search.substring(1);

        urlParams = {};
        while (match = search.exec(query))
            urlParams[decode(match[1])] = decode(match[2]);
    })();


    var userid = urlParams["userid"];
    var api = "../api/usersubmission/{0}"
    api = api.replace("{0}", userid)
    $.ajax({
        type: "GET",
        crossDomain: true,
        url: api,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {
                if (item.Msg == "1") {
                  
                }
                else {

                }

            })
            document.getElementById("UserList").innerHTML = t;
        },

        error: function (msg) {

        }

    });
}
function Red(param) {
    window.location.href = "usersubmission-details.html?userid=" + param;
}