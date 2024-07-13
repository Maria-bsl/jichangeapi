var cid;
var check;
var jk = false;
function validreg(e) {

    jk = true;
}

function resetdeposit() {
    var txtRegion = document.getElementById('ddlvendor');
    var txtReason = document.getElementById('txtReason');
    var ddlcou = document.getElementById("ddlaccount");
    txtRegion.value = 0;
    ddlcou.value = 0;
    txtReason.value = '';
}



// function clearconsole() {
//     console.log(window.console);
//     if (window.console || window.console.firebug) {
//         console.clear();
//     }
// }
function getdeposit() {
    // jQuery.noConflict();
    var v = document.getElementById('ddlvendor');
    var e = document.getElementById("ddlaccount");
    var accvalue = e.options[e.selectedIndex].value;
    var acctext = e.options[e.selectedIndex].text;

    var venvalue = v.options[v.selectedIndex].value;
    var ventext = v.options[v.selectedIndex].text;
    var txtReason = document.getElementById('txtReason');
    txtSID = document.getElementById('txtSID'),
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
            csno: venvalue,
            account: accvalue,
            reason: txtReason.value.trim(),
            sno: txtSID.value,
            dummy: check
        }


        return data;
    }
    

}



function validatedeposit() {
    var txtReason = document.getElementById('txtReason');
    var v = document.getElementById('ddlvendor');
    var e = document.getElementById("ddlaccount");
    var ddlvalue = e.options[e.selectedIndex].value;
    var venvalue = v.options[v.selectedIndex].value;
    // var ddltext = e.options[e.selectedIndex].text;
    result = '';
    
    
    if (venvalue == 0) {
        result += 'Vendor is required.<br/>';

    }
    if (ddlvalue == 0) {
        result += 'Account is required.<br/>';
    } 
    if (txtReason.value.trim().length == 0) {
        result += 'Reason is required.<br/>';
    }
    
    


    return result;


}