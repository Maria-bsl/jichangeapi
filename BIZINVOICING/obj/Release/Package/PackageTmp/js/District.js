var cid;
var check;
var jk = false;
function validdist(e) {

    jk = true;
}

function resetSMTPdist() {
    var txtEmail = document.getElementById('txtname');
    var ddlreg = document.getElementById('ddlreg');
    jQuery("#rbtrue").prop("checked", true);
    ddlreg.value = 0;
    txtEmail.value = '';
}



function getDistValuesdist() {

    //jQuery.noConflict();

    var txtEmail = document.getElementById('txtname'),
        txtSID = document.getElementById('txtdid');
    var ddlreg = document.getElementById("ddlreg");

    var result = ddlreg.options[ddlreg.selectedIndex].value;
    var tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,

        rblreg = jQuery("input[name='gender']:checked"),
        hdnEmployee = document.getElementById('hdnEmployee');
    if (txtSID.value == '') {
        txtSID.value = '0';
    }

    var table = jQuery('#tbl-smtp').DataTable();
    selectedRow = table.rows('.selected').data();
    check = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {
            district_name: txtEmail.value.trim(),
            region_id: result,
            sno: txtSID.value,
            district_status: rblreg.val(),
            dummy: check,
        }

        return data;
    }
    else if (hdnEmployee.value == "U") {

        var table = $('#tbl-smtp').DataTable();
        var row = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < row.length; i++) {
            var name = row[i].cells[3].innerHTML.toLowerCase().trim();
            var newname = txtEmail.value.toLowerCase().trim();
            if (name == newname && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                district_name: txtEmail.value.trim(),
                sno: txtSID.value,
                region_id: result,
                district_status: rblreg.val(),
                dummy: check,
            }
        }
        else {
            var data = {
                district_name: txtEmail.value.trim(),
                sno: txtSID.value,
                region_id: result,
                district_status: rblreg.val(),
                dummy: check,
            }
        }
        return data;
    }

}

function getDistIDdist(glob) {

    //jQuery.noConflict();


    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();


    // alert(glob.value);

    if (hdnEmployee.value == "D") {

        var data = {

            sno: glob,

        }

        return data;
    }
}

function validateEmployeedist() {

    var txtEmail = document.getElementById('txtname'),
        txtAdd = document.getElementById('ddlreg');
    var results = txtAdd.options[txtAdd.selectedIndex].value;
    result = '';
    var chosen = "";
    var len = document.forms[0].gender.length;

    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }

    if (txtAdd.value == 0) {
        result += 'Region is required<br/>';
    }
    if (txtEmail.value.trim().length == 0) {
        result += 'District is required.<br/>';
    }
    else {
        var d = alpha(txtEmail.value.trim());
        if (d != false) {
            result += d;
        }
    }
    if (chosen == "") {
        result += 'Status is required.<br/>';
    }
    return result;
}
