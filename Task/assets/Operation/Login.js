document.getElementById("BtnLog").addEventListener("click", Login);
var clicked = false;
document.addEventListener('keypress', function (e) {
    var keynum = e.keyCode || e.which;
    if (keynum == 13) {
        document.getElementById("BtnLog").click()
    }
});
function Login() {
    document.getElementById("BtnLog").innerHTML = "<img src='assets/images/loading-gallery.gif' style='height:25px'/>";
    var ID = document.getElementById('UID').value;
    var password = document.getElementById('Pass').value;

    var apiUrl = "api/Login";
    $.ajax({

        type: "Post",
        crossDomain: true,
        url: apiUrl,
        data: "{'adminuseremail':'" + ID + "','adminuserpassword':'" + password + "'}",
        contentType: "application/json; charset=utf-8",


        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {
                if (item.Msg == "1") {
                    setCookie("UserNam", ID, 30);
                    setCookie("Password", password, 30);
                    setCookie("clientid", item.clientid, 30);
                    window.location.href = "index.html";
                }
                else {
                    document.getElementById("btnPopMsg").click();
                    document.getElementById("BtnLog").innerHTML="Sign In"
                }
            })
          
        },
        error: function () {
        }
    });
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
