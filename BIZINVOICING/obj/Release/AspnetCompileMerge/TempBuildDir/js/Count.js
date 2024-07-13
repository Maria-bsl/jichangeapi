
var cid;
var check;
var jk;

function validcount(e) {

    jk = true;
}

function resetSMTPcount() {
    var txtname = document.getElementById('txtname');
    // var ddlCat = document.getElementById('ddlCat');
    txtSID = document.getElementById('txtdid');
    txtSID = 0;
    // ddlCat.value = 0;
    txtname.value = '';


}


function GetCount() {

    var txtEmail = document.getElementById('txtname');
    txtSID = document.getElementById('txtdid');
    // var ddlreg = document.getElementById("ddlCat");

    // var result = ddlreg.options[ddlreg.selectedIndex].value;
    // var result1 = ddlreg.options[ddlreg.selectedIndex].html;
    hdnEmployee = document.getElementById('hdnEmployee');
    if (txtSID.value == '') {
        txtSID.value = '0';
    }

    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    check = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {

            country_name: txtEmail.value.trim(),
            vat_cat: result,
            // vat_desc: result1,
            sno: txtSID.value,
            dummy: check,

        }
        return data;
    }
    else if (hdnEmployee.value == "U") {
        tblemployee = document.getElementById('tbl-smtp'),
            rows = tblemployee.rows;
        for (var i = 0; i < rows.length; i++) {
            var name = rows[i].cells[1].innerHTML.toLowerCase().trim();
            var newname = txtEmail.value.toLowerCase().trim();
            if (name == newname && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                country_name: txtEmail.value.trim(),
                vat_cat: result,
                // vat_desc: result1,
                sno: txtSID.value,
                dummy: check,

            }
        }
        else {
            var data = {
                country_name: txtEmail.value.trim(),
                vat_cat: result,
                vat_desc: result1,
                sno: txtSID.value,
                dummy: check,
            }
        }
        return data;
    }
}

function validateEmployeecount() {

    var txtEmail = document.getElementById('txtname');
    // txtAdd = document.getElementById('ddlCat');
    // var results = txtAdd.options[txtAdd.selectedIndex].value();
    result = '';
    if (txtEmail.value.trim().length == 0) {
        result += 'Vat Percentage required<br/>';
    }
    else {
        var d = alpha(txtEmail.value.trim());
        if (d != false) {
            result += d;
        }
    }

    // if (txtAdd.value == 0) {
    //     result += 'Vat Category required<br/>';
    // }
    return result;
}