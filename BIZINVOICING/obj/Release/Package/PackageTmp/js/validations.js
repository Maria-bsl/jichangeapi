function alphaFewsym(val) {
  var alphanumers = /^[a-zA-Z-.',&0-9\s]+$/;
  if (alphanumers.test(val)) {
    return false;
  } else {
    return 'Only Alphabets and Numbers are allowed. Please enter valid data.<br/>';
  }
}

function alphaFewsymlatest(val) {
  var alphanumers = /^[a-zA-Z0-9 .&'-]+$/;
  if (alphanumers.test(val.value)) {
    return false;
  } else {
    return 'Only Alphabets and Numbers are allowed. Please enter valid data.<br/>';
  }
}
function isNumber(evt) {
  evt = evt ? evt : window.event;
  var charCode = evt.which ? evt.which : evt.keyCode;
  if (charCode > 31 && (charCode < 48 || charCode > 57)) {
    return false;
  }
  return true;
}

function alphaFewsymrole(val) {
  var alphanumers = /^[a-zA-Z-.'0-9\s]+$/;
  if (alphanumers.test(val)) {
    return false;
  } else {
    return 'Only Alphabets and Numbers are allowed. Please enter valid data.<br/>';
  }
}
function mobileNumValidation(v) {
  $('#' + v).keypress(function (e) {
    if (
      this.value.length == 12 ||
      (e.which > 57 && e.which < 65) ||
      (e.which > 90 && e.which < 97) ||
      e.which > 122 ||
      e.which < 48 ||
      e.which > 57
    ) {
      e.preventDefault();
    }
  });
}
function whitespacevalidation(v) {
  jQuery(document).ready(function () {
    $('#' + v).keydown(function (event) {
      if (event.keyCode == 32) {
        event.preventDefault();
      }
    });
  });
}
function alpha(val) {
  var alphanumers = /^[a-zA-Z0-9\s]+$/;
  if (alphanumers.test(val)) {
    return false;
  } else {
    return 'Only Alphabets and Numbers are allowed. Please enter valid data.<br/>';
  }
}
function notallowtyping(b) {
  var o = document.getElementById(b);
  o.addEventListener(
    'keydown',
    function (e) {
      if (e.keyCode >= 37 && e.keyCode <= 40) {
        return; // arrow keys
      }
      if (e.keyCode === 8 || e.keyCode === 46) {
        return; // backspace / delete
      }
      e.preventDefault();
    },
    false
  );
}
function validatePassword1(p) {
  var strongRegex = new RegExp(
    '^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})'
  );
  if (!strongRegex.test(p.value)) {
    error =
      'Password length must be between 8 to 14 characters, one digit, one special character, one lower case and one upper case.';
    message =
      'Password length must be between 8 to 14 characters, one digit, one special character, one lower case and one upper case.';
    notifyMessage(message, 'danger');
    pwd = false;
    return false;
  } else {
    pwd = true;
    return true;
  }
}

function decodeHTMLEntities(text) {
  var entities = [
    ['amp', '&'],
    ['apos', "'"],
    ['#x27', "'"],
    ['#x2F', '/'],
    ['#39', "'"],
    ['#47', '/'],
    ['lt', '<'],
    ['gt', '>'],
    ['nbsp', ' '],
    ['quot', '"'],
  ];
  for (var i = 0, max = entities.length; i < max; ++i)
    text = text.replace(
      new RegExp('&' + entities[i][0] + ';', 'g'),
      entities[i][1]
    );
  return text;
}
function ConvertJsonDateToDate(date) {
  var parsedDate = new Date(parseInt(date.substr(6)));
  var newDate = new Date(parsedDate);
  var month = ('0' + (newDate.getMonth() + 1)).slice(-2);
  var day = ('0' + newDate.getDate()).slice(-2);
  var year = newDate.getFullYear();
  return day + '/' + month + '/' + year;
}
function showModal(e) {
  jQuery('#' + e).modal({
    show: 'false',
    backdrop: 'static',
    keyboard: false,
  });
}
function hideModal(e) {
  jQuery('#' + e).modal('hide');
}
function notifyMessage(message, type) {
  jQuery.notify(
    {
      // options
      message: message,
    },
    {
      // settings
      type: type,
      delay: 2000,
    }
  );
}

function avoidEnter(e) {
  jQuery('#' + e).keypress(function (event) {
    if (event.which == 13) {
      event.preventDefault();
    }
  });
}
function applyDatatable(e) {
  jQuery('#' + e).dataTable({
    columnDefs: [{ bSortable: false, aTargets: [-1] }],
    //"columnDefs": [{ "orderable": false, "targets": -1, "bSortable": false} ],
    iDisplayLength: 10,
    aLengthMenu: [
      [5, 10, 25, 50, 100, -1],
      [5, 10, 25, 50, 100, 'All'],
    ],
    //"scrollY": "250px",
    scrollCollapse: true,
    responsive: true,
    //"oLanguage": { "sEmptyTable": "My Custom Message On Empty Table" },

    //"scrollX": true,
  });

  //jQuery('#' + e).DataTable().order(['asc']).draw();

  jQuery.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
}

function disablefuturedate(e) {
  var today, datepicker;
  today = new Date(
    new Date().getFullYear(),
    new Date().getMonth(),
    new Date().getDate()
  );
  $(document).ready(function () {
    $('#' + e).datepicker({
      uiLibrary: 'bootstrap4',
      format: 'dd/mm/yyyy',
      maxDate: today,
      iconsLibrary: 'fontawesome',
      weekStartDay: 1,
    });
  });
}
function disablepastdate(st, e) {
  jQuery('#' + e).datepicker('destroy');
  jQuery('#' + e).datepicker({
    uiLibrary: 'bootstrap4',
    minDate: st,
    format: 'dd/mm/yyyy',
    iconsLibrary: 'fontawesome',
  });
}

function Comma(Num) {
  Num += '';
  Num = Num.replace(',', '');
  Num = Num.replace(',', '');
  Num = Num.replace(',', '');
  Num = Num.replace(',', '');
  Num = Num.replace(',', '');
  Num = Num.replace(',', '');
  x = Num.split('.');
  x1 = x[0];
  x2 = x.length > 1 ? '.' + x[1] : '';
  var rgx = /(\d+)(\d{3})/;
  while (rgx.test(x1)) x1 = x1.replace(rgx, '$1' + ',' + '$2');
  return x1 + x2;
}
