var cid;
var check;
var jk = false;
function valid_Email(e) {

    jk = true;
}
var today, datepicker;
today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

/*$('#txtdate').datepicker({
    uiLibrary: 'bootstrap4',
    format: 'dd/mm/yyyy',
    iconsLibrary: 'fontawesome',
    weekStartDay: 1,
});*/

function resetSMTP_Email() {
    var txttext = document.getElementById('txtEtext');
    var ddlflow = document.getElementById('ddlflow');
    var txtsub = document.getElementById('txtsub');
    var txtsubloc = document.getElementById('txtsubloc');
    var txtloc = document.getElementById('txtloctext');
    ddlflow.selectedIndex = 0;
    txttext.value = '',
        txtsub.value = '',
        txtsubloc.value = '',
        txtloc.value = '';
}



function getSMTPValues_Email() {
    // jQuery.noConflict();
    var txttext = document.getElementById('txtEtext');
    var ddlflow = document.getElementById('ddlflow');
    var txtsub = document.getElementById('txtsub');
    var txtsubloc = document.getElementById('txtsubloc');
    var txtloc = document.getElementById('txtloctext');
    txtSID = document.getElementById('txtSID'),

        hdnEmployee = document.getElementById('hdnEmployee');
    if (txtSID.value == '') {
        txtSID.value = '0';
    }

    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    // check = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {
            flow: ddlflow.value,
            text: txttext.value.trim(),
            loctext: txtloc.value.trim(),
            sub: txtsub.value.trim(),
            subloc: txtsubloc.value.trim(),
            sno: txtSID.value,
        }

        return data;
    }
    else if (hdnEmployee.value == "U") {

        var data = {
            flow: ddlflow.value,
            text: txttext.value.trim(),
            loctext: txtloc.value.trim(),
            sub: txtsub.value.trim(),
            subloc: txtsubloc.value.trim(),
            sno: txtSID.value,
        }



        return data;

    }

}

function getSMTPID_Email(glob) {
    //jQuery.noConflict();


    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();

    if (hdnEmployee.value == "D") {

        var data = {

            sno: glob,

        }

        return data;
    }
}

function validateEmployee_Email() {

    var txttext = document.getElementById('txtEtext');
    var ddlflow = document.getElementById('ddlflow');
    var txtsub = document.getElementById('txtsub');

    result = '';
    if (ddlflow.value == 0) {
        result += 'Flow Id is Required .<br/>';
    }

    if (txtsub.value.trim().length == 0) {
        result += 'Subject is Required .<br/>';
    }
    if (txttext.value.trim().length == 0) {
        result += ' Email Text is Required.<br/>';
    }

    return result;
}


function deleteSMTPGrid_Email() {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected');

    table.row(selectedRow).remove().draw();
}
