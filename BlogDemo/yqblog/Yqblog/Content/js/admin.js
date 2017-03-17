var urlPath = Urls.RootPath;
$(function () {
    $('body').on('click', ".calendaricon", function () {
        var index = $('.calendaricon').index($(this));
        $($('.hasDatepicker').get(index)).trigger('focus');
    });

    $(":text,:password,select,:checkbox").on('change', function () {
        $('.field-validation-error').hide();
    });


    $('.btn_cancel').on('click', function () {
        history.go(-1);
    });

    $('.btn_help').on('click', function () {
        var content = $("#" + "content_" + WebLang.Lang + "_" + $(this).attr("id")).html();
        ModalView("ModalHelp", "Help", content);
    });

    $('#ModalHelp .modal-yes').click(function () {
        $('#ModalHelp .modal').modal('hide');
    });
});

function Go(url) {
    window.location.href = url;
}

function ReplaceKESpace(str) {
    var re = /(<p>(\s|\s*&nbsp;\s*|\s*<br\s*\/?\s*>\s*)*<\/p>)+/ig;
    var re2 = /((\s|\s*&nbsp;\s*|\s*<br\s*\/?\s*>\s*)*)+/ig;
    var newstr = str;
    if (newstr.replace(re, "").replace(re2, "") == "") {
        newstr = "";
    }
    else {
        newstr = newstr.replace(re, "<br/>");
    }
    return newstr;
}

function GetCurrentDate() {
    var date = new Date();
    var now = '';
    now = date.getFullYear() + "-";
    now = now + (date.getMonth() + 1) + "-";
    now = now + date.getDate();
    return now;
}

function ModalView(id, title, content) {
    var tar = $('#' + id).find('.modal');
    tar.find('.modal-body-title').html(title);
    tar.find('.modal-body-content').html(content);
    tar.modal({
        backdrop: true
    });
}

function ScrollToTop(el) {
    $('html, body').animate({ scrollTop: $(el).offset().top - 50 }, 'slow');
}
