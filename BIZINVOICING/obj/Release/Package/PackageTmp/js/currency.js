var cid;
var check;
var jk = false;
function valid(e) {
    jk = true;
}
function resetRegion() {
    var txtcode = document.getElementById('txtcode');
    var txtname = document.getElementById("txtname");
    txtcode.value = '';
    txtname.value = '';
}
function showcurrencyModal(e) {
    var txtcode = document.getElementById('txtcode');
    var txtname = document.getElementById('txtname');
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        hdnEmployee = document.getElementById('hdnEmployee');
    jk = false;
    if (e.value.indexOf('Edit') > -1) {
        jQuery("#btnSubmit").html("Update");
        jQuery('#txtcode').attr('readonly', 'true');
        for (var i = 0, row = null; i < rows.length; i++) {
            row = rows[i];
            if (row.cells[3].innerHTML.indexOf(e.id) > -1) {
                txtSID.value = jQuery(e).data('sid');
                txtcode.value = row.cells[1].innerHTML;
                txtname.value = row.cells[2].innerHTML;

                break;
            }
        }

        hdnEmployee.value = 'U';
    }
    else if (e.value.indexOf('Add') > -1) {

        resetRegion();
        jQuery("#txtcode").attr("readonly", false);
        jQuery("#btnSubmit").html("Save");

        hdnEmployee.value = 'C';


    }

}


function getCurrencyValues() {
    // jQuery.noConflict();
    var txtcode = document.getElementById('txtcode');
    var txtname = document.getElementById("txtname");

    txtSID = document.getElementById('txtSID'),
        tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        hdnEmployee = document.getElementById('hdnEmployee');

    if (txtSID.value == '') {
        txtSID.value = '0';
    }

    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    var check = true;
    var dt = '';
    var dn = '';
    var doublecheck = false;
    var doublechecked = false;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var tblemployee = document.getElementById('tbl-smtp'),
            rows = tblemployee.rows;
        for (var i = 1, row = null; i < rows.length; i++) {
            row = rows[i];
            var code;
            var name;
            var rowCount = $('#tbl-smtp tr').length;
            if (rowCount == 2) {
            }
            else {
                row.cells[1].innerHTML == undefined ? code = '' : code = row.cells[1].innerHTML.toLowerCase().trim();
                row.cells[2].innerHTML == undefined ? name = '' : name = row.cells[2].innerHTML.toLowerCase().trim();
                if (txtcode.value == code) {
                    dt += txtcode.value;
                    doublecheck = true;

                }
                else if (txtname.value.toLowerCase().trim() == name) {

                    dn += txtname.value;
                    doublechecked = true;

                }
            }
        }

        if (doublecheck == true) {
            message = dt + ' Currency code already exists.';
            type = 'danger';
            notifyMessage(message, type);

        }
        else if (doublechecked == true) {
            message = dn + ' Currency name already exists.';
            type = 'danger';
            notifyMessage(message, type);
        }
        else {
            var data = {
                code: txtcode.value.trim(),
                cname: txtname.value.trim(),
                sno: txtSID.value,
                dummy: check,

            }
            return data;
        }
    }
    else if (hdnEmployee.value == "U") {
        var table = $('#tbl-smtp').DataTable();
        var row = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < row.length; i++) {
            var name = row[i].cells[2].innerHTML.toLowerCase().trim();
            var newname = txtname.value.toLowerCase().trim();
            if (name == newname && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                code: txtcode.value.trim(),
                cname: txtname.value.trim(),
                sno: txtSID.value,
                dummy: check,

            }
        }
        else {
            var data =
            {
                code: txtcode.value.trim(),
                cname: txtname.value.trim(),
                sno: txtSID.value,
                dummy: check,
            }
        }

        return data;
    }

}

function getcurrencyID(glob) {
    // jQuery.noConflict();
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();

    if (hdnEmployee.value == "D") {

        var data = {

            sno: glob,

        }

        return data;
    }
}

function validateEmployeecur() {

    var txtcode = document.getElementById('txtcode');
    var txtname = document.getElementById("txtname");

    result = '';

    if (txtcode.value.trim().length == 0) {
        result += 'Currency Code is required.<br/>';
    } else {
        var d = alpha(txtcode.value.trim());
        if (d != false) {
            result += d;
        }
    }
    if (txtname.value.trim().length == 0) {
        result += 'Currency Name is required.<br/>';
    } else {
        var d = alpha(txtname.value.trim());
        if (d != false) {
            result += d;
        }
    }

    return result;
}



function deletecurGrid() {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected');

    table.row(selectedRow).remove().draw();
}
