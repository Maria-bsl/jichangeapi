var doublemail;
var doublemob;
var cid;
var check;
var jk = false;
function validemp_Buser(e) {
    jk = true;
}
function checkduplicateemp_Buser(e) {
    var name = document.getElementById('txtuname').value;
    binddupliacteDetailsemp_Buser(name);
}
function ValidateEmailemp_Buser() {

    var email = document.getElementById('txtEmail').value;
    var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (reg.test(email)) {
        // lblError.innerHTML = "";
        doublemail = true;

    }
    else {
        doublemail = false;
    }
    binddupliacteDetailsEmailemp_Buser(email);
}
function mobileNumValiadteemp_Buser() {
    var mobileNum = jQuery('#txtMobile').val();
    var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    if (mobileNum.substr(0000, 4) != 0000 || mobileNum == '') {
        if (validateMobNum.test(mobileNum) && mobileNum.length == 12) {
            lblError2.innerHTML = "";
            doublemob = true;
            jQuery("#lblError2").hide();
        }
        else {
            jQuery("#lblError2").show();
            lblError2.innerHTML = "";
            doublemob = false;
            message = "Mobile No.should be 12 digits";
        }
    }
    else {
        jQuery("#lblError2").show();
        doublemob = false;
        lblError2.innerHTML = "Invalid Mobile Number.";
        message = "Invalid Mobile Number.";
        jQuery('#lblError2').css('color', 'Red');
    }
}
function resetchru_Buser() {
    var txtempid = document.getElementById('txtempid');
    var txtf = document.getElementById('txtfirstname');
    var txtm = document.getElementById('txtmiddle');
    var txtl = document.getElementById('txtlast');
    var ddesg = document.getElementById('ddldesg');
    var dbra = document.getElementById('ddlbra');
    var txtemail = document.getElementById('txtEmail');
    var txtmo = document.getElementById('txtMobile');
    var txtunsr = document.getElementById('txtuname');
    var radioValue = jQuery('input[name="gender"]').prop('checked', false);
    txtempid.value = '';
    txtf.value = '';
    ddesg.selectedIndex = 0;
    dbra.selectedIndex = 0;
    jQuery("#ddlbran").val(0).change();
    txtm.value = '';
    txtl.value = '';
    txtemail.value = '';
    txtmo.value = '';
    radioValue = '';
    txtunsr.value = ''

}

function getSMTPValues_Buser() {
    if (doublemail == false) {
        jQuery("#loader").css("display", "none");
        message = "Invalid Email";
        type = 'danger';
        notifyMessage(message, type);
    }
    else if (doublecheckemail == false) {
        jQuery("#loader").css("display", "none");
        message = "Email Already Exists";
        type = 'danger';
        notifyMessage(message, type);
    }
    else if (doublemob == false) {
        jQuery("#loader").css("display", "none");
        message = message;
        type = 'danger';
        notifyMessage(message, type);
    }
    else if (doubleuser == false) {
        jQuery("#loader").css("display", "none");
        message = "User Name Already Exists";
        type = 'danger';
        notifyMessage(message, type);
    }
    else {
        jQuery("#loader").css("display", "block");
        //jQuery.noConflict();
        var txtempid = document.getElementById('txtempid');
        //  var txtfname = document.getElementById('txtfName');
        var txtf = document.getElementById('txtfirstname');
        var txtm = document.getElementById('txtmiddle');
        var txtl = document.getElementById('txtlast');
        var ddesg = document.getElementById('ddldesg');
        var dbra = document.getElementById('ddlbra');
        var txtemail = document.getElementById('txtEmail');
        var txtmo = document.getElementById('txtMobile');
        var txtunsr = document.getElementById('txtuname');
        var bnvalue = ddesg.options[ddesg.selectedIndex].value;
        var bravalue = dbra.options[dbra.selectedIndex].value;
        txtSID = document.getElementById('txtSID');
        rblGender = jQuery("input[name='gender']:checked");
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
                empid: txtempid.value.trim(),
                fname: txtf.value.trim(),
                mname: txtm.value.trim(),
                lname: txtl.value.trim(),
                desg: bnvalue,
                email: txtemail.value.trim(),
                mobile: txtmo.value,
                user: txtunsr.value.trim(),
                gender: rblGender.val(),
                sno: txtSID.value,
                dummy: check,
                branch: bravalue,

            }

            return data;
        }
        else if (hdnEmployee.value == "U") {
            tblemployee = document.getElementById('tbl-smtp'),
                rows = tblemployee.rows;
            for (var i = 0; i < rows.length; i++) {
                var name = rows[i].cells[1].innerHTML.toLowerCase().trim();
                var newname = txtempid.value.toLowerCase().trim();
                if (name == newname && jk == true) {
                    check = false;
                }
                else { }
            }
            if (check == true) {
                var data = {
                    empid: txtempid.value.trim(),
                    fname: txtf.value.trim(),
                    mname: txtm.value.trim(),
                    lname: txtl.value.trim(),
                    desg: bnvalue,
                    email: txtemail.value.trim(),
                    mobile: txtmo.value.trim(),
                    user: txtunsr.value.trim(),
                    sno: txtSID.value,
                    gender: rblGender.val(),
                    dummy: check,
                    branch: bravalue,
                }
            }
            
            return data;
        }

    }
}

function validateEmployeechemp_Buser() {

    var txtempid = document.getElementById('txtempid');
    var txtf = document.getElementById('txtfirstname');
    var txtl = document.getElementById('txtlast');
    var ddesg = document.getElementById('ddldesg');
    var dbra = document.getElementById('ddlbra');
    //var bran = document.getElementById('ddlbran');
    var txtemail = document.getElementById('txtEmail');
    var txtmo = document.getElementById('txtMobile');
    var txtunsr = document.getElementById('txtuname');
    rblGender = jQuery("input[name='gender']:checked");
    result = '';
    var chosen = "";
    var len = document.forms[0].gender.length;
    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }
    if (txtempid.value.trim().length == 0) {
        result += 'EmployeeID is required.<br/>';
    } else {
        var d = alpha(txtempid.value.trim());
        if (d != false) {
            result += d;
        }
    }
    if (txtf.value.trim().length == 0) {
        result += 'First Name is required.<br/>';
    } else {
        var d = alpha(txtf.value.trim());
        if (d != false) {
            result += d;
        }
    }
    if (txtl.value.trim().length == 0) {
        result += 'Last Name is required.<br/>';
    } else {
        var d = alpha(txtl.value.trim());
        if (d != false) {
            result += d;
        }
    }
    if (txtunsr.value.trim().length == 0) {
        result += 'UserName is required.<br/>';
    } else {
        var d = alpha(txtunsr.value.trim());
        if (d != false) {
            result += d;
        }
    }
    if (ddesg.value == 0) {
        result += 'Designation is required.<br/>';
    }
    if (dbra.value == 0) {
        result += 'Branch is required.<br/>';
    }

    if (txtemail.value.trim().length == 0) {
        result += 'Email ID is required.<br/>';
    }
    if (txtmo.value.trim().length == 0) {
        result += 'Mobile No is required.<br/>';
    }
  
    if (chosen == "") {
        result += 'Status is Required.<br/>';
    }
    return result;
}
