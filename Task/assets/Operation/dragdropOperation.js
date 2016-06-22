
function clearContent() {
    document.getElementById('componentsProperties').innerHTML = "";
}
function setTitle() {
    //read querystring
    document.getElementById("btnPublish").innerText = "Creating..."
    document.getElementById("btnPublish").setAttribute("disabled", "disabled");
    
    if (document.getElementById('proLblTitle').value != '') {
      
        var formName = document.getElementById('proLblTitle').value;
        var clientId = getCookie("clientid");
        document.getElementById('lblFormTitle').innerHTML = document.getElementById('proLblTitle').value;
     
        //create form in database
        $.ajax({
            type: "POST",
            crossDomain: true,
            url: "api/createtask",
            data: "{'formName':'" + formName + "', 'clientId':'" + clientId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var a = JSON.parse(data)
                var t = "";
                var rr = parseInt(a.length) - 1;
                $.each(a, function (key, item) {
                
                    if(item.Msg=="1")
                    {
                        document.getElementById('hdnFormId').value = item.formid;
                        document.getElementById("Messge").innerHTML = "Form is Created.!"
                        document.getElementById("btnPublish").innerText = "Create"
                        document.getElementById("Sometext").style.display = "none"
                        document.getElementById("DragComponents").style.display = "block"
                        document.getElementById("btnCan").click()
                        document.getElementById("btnSave").removeAttribute("disabled")
                    }
                    else if(item.Msg=="0"){
                        document.getElementById("Messge").innerHTML = "Error while creating form.!"
                        document.getElementById("btnPublish").innerText = "Create"
                    }
                    else if(item.Msg=="-2")
                    {
                        document.getElementById("Messge").innerHTML = "Error while creating form.!"
                        document.getElementById("btnPublish").innerText = "Create"
                    }
                });
          
            },
            
            error: function (msg) {

            }

        })
    }
    else
    {
        document.getElementById("Messge").innerHTML = "Form Title must be enter.!"
        document.getElementById("btnPublish").removeAttribute("disabled");
        document.getElementById("btnPublish").innerText = "Create"

    }
        
}
function setEditTxtLabel() {

    if (document.getElementById('proLblText').value != '') {
        var id = document.getElementById('hiddenLabel').value;
        document.getElementById(id).innerHTML = document.getElementById('proLblText').value;
        if (document.getElementById('chkLblVal').checked) {
            validationId = '1';
            saveFormObject(validationId, id);
        }
        else {
            validationId = '0';
            saveFormObject(validationId, id);
        }
        document.getElementById('componentsProperties').innerHTML = "";
    }
    else
    {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "Fill Lebel Before Saving.!";

    }
}
function setLabel() {

    if (document.getElementById('proLblText').value != '') {
        var id = document.getElementById('hiddenLabel').value;
        document.getElementById(id).innerHTML = document.getElementById('proLblText').value;
        document.getElementById('componentsProperties').innerHTML = "";
        validationId = '0';
        saveFormObject(validationId, id);
    }
    else {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "Fill Lebel Before Saving.!";

    }
       
}

function setCheckbox() {
    if (document.getElementById('proLblText').value != '') {
        var id = document.getElementById('hiddenLabel').value;
        document.getElementById(id).innerHTML = document.getElementById('proLblText').value;
        document.getElementById('componentsProperties').innerHTML = "";
        validationId = '0';
        saveFormObject(validationId, id);
    }
    else {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "Fill Lebel Before Saving.!";

    }
}
function EditEditTxtLabel(id) {
    var dem = id + "Btn"
    var t = "<table class='table table-bordered' style=\"margin-top:3%;font-family:Verdana;font-size:12px\"><tr><td>Enter Label:</td></tr><tr><td><input type='text' class='form-control' id='proLblText' style='font-family:Verdana;font-size:12px'></td></tr>" +
    "<tr><td><input type='Checkbox' id='chkLblVal' class='form-control' style='height:20px;width:25px'  value='Validate'>Validate</td></tr>" +
    "<tr><td><input type='button' value='Save' class='btn btn-success' onclick='setEditTxtLabel()' /><input type='button' class='btn btn-danger' style='margin-left:5px' value='Cancel' onclick='clearContent()' /></td></tr>" +
    "</table>" + "<input type='hidden' id='hiddenLabel' value='" + id + "'>";
    document.getElementById("componentsProperties").innerHTML = t;
    document.getElementById(dem).style.display = "none"
    document.getElementById(id).style.display = "block"
}
function EditLabel(id) {
    var dem = id + "Btn"
    var t = "<table class='table table-bordered' style=\"margin-top:3%;font-family:Verdana;font-size:12px\"><tr><td>Enter Label:</td></tr><tr><td><input type='text' id='proLblText'  class='form-control' style='font-family:Verdana;font-size:12px'></td></tr>" +
    "<tr><td><input type='button' value='Save' class='btn btn-success' onclick='setLabel()'/><input type='button' class='btn btn-danger' style='margin-left:5px'  value='Cancel' onclick='clearContent()'/></td></tr>" +
    "</table>" + "<input type='hidden' id='hiddenLabel' value='" + id + "'>";
    document.getElementById("componentsProperties").innerHTML = t;
    document.getElementById(dem).style.display = "none"
    document.getElementById(id).style.display = "block"
}
function EditDynamicLabel(id) {
    var dem = id + "Btn"
    var t = "<table class='table table-bordered' style=\"margin-top:3%;font-family:Verdana;font-size:12px\"><tr><td>Enter Two Labels Separated by Comma:</td></tr><tr><td><input type='text' id='proLblText'   class='form-control'  style='font-family:Verdana;font-size:12px'/></td></tr>" +
    "<tr><td><input type='button' value='Save' class='btn btn-success' onclick='setDynamicLabel()'/><input type='button' class='btn btn-danger' style='margin-left:5px' value='Cancel' onclick='clearContent()'/></td></tr>" +
    "</table>" + "<input type='hidden' id='hiddenLabel' value='" + id + "'>";
    document.getElementById("componentsProperties").innerHTML = t;
    document.getElementById(dem).style.display = "none"
    document.getElementById(id).style.display = "block"
}
function setDynamicLabel() {
    var temp = document.getElementById("tbl").innerHTML;
    var count = (temp.match(/select id/g) || []).length;
    var str = document.getElementById('proLblText').value;
    if (document.getElementById('proLblText').value != '') {
       
        var str_array = str.split(',');
        var id = document.getElementById('hiddenLabel').value;
        document.getElementById(id).innerHTML = str_array[0];
        document.getElementById('draggedDynSpntLabel' + count).innerHTML = str_array[1];
        document.getElementById('componentsProperties').innerHTML = "";
        validationId = '0';

        saveFormObjectDyn(validationId, id, str);

    }
    else {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "Fill Lebel Before Saving.!";

    }
}
function EditCheckbox(id) {
    var dem = id + "Btn"
    var t = "<table class='table table-bordered' style=\"margin-top:3%;font-family:Verdana;font-size:12px\"><tr><td>Checkbox Value:</td></tr><tr><td><input type='text' id='proLblText'   class='form-control'  style='font-family:Verdana;font-size:12px'></td></tr>" +
    "<tr><td><input type='button' class='btn btn-success' value='Save' onclick='setCheckbox()'/><input type='button' class='btn btn-danger' style='margin-left:5px' value='Cancel' onclick='clearContent()'/></td></tr>" +
    "</table>" + "<input type='hidden' id='hiddenLabel' value='" + id + "'>";
    document.getElementById("componentsProperties").innerHTML = t;
    document.getElementById(dem).style.display = "none"
    document.getElementById(id).style.display = "block"
}
function EditStaSpinner(id) {
    var t = "<table class='table table-bordered' style=\"margin-top:3%;font-family:Verdana;font-size:12px\"><tr><td>Multiple Spinner Values With Comma:</td></tr><tr><td><input type='text' id='proLblStaSpnr'   class='form-control'  style='font-family:Verdana;font-size:12px'></td></tr>" +
    "<tr><td><input type='button' class='btn btn-success' value='Save' onclick='setStaSpinner()'/><input type='button' class='btn btn-danger' style='margin-left:5px' value='Cancel' onclick='clearContent()'/></td></tr>" +
    "</table>" + "<input type='hidden' id='hiddenLabel' value='" + id + "'>";
    document.getElementById("componentsProperties").innerHTML = t;

}
function setStaSpinner() {
    var id = document.getElementById('hiddenLabel').value;
    var select = document.getElementById(id);
    if (document.getElementById('proLblStaSpnr').value != '') {
        select.options.length = 0;
        var str = document.getElementById('proLblStaSpnr').value;

        var str_array = str.split(',');

        for (var i = 0; i < str_array.length; i++) {
            var opt = document.createElement('option');
            opt.value = str_array[i];
            opt.innerHTML = str_array[i];
            select.appendChild(opt);
        }
        saveStaSpnrVal(str);

        document.getElementById('componentsProperties').innerHTML = "";
    }
    else {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "Fill Spinner Values Before Saving.!";

    }
       
}
function EditDynSpinner(id) {
    var t = "<table class='table table-bordered' style=\"margin-top:3%;font-family:Verdana;font-size:12px\"><tr><td>Multiple Spinner Values With Comma:</td></tr><tr><td><input type='text' id='proLblDynSpnr'   class='form-control'  style='font-family:Verdana;font-size:12px'></td></tr>" +
    "<tr><td><input type='button' class='btn btn-success' value='Save' onclick='setDynSpinner()'/><input type='button' class='btn btn-danger' style='margin-left:5px' value='Cancel' onclick='clearContent()'/></td></tr>" +
    "</table>" + "<input type='hidden' id='hiddenLabel' value='" + id + "'>";
    document.getElementById("componentsProperties").innerHTML = t;

}
function EditDynSpinnerDept(id) {
    var t = "<table class='table table-bordered' style=\"margin-top:3%;font-family:Verdana;font-size:12px\"><tr><td>Dependant Spinner</td></tr><tr><td><select id='draggedDynSpinnerSel'   class='form-control'  style='font-family:Verdana; font-size:14px;'></select></td></tr><tr><td>Multiple Spinner Values With Comma:</td></tr><tr><td><input type='text' id='proLblDynSpnr'   class='form-control'  style='font-family:Verdana;font-size:12px'></td></tr>" +

    "<tr><td><input type='button' class='btn btn-success' value='Save' onclick='setDeptDynSpinner()'/><input type='button' class='btn btn-danger' style='margin-left:5px' value='Cancel' onclick='clearContent()'/></td></tr>" +
    "</table>" + "<input type='hidden' id='hiddenLabel' value='" + id + "'>";
    document.getElementById("componentsProperties").innerHTML = t;

    var select1 = document.getElementById("draggedDynSpinnerSel");
    var select2 = document.getElementById(document.getElementById("hdnDeptDynSpinner").value);
    select1.innerHTML = select2.innerHTML + select1.innerHTML;


}
function setDynSpinner() {
    var id = document.getElementById('hiddenLabel').value;
    var select = document.getElementById(id);
    if (document.getElementById('proLblDynSpnr').value != '') {
        select.options.length = 0;
        var str = document.getElementById('proLblDynSpnr').value;
        var str_array = str.split(',');

        saveDynSpnrVal(str);
        getDynSpnrDetails(id);
        var temp = document.getElementById("tbl").innerHTML;
        var count = (temp.match(/select id/g) || []).length;
        var temp = document.getElementById("tbl").innerHTML;
        var count = (temp.match(/select id/g) || []).length;
        document.getElementById('hdnDeptDynSpinner').value = "draggedDynSpinner" + (count - 2);

        document.getElementById('componentsProperties').innerHTML = "";

    }
    else {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "Fill Spinner Values Before Saving.!";

    }
}
function setDeptDynSpinner() {
    var temp = document.getElementById("tbl").innerHTML;
    var count = (temp.match(/select id/g) || []).length;
    document.getElementById('hdnDeptDynSpinner').value = "draggedDynSpinner" + (count - 1);
    var oldid = document.getElementById('hdnDeptDynSpinner').value;

    var select = document.getElementById(oldid);
    var select1 = document.getElementById('draggedDynSpinnerSel');
    if (document.getElementById('proLblDynSpnr').value != '') {
        var str = document.getElementById('proLblDynSpnr').value;
        var str_array = str.split(',');
        for (var i = 0; i < str_array.length; i++) {
            var opt = document.createElement('option');
            opt.value = str_array[i];
            opt.innerHTML = str_array[i];
            select.appendChild(opt);
        }
        var colId = select1.options[select1.selectedIndex].value;

        SaveDynDeptSpinrValue(str, colId);
        document.getElementById('hdnDeptDynSpinner').value = "draggedDynSpinner" + (count - 2);
        document.getElementById('componentsProperties').innerHTML = "";

    }
    else {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "Fill Spinner Values Before Saving.!";

    }
}
function EditOption(id) {

    var t = "<table class='table table-bordered' style=\"margin-top:3%;font-family:Verdana;font-size:12px\">"+
        "<tr><td>Enter Label:</td></tr><tr><td><input class='form-control' type='text' id='proLblText' style='font-family:Verdana;font-size:12px'></td></tr>" +
    "<tr><td><input type='button' value='Save' class='btn btn-success' onclick='setOption()' /><input type='button' class='btn btn-danger' value='Cancel' style='margin-left:5px' onclick='clearContent()' /></td></tr>" +
    "</table>" + "<input type='hidden' id='hiddenLabel' value='" + id + "'>";
    document.getElementById("componentsProperties").innerHTML = t;

}
function setOption() {
    var id = document.getElementById('hiddenLabel').value;
    var str = document.getElementById('proLblText').value;
    if (document.getElementById('proLblText').value != '') {
        var id = document.getElementById('hiddenLabel').value;
        document.getElementById(id).innerHTML = document.getElementById('proLblText').value;


    }
    validationId = '0';
    saveFormObject(validationId, str);
    document.getElementById('componentsProperties').innerHTML = "";
}
function EditButton() {
    var title = prompt("Please Enter Button Label", "BUTTON");

    if (title != null) {
        document.getElementById("draggedbtn").value = title
    }
}
function drop(ev) {
    //check form is created or not
    var title = document.getElementById('lblFormTitle').innerHTML;
    if (title == 'No Title') {
   
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "Please Create a Form Before Adding Objects.!";

   
    
        return;
    }


    //getting the dropped object and creating the quivalent object in the form
    var obj = ev.dataTransfer.getData("text");
    var objId = ev.dataTransfer.getData("ID");
    document.getElementById("hdnObjTypeId").value = objId;
    if (obj == "Edittext") {
        //generate dynamic id of label
        var temp = document.getElementById("tbl").innerHTML;
        var count = (temp.match(/<label/g) || []).length;
        var cou = count + 1;
        document.getElementById("tbl").innerHTML = document.getElementById("tbl").innerHTML + "<tr><td style='width:40%'><input id='draggedEditText" + cou + "Btn' type='button' onclick=EditEditTxtLabel('draggedEditText" + cou + "') class='btn btn-default' value='Add Label' />" +
        "<label id='draggedEditText" + cou+ "' style='font-family:Verdana;font-size:12px;display:none;padding:5px'>LABEL HERE</label></td><td>"+
        "<input id='txtEditText' type='text' class='form-control' /></td></tr>";
            
    }
    else if (obj == "Static Spinner") {
        //generate dynamic id of label
        var temp = document.getElementById("tbl").innerHTML;
        var count = (temp.match(/select id/g) || []).length;
        var cou = count + 1;
        document.getElementById("tbl").innerHTML = document.getElementById("tbl").innerHTML + "<tr><td style='width:40%'> <input id='draggedStaSpntLabel" + cou + "Btn' type='button' onclick=EditLabel('draggedStaSpntLabel" + cou + "') class='btn btn-default' value='Add Label'    />" +
            "<label id='draggedStaSpntLabel" + cou + "' style='font-family:Verdana; font-size:12px;display:none;padding:5px' onclick=EditLabel('draggedStaSpntLabel" + cou + "')>LABEL HERE</label></td>" +
            "<td><select id='draggedStaSpinner" + count + "' class='form-control'' style='font-family:Verdana;font-size:14px;' onclick='EditStaSpinner(this.id)'>" +
            "<option value='VALUE1'>VALUE1</option><option value='VALUE2'>VALUE2</option><option value='VALUE3'>VALUE3</option></select></td></tr>"

            
    }
    else if (obj == "CheckBox") {
        var temp = document.getElementById("tbl").innerHTML;
        var count = (temp.match(/checkbox/g) || []).length;
        var cou = count + 1;
        document.getElementById("tbl").innerHTML = document.getElementById("tbl").innerHTML + "<tr><td style='width:40%'>" +
            "<input id='chkLabel" + cou + "Btn' type='button' onclick=EditCheckbox('chkLabel" + cou + "') class='btn btn-default' value='Add Value'  />" +
            "<label id='chkLabel" + (count + 1) + "' onclick='EditCheckbox(this.id)' style='cursor:pointer;display:none;padding:5px'>CHECKBOX VALUE</label>" +
            "</td><td><input type='checkbox' id='draggedChk' value='CHECKBOX VALUE' class='form-control' style='width:25px;height:25px'></td></tr>"
    }
    else if (obj == "Date Picker") {
        var cou = count + 1;
        document.getElementById("tbl").innerHTML = document.getElementById("tbl").innerHTML + "<tr><td style='width:40%'>"+
        "<input id='draggedDtPckLabel" + cou + "Btn' type='button' onclick=EditLabel('draggedDtPckLabel" + cou + "') class='btn btn-default' value='Add Value'      />"+
        "<label id='draggedDtPckLabel" + cou + "' style='font-family:Verdana; font-size:12px;display:none;padding:5px' onclick='EditLabel(this.id)'>LABEL HERE</label>"+
        "</td><td style='padding:15px'><i class='fa fa-calendar' style='font-size:25px'> </i></td></tr>";
          
    }

    else if (obj == "Camera") {
        var cou = count + 1;
        document.getElementById("tbl").innerHTML = document.getElementById("tbl").innerHTML + "<tr><td style='width:40%'>"+
            "<input id='draggedCamLabel" + cou + "Btn' type='button' onclick=EditLabel('draggedCamLabel" + cou + "') class='btn btn-default' value='Add Value'/>"+
            "<label id='draggedCamLabel" + cou + "' style='font-family:Verdana; font-size:12px;padding:5px;display:none' onclick='EditLabel(this.id)'>LABEL HERE</label>"+
            "</td><td style='padding:15px'><i class='fa fa-camera-retro' style='font-size:25px' ></i></td></tr>";
            
          
    }
    else if (obj == "GPS") {
        var cou = count + 1;
        document.getElementById("divFooter").innerHTML = document.getElementById("divFooter").innerHTML + "<input id='draggedGPS' type='button' Value='GPS' class='btn btn-default'/>"
    }
    else if (obj == "Dynamic Spinner") {
        //generate dynamic id of label
        var temp = document.getElementById("tbl").innerHTML;
        var count = (temp.match(/select id/g) || []).length;
        var cou = count + 1;
        var couI = count + 2;
        document.getElementById("tbl").innerHTML = document.getElementById("tbl").innerHTML + "<tr><td style='width:40%'>"+
            "<input id='draggedDynSpntLabel" + cou + "Btn' type='button' onclick=EditDynamicLabel('draggedDynSpntLabel" + cou + "') class='btn btn-default' value='Add Value'/>"+
            "<label id='draggedDynSpntLabel" + cou + "' style='font-family:Verdana; font-size:12px;display:none;padding:5px' onclick='EditDynamicLabel(this.id)'>LABEL HERE</label></td>"+
            "<td><select id='draggedDynSpinner" + count + "' class='form-control' style='font-family:Verdana; font-size:14px;' onclick='EditDynSpinner(this.id)'></select>"+
            "</td></tr><tr><td><label id='draggedDynSpntLabel" + couI + "' style='font-family: Verdana; font-size: 12px; padding: 5px'>LABEL HERE</label></td>"+
            "<td><select id='draggedDynSpinner" +cou + "' class='form-control' style='font-family:Verdana; font-size:14px;' onclick='EditDynSpinnerDept(this.id)'></select>"+
            "</td></tr>";
           

    }
    else if (obj == "Option") {
        //generate dynamic id of label
        var temp = document.getElementById("tbl").innerHTML;
        var count = (temp.match(/<div/g) || []).length;
        var cou = count + 1;
        var couI = count + 2;
        document.getElementById("tbl").innerHTML = document.getElementById("tbl").innerHTML + "<tr><td style='width:40%'>"+
        "<input id='draggedOptionLbl" + cou + "Btn' type='button' onclick=EditLabel('draggedOptionLbl" + cou + "') class='btn btn-default' value='Add Value'    />"+
        "<label id='draggedOptionLbl" + cou + "' style='font-family:Verdana; font-size:12px;padding:5px;display:none' onclick='EditLabel(this.id)'>LABEL HERE</label>"+
        "</td><td><div id='draggedOption" + cou + "' style='' class='btn btn-success' >YES</div>"+
        "<div id='draggedOption" + couI + "' class='btn btn-danger'>NO</div></td></tr>";

    }
}
function drag(ev, obj, objID) {
    //getting dragged object id
    ev.dataTransfer.setData("text", obj);
    ev.dataTransfer.setData("ID", objID);
}
function allowDrop(ev) {
    ev.preventDefault();
}
function saveForm() {
    //getting client id from the querystring
    var userId = '1';
    if (document.getElementById("tbl").innerHTML!= "")
    {
        var formId = document.getElementById('hdnFormId').value;
        

        var clientId = getCookie("clientid");
        //check GPS and save it 
        if (document.getElementById('divFooter').innerHTML != "") {
            saveGPSObject('0');
            window.location = './MapForm.aspx?CID=' + clientId;
        }
    }
    else {
        document.getElementById("btnmodelII").click();
        document.getElementById("MessgeII").innerHTML = "you must add Controls.!";
    }
   


}
function saveFormObject(validationID, id) {

    var label = document.getElementById(id).innerHTML;
    var formid = document.getElementById('hdnFormId').value;
    var formobjectid = $('#tbl tr').length;
    var objectTypeId = document.getElementById("hdnObjTypeId").value;
    var validation = validationID;
    $.ajax({
        type: "PUT",
        crossDomain: true,
        url: "api/createtask",
        data: "{'label':'" + label + "', 'formid':'" + formid + "',  'formobjectid':'" + formobjectid + "',  'objecttypeid':'" + objectTypeId + "',  'validationId':'" + validation + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {
                
                if(item.Msg=="1")
                {
                   
                }
                else if(item.Msg=="0"){
                  
                }
               
            });
          
        },
            
        error: function (msg) {

        }
     
    });

}
function saveGPSObject(validationID) {

    var label = 'GPS';
    var formid = document.getElementById('hdnFormId').value;
    var formobjectid = $('#tbl tr').length + 1;
    var objectTypeId = '106';
    var validation = validationID;
    $.ajax({
        type: "PUT",
        crossDomain: true,
        url: "api/createtask",
        data: "{'label':'" + label + "', 'formid':'" + formid + "',  'formobjectid':'" + formobjectid + "',  'objecttypeid':'" + objectTypeId + "',  'validationId':'" + validation + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {

                if (item.Msg == "1") {

                }
                else if (item.Msg == "0") {

                }

            });

        },

        error: function (msg) {

        }

    });

}
function saveFormObjectDyn(validationID, id, label) {

    var formid = document.getElementById('hdnFormId').value;
    var formobjectid = $('#tbl tr').length;
    var objectTypeId = document.getElementById("hdnObjTypeId").value;
    var validation = validationID;
    $.ajax({
        type: "PUT",
        crossDomain: true,
        url: "api/createtask",
        data: "{'label':'" + label + "', 'formid':'" + formid + "',  'formobjectid':'" + formobjectid + "',  'objecttypeid':'" + objectTypeId + "',  'validationId':'" + validation + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {

                if (item.Msg == "1") {

                }
                else if (item.Msg == "0") {

                }

            });

        },

        error: function (msg) {

        }


    });
}
function saveStaSpnrVal(value) {


    var formid = document.getElementById('hdnFormId').value;
    var formobjectid = $('#tbl tr').length;
    var val = value;
    $.ajax({
        type: "DELETE",
        crossDomain: true,
        url: "api/createtask",
        data: "{'formid':'" + formid + "',  'formobjectid':'" + formobjectid + "',  'val':'" + val + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {

                if (item.Msg == "1") {

                }
                else if (item.Msg == "0") {

                }

            });

        },

        error: function (msg) {

        }
    });
}

function saveDynSpnrVal(value) {

    var data = '';
    var formid = document.getElementById('hdnFormId').value;
    var formobjectid = $('#tbl tr').length;
    var val = value;
    $.ajax({
        type: "POST",
        crossDomain: true,
        url: "api/taskspiner",
        data: "{'formid':'" + formid + "',  'formobjectid':'" + formobjectid + "',  'spinnerName':'" + val + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {

                if (item.Msg == "1") {

                }
                else if (item.Msg == "0") {

                }

            });

        },

        error: function (msg) {

        }
    });
 
}
function getDynSpnrDetails(id) {
    var formid = document.getElementById('hdnFormId').value;
    var objectid = $('#tbl tr').length;
    var select = document.getElementById(id);

    select.options.length = 0;

    $.ajax({
        type: "PUT",
        crossDomain: true,
        url: "api/taskspiner",
        data: "{'formid':'" + formid + "', 'objecttypeid':'" + objectid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {
                if (item.Msg == "1") {

                    var opt = document.createElement('option');
                    opt.value = this.idcolumn;
                    opt.innerHTML = this.dependentspinner;
                    select.appendChild(opt);
                }
                else {

                }
            });
        },
        error: function (msg) {

        }


    });

}
function SaveDynDeptSpinrValue(citylist, idcolumn) {
    $.ajax({
        type: "DELETE",
        crossDomain: true,
        url: "api/taskspiner",
        data: "{'citylist':'" + citylist + "',  'idcolumn':'" + idcolumn + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var a = JSON.parse(data)
            var t = "";
            var rr = parseInt(a.length) - 1;
            $.each(a, function (key, item) {

                if (item.Msg == "1") {

                }
                else if (item.Msg == "0") {

                }

            });

        },
        error: function (msg) {

        }

    });

}
















//=========================================
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