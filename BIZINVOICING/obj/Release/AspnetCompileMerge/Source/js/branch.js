var cid;
var check;
var jk = false;
function valid(e) {
    jk = true;
}
function resetBranch() {
    var txtbranch = document.getElementById('txtbranch');
    var txtloc = document.getElementById("txtloc");
    txtbranch.value = '';
    txtloc.value = '';
    jQuery("#rbtrue").prop("checked", true);
}
function showBranchModal(e) {
    var txtbranch = document.getElementById('txtbranch');
    var txtloc = document.getElementById("txtloc");
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        hdnEmployee = document.getElementById('hdnEmployee');
    jk = false;
    if (e.value.indexOf('Edit') > -1) {
        jQuery("#btnSubmit").html("Update");
        //jQuery('#txtcode').attr('readonly', 'true');
        for (var i = 0, row = null; i < rows.length; i++) {
            row = rows[i];
            if (row.cells[4].innerHTML.indexOf(e.id) > -1) {
                txtSID.value = jQuery(e).data('sid');
                txtbranch.value = row.cells[1].innerHTML;
                txtloc.value = row.cells[2].innerHTML;
                var flag = row.cells[3].innerHTML;
                if (flag == 'Active') {
                    jQuery("#rbtrue").prop("checked", true);
                    jQuery("#rbfalse").prop("checked", false);
                }
                else if (flag == 'InActive') {
                    jQuery("#rbtrue").prop("checked", false);
                    jQuery("#rbfalse").prop("checked", true);
                } else {
                    jQuery("#rbtrue").prop("checked", false);
                    jQuery("#rbfalse").prop("checked", false);
                }
                break;
            }
        }

        hdnEmployee.value = 'U';
    }
    else if (e.value.indexOf('Add') > -1) {
        
        resetBranch();
        //jQuery("#txtcode").attr("readonly", false);
        jQuery("#btnSubmit").html("Save");

        hdnEmployee.value = 'C';


    }

}


function getBranchValues() {
    // jQuery.noConflict();
    var txtbranch = document.getElementById('txtbranch');
    var txtloc = document.getElementById("txtloc");
    

    txtSID = document.getElementById('txtSID'),
        rblGender = jQuery("input[name='gender']:checked"),
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
                if ((txtbranch.value == code) && (txtloc.value == name)) {
                    dt += txtbranch.value;
                    dn += txtloca.value;
                    doublecheck = true;

                }
                /*else if (txtname.value.toLowerCase().trim() == name) {

                    dn += txtname.value;
                    doublechecked = true;

                }*/
            }
        }

        if (doublecheck == true) {
            message = dt +' '+ dn+ ' Branch and location already exists.';
            type = 'danger';
            notifyMessage(message, type);

        }
        /*else if (doublechecked == true) {
            message = dn + ' Currency name already exists.';
            type = 'danger';
            notifyMessage(message, type);
        }*/
        else {
            var data = {
                branch: txtbranch.value.trim(),
                location: txtloc.value.trim(),
                status: rblGender.val(),
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
            var newname = txtbranch.value.toLowerCase().trim();
            if (name == newname && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                branch: txtbranch.value.trim(),
                location: txtloc.value.trim(),
                status: rblGender.val(),
                sno: txtSID.value,
                dummy: check,

            }
        }
        else {
            var data = {
                branch: txtbranch.value.trim(),
                location: txtloc.value.trim(),
                status: rblGender.val(),
                sno: txtSID.value,
                dummy: check,

            }
        }

        return data;
    }

}

function getBranchID(glob) {
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

function validateEmployeebra() {

    var txtbranch = document.getElementById('txtbranch');
    var txtloc = document.getElementById("txtloc");
    var chosen = "";
    result = '';
    var len = document.forms[0].gender.length;

    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }


    if (txtbranch.value.trim().length == 0) {
        result += 'Branch is required.<br/>';
    } /*else {
        var d = alpha(txtcode.value.trim());
        if (d != false) {
            result += d;
        }
    }*/
    if (txtloc.value.trim().length == 0) {
        result += 'Location is required.<br/>';
    } /*else {
        var d = alpha(txtname.value.trim());
        if (d != false) {
            result += d;
        }
    }*/
    if (chosen == "") {
        result += 'Status is required.<br/>';
    }
    return result;
}



function deletebraGrid() {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected');

    table.row(selectedRow).remove().draw();
}
