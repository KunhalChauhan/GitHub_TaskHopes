function checkAuth() {
    if (getCookie("clientid") == null) {
        window.location.href = "Sign-in.html";
    }
    else {
        LoadUsers();
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
function Check(param)
{
    var a = param + ","
    var aa = "," + param
    if (document.getElementById("chk" + param).checked == true) {
        document.getElementById("chk" + param).checked = false;
        if (document.getElementById("hdnuserid").value.indexOf(param) == 0) {
            if (document.getElementById("hdnuserid").value.indexOf(",") == "-1") {
                document.getElementById("hdnuserid").value = document.getElementById("hdnuserid").value.replace(param, "0")
            }
            else {
                document.getElementById("hdnuserid").value = document.getElementById("hdnuserid").value.replace(a, "")
            }
        }
        else {
            document.getElementById("hdnuserid").value = document.getElementById("hdnuserid").value.replace(aa, "")
        }
    }
    else {
        document.getElementById("chk" + param).checked = true;
        if (document.getElementById("hdnuserid").value == "0") {
            document.getElementById("hdnuserid").value = param;
        }
        else {
            document.getElementById("hdnuserid").value = document.getElementById("hdnuserid").value + "," + param;
        }
    }
   
}

function LoadUsers()
{
    var clientid = getCookie("clientid")
    var api = "api/TaskAssigntouser/{0}"
    api = api.replace("{0}", clientid)
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
            $.each(a, function (key, item) {
                if (item.Msg == "1") {
                    if (item.Checked == "1")
                    {
                        if (key == 0)
                        {
                            document.getElementById("hdnuserid").value = item.userid;
                        }
                        else {
                            document.getElementById("hdnuserid").value = document.getElementById("hdnuserid").value +','+ item.userid;
                        }
                        
                        t = t + "<tr style='cursor:pointer' onclick=Check('" + item.userid + "')>" +
                     "<td><input id='chk" + item.userid + "' checked='checked' type='checkbox' class='form-control' style='height: 15px; width: 15px' /></td><td>" + item.username + "</td>" +
                     "<td>" + item.userrole + "</td></tr>";
                    }
                    else {
                        t = t + "<tr style='cursor:pointer' onclick=Check('" + item.userid + "')>" +
                     "<td><input id='chk" + item.userid + "'  type='checkbox' class='form-control' style='height: 15px; width: 15px' /></td><td>" + item.username + "</td>" +
                     "<td>" + item.userrole + "</td></tr>";
                    }
                 

                }
                else {

                }

            })
            document.getElementById("UsersList").innerHTML = t;
        },

        error: function (msg) {

        }

    });
}

function UpdateMapping() {
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


    var FormId = urlParams["formid"];
    if (document.getElementById("hdnuserid").value == "0") {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "you must choose atleast one user.!"
    }
    else {
        var userid = document.getElementById("hdnuserid").value;
        $.ajax({
            type: "POST",
            crossDomain: true,
            url:"api/TaskAssigntouser",
            data: "{'FormId':'" + FormId + "','userid':'" + userid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var a = JSON.parse(data)
                var t = "";
                var rr = parseInt(a.length) - 1;
                $.each(a, function (key, item) {
                    if (item.Msg == "1") {
                        SavedForm()
                    }
                    else {

                    }
                })

            },

            error: function (msg) {

            }

        });
    }
}

function SavedForm() {
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


    var FormId = urlParams["formid"];
    if (document.getElementById("hdnuserid").value == "0") {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "you must choose atleast one user.!"
    }
    else {
        var userid = document.getElementById("hdnuserid").value;
        $.ajax({
            type: "PUT",
            crossDomain: true,
            url: "api/TaskAssigntouser",
            data: "{'FormId':'" + FormId + "','userid':'" + userid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var a = JSON.parse(data)
                var t = "";
                var rr = parseInt(a.length) - 1;
                $.each(a, function (key, item) {
                    if (item.Msg == "1") {
                        document.getElementById("btnmodelII").click();
                        document.getElementById("MessgeII").innerHTML = "Form Assign to the Users.!"
                    }
                    else {

                    }
                })

            },

            error: function (msg) {

            }

        });
    }
}