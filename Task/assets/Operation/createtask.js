function checkAuth() {
    if (getCookie("clientid") == null) {
        window.location.href = "Sign-in.html";
    }
    else {
        getAllObjects();
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
function getAllObjects() {
    document.getElementById("controls").innerHTML = "<a><img src='assets/images/loading-gallery.gif' height='45px' width='45px' style='margin-top:40%'/></a>";
    $.ajax({
        type: "GET",
        crossDomain: true,
        url: "api/createtask",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {
                if (item.Msg == "1") {
                    if (item.objecttypename == "Edittext")
                    {
                        t = t + "<div class='col-md-12 over'  style='cursor:move;height:40px;background-color:#5acb61;color:white;padding:8px;margin-top:5px;box-shadow: 0px 2px 2px #ccc5c5;text-align:left'"
                            + " draggable='true' ondragstart=\"drag(event,'" + item.objecttypename + "','" + item.objecttypeid + "')\">" +
                            "<label id='lblLebel'  ><i class='fa fa-file-text-o'></i> " + item.objecttypename + "</label></div >";
                    }
                    else if (item.objecttypename == "Static Spinner")
                    {
                        t = t + "<div class='col-md-12 over'  style='cursor:move;height:40px;background-color:#5acb61;color:white;padding:8px;margin-top:5px;box-shadow: 0px 2px 2px #ccc5c5;text-align:left'"
                            + " draggable='true' ondragstart=\"drag(event,'" + item.objecttypename + "','" + item.objecttypeid + "')\">" +
                            "<label id='lblLebel'  ><i class='fa  fa-caret-square-o-down'></i> " + item.objecttypename + "</label></div >";
                    }
                    else if (item.objecttypename == "CheckBox") {
                        t = t + "<div class='col-md-12 over'  style='cursor:move;height:40px;background-color:#5acb61;color:white;padding:8px;margin-top:5px;box-shadow: 0px 2px 2px #ccc5c5;text-align:left'"
                            + " draggable='true' ondragstart=\"drag(event,'" + item.objecttypename + "','" + item.objecttypeid + "')\">" +
                            "<label id='lblLebel'  ><i class='fa fa-check-square-o'></i> " + item.objecttypename + "</label></div >";
                    }
                    else if (item.objecttypename == "Date Picker") {
                        t = t + "<div class='col-md-12 over'  style='cursor:move;height:40px;background-color:#5acb61;color:white;padding:8px;margin-top:5px;box-shadow: 0px 2px 2px #ccc5c5;text-align:left'"
                            + " draggable='true' ondragstart=\"drag(event,'" + item.objecttypename + "','" + item.objecttypeid + "')\">" +
                            "<label id='lblLebel'  ><i class='fa fa-calendar'></i> " + item.objecttypename + "</label></div >";
                    }
                    else if (item.objecttypename == "Camera") {
                        t = t + "<div class='col-md-12 over'  style='cursor:move;height:40px;background-color:#5acb61;color:white;padding:8px;margin-top:5px;box-shadow: 0px 2px 2px #ccc5c5;text-align:left'"
                            + " draggable='true' ondragstart=\"drag(event,'" + item.objecttypename + "','" + item.objecttypeid + "')\">" +
                            "<label id='lblLebel'  ><i class='fa fa-camera'></i> " + item.objecttypename + "</label></div >";
                    }
                    else if (item.objecttypename == "GPS") {
                        t = t + "<div class='col-md-12 over'  style='cursor:move;height:40px;background-color:#5acb61;color:white;padding:8px;margin-top:5px;box-shadow: 0px 2px 2px #ccc5c5;text-align:left'"
                                                    + " draggable='true' ondragstart=\"drag(event,'" + item.objecttypename + "','" + item.objecttypeid + "')\">" +
                                                    "<label id='lblLebel'  ><i class='fa fa-road'></i> " + item.objecttypename + "</label></div >";
                    }
                    else if (item.objecttypename == "Dynamic Spinner") {
                        t = t + "<div class='col-md-12 over'  style='cursor:move;height:40px;background-color:#5acb61;color:white;padding:8px;margin-top:5px;box-shadow: 0px 2px 2px #ccc5c5;text-align:left'"
                            + " draggable='true' ondragstart=\"drag(event,'" + item.objecttypename + "','" + item.objecttypeid + "')\">" +
                            "<label id='lblLebel'  ><i class='fa fa-refresh'></i> " + item.objecttypename + "</label></div >";
                    }
                    else if (item.objecttypename == "Option") {
                        t = t + "<div class='col-md-12 over'  style='cursor:move;height:40px;background-color:#5acb61;color:white;padding:8px;margin-top:5px;box-shadow: 0px 2px 2px #ccc5c5;text-align:left'"
                            + " draggable='true' ondragstart=\"drag(event,'" + item.objecttypename + "','" + item.objecttypeid + "')\">" +
                            "<label id='lblLebel'  ><i class='fa fa-circle-o'></i> " + item.objecttypename + "</label></div >";
                    }
                    
                }
                else {

                }
            })
            document.getElementById("controls").innerHTML = t + "<input type='hidden' id='hdnFormId' value='101'>" + "<input type='hidden' id='hdnObjTypeId' value='10000'>" + "<input type='hidden' id='hdnDeptDynSpinner' value='10000'>";
        },

        error: function (msg) {

        }

    });
}