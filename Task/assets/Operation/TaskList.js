function checkAuth() {
    if (getCookie("clientid") == null) {
        window.location.href = "Sign-in.html";
    }
    else {
        GetAllTask()
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
function GetAllTask() {
    //document.getElementById("controls").innerHTML = "<a><img src='assets/images/loading-gallery.gif' height='45px' width='45px' style='margin-top:40%'/></a>";
    var Clientid=getCookie("clientid")
    var api="api/Tasklist/{0}"
    api=api.replace("{0}",Clientid)
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
                   
                    t = t + "<tr><td>" + item.formname + "</td><td>" + item.dateofcreation + "</td><td>Not Assigned</td>" +
                        "<td style='width:10%'><div class='btn-group'> <button class='btn btn-white dropdown-toggle' data-toggle='dropdown'>ACTION <span class='caret' style='font-size:18px'></span></button> " +
                        "<ul class='dropdown-menu'> <li><a href='Task-users.html?formid=" + item.formid + "'>Assign to Users</a></li><li><a href='Task-Groups.html?formid=" + item.formid + "'>Assign to Group</a></li><li><a  data-toggle='modal' href='#modal'>Assign to All</a></li><li><a data-toggle='modal' href='#modalII'>Delete the Task</a></li></ul> </div></td></tr>";
                }
                else {

                }
            })
            document.getElementById("TaskList").innerHTML = t;
        },

        error: function (msg) {

        }

    });
}