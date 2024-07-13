var cid;
var check;
var jk = false, ak = false;

function validward(e) {

    jk = true;
}
function valuesward(e) {
    ak = false;
    if (jQuery("#ints").val() == "Initail") {
        //  alert("tt");
        ak = true;
    }
    else {
        // alert("tc");
        ak = false;
    }
}
function resetSMTPward() {
    var txtward = document.getElementById('txtname');
    var ddlreg = document.getElementById('ddlreg');
    var ddldist = document.getElementById('ddldno');
    jQuery("#rbtrue").prop("checked", true);
    ddldist.value = 0;
    ddlreg.value = 0;
    txtward.value = '';
}



function getDistValuesward() {
    var txtward = document.getElementById('txtname'),
        txtSID = document.getElementById('txtdid'),
        ddlreg = document.getElementById('ddlreg'),
        ddldist = document.getElementById('ddldno');
    var rsno = ddlreg.options[ddlreg.selectedIndex].value,
        dsno = ddldist.options[ddldist.selectedIndex].value,
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
            ward_name: txtward.value.trim(),
            region_id: rsno,
            district_sno: dsno,
            sno: txtSID.value,
            ward_status: rblreg.val(),
            dummy: check,
        }

        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = $('#tbl-smtp').DataTable();
        var row = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < row.length; i++) {
            var name = row[i].cells[5].innerHTML.toLowerCase().trim();
            var newname = txtward.value.toLowerCase().trim();
            var dis = row[i].cells[3].innerHTML.toLowerCase().trim();
            var disno = dsno;
            var dis1 = row[i].cells[1].innerHTML.toLowerCase().trim();
            var disno1 = rsno;
            if (name == newname && dis == disno && dis1 == disno1 && jk == true && ak == true) {
                check = false;
            }
            else if (name == newname && dis == disno && dis1 == disno1 && jk == false && ak == true) {
                check = false;
            }
            else if (name == newname && dis == disno && dis1 == disno1 && jk == false && ak == false) {
                check = true;
            }
            else if (name == newname && dis == disno && dis1 == disno1 && jk == true && ak == false) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                ward_name: txtward.value.trim(),
                region_id: rsno,
                district_sno: dsno,
                sno: txtSID.value,
                ward_status: rblreg.val(),
                dummy: check,
            }
        }
        else {
            var data = {
                ward_name: txtward.value.trim(),
                region_id: rsno,
                district_sno: dsno,
                sno: txtSID.value,
                ward_status: rblreg.val(),
                dummy: check,
            }
        }


        return data;
    }
}

function getwardIDward(glob) {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "D") {
        var data = {
            sno: glob,
        }
        return data;
    }
}

function validateEmployeeward() {

    var txtward = document.getElementById('txtname');
    var txtSID = document.getElementById('txtdid');
    var ddlreg = document.getElementById('ddlreg');
    var ddldist = document.getElementById('ddldno');
    result = '';
    var chosen = "";
    var len = document.forms[0].gender.length;

    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }
    if (ddlreg.value == 0) {
        result += 'Region Name is Required<br/>';
    }
    if (ddldist.value == 0) {
        result += 'District Name is Required<br/>';
    }
    if (txtward.value.trim().length == 0) {
        result += 'Ward Name is Required.<br/>';
    } else {
        var d = alpha(txtward.value.trim());
        if (d != false) {
            result += d;
        }
    }
    if (chosen == "") {
        result += 'Status is Required.<br/>';
    }
    return result;
}