var cid;
var check;
var jk = false;
function validreg(e) {

    jk = true;
}

function resetRegionreg() {
    var txtRegion = document.getElementById('txtregion');
    var ddlcou = document.getElementById("ddlcountry");
    jQuery("#rbtrue").prop("checked", true);
    txtRegion.value = '';
    ddlcou.value = 0;
}



// function clearconsole() {
//     console.log(window.console);
//     if (window.console || window.console.firebug) {
//         console.clear();
//     }
// }
function getRegionValuesreg() {
    // jQuery.noConflict();
    var txtRegion = document.getElementById('txtregion');
    var e = document.getElementById("ddlcountry");
    var ddlvalue = e.options[e.selectedIndex].value;
    var ddltext = e.options[e.selectedIndex].text;

    txtSID = document.getElementById('txtSID'),
        rblGender = jQuery("input[name='gender']:checked"),
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
            region: txtRegion.value.trim(),
            country: ddltext,
            csno: ddlvalue,
            sno: txtSID.value,
            Status: rblGender.val(),
            dummy: check,
        }


        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = $('#tbl-smtp').DataTable();
        var row = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < row.length; i++) {
            var name = row[i].cells[2].innerHTML.toLowerCase().trim();
            var newname = txtRegion.value.toLowerCase().trim();
            if (name == newname && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                region: txtRegion.value.trim(),
                country: ddltext,
                csno: ddlvalue,
                sno: txtSID.value,
                Status: rblGender.val(),
                dummy: check,
            }
        }
        else {
            var data = {
                region: txtRegion.value.trim(),
                country: ddltext,
                csno: ddlvalue,
                sno: txtSID.value,
                Status: rblGender.val(),
                dummy: check,
            }
        }

        return data;
    }

}

function getSMTPIDreg(glob) {
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

function validateEmployeereg() {

    var txtRegion = document.getElementById('txtregion');
    var e = document.getElementById("ddlcountry");
    var ddlvalue = e.options[e.selectedIndex].value;
    // var ddltext = e.options[e.selectedIndex].text;
    result = '';
    var chosen = "";

    var len = document.forms[0].gender.length;

    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }
    if (ddlvalue == 0) {
        result += 'Country is required.<br/>';

    }
    if (txtRegion.value.trim().length == 0) {
        result += 'Region is required.<br/>';
    } else {
        var d = alpha(txtRegion.value.trim());
        if (d != false) {
            result += d;
        }
    }
    if (chosen == "") {
        result += 'Status is required.<br/>';
    }


    return result;


}