var cid;
var check;
var jk = false;
function valid_Desg(e) {

    jk = true;
}
function resetRegion_Desg() {
    var txtdesg = document.getElementById('txtdesg');

    txtdesg.value = '';

}

jQuery(document).ready(function () {
    jQuery('#txtdesg').bind('copy paste cut', function (e) {
        e.preventDefault();
    });
});



function getDesigValues_Desg() {
    // jQuery.noConflict();
    var txtdesg = document.getElementById('txtdesg');

    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        txtSID = document.getElementById('txtSID'),

        hdnEmployee = document.getElementById('hdnEmployee');

    if (txtSID.value == '') {
        txtSID.value = '0';
    }

    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    var check = true
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {
            desg: txtdesg.value.trim(),
            sno: txtSID.value,
            dummy: check,

        }

        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = $('#tbl-smtp').DataTable();
        var row = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < row.length; i++) {
            var name = row[i].cells[1].innerHTML.toLowerCase().trim();
            var newname = txtdesg.value.toLowerCase().trim();
            if (name == newname && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                desg: txtdesg.value.trim(),
                sno: txtSID.value,
                dummy: check,

            }
        }
        else {
            var data = {
                desg: txtdesg.value.trim(),
                sno: txtSID.value,
                dummy: check,
            }
        }

        return data;
    }

}

function getSMTPID_Desg(glob) {
    //  jQuery.noConflict();


    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();

    if (hdnEmployee.value == "D") {

        var data = {

            sno: glob,

        }

        return data;
    }
}

function validateEmployeedesg_Desg() {

    var txtdesg = document.getElementById('txtdesg');

    result = '';

    if (txtdesg.value.trim().length == 0) {
        result += 'Designation is Required .<br/>';
    } else {
        var d = alpha(txtdesg.value.trim());
        if (d != false) {
            result += d;
        }
    }
    return result;
}


function deletecurGrid_Desg() {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected');

    table.row(selectedRow).remove().draw();
}
