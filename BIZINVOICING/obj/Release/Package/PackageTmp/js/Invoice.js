var doublemail;
var doublemob;

var cid, error;

function showDivInvoi() {
    jQuery("#btnSubmit").click(function () {
        jQuery(this).hide();
        jQuery("#loader").css("display", "block");
    }, 2000);
    //}
    //function resetchruInvoi() {
    //    debugger

    //    jQuery("#ddlcpny").dropdown('clear')
    //    jQuery("#ddlcid").dropdown('clear')
    //    jQuery("#ddlccy").dropdown('clear')
    //    jQuery("#ddlcpny").val(0).change();
    //    jQuery("#ddlcid").val(0).change();
    //    jQuery("#ddlccy").val(0).change();
    //    jQuery("#txtchname").val("");
    //    jQuery("#txtdate").val("");
    //    jQuery("#txtvat").val("");
    //    jQuery("#txtttvat").val("");
    //    jQuery("#txtinr").val("");

    //    jQuery("#txtwarrenty").val("");
    //    jQuery("#txtwarrenty").val("");
    //    jQuery("#txtDstatus").val("");





    //    jQuery("#tblCustomers  TBODY").children().not(":last").remove();
    //    var tRows = document.getElementById('tblCustomers');
    //    var rows = tRows.rows;
    //    for (var Index = 1, row = null; Index < rows.length; Index++) {
    //        row = rows[Index];
    //        row.cells[0].innerHTML = Index;
    //    }
    //}

    //function showchModal(e) {
    //    debugger;
    //    txtSID = document.getElementById('txtSID');
    //    var tblemployee = document.getElementById('tbl-smtp');
    //    rows = tblemployee.rows;
    //    hdnEmployee = document.getElementById('hdnEmployee');

    //    if (e.value.indexOf('Edit') > -1) {
    //        jQuery("#txtac").val('');
    //        jQuery("#dlstatus").val(0).change();
    //        for (var i = 0, row = null; i < rows.length; i++) {
    //            row = rows[i];

    //           // if (row.cells[7].innerHTML.indexOf(e.id) > -1) {
    //                txtSID.value = jQuery(e).data('sid');
    //                jQuery.ajax({
    //                    type: 'POST',
    //                    url: '@Url.Action("GetInvoiceDetailsbyid", "Invoice")',
    //                    cache: true,
    //                    data: JSON.stringify({ 'invid': jQuery(e).data('sid') }),
    //                    contentType: 'application/json; charset=utf-8',
    //                    dataType: 'json',
    //                    success: function (data) {

    //                        if (data != null) {

    //                            BindCompany();
    //                            BindCustomer();
    //                            BindCurrency();
    //                            BindVatPer();

    //                            $("#txtchname").val(data.invoice_no);


    //                            //bindCommentDetails(data.Church_Reg_Sno);
    //                            //bindregDetails(data.Country_Sno, data.Region_Id);
    //                            //txtregid.value = data.Region_Id;
    //                            ///// binddistDetails(data.Region_Id, data.District_Sno);
    //                            //txtDID.value = data.District_Sno;
    //                            ////bindwdDetails(data.Region_Id, data.District_Sno, data.Ward_Sno);
    //                            //txtWID.value = data.Ward_Sno;
    //                            //txtch.value = data.Chruch_Name;
    //                            //txtau.value = data.Authorised_Username;
    //                            //txtregno.value = data.Reg_No;
    //                            //jQuery("#ddlcouty").val(data.Country_Sno).change();
    //                            //txtphy.value = data.Physical_Address;
    //                            //txtweb.value = data.Church_Website;
    //                            //txtph.value = data.Phone_No;
    //                            //txtem.value = data.Email_Address;
    //                            //txtmo.value = data.Mobile_No;
    //                            ////dn.value = data.Branch_Sno;
    //                            //jQuery("#ddlbr").val(data.Branch_Sno).change();
    //                            //jQuery("#ddltype").val(data.Instype).change();
    //                            ////dltype.value = data.Instype;
    //                            //bindInsttypeDetails(data.Instype);
    //                            //txtunsr.value = data.UFullName;
    //                            //jQuery('#txtchname').prop("disabled", true);
    //                            //jQuery('#txtreno').prop("disabled", true);
    //                            //jQuery('#txtauname').prop("disabled", true);
    //                            //var flag = data.Final_Status;

    //                        }
    //                        jQuery("#loader").css("display", "none");
    //                        jQuery.ajax({
    //                            type: 'POST',
    //                            url: '@Url.Action("GetInvoiceInvoicedetails", "Invoice")',
    //                            data: JSON.stringify({ 'Sno': jQuery(e).data('sid') }),
    //                            contentType: 'application/json; charset=utf-8',
    //                            dataType: 'json',
    //                            cache: false,
    //                            success: function (data) {
    //                                var i = 1
    //                                //var dtcount = data.length;
    //                                jQuery("#tblCustomers  TBODY").children().not(":last").remove();
    //                                jQuery.each(data, function (key, value) {
    //                                    var tBody = jQuery("#tblchruch  TBODY")[0];
    //                                    var row = tBody.insertRow(0);
    //                                    var cell = jQuery(row.insertCell(0));
    //                                    var cell1 = jQuery(row.insertCell(1));
    //                                    var t1 = document.createElement("input");



    //                                    i++;
    //                                })
    //                                var tRows = document.getElementById('tblchruch');
    //                                var rows = tRows.rows;
    //                                for (var Index = 1, row = null; Index < rows.length; Index++) {
    //                                    row = rows[Index];
    //                                    row.cells[0].innerHTML = Index;
    //                                }
    //                                $('#tblchruch tr:last').show();
    //                            }
    //                        })
    //                    }
    //                })
    //            //}
    //        }
    //        hdnEmployee.value = 'U';
    //    }
    //    else if (e.value.indexOf('Add') > -1) {
    //        resetchruInvoi();
    //        txtcpny.value = 0;
    //        txtcid.value = 0;
    //        txtccy.value = 0;
    //        jQuery("#btnSubmit").show();
    //        jQuery("#btnSubmit").html("Save");
    //        hdnEmployee.value = 'C';
    //        jQuery("#loader").css("display", "none");
    //        var tRows = document.getElementById('tblchruch');
    //        var rows = tRows.rows;
    //        for (var Index = 1, row = null; Index < rows.length-1; Index++) {
    //            row = rows[Index];
    //            row.cells[0].innerHTML = Index;
    //        }
    //    }

    //}

    function getSMTPValuesInvoi() {

        var txtac = jQuery("#txtac").val();
        binddupliacteAccountDetails(txtac);
        var dlstatus = jQuery("#dlstatus").val();
        var table = "#tblchruch tr";
        var count = jQuery(table).not("thead tr").length;
        for (var i = 1; i <= count; i++) {

            var ac = jQuery(table).eq(i).find('td:eq(1) input').val();
            var st = jQuery(table).eq(i).find('td:eq(2) option:selected').val();
            for (var j = i + 1; j <= count; j++) {
                var ac1 = jQuery(table).eq(j).find('td:eq(1) input').val();
                var st1 = jQuery(table).eq(j).find('td:eq(2) option:selected').val();
                if (ac == ac1 && st == st1 && ac != '' && st1 != '') {
                    var m = Number(i) - 1;
                    var s = Number(j) - 1;
                    jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'Red');
                    jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'Red');
                    //datagrid = false;
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

        if (doubleacc == false) {
            jQuery("#loader").css("display", "none");
            message = "Account number already exists";
            type = 'danger';
            notifyMessage(message, type);
        }
        else if (doublecheck == false) {
            jQuery("#loader").css("display", "none");
            message = "Registration number already exists";
            type = 'danger';
            notifyMessage(message, type);
        }
        else if (doublemail == false) {
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
            message = error;
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
            var txtch = document.getElementById('txtchname');
            var txtau = document.getElementById('txtauname');
            var txtregno = document.getElementById('txtreno');
            var dg = document.getElementById('ddlreg');
            var dregvalue = dg.options[dg.selectedIndex].value;
            var dc = document.getElementById('ddlcouty');
            var dcvalue = dc.options[dc.selectedIndex].value;
            var dis = document.getElementById('ddldis');
            var disvalue = dis.options[dis.selectedIndex].value;
            var dw = document.getElementById('ddlwd');
            var dwvalue = dw.options[dw.selectedIndex].value;
            var txtphy = document.getElementById('txtphy');
            var txtweb = document.getElementById('txtweb');
            var txtph = document.getElementById('txtphone');
            var txtem = document.getElementById('txtemail');
            var txtmo = document.getElementById('txtmobile');
            var dn = document.getElementById('ddlbr');
            var bnvalue = dn.options[dn.selectedIndex].value;
            txtSID = document.getElementById('txtSID');
            rblGender = jQuery("input[name='gender']:checked");
            var txtunsr = document.getElementById('txtuname');
            var dltype = document.getElementById('ddltype');
            var btype = dltype.options[dltype.selectedIndex].value;
            var txtcmt = document.getElementById('txtcomt');

            hdnEmployee = document.getElementById('hdnEmployee');
            if (txtSID.value == '') {
                txtSID.value = '0';
            }
            //ValidateEmail();
            //mobileNumValiadte();
            var table = jQuery('#tbl-smtp').DataTable(),
                selectedRow = table.rows('.selected').data();
            var count = jQuery("#tblchruch tr").not("thead tr").length;

            var txtac = document.getElementById('txtac');
            var dn = document.getElementById('dlstatus');
            var st = dn.options[dn.selectedIndex].value;
            var orderArr = [];
            orderArr.length = 0;
            // jQuery.noConflict();
            jQuery.each(jQuery("#tblchruch tbody tr"), function ($) {
                orderArr.push({
                    Bank_Account_No: jQuery(this).find('td:eq(1) input').val(),
                    Status: jQuery(this).find('td:eq(2) option:selected').val(),
                });
            });

            if (hdnEmployee.value == "C") {
                txtSID.value = '0';
                var data = {
                    chname: txtch.value.trim(),
                    auname: txtau.value.trim(),
                    regno: txtregno.value.trim(),
                    regvalue: dregvalue,
                    cno: dcvalue,
                    distno: disvalue,
                    dwno: dwvalue,
                    phyad: txtphy.value.trim(),
                    web: txtweb.value.trim(),
                    phone: txtph.value.trim(),
                    email: txtem.value.trim(),
                    mobi: txtmo.value,
                    brach: bnvalue,
                    sno: txtSID.value,
                    gender: rblGender.val(),
                    type: btype,
                    uname: txtunsr.value.trim(),
                    lastrow: orderArr.length,
                    details: orderArr,
                    cmt: txtcmt.value.trim()
                }

                return data;
            }
            else if (hdnEmployee.value == "U") {

                var data = {
                    chname: txtch.value.trim(),
                    auname: txtau.value.trim(),
                    regno: txtregno.value.trim(),
                    regvalue: dregvalue,
                    cno: dcvalue,
                    distno: disvalue,
                    dwno: dwvalue,
                    phyad: txtphy.value.trim(),
                    web: txtweb.value.trim(),
                    phone: txtph.value.trim(),
                    email: txtem.value.trim(),
                    mobi: txtmo.value.trim(),
                    brach: bnvalue,
                    sno: txtSID.value,
                    gender: rblGender.val(),
                    type: btype,
                    uname: txtunsr.value.trim(),
                    lastrow: orderArr.length,
                    details: orderArr,
                    cmt: txtcmt.value.trim()
                }

                return data;
            }
        }
    }

    function validateEmployeechInvoi() {
        
        var txtch = document.getElementById('txtchname');
        var txtau = document.getElementById('txtdate');
        var dg = document.getElementById('ddlcpny');
        var dc = document.getElementById('ddlcid');
        var dis = document.getElementById('ddlccy');
        var txttwvat = document.getElementById('txttwvat');
        var txtvat = document.getElementById('txtvat');
        var txtttvat = document.getElementById('txtttvat');
        var count = jQuery("#tblchruch tr").not("thead tr").length;
        result = '';
        if (txtch.value.trim().length == 0) {
            result += 'Invoice No is Required.<br/>';
        }
        if (txtau.value.trin() == 0) {
            result += 'Invoice date is Required.<br/>';
        }
        if (dg.value == 0) {
            result += 'Company is Required.<br/>';
        }

        if (dc.value == 0) {
            result += 'Customers is Required.<br/>';
        }

        if (dis.value == 0) {
            result += 'Currency is Required.<br/>';
        }
        if (txttwvat.value.trim().length == 0) {
            result += 'Total Without Vat is Required.<br/>';
        }
        if (txtvat.value.trim().length == 0) {
            result += 'Vat Amount is Required.<br/>';
        }
        if (txtttvat.value.trim().length == 0) {
            result += 'Total Amount is Required.<br/>';
        }
        if (txtacc.value.trim().length == 0) {
            result += 'You need to map the account.<br/>';
        }
            //if (rt == true) {
            //    var dn = document.getElementById('dlstatus');
            //    var txtact = document.getElementById('txtac');
            //    if (txtact.value.trim().length == 0 && dn.value == 0) {
            //        result += 'Bank Account Details are required.<br/>';
            //    }
            //}
        return result;
    }
    
}