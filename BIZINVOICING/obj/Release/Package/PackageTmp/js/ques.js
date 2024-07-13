var cid;
var check;
var jk = false;
function valid_Ques(e) {

    jk = true;
}
function resetSMTP_Ques() {
    var txtEmail = document.getElementById('txtname');
    // txtsts = document.getElementById('txtsts');
    //var radios = jQuery('input:radio[name=gender]');
    //if (radios.is(':checked') == false) {
    //    radios.filter('[value=Active]').prop('checked', true);
    //}
    jQuery("#rbtrue").prop('checked', true);
    txtEmail.value = '';
}



function GetCount_Ques() {

    // jQuery.noConflict();
    var txtEmail = document.getElementById('txtname');
    //  txtsts = document.getElementById('txtsts'),
    txtSID = document.getElementById('txtdid'),
        rblreg = jQuery("input[name='gender']:checked"),
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
            q_name: txtEmail.value.trim(),
            q_qtatus: rblreg.val(),
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
            var newname = txtEmail.value.toLowerCase().trim();
            if (name == newname && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                q_name: txtEmail.value.trim(),
                q_qtatus: rblreg.val(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        else {
            var data = {
                q_name: txtEmail.value.trim(),
                q_qtatus: rblreg.val(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        return data;

    }
}

function getques_Ques(glob) {
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

function validateEmployee_Ques() {

    var txtEmail = document.getElementById('txtname'),
        //  txtsts = document.getElementById('txtsts');
        result = '';
    var chosen = "";
    var len = document.forms[0].gender.length;

    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }
    if (txtEmail.value.trim().length == 0) {
        result += 'Security Question is required.<br/>';
    } else {
        var d =  alphaFewsymlatest(txtEmail.value.trim());
        if (d != false) {
            result += d;
        }
    }
    if (chosen == "") {
        result += 'Required Question Status.<br/>';
    }


    return result;
}

function deleteQuesGrid_Ques() {

    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected');

    table.row(selectedRow).remove().draw();
}

