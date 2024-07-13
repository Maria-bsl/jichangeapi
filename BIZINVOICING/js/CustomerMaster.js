var cid;
var check;
var jk = false;
var doublemob;
var doublecheckemail = true;
var doubleemail;
var doubleTin;
function validCust(e) {
    jk = true;
}
function showDiv() {
    jQuery("#btnSubmit").click(function () {
        jQuery(this).hide();
        jQuery("#loader").css("display", "block");
    }, 2000);
}
function PagerClick(index) {
    document.getElementById("hfCurrentPageIndex").value = index;
    document.forms[0].submit();
}
function alpha(e) {
    var k;
    document.all ? k = e.keyCode : k = e.which;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
}
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function validCustom(e) {
    ak = true;
}
// function binddupliacteDetailsEmail(name) {
//    jQuery.ajax({
//        type: 'POST',
//        url: '@Url.Action("CheckdupliacteEmail", "INSTIREG")',
//        data: JSON.stringify({ 'name': name }),
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        success: function (data) {
//            if (data == true) {
//                doublecheckemail = false;
//            }
//            else {
//                doublecheckemail = true;
//                lblError1.innerHTML = "";
//            }
//        }
//    })
//} 
function GetCust() {
    if (doublemob == false || doubleemail==false ||doubleTin ==false) {
        jQuery("#loader").css("display", "none");
        //message = "Mobile No.should be 12 digits";
        //type = 'danger';
        //notifyMessage(message, type);
    }

    else {
        var txtCusName = document.getElementById('txtcusname');
        /*var txtPono = document.getElementById("txtpono");
        var txtAdd = document.getElementById("txtadd");
        var txtTinno = document.getElementById("txttinno");
        var txtVatno = document.getElementById("txtvatno");
        var txtCperson = document.getElementById("txtcperson");*/
        var txtEmail = document.getElementById("txtemail");
        var txtMob = document.getElementById("txtmob");
        txtSID = document.getElementById('txtSID');
        //var ddlcomp = document.getElementById("comp");
        ////alert(ddlcomp);
        // var compsno = ddlcomp.options[ddlcomp.selectedIndex].value;
           //comptxt = ddlcomp.options[ddlcomp.selectedIndex].text,

       /* var ddlREG = document.getElementById("ddlreg");
        var regsno = ddlREG.options[ddlREG.selectedIndex].value,
            ddlreg = ddlREG.options[ddlREG.selectedIndex].text,
         ddlDIST = document.getElementById("ddldist");
        var distsno = '0',//ddlDIST.options[ddlDIST.selectedIndex].value
            //ddldist = ddlDIST.options[ddlDIST.selectedIndex].text,
         ddlWARD = document.getElementById("ddlward");
        var wardsno = '0',//ddlWARD.options[ddlWARD.selectedIndex].value
            //ddlward = ddlWARD.options[ddlWARD.selectedIndex].text,
            rblcheck = jQuery("input[name='checker']:checked"),
        hdnEmployee = document.getElementById('hdnEmployee');
        if (txtSID.value == '') {
            txtSID.value = '0';
        }
        if (jQuery("#ddldist option").length != '0') {
            distsno = ddlDIST.options[ddlDIST.selectedIndex].value;
        }
        if (jQuery("#ddlward option").length != '0') {
            wardsno = ddlWARD.options[ddlWARD.selectedIndex].value;
        }*/
        var table = jQuery('#tbl-smtp').DataTable(),
            selectedRow = table.rows('.selected').data();
        check = true;

        if (hdnEmployee.value == "C") {
            txtSID.value = '0';
            jk = false;
            var data = {
                CSno: txtSID.value,
               // Compsno: compsno,
                CName: txtCusName.value.trim(),
                PostboxNo: "",
                Address: "",
                regid: 0,
                distsno: 0,
                wardsno: 0,
                Tinno: "",
                VatNo: "",
                CoPerson: "",
                Mail: txtEmail.value.trim(),//may be changed
                Mobile_Number: txtMob.value.trim(),
                dummy: check,
                check_status: "",
            }
            return data;
        }
        else if (hdnEmployee.value == "U") {
            var table = jQuery('#tbl-smtp').DataTable();
            var rows = table.rows({ page: 'all' }).nodes();
            for (var i = 0; i < rows.length; i++) {
                var name = rows[i].cells[1].innerHTML.toLowerCase().trim();
                var newname = txtCusName.value.toLowerCase().trim();
                //var tin = rows[i].cells[10].innerHTML;
                //var newtin = txtTinno.value.trim();
                //var email = rows[i].cells[13].innerHTML;
                //var newemail = txtEmail.value.trim();
                //var mob = rows[i].cells[14].innerHTML;
                //var newmob = txtMob.value.trim();
                //if (/*name == newname &&*/ tin == newtin && jk == true) {
                //    check = false;
                //}
                //if (name != newname /*&& tin == newtin*/ && jk == true) {
                //    check = true;
                //}
                //else if (tin == newtin && jk == true) {
                //    check = false;
                //}
                //else if (tin != newtin && jk == true) {
                //    check = true;
                //}
                if (name == newname && jk == true ) {
                    check = false;
                }
                //else if (name == newname && jk == true && tin == newtin && ak == true) {
                //    check = true;
                //}
                else { }
            }
            if (check == true) {
                var data = {
                    CSno: txtSID.value,
                    // Compsno: compsno,
                    CName: txtCusName.value.trim(),
                    PostboxNo: "",
                    Address: "",
                    regid: 0,
                    distsno: 0,
                    wardsno: 0,
                    Tinno: "",
                    VatNo: "",
                    CoPerson: "",
                    Mail: txtEmail.value.trim(),//may be changed
                    Mobile_Number: txtMob.value.trim(),
                    dummy: check,
                    check_status: "",
                }
            }
            else {
                var data = {
                    CSno: txtSID.value,
                    // Compsno: compsno,
                    CName: txtCusName.value.trim(),
                    PostboxNo: "",
                    Address: "",
                    regid: 0,
                    distsno: 0,
                    wardsno: 0,
                    Tinno: "",
                    VatNo: "",
                    CoPerson: "",
                    Mail: txtEmail.value.trim(),//may be changed
                    Mobile_Number: txtMob.value.trim(),
                    dummy: check,
                    check_status: "",
                }
            }
            return data;
        }
        else if (hdnEmployee.value == "D") {
            var data = {
                employeeId: selectedRow[0][2],
                employeeName: selectedRow[0][3],
                jobId: 0,
                joined: selectedRow[0][5],
                salary: selectedRow[0][6],
                deptId: 0,
                active: 0,
                opType: hdnEmployee.value,
            }
            return data;
        }
    }

}
function getDistID(glob) {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "D") {
        var data = {
            sno: glob,
        }
        return data;
    }
}
function validateCust() {

    var txtCusName = document.getElementById('txtcusname');
    /*var txtPono = document.getElementById("txtpono");
    var txtAdd = document.getElementById("txtadd");
    var txtTinno = document.getElementById("txttinno");
    var txtVatno = document.getElementById("txtvatno");
    var txtCperson = document.getElementById("txtcperson");*/
    var txtEmail = document.getElementById("txtemail");
    var txtMob = document.getElementById("txtmob");
   /*  var ddlReg = document.getElementById("ddlreg");
    var ddlDist = document.getElementById("ddldist");
    var ddlWard = document.getElementById("ddlward");*/
    result = '';

    /*var chosen = "";
    var len = document.forms[0].checker.length;

    for (i = 0; i < len; i++) {
        if (document.forms[0].checker[i].checked) {
            chosen = document.forms[0].checker[i].value
        }
    }*/

    if (txtCusName.value.trim().length == 0) {
        result += 'Customer Name is Required.<br/>';
    }
    /*if (txtPono.value.trim().length == 0) {
        result += 'PostBox Number is Required.<br/>';
    }
    if (txtAdd.value.trim().length == 0) {
        result += 'Address is Required.<br/>';
    }
    if (ddlReg.value == 0) {
        result += 'Region is Required.<br/>';
    }
    if (ddlDist.value == 0) {
        result += 'District is Required.<br/>';
    }*/
    //if (ddlWard.value == 0) {
    //    result += 'Ward is Required.<br/>';
    //}
    /*if (txtTinno.value.trim().length == 0) {
        result += 'Tin Number is Required.<br/>';
    }*/
    //if (txtTinno.value.length>10) {
    //    result += 'Tin Number length should be 9 digits <br/>';
    //}
    /*if (txtVatno.value.trim().length == 0) {
        result += 'Vat No is Required.<br/>';
    }*/
    /*if (txtCperson.value.trim().length == 0) {
        result += 'Contact Person is Required.<br/>';
    }
    if (txtEmail.value.trim().length == 0) {
        result += 'Email is Required.<br/>';
    }*/
    if (txtMob.value.length == 0) {
        result += 'MobileNumber is Required.<br/>';
    }
    /*if (chosen == "") {
        result += 'Checker/Maker is Required.<br/>';
    }*/
    return result;

}
function resetCust() {
    var txtCusName = document.getElementById('txtcusname');
    /*var txtPono = document.getElementById("txtpono");
    var txtAdd = document.getElementById("txtadd");
    var txtTinno = document.getElementById("txttinno");
    var txtVatno = document.getElementById("txtvatno");
    var txtCperson = document.getElementById("txtcperson");*/
    var txtEmail = document.getElementById("txtemail");
    var txtMob = document.getElementById("txtmob");

    //ddlreg ddldist ddlward
    txtCusName.value = '';
    txtEmail.value = '';
    txtMob.value = '';
    //jQuery('#ddldist option').remove();
    //jQuery("#ddldist").append(jQuery("<option></option>").val(0).html('Select'));
    //jQuery('#ddlward option').remove();
    //jQuery("#ddlward").append(jQuery("<option></option>").val(0).html('Select'));
    jQuery("#tbl-smtp1 tbody").empty();
    
    jQuery("#lblError").hide();
    jQuery("#lblError2").hide();
    jQuery("#lblError4").hide();
}
function TinValiadte() {
    var Tin = jQuery('#txttinno').val();
     var validateTinNo = /^\d*(?:\.\d{1,2})?$/;
   
    if (validateTinNo.test(Tin) && Tin.length == 9) {
            lblError4.innerHTML = "";
            doubleTin = true;
    }
    else {
            doubleTin = false;
            //lblError.innerHTML = "";
            jQuery('#lblError4').show();
            error = "Invalid TIN Number";
            lblError4.innerHTML = "Invalid TIN Number";
            jQuery('#lblError4').css('color', 'Red');
     }
   
}
function ValidateEmail() {
    var email = jQuery('#txtemail').val();
    var reg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/;       
    if (reg.test(email)) {
        lblError2.innerHTML = "";
        doubleemail = true;
    }
    else {
        
        doubleemail = false;
        error = "Invalid Email";
        lblError2.innerHTML = "Invalid Email";
        jQuery('#lblError2').css('color', 'Red');
    }
    //binddupliacteDetailsEmail(email);
} 
function mobileNumDivisionValiadte() {
    var mobileNum = jQuery('#txtmob').val();
    var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    if (mobileNum.substr(0, 4) != 0000 || mobileNum == '') {
        if (validateMobNum.test(mobileNum) && mobileNum.length == 12) {
            lblError.innerHTML = "";
            doublemob = true;
        }
        else {
            doublemob = false;
            jQuery('#lblError').show();
            error = "Invalid Mobile Number";
            lblError.innerHTML = "Invalid Mobile Number.";
            jQuery('#lblError').css('color', 'Red');
        }
    }
    
}

function deleteCountGrid() {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected');
    table.row(selectedRow).remove().draw();
}
function getID(e) {
    var table = document.getElementById('tbl-smtp'),
        rows = table.rows,
        rowNumber = 0;
    for (var Index = 1, row = null; Index < rows.length; Index++) {
        row = rows[Index];
        rowNumber = Index - 1;
    }
}
