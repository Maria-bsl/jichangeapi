var cid;
var check;
var doubleacc = true;
var doublemob;
var ak = false;
var doubleemail;
var doubleTin;

var jk = false;
function validCompany(e) {
    jk = true;
}
function validCo(e) {
    jk = false;
}
function validComp(e) {
    onkeyup = "TinnoValiadte(this);"    
    ak = true;
}
function showDiv() {
    jQuery("#btnSubmit").click(function () {
        jQuery(this).hide();
       // jQuery("#loader").css("display", "block");
    }, 2000);
}
function PagerClick(index) {
    document.getElementById("hfCurrentPageIndex").value = index;
    document.forms[0].submit();
}
function Removegrade(button) {
    var x = confirm("Are you sure you want to Delete?");
    if (x) {
        var tRows = document.getElementById('tblchruch');
        var row1 = jQuery(button).closest("TR");
        var name1 = jQuery("TD", row1).eq(0).html();
        var len1 = tRows.rows.length - 1;
        if (len1 == name1) {
            if (len1 != 1) {
                $('#tblchruch tr:last').hide();
                rt = false;
                dupicaterow = true;
               
                jQuery('#txtaccno').val('').change();
                
            }
        } else {
            if (len1 != 2) {
                var row2 = jQuery(button).closest("TR");
                var table2 = jQuery("#tblchruch")[0];
                table2.deleteRow(row2[0].rowIndex);
            }
        }
        var hd = $("#tblchruch tr:last").is(":visible")
        if (hd == false) {
            if (len1 == 3 || len1 == 2) {
                var tr = '<button type="button" id="btnAdd" class="btn btn-biz_logic btn-sm" value="Add" onclick="AddNewrowGrade(this)"><i class="icofont icofont-plus"></i>Add New Row</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="btn  btn-outline-dark btn-sm" onclick="Removegrade(this);return false;"><i class="icofont icofont-ui-delete"></i>Delete</button>';
            } else {
                var tr = '<button type="button" id="btnAdd" class="btn btn-biz_logic btn-sm" value="Add" onclick="AddNewrowGrade(this)"><i class="icofont icofont-plus"></i></i>Add New Row</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="btn  btn-outline-dark btn-sm" onclick="Removegrade(this);return false;"><i class="icofont icofont-ui-delete"></i>Delete</button>';
            }
        }
        $("#tblchruch").find("tr:last").prev().find("td:last").html(tr);
        var rows = tRows.rows;
        for (var Index = 1, row = null; Index < rows.length; Index++) {
            row = rows[Index];
            row.cells[0].innerHTML = Index;
            var d = Number(Index) - 1;
            jQuery('#tblchruch tbody tr:eq(' + d + ')').css('backgroundColor', 'White');
        }
    };
}
function resetSGD() {
    var txtCompName = document.getElementById('txtcompname');
    var txtPono = document.getElementById("txtpono");
    var txtAdd = document.getElementById("txtadd");
    var txtTinno = document.getElementById("txttinno");
    var txtVatno = document.getElementById("txtvatno");
    var txtDperson = document.getElementById("txtdname");
    var txtEmail = document.getElementById("txtemail");
    var txtTelno = document.getElementById("txttelno");
    var txtFaxno = document.getElementById("txtfaxno");
    var txtMob = document.getElementById("txtmob");
    var txtBankname = document.getElementById("txtbankname");
    var txtBankbranch = document.getElementById("txtbankbranch");
    var txtAcc = document.getElementById("txtaccno");
    var txtSwiftcode = document.getElementById("txtswiftcode");
    //txtbankname, txtbankbranch, txtaccno, txtswiftcode
    txtSID = document.getElementById('txtSID');
    //var ddlREG = document.getElementById("ddlreg");
    ////var regsno = ddlREG.options[ddlREG.selectedIndex].value,
    ////    ddlreg = ddlREG.options[ddlREG.selectedIndex].text,
    // var  ddlDIST = document.getElementById("ddldist");
    ////var distsno = ddlDIST.options[ddlDIST.selectedIndex].value,
    ////    ddldist = ddlDIST.options[ddlDIST.selectedIndex].text,
    //  var  ddlWARD = document.getElementById("ddlward");
    //var wardsno = ddlWARD.options[ddlWARD.selectedIndex].value,
    //    ddlward = ddlWARD.options[ddlWARD.selectedIndex].text,
    txtSID = document.getElementById('txtSID');
    txtSID = 0;
    txtCompName.value = '';
    txtPono.value = '';
    txtAdd.value = '';
    txtTinno.value = '';
    txtVatno.value = '';
    txtDperson.value = '';
    txtTelno.value = '';
    txtFaxno.value = '';
    //txtBankname.value = '';
    //txtBankbranch.value = '';
    //txtAcc.value = '';
    //txtSwiftcode.value = '';
    txtEmail.value = '';
    txtMob.value = '';
    //jQuery("#txtbankname").val('');
    //jQuery("#txtbankbranch").val('');
    jQuery("#txtaccno").val('');
    //jQuery("#txtswiftcode").val('');
    //jQuery('#txtcompname').prop("disabled", false);
    //jQuery('#txtpono').prop("disabled", false);
    //jQuery('#txtadd').prop("disabled", false);
    //jQuery('#txttinno').prop("disabled", false);
    //jQuery('#txtvatno').prop("disabled", false);
    //jQuery('#txtemail').prop("disabled", false);
    //jQuery('#txtdname').prop("disabled", false);
    //jQuery('#txttelno').prop("disabled", false);
    //jQuery('#txtfaxno').prop("disabled", false);
    //jQuery('#txtmob').prop("disabled", false);
    jQuery("#ddlward").val('');
    //jQuery("#ddlreg").val('');
    jQuery("#ddldist").val('');
    jQuery("#ddlreg").val(0).change();
    jQuery("#ddlbra").val(0).change();
  // jQuery("#ddldist").val(0).change();
   // jQuery("#ddlward").val(0).change();
   jQuery('#ddlreg').prop("disabled", false);
   jQuery('#ddldist').prop("disabled", false);
   jQuery('#ddlward').prop("disabled", false);
    jQuery("#tbl-smtp1 tbody").empty();
    //jQuery("#ddlcur").val(0).change();
    //jQuery("#txtamt").val('');
    jQuery("#lblError").hide();
    jQuery("#lblError2").hide();
    jQuery("#lblError4").hide();
    var tRows = document.getElementById('tblchruch');
    var rows = tRows.rows;
    for (var Index = 1, row = null; Index < rows.length; Index++) {
        row = rows[Index];
        row.cells[0].innerHTML = Index;
    }
   
    //jQuery('#txtdate').prop("disabled", false);
    jQuery("#tblchruch  TBODY").children().not(":last").remove();
}
function resetSGDLogin() {
    var txtCompName = document.getElementById('txtcompname');
    var txtMob = document.getElementById("txtmob");
    var txtBankname = document.getElementById("txtbankname");
    var txtBankbranch = document.getElementById("txtbankbranch");
    var txtAcc = document.getElementById("txtaccno");
    var txtSwiftcode = document.getElementById("txtswiftcode");
    //txtbankname, txtbankbranch, txtaccno, txtswiftcode
    txtSID = document.getElementById('txtSID');
    //var ddlREG = document.getElementById("ddlreg");
    ////var regsno = ddlREG.options[ddlREG.selectedIndex].value,
    ////    ddlreg = ddlREG.options[ddlREG.selectedIndex].text,
    // var  ddlDIST = document.getElementById("ddldist");
    ////var distsno = ddlDIST.options[ddlDIST.selectedIndex].value,
    ////    ddldist = ddlDIST.options[ddlDIST.selectedIndex].text,
    //  var  ddlWARD = document.getElementById("ddlward");
    //var wardsno = ddlWARD.options[ddlWARD.selectedIndex].value,
    //    ddlward = ddlWARD.options[ddlWARD.selectedIndex].text,
    txtSID = document.getElementById('txtSID');
    txtSID = 0;
    txtCompName.value = '';
    txtMob.value = '';
    txtAcc.value = '';
    //jQuery("#txtbankname").val('');
    //jQuery("#txtbankbranch").val('');
    
    //jQuery("#txtswiftcode").val('');
    //jQuery('#txtcompname').prop("disabled", false);
    //jQuery('#txtpono').prop("disabled", false);
    //jQuery('#txtadd').prop("disabled", false);
    //jQuery('#txttinno').prop("disabled", false);
    //jQuery('#txtvatno').prop("disabled", false);
    //jQuery('#txtemail').prop("disabled", false);
    //jQuery('#txtdname').prop("disabled", false);
    //jQuery('#txttelno').prop("disabled", false);
    //jQuery('#txtfaxno').prop("disabled", false);
    //jQuery('#txtmob').prop("disabled", false);
    
    jQuery("#ddlbra").val(0).change();
    // jQuery("#ddldist").val(0).change();
    // jQuery("#ddlward").val(0).change();
    jQuery("#tbl-smtp1 tbody").empty();
    //jQuery("#ddlcur").val(0).change();
    //jQuery("#txtamt").val('');
    jQuery("#lblError").hide();
    jQuery("#lblError2").hide();
    jQuery("#lblError4").hide();
    
}
function AddNewrowGrade(e) {
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        var accno = jQuery(table).eq(i).find('td:eq(1) input').val();
        for (var j = i + 1; j <= count; j++) {
            var accno1 = jQuery(table).eq(j).find('td:eq(1) input').val();
            if (accno == accno1) {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'Red');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'Red');
                message = 'Duplicate Row.Please Remove.';
                type = 'danger';
                notifyMessage(message, type);
                dupicaterow == false;
                return;
            }
            else {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'White');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'White');
            }
        }
    }
   if (dupicaterow == true) {
       if (doubleacc == true) {
           if (rt == false) {
               $('#tblchruch tr:last').show();
               var tr = '<button class="btn btn-outline-dark btn-sm" onclick="Removegrade(this);return false;"><i class="icofont icofont-ui-delete"></i>Delete</button>';
               $("#tblchruch").find("tr:last").prev().find("td:last").html(tr);
               rt = true;
           }
           else {

               var accno = jQuery("#txtaccno");
          
               if (accno.val() != 0 && accno.val() != '')
               {
                   var tBody = jQuery("#tblchruch > TBODY")[0];
                   var row = tBody.insertRow(0);
                   var cell = jQuery(row.insertCell(0));
                   /*var cell1 = jQuery(row.insertCell(1));
                   var t1 = document.createElement("input");
                   t1.id = "txtbankname1";
                   t1.value = bankname.val() == undefined ? '' : bankname.val();
                   t1.className = "form-control col-10";
                   cell1.html(t1);

                   var cell2 = jQuery(row.insertCell(2));
                   var t2 = document.createElement("input");
                   t2.id = "txtbankbranch1";
                   t2.value = bankbranch.val() == undefined ? '' : bankbranch.val();
                   t2.className = "form-control col-10";
                   cell2.html(t2);*/

                   var cell3 = jQuery(row.insertCell(1));
                   var t3 = document.createElement("input");
                   t3.id = "txtaccno1";
                   t3.value = accno.val() == undefined ? '' : accno.val();
                   t3.className = "form-control col-10";
                   cell3.html(t3);

                   //txtbankname, txtbankbranch, c, txtswiftcode
                   //txtbankname, txtbankbranch, txtaccno, txtswiftcode
                   cell5 = jQuery(row.insertCell(2));
                   var btnRemove = jQuery('<button class="btn btn-outline-dark btn-sm" onclick="Removegrade(this);return false;"><i class="icofont icofont-ui-delete"></i> Delete</button>');
                   cell5.append(btnRemove);

                   accno.val("");
                   //jQuery('#tblchruch tr td:nth-child(2)').hide();
                   //jQuery('#tblchruch tr td:nth-child(5)').hide();
                   var tRows = document.getElementById('tblchruch');
                   var rows = tRows.rows;
                   for (var Index = 1, row = null; Index < rows.length; Index++)
                   {
                       row = rows[Index];
                       row.cells[0].innerHTML = Index;
                   }
               }
               else {
                   message = 'Required Details are missing.';
                   type = 'danger';
                   notifyMessage(message, type);
               }
           }

       }
       else
       {
        message = 'Account Number already exits';
        type = 'danger';
        notifyMessage(message, type);
       }
   }
}
function getSMTPValuesgradedetails() {
   // binddupliacteAccountDetails(txtaccno);
  
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        
        //var bankname = jQuery(table).eq(i).find('td:eq(1) input').val();
        //var bankbranch = jQuery(table).eq(i).find('td:eq(2) input').val();
        var accno = jQuery(table).eq(i).find('td:eq(1) input').val();
        //var swiftcode = jQuery(table).eq(i).find('td:eq(4) input').val();
         //txtbankname, txtbankbranch, txtaccno, txtswiftcode
        for (var j = i + 1; j <= count; j++) {
            //var bankname1 = jQuery(table).eq(j).find('td:eq(1) input').val();
            //var bankbranch1 = jQuery(table).eq(j).find('td:eq(2) input').val();
            var accno1 = jQuery(table).eq(j).find('td:eq(1) input').val();
            //var swiftcode1 = jQuery(table).eq(j).find('td:eq(4) input').val();
            if (accno == accno1 ) {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'Red');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'Red');
                message = "Duplicate Row.Please Remove";
                type = 'danger';
                notifyMessage(message, type);
                return;
            }
            else {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'White');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'White');
            }
        }
    }
    
    if (doublemob == false || doubleemail == false || doubleTin == false) {
        jQuery("#loader").css("display", "none");
        //message = "Mobile No.should be 12 digits";
        //type = 'danger';
        //notifyMessage(message, type);
    }
    else {
        
        var txtCompName = document.getElementById('txtcompname');
        var txtPono = document.getElementById("txtpono");
        var txtAdd = document.getElementById("txtadd");
        var txtTinno = document.getElementById("txttinno");
        var txtVatno = document.getElementById("txtvatno");
        var txtDperson = document.getElementById("txtdname");
        var txtEmail = document.getElementById("txtemail");
        var txtTelno = document.getElementById("txttelno");
        var txtFaxno = document.getElementById("txtfaxno");
        var txtMob = document.getElementById("txtmob");
        var txtLogo = document.getElementById("imglogo");
        var txtSig = document.getElementById("txtsig");
        txtSID = document.getElementById('txtSID');
        var ddlREG = document.getElementById("ddlreg");
        var ddlBRA = document.getElementById("ddlbra");
        var regsno = ddlREG.options[ddlREG.selectedIndex].value,
            ddlreg = ddlREG.options[ddlREG.selectedIndex].text,
            rblcheck = jQuery("input[name='checker']:checked"),
            ddlDIST = document.getElementById("ddldist");
        var distsno = '0',//ddlDIST.options[ddlDIST.selectedIndex].value
            //ddldist = ddlDIST.options[ddlDIST.selectedIndex].text,
            ddlWARD = document.getElementById("ddlward");
        var wardsno = '0',//ddlWARD.options[ddlWARD.selectedIndex].value
         brasno = ddlBRA.options[ddlBRA.selectedIndex].value,
            //ddlward = ddlWARD.options[ddlWARD.selectedIndex].text,
            //tblemployee = document.getElementById('tbl-smtp'),
            //rows = tblemployee.rows,
            txtSID = document.getElementById('txtSID');
            hdnEmployee = document.getElementById('hdnEmployee');

        if (txtSID.value == '') {
            txtSID.value = '0';
        }
        if (jQuery("#ddldist option").length != '0') {
            distsno = ddlDIST.options[ddlDIST.selectedIndex].value;
        }
        if (jQuery("#ddlward option").length != '0') {
            wardsno = ddlWARD.options[ddlWARD.selectedIndex].value;
        }
        
        //var table = jQuery('#tbl-smtp').DataTable(),
        //    selectedRow = table.rows('.selected').data();
        var count = jQuery("#tblchruch tr").not("thead tr").length;
        //var dn = document.getElementById('txttype');
        //var st = dn.options[dn.selectedIndex].value;
        var orderArr = [];
        orderArr.length = 0;
        jQuery.each(jQuery("#tblchruch tbody tr"), function ($) {
            orderArr.push({
                //BankName: jQuery(this).find('td:eq(1) input').val(),
                //BankBranch: jQuery(this).find('td:eq(2) input').val(),
                AccountNo: jQuery(this).find('td:eq(1) input').val(),//.val()
                //Swiftcode: jQuery(this).find('td:eq(4) input').val(),
                //Currency_Sno: jQuery(this).find('td:eq(5) option:selected').val(),
                //Term_Amount: jQuery(this).find('td:eq(6) input').val().replace(',', ''),
            });
        });
        check = true;
        if (hdnEmployee.value == "C") {
            txtSID.value = '0';
            jk = false;
            var data = {
                compsno: txtSID.value,
                compname: txtCompName.value.trim(),
                pbox: txtPono.value.trim(),
                addr: txtAdd.value.trim(),
                rsno: regsno,
                dsno: distsno,
                wsno: wardsno,
                tin: txtTinno.value.trim(),
                vat: txtVatno.value.trim(),
                dname: txtDperson.value.trim(),
                email: txtEmail.value.trim(),
                telno: txtTelno.value.trim(),
                fax: txtFaxno.value.trim(),
                mob: txtMob.value.trim(),
                lastrow: orderArr.length,
                details: orderArr,
                dummy: check,
                branch: brasno,
                check_status: rblcheck.val()
            }
            return data;
        }
        else if (hdnEmployee.value == "U") {
            
            var table = jQuery('#tbl-smtp').DataTable();
            var rows = table.rows({ page: 'all' }).nodes();
            /*for (var i = 0; i < rows.length; i++) {
                var tin = rows[i].cells[10].innerHTML;
                var newtin = txtTinno.value.trim();
                var name = rows[i].cells[1].innerHTML.toLowerCase().trim();
                var newname = txtCompName.value.toLowerCase().trim();

                if (name == newname && jk == true || tin == newtin && ak == true) {
                    check = false;
                }
                else { }
            }*/
            
            if (check == true) {
                var data = {
                    compsno: txtSID.value,
                    compname: txtCompName.value.trim(),
                    pbox: txtPono.value.trim(),
                    addr: txtAdd.value.trim(),
                    rsno: regsno,
                    dsno: distsno,
                    wsno: wardsno,
                    tin: txtTinno.value.trim(),
                    vat: txtVatno.value.trim(),
                    dname: txtDperson.value.trim(),
                    email: txtEmail.value.trim(),
                    telno: txtTelno.value.trim(),
                    fax: txtFaxno.value.trim(),
                    mob: txtMob.value.trim(),
                    lastrow: orderArr.length,
                    details: orderArr,
                    dummy: check,
                    branch: brasno,
                    check_status: rblcheck.val()
                }
            }
            else {
                var data = {
                    compsno: txtSID.value,
                    compname: txtCompName.value.trim(),
                    pbox: txtPono.value.trim(),
                    addr: txtAdd.value.trim(),
                    rsno: regsno,
                    dsno: distsno,
                    wsno: wardsno,
                    tin: txtTinno.value.trim(),
                    vat: txtVatno.value.trim(),
                    dname: txtDperson.value.trim(),
                    email: txtEmail.value.trim(),
                    telno: txtTelno.value.trim(),
                    fax: txtFaxno.value.trim(),
                    mob: txtMob.value.trim(),
                    lastrow: orderArr.length,
                    details: orderArr,
                    dummy: check,
                    branch: brasno,
                    check_status: rblcheck.val()
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
function getSMTPValuesgradedetailsLogin() {
    // binddupliacteAccountDetails(txtaccno);

    

    if (doublemob == false || doubleemail == false || doubleTin == false) {
        jQuery("#loader").css("display", "none");
        //message = "Mobile No.should be 12 digits";
        //type = 'danger';
        //notifyMessage(message, type);
    }
    else {

        var txtCompName = document.getElementById('txtcompname');
        var txtMob = document.getElementById("txtmob");
        var accno = document.getElementById("txtaccno");
        var txtLogo = document.getElementById("imglogo");
        var txtSig = document.getElementById("txtsig");
        txtSID = document.getElementById('txtSID');
        var ddlBRA = document.getElementById("ddlbra");
        var regsno = '0';
        var distsno = '0';
        var wardsno = '0',
            brasno = ddlBRA.options[ddlBRA.selectedIndex].value,
            rblcheck = jQuery("input[name='checker']:checked"),
            //ddlward = ddlWARD.options[ddlWARD.selectedIndex].text,
            //tblemployee = document.getElementById('tbl-smtp'),
            //rows = tblemployee.rows,
            txtSID = document.getElementById('txtSID');
        hdnEmployee = document.getElementById('hdnEmployee');

        if (txtSID.value == '') {
            txtSID.value = '0';
        }
        
        
        check = true;
        if (hdnEmployee.value == "C") {
            txtSID.value = '0';
            jk = false;
            var data = {
                compsno: txtSID.value,
                compname: txtCompName.value.trim(),
                pbox: '',
                addr: '',
                rsno: regsno,
                dsno: distsno,
                wsno: wardsno,
                tin: '',
                vat: '',
                dname: '',
                email: '',
                telno: '',
                fax: '',
                mob: txtMob.value.trim(),
                accno: accno.value.trim(),
                dummy: check,
                branch: brasno,
                check_status: rblcheck.val()
            }
            return data;
        }
        else if (hdnEmployee.value == "U") {

            var table = jQuery('#tbl-smtp').DataTable();
            var rows = table.rows({ page: 'all' }).nodes();
            /*for (var i = 0; i < rows.length; i++) {
                var tin = rows[i].cells[10].innerHTML;
                var newtin = txtTinno.value.trim();
                var name = rows[i].cells[1].innerHTML.toLowerCase().trim();
                var newname = txtCompName.value.toLowerCase().trim();

                if (name == newname && jk == true || tin == newtin && ak == true) {
                    check = false;
                }
                else { }
            }*/

            if (check == true) {
                var data = {
                    compsno: txtSID.value,
                    compname: txtCompName.value.trim(),
                    pbox: '',
                    addr: '',
                    rsno: regsno,
                    dsno: distsno,
                    wsno: wardsno,
                    tin: '',
                    vat: '',
                    dname: '',
                    email: '',
                    telno: '',
                    fax: '',
                    mob: txtMob.value.trim(),
                    lastrow: orderArr.length,
                    details: orderArr,
                    dummy: check,
                    branch: brasno,
                    check_status: rblcheck.val()
                }
            }
            else {
                var data = {
                    compsno: txtSID.value,
                    compname: txtCompName.value.trim(),
                    pbox: '',
                    addr: '',
                    rsno: regsno,
                    dsno: distsno,
                    wsno: wardsno,
                    tin: '',
                    vat: '',
                    dname: '',
                    email: '',
                    telno: '',
                    fax: '',
                    mob: txtMob.value.trim(),
                    lastrow: orderArr.length,
                    details: orderArr,
                    dummy: check,
                    branch: brasno,
                    check_status: rblcheck.val()
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
function getControl() {
        var txtcontrol = document.getElementById('txtcontrol');
            var data = {
                control: txtcontrol.value.trim()
            }
            return data;
       
   
}
function TinnoValiadte() {
    var Tin = jQuery('#txttinno').val();
    //var Tinno;
    //var Tin = document.getElementById('#txttinno');
    //Tin.value = Tinno;
    var validateTinNo = /^\d*(?:\.\d{1,2})?$/;
    //if (Tin == '') {
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
    // }
    //else {
    //    doublemob = false;
    //   error = "Invalid Mobile Number.";
    //    lblError.innerHTML = "Invalid Mobile Number.";
    //    jQuery('#lblError').css('color', 'Red');
    //}
}
function ValidateEmail() {
    var email = jQuery('#txtemail').val();
    // var email = document.getElementById('#txtemail');
    var reg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/;

    if (reg.test(email)) {
        lblError2.innerHTML = "";
        doubleemail = true;
    }
    else {
        //doublemail = false;
        //lblError2.innerHTML = "Invalid Email.";
        //jQuery('#lblError2').css('color', 'Red');
        doubleemail = false;
        //lblError.innerHTML = "";
        jQuery('#lblError2').show();
        error = "Invalid Email";
        lblError2.innerHTML = "Invalid Email";
        jQuery('#lblError2').css('color', 'Red');
    }
    //binddupliacteDetailsEmail(email);
}
function mobileValiadte() {
    var mobileNum = jQuery('#txtmob').val();
    var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    if (mobileNum.substr(0, 4) != 0000 || mobileNum == '') {
        if (validateMobNum.test(mobileNum) && mobileNum.length == 12) {
            lblError.innerHTML = "";
            doublemob = true;
        }
        else {
            doublemob = false;
            //lblError.innerHTML = "";
            jQuery('#lblError').show();
            error = "Invalid Mobile Number";
            lblError.innerHTML = "Invalid Mobile Number.";
            jQuery('#lblError').css('color', 'Red');
        }
    }
    //else {
    //    doublemob = false;
    //   error = "Invalid Mobile Number.";
    //    lblError.innerHTML = "Invalid Mobile Number.";
    //    jQuery('#lblError').css('color', 'Red');
    //}
}

function validateCompanyBank() {
    //alert("test");
    //debugger
    var txtCompName = document.getElementById('txtcompname');
    var txtPono = document.getElementById("txtpono");
    var txtAdd = document.getElementById("txtadd");
    var txtTinno = document.getElementById("txttinno");
    var txtVatno = document.getElementById("txtvatno");
    var txtDperson = document.getElementById("txtdname");
    var txtEmail = document.getElementById("txtemail");
    var txtTelno = document.getElementById("txttelno");
    var txtFaxno = document.getElementById("txtfaxno");
    var txtMob = document.getElementById("txtmob");
    //var bankname = document.getElementById("txtbankname");
    //var bankbranch = document.getElementById("txtbankbranch");
    //var accno = document.getElementById("txtaccno");
    //var swiftcode = document.getElementById("txtswiftcode");
    //txtbankname, txtbankbranch, txtaccno, txtswiftcode
    txtSID = document.getElementById('txtSID');
    var ddlREG = document.getElementById("ddlreg");
    //var regsno = ddlREG.options[ddlREG.selectedIndex].value,
    //    ddlreg = ddlREG.options[ddlREG.selectedIndex].text,
      var  ddlDIST = document.getElementById("ddldist");
    //var distsno = ddlDIST.options[ddlDIST.selectedIndex].value,
    //    ddldist = ddlDIST.options[ddlDIST.selectedIndex].text,
    var ddlWARD = document.getElementById("ddlward");
    var ddlbra = document.getElementById("ddlbra");
    //var wardsno = ddlWARD.options[ddlWARD.selectedIndex].value,
    //    ddlward = ddlWARD.options[ddlWARD.selectedIndex].text,
    result = '';
    var chosen = "";
    var len = document.forms[0].checker.length;

    for (i = 0; i < len; i++) {
        if (document.forms[0].checker[i].checked) {
            chosen = document.forms[0].checker[i].value
        }
    }
    if (txtCompName.value.trim().length == 0) {
        result += 'Vendor Name is Required .<br/>';
    }
    
    /*if (txtPono.value.trim().length == 0) {//
        result += 'PostBox No is Required .<br/>';
    }
    if (txtAdd.value.trim().length == 0) {
        result += 'Address is Required .<br/>';
    }
    if (ddlREG.value == 0) {
        result += 'Region is Required<br/>';
    }*/
    //debugger
    //if (ddlDIST.value == 0) {
    //    result += 'District is Required<br/>';
    //}
    //if (ddlWARD.value == 0) {
    //    result += 'Ward is Required<br/>';
    //}
    /*if (txtTinno.value.trim().length == 0) {
        result += 'Tin No is Required .<br/>';
    }
    if (txtVatno.value.trim().length == 0) {
        result += 'Vat No is Required .<br/>';
    }
    if (txtDperson.value.trim().length == 0) {
        result += 'Director Name is Required .<br/>';
    }
    if (txtEmail.value.trim().length == 0) {
        result += 'Email is Required .<br/>';
    }
    if (txtTelno.value.trim().length == 0) {
        result += 'Telephone No is Required .<br/>';
    }*/
    //if (txtFaxno.value.trim().length == 0) {
    //    result += 'Fax No is Required .<br/>';
    //}
    if (txtMob.value.trim().length == 0) {
        result += 'Mobile No is Required .<br/>';
    }
    if (ddlbra.value == 0) {
        result += 'Branch is Required<br/>';
    }
    if (chosen == "") {
        result += 'Checker/Maker is Required.<br/>';
    }
    /*var bankname = jQuery("#txtbankname1").val();
    var bankbranch = jQuery("#txtbankbranch1").val();*/
    var accno = jQuery("#txtaccno1").val();
    //var swiftcode = jQuery("#txtswiftcode1").val();
    var hd = $("#tblchruch tr:last").is(":visible");
    if (hd == true) {
        if (accno == '') {
            result += 'Required Details are missing.';
        }
    }
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        //var bn = jQuery(table).eq(i).find('td:eq(1) input').val();
        //var bb = jQuery(table).eq(i).find('td:eq(2) input').val();
        var ac = jQuery(table).eq(i).find('td:eq(1) input').val();
        //var sc= jQuery(table).eq(i).find('td:eq(4) input').val();
        if (ac == '') {
          result += 'Required Details are missing.';
        }
    }
    return result;
}
function validateCompanyBankLogin() {
    //alert("test");
    //debugger
    var txtCompName = document.getElementById('txtcompname');
    var txtMob = document.getElementById("txtmob");
    txtSID = document.getElementById('txtSID');
    var ddlbra = document.getElementById("ddlbra");
    var accno = document.getElementById("txtaccno");
    //var wardsno = ddlWARD.options[ddlWARD.selectedIndex].value,
    //    ddlward = ddlWARD.options[ddlWARD.selectedIndex].text,
    result = '';
    var chosen = "";
    var len = document.forms[0].checker.length;

    for (i = 0; i < len; i++) {
        if (document.forms[0].checker[i].checked) {
            chosen = document.forms[0].checker[i].value
        }
    }
    if (txtCompName.value.trim().length == 0) {
        result += 'Vendor Name is Required .<br/>';
    }

    if (txtMob.value.trim().length == 0) {
        result += 'Mobile No is Required .<br/>';
    }
    if (ddlbra.value == 0) {
        result += 'Branch is Required<br/>';
    }
    if (chosen == "") {
        result += 'Checker/Maker is Required.<br/>';
    }
    if (accno.value.trim().length == 0) {
        result += 'Account No is Required .<br/>';
    }
    /*var bankname = jQuery("#txtbankname1").val();
    var bankbranch = jQuery("#txtbankbranch1").val();*/
    
    //var swiftcode = jQuery("#txtswiftcode1").val();
    
    return result;
}
function validateControl() {
    //alert("test");
    //debugger
    //var lblcn = jQuery('#lblcn').text();
    var txtcontrol = document.getElementById('txtcontrol');
    //alert(lblcn);
    result = '';
    if (txtcontrol.value.trim().length == 0) {
        result += 'Control No. is Required .<br/>';
    }
   
    return result;
}
function getSMTPID(glob) {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "D") {
        var data = {
            sno: glob,
        }
        return data;
    }
}
function deleteSMTPGrid() {
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
        rowNumber = Index - 1
    }
}