
function checkAuth() {
    if (getCookie("clientid") == null) {
        window.location.href = "Sign-in.html";
    }
    else {
        Loadgroup();
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
function Check(param) {
    var a = param + ","
    var aa=","+param
    if (document.getElementById("chk" + param).checked == true) {
        document.getElementById("chk" + param).checked = false;
        if(document.getElementById("hdnusergroupid").value.indexOf(param)==0)
        {
            if( document.getElementById("hdnusergroupid").value.indexOf(",")=="-1")
            {
                document.getElementById("hdnusergroupid").value= document.getElementById("hdnusergroupid").value.replace(param, "0")
            }
            else {
                document.getElementById("hdnusergroupid").value= document.getElementById("hdnusergroupid").value.replace(a, "")
            }
        }
        else {
            document.getElementById("hdnusergroupid").value=document.getElementById("hdnusergroupid").value.replace(aa, "")
        }
    }
    else {
        document.getElementById("chk" + param).checked = true;
        if(document.getElementById("hdnusergroupid").value=="0")
        {
            document.getElementById("hdnusergroupid").value = param;
        }
        else {
            document.getElementById("hdnusergroupid").value =document.getElementById("hdnusergroupid").value+","+ param;
        }
    }

}
function Loadgroup() {
    var clientid = getCookie("clientid")
    var api = "api/TaskassigntoGroups/{0}"
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
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {
                if (item.Msg == "1") {
                    t = t + "<tr style='cursor:pointer' onclick=Check('" + item.usergroupid + "')>" +
                        "<td><input id='chk" + item.usergroupid + "'  type='checkbox' class='form-control' style='height: 15px; width: 15px' /></td>" +
                        "<td>" + item.usergroupname + "</td><td>20</td></tr>";

                }
                else {

                }

            })
            document.getElementById("GroupList").innerHTML = t;
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
    if (document.getElementById("hdnusergroupid").value== "0")
    {

        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "You must choose atleast one Group.!"
    }
    else {
        var usergroupid = document.getElementById("hdnusergroupid").value;
       
        $.ajax({
            type: "POST",
            crossDomain: true,
            url: "api/TaskassigntoGroups",
            data: "{'FormId':'" + FormId + "','usergroupid':'" + usergroupid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var a = JSON.parse(data)
                var t = "";
                var rr = parseInt(a.length) - 1;
                $.each(a, function (key, item) {
                    if (item.Msg == "1") {
                        document.getElementById("btnmodelII").click();
                        document.getElementById("MessgeII").innerHTML = "Form Assign to the Group.!"
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