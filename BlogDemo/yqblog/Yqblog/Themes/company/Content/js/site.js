var urlPath = Urls.RootPath + (WebLang.Lang == Config.DefaultLang ? "" : WebLang.Lang + "/");
var loadingImgSmall = "<img class=\"loadingImgSmall\" src=\"" + Urls.LoadingImgSmall + "\">";
$(function () {
    $.scrollUp({ scrollText: '' });

    $('body').on('click', ".calendaricon", function () {
        var index = $('.calendaricon').index($(this));
        $($('.hasDatepicker').get(index)).trigger('focus');
    });

    /*web start*/
    $.get(urlPath + "account/ajaxlogoninfo/", { rn: Math.random() }, function (re) {
        $("#logonAndLangInfo").html(re);
    });

    $('ul.mainNav li').hover(function () {
        $(this).find("ul").first().show();
        $(this).addClass("menuhover");
    }, function () {
        $(this).children("ul").hide();
        $(this).removeClass("menuhover");
    });

    $('body').on('mouseenter', ".operationHoverWrap", function () {
        $(this).find(".operationHover").first().css("visibility", "visible");
    });

    $('body').on('mouseleave', ".operationHoverWrap", function () {
        $(this).find(".operationHover").first().css("visibility", "hidden");
    });

    $('body').on('focus', ":text,:password,select,:checkbox,textarea", function () {
        var tar = $(this);
        tar.removeClass("form_error");
        TipTarget(tar).hide();
        $('.summary_tip_wrap').hide();
    });

    $('body').on('click', ".validateImg", function () {
        var url = $(this).attr("src");
        $(this).attr("src", url + "&rn=" + Math.random());
    });

    $("#searchBtn").click(function () {
        if ($.trim($("#searchKey").val()) != "") {
            window.location.href = urlPath + "search/" + $.trim($("#searchKey").val()).replace(/\./g, "");
        }
    });

    $(document).keypress(function (event) {
        if (event.keyCode == 13 && $('#searchKey').is(':focus')) {
            $("#searchBtn").trigger('click');
        }
    });
    /*web end*/

    /*logon and register start*/
    $('#BtnLogon').on('click', function () {
        var form = $('#loginForm');
        var tar = $('#UserName');
        var valid = true;
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.UserNameCanNotBeEmpty).show();
            valid = false;
        }
        tar = $('#Password');
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.PasswordCanNotBeEmpty).show();
            valid = false;
        }
        var loadingWrap = $(this).closest(".loadingwrap");
        if (valid) {
            loadingWrap.append(loadingImgSmall);
            $.ajax({
                url: urlPath + "account/logon/",
                type: "post",
                data: form.serialize(),
                success: function (re) {
                    loadingWrap.find('.loadingImgSmall').remove();
                    if (re == "") {
                        var url = ($('#returnUrl').val() == "" || $('#returnUrl').val().indexOf("account") > -1) ? Urls.RootPath : $('#returnUrl').val();
                        Go(url);
                    } else {
                        form.find('.summary_tip').html(re);
                        form.find('.summary_tip_wrap').show();
                    }
                }
            });
        }
    });

    $('#BtnRegister').on('click', function () {
        var form = $('#registerForm');
        var tar = $('#UserName');
        var valid = true;
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.UserNameCanNotBeEmpty).show();
            valid = false;
        }
        tar = $('#Password');
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.PasswordCanNotBeEmpty).show();
            valid = false;
        }
        if ($.trim(tar.val()) != "" && $.trim(tar.val()).length < 6) {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.PasswordLengthLimit).show();
            valid = false;
        }
        tar = $('#ConfirmPassword');
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.ConfirmPasswordCanNotBeEmpty).show();
            valid = false;
        }
        if ($.trim(tar.val()) != "" && $.trim(tar.val()) != $.trim($('#Password').val())) {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.ConfirmPasswordNotEqual).show();
            valid = false;
        }
        var loadingWrap = $(this).closest(".loadingwrap");
        if (valid) {
            loadingWrap.append(loadingImgSmall);
            $.ajax({
                url: urlPath + "account/register/",
                type: "post",
                data: form.serialize(),
                success: function (re) {
                    loadingWrap.find('.loadingImgSmall').hide();
                    if (re == "") {
                        var url = ($('#returnUrl').val() == "" || $('#returnUrl').val().indexOf("account") > -1) ? Urls.RootPath : $('#returnUrl').val();
                        Go(url);
                    } else {
                        form.find('.summary_tip').html(re);
                        form.find('.summary_tip_wrap').show();
                    }
                }
            });
        }
    });

    $('#BtnUserProfile').on('click', function () {
        var isSendEmail = $("input[name='IsSendEmail']:checked").val();
        var emailInput = $('#Email');

        if (isSendEmail == "true") {
            if ($.trim(emailInput.val()) == "") {
                emailInput.siblings(".form_tip").html(lang.EmailCanNotBeEmpty).show();
                return false;
            }
            if (!isEmail($.trim(emailInput.val()))) {
                emailInput.siblings(".form_tip").html(lang.EmailFormatInvalid).show();
                return false;
            }
        }

        if ($.trim(emailInput.val()) != "") {
            if (!isEmail($.trim(emailInput.val()))) {
                emailInput.siblings(".form_tip").html(lang.EmailFormatInvalid).show();
                return false;
            }
        }

        var form = $('#userProfileForm');
        var loadingWrap = $(this).closest(".loadingwrap");
        loadingWrap.append(loadingImgSmall);
        $.ajax({
            url: urlPath + "account/userprofile/",
            type: "post",
            data: form.serialize(),
            success: function (re) {
                loadingWrap.find('.loadingImgSmall').remove();
                if (re == "") {
                    Go(urlPath + "account/ucenter/");
                } else {
                    form.find('.summary_tip').html(re);
                    form.find('.summary_tip_wrap').show();
                }
            }
        });
    });

    $('#BtnChangePassword').on('click', function () {
        var form = $('#changePasswordForm');
        var valid = true;
        var tar = $('#OldPassword');
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.OldPasswordCanNotEmpty).show();
            valid = false;
        }
        tar = $('#NewPassword');
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.NewPasswordCanNotEmpty).show();
            valid = false;
        }
        if ($.trim(tar.val()) != "" && $.trim(tar.val()).length < 6) {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.PasswordLengthLimit).show();
            valid = false;
        }
        tar = $('#ConfirmPassword');
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.ConfirmPasswordCanNotBeEmpty).show();
            valid = false;
        }
        if ($.trim(tar.val()) != $.trim($('#NewPassword').val())) {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.ConfirmPasswordNotEqual).show();
            valid = false;
        }
        var loadingWrap = $(this).closest(".loadingwrap");
        if (valid) {
            loadingWrap.append(loadingImgSmall);
            $.ajax({
                url: urlPath + "account/changepassword/",
                type: "post",
                data: form.serialize(),
                success: function (re) {
                    loadingWrap.find('.loadingImgSmall').remove();
                    if (re == "") {
                        $('#changePasswordForm').hide();
                        $('#changePasswordSuccess').show();
                    } else {
                        form.find('.summary_tip').html(re);
                        form.find('.summary_tip_wrap').show();
                    }
                }
            });
        }
    });

    $('#backToChangePassword').on('click', function () {
        $('#changePasswordSuccess').hide();
        $('#OldPassword').val('');
        $('#NewPassword').val('');
        $('#ConfirmPassword').val('');
        $('#changePasswordForm').show();
    });
    /*logon and register end*/


    /*comment start*/
    $('.article_comment').on('click', "#BtnCommentSubmit", function () {
        var form = $('#CommentForm');
        var tar = $('#UserName');
        var valid = true;
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.UserNameCanNotBeEmpty).show();
            valid = false;
        }
        tar = $('#Content');
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.ContentCanNotEmpty).show();
            valid = false;
        }
        if (valid) {
            $(this).attr("disabled", "disabled");
            $(this).closest(".loadingwrap").append(loadingImgSmall);
            $.ajax({
                url: urlPath + "home/ajaxcomment/",
                type: "post",
                data: form.serialize(),
                dataType: 'json',
                success: function (re) {
                    $('#BtnCommentSubmit').removeAttr("disabled");
                    $('#BtnCommentSubmit').closest(".loadingwrap").find('.loadingImgSmall').remove();
                    if (re["error"] == '') {
                        if ($('#ValidationCode')[0]) {
                            $('#ValidationCode').val('');
                            $(".validateImg").trigger('click');
                        }
                        InitializeCommentForm();
                        GetCommentListByOrderId(re["value"]);
                    } else if (re["error"] == 'error.validationcode') {
                        $('.validateCodeTip').show();
                    }
                }
            });
        }
    });


    $('.article_comment').on('click', ".item-reply", function () {
        var user = $('#UserName').val();
        if ($('#Email').length > 0) {
            var email = $('#Email').val();
            var web = $('#Url').val();
        }
        ClearCommentForm();
        $('#ParentId').val($(this).parent().attr("itemId"));
        var form = $('#commentform').html();
        if (form == "") {
            form = $('#formbox').html();
            $('#formbox').remove();
        } else {
            $('#commentform').html("").hide();
        }
        var box = $(this).closest('.floor-box');
        if (box.find("#formbox").length == 0) {
            box.append("<div class=\"box-form\" id=\"formbox\">" + form + "</div>");
            $('#UserName').val(user);
            if ($('#Email').length > 0) {
                $('#Email').val(email);
                $('#Url').val(web);
            }
            $('.btn-reply-cancel').show();
            GoA('commentFormA');
            $('#Content').focus();
        }
    });

    $('.article_comment').on('click', ".item-edit", function () {
        InitializeCommentForm();
        GoA('commentFormA');
        $('#CommentId').val($(this).parent().attr("itemId"));
        $('.form-remark').html('Edit:<a class=\"current-item\" order=\"' + $(this).parent().attr("order") + '\">#' + $(this).parent().attr("order") + '</a>');
        $('#Content').val('loading...');
        $('.btn-edit-cancel').show();
        $.ajax({
            url: urlPath + "home/getonecomment/",
            type: "post",
            data: { id: $(this).parent().attr("itemId"), rn: Math.random() },
            dataType: 'json',
            success: function (re) {
                $('#UserId').val(re["UserId"]);
                $('#UserName').val(re["UserName"]);
                $('#Email').val(re["Email"]);
                $('#Url').val(re["Url"]);
                $('#Content').val(re["Content"]).focus();
            }
        });
    });

    $('.article_comment').on('click', ".btn-reply-cancel", function () {
        InitializeCommentForm();
    });

    $('.article_comment').on('click', ".btn-edit-cancel", function () {
        InitializeCommentForm();
    });

    $('.article_comment').on('click', ".item-delete", function () {
        $('#current-comment').val($(this).parent().attr("itemId"));
    });

    $('#comment-delete-yes').on('click', function () {
        InitializeCommentForm();
        $('#modalbackdroptrue').modal('hide');
        $('#' + $('#current-comment').val()).siblings('.comment_loading').show();
        $.post(urlPath + "home/deletecomment/", { id: $('#current-comment').val(), orderType: $('#CommentOrderType').val(), rn: Math.random() }, function (re) {
            $("#commentlist").html(re);
        });
    });

    $('.article_comment').on('click', ".current-item", function () {
        GoA('item' + $(this).attr('order'));
    });
    /*comment end*/


    /*note start*/
    $('.note').on('click', "#BtnNoteSubmit", function () {
        var form = $('#NoteForm');
        var tar = $('#UserName');
        var valid = true;
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.UserNameCanNotBeEmpty).show();
            valid = false;
        }
        tar = $('#Content');
        if ($.trim(tar.val()) == "") {
            tar.addClass("form_error");
            TipTarget(tar).html(lang.ContentCanNotEmpty).show();
            valid = false;
        }
        if (valid) {
            $(this).attr("disabled", "disabled");
            $(this).closest(".loadingwrap").append(loadingImgSmall);
            $.ajax({
                url: urlPath + "home/ajaxnote/",
                type: "post",
                data: form.serialize(),
                dataType: 'json',
                success: function (re) {
                    $('#BtnNoteSubmit').removeAttr("disabled");
                    $('#BtnNoteSubmit').closest(".loadingwrap").find('.loadingImgSmall').remove();
                    if (re["error"] == '') {
                        if ($('#ValidationCode')[0]) {
                            $('#ValidationCode').val('');
                            $(".validateImg").trigger('click');
                        }
                        InitializeNoteForm();
                        GetNoteListByOrderId(re["value"]);
                    } else if (re["error"] == 'error.validationcode') {
                        $('.validateCodeTip').show();
                    }
                }
            });
        }
    });

    $('.note').on('click', ".item-reply", function () {
        var user = $('#UserName').val();
        if ($('#Email').length > 0) {
            var email = $('#Email').val();
            var web = $('#Url').val();
        }
        ClearNoteForm();
        $('#ParentId').val($(this).parent().attr("itemId"));
        var form = $('#noteform').html();
        if (form == "") {
            form = $('#formbox').html();
            $('#formbox').remove();
        } else {
            $('#noteform').html("").hide();
        }
        var box = $(this).closest('.floor-box');
        if (box.find("#formbox").length == 0) {
            box.append("<div class=\"box-form\" id=\"formbox\">" + form + "</div>");
            $('#UserName').val(user);
            if ($('#Email').length > 0) {
                $('#Email').val(email);
                $('#Url').val(web);
            }
            $('.btn-reply-cancel').show();
            GoA('noteFormA');
            $('#Content').focus();
        }
    });

    $('.note').on('click', ".item-edit", function () {
        InitializeNoteForm();
        GoA('noteFormA');
        $('#NoteId').val($(this).parent().attr("itemId"));
        $('.form-remark').html('Edit:<a class=\"current-item\" order=\"' + $(this).parent().attr("order") + '\">#' + $(this).parent().attr("order") + '</a>');
        $('#Content').val('loading...');
        $('.btn-edit-cancel').show();
        $.ajax({
            url: urlPath + "home/getonenote/",
            type: "post",
            data: { id: $(this).parent().attr("itemId"), rn: Math.random() },
            dataType: 'json',
            success: function (re) {
                $('#UserId').val(re["UserId"]);
                $('#UserName').val(re["UserName"]);
                $('#Email').val(re["Email"]);
                $('#Url').val(re["Url"]);
                $('#Content').val(re["Content"]).focus();
            }
        });
    });

    $('.note').on('click', ".btn-reply-cancel", function () {
        InitializeNoteForm();
    });

    $('.note').on('click', ".btn-edit-cancel", function () {
        InitializeNoteForm();
    });

    $('.note').on('click', ".item-delete", function () {
        $('#current-note').val($(this).parent().attr("itemId"));
        var tar = $('#modalbackdroptrue');
        tar.modal({
            backdrop: true
        });
    });

    $('#note-delete-yes').on('click', function () {
        InitializeNoteForm();
        $('#modalbackdroptrue').modal('hide');
        $('#' + $('#current-note').val()).siblings('.note_loading').show();
        $.post(urlPath + "home/deletenote/", { id: $('#current-note').val(), orderType: $('#NoteOrderType').val(), rn: Math.random() }, function (re) {
            $("#notelist").html(re);
        });
    });

    $('.note').on('click', ".current-item", function () {
        GoA('item' + $(this).attr('order'));
    });
    /*note end*/

    /*vote start*/
    $('body').on('click', ".vote_favor", function () {
        var voteitem = $(this).closest('.vote');
        var voteid = voteitem.attr("voteid");
        $.ajax({
            url: urlPath + "home/AjaxVote/",
            type: "post",
            data: { id: voteid, vote: 1 },
            dataType: 'json',
            success: function (re) {
                voteitem.find(".vote_tip").html(re["message"]).show();
                setTimeout(function () { voteitem.find(".vote_tip").fadeOut(); }, 1000);
                if (re["error"] == '') {
                    voteitem.find(".vote_favor_value").text(re["value"]);
                }
            }
        });
    });

    $('body').on('click', ".vote_against", function () {
        var voteitem = $(this).closest('.vote');
        var voteid = voteitem.attr("voteid");
        $.ajax({
            url: urlPath + "home/AjaxVote/",
            type: "post",
            data: { id: voteid, vote: 0 },
            dataType: 'json',
            success: function (re) {
                voteitem.find(".vote_tip").html(re["message"]).show();
                setTimeout(function () { voteitem.find(".vote_tip").fadeOut(); }, 1000);
                if (re["error"] == '') {
                    voteitem.find(".vote_against_value").text(re["value"]);
                }
            }
        });
    });
    /*vote end*/

    /*articlelist start*/
    $('body').on('click', "#setArticleOrder", function () {
        var currentOrder = $('#ArticleListOrder').val();
        var order = "asc";
        if (currentOrder == "asc") {
            order = "desc";
        }
        $('#ArticleListOrder').val(order);
        ArticlePageClick(1, $(this));
    });

    $('body').on('click', "#articleViewAll", function () {
        $('#ArticleRecommend').val(0);
        ArticlePageClick(1, $(this));
    });

    $('body').on('click', "#articleViewCommended", function () {
        $('#ArticleRecommend').val(1);
        ArticlePageClick(1, $(this));
    });
    /*articlelist end*/

    /*album start*/
    $('.albumview-switchtolist').on('click', function () {
        $('.photo-carousel-wrap').hide();
        $('.album-photolist').html($('.carousel-inner').html());
        $('.album-photolist').show();
        $('.album-photolist div').removeClass('carousel-caption');
        //$('.album-switch').find('.icon-th-list').addClass('icon-white');
        //$('.album-switch').find('.icon-th-large').removeClass('icon-white');
    });

    $('.albumview-switchtoalbum').on('click', function () {
        $('.album-photolist').hide();
        $('.photo-carousel-wrap').show();
        //$('.album-switch').find('.icon-th-large').addClass('icon-white');
        //$('.album-switch').find('.icon-th-list').removeClass('icon-white');
    });
    /*album end*/


});

/*fun_comment start*/

function GetArticleAjaxInfo() {
    var orderid = getQueryString("f") == "" ? 0 : getQueryString("f");
    $.get(urlPath + "home/articleajaxinfo/", { id: $('#ArticleId').val(), orderId: orderid, orderType: $('#CommentOrderType').val(), rn: Math.random() }, function(re) {
        $("#commentlist").html(re);
        if (orderid > 0) {
            GoA('item' + orderid);
        } else {
            if (window.location.href.indexOf("#commentA") > -1) {
                GoA('commentA');
            }
        }
    });
}

function GetComments(pageno, tar) {
    if (typeof (tar) != "undefined")
    { tar.closest(".loadingwrap").append(loadingImgSmall); }
    $.get(urlPath + "home/CommentList/", { id: $('#ArticleId').val(), orderType: $('#CommentOrderType').val(), p: pageno, rn: Math.random() }, function(re) {
        $("#commentlist").html(re);
    });
}

function GetCommentListByOrderId(orderid) {
    $.get(urlPath + "home/GetCommentListByOrderId/", { id: $('#ArticleId').val(), orderType: $('#CommentOrderType').val(), orderid: orderid, rn: Math.random() }, function(re) {
        $("#commentlist").html(re);
        GoA('item' + orderid);
    });
}

function InitializeCommentForm() {
    var user = $('#UserName').val();
    if ($('#Email').length > 0) {
        var email = $('#Email').val();
        var web = $('#Url').val();
    }
    if ($('#commentform').html() == "") {
        var form = $('#formbox').html();
        $('#formbox').remove();
        $('#commentform').html(form);
        $('#UserName').val(user);
        if ($('#Email').length > 0) {
            $('#Email').val(email);
            $('#Url').val(web);
        }
    }
    ClearCommentForm();
    $('#commentform').show();
}

function ClearCommentForm() {
    $('#ParentId').val(0);
    $('#CommentId').val(0);
    $('.form-remark').html('');
    $('#Content').val('');
    $('.btn-reply-cancel').hide();
    $('.btn-edit-cancel').hide();
}

function GetCommentForm() {
    $.get(urlPath + "home/ajaxcommentform/", { rn: Math.random() }, function (re) {
        $("#commentFormWrap").html(re);
        $('#CommentArticleId').val($('#ArticleId').val());
    });
}
/*fun_comment end*/

/*fun_note start*/

function GetNotes(pageno,tar ) {
    if (typeof (tar) != "undefined")
    { tar.closest(".loadingwrap").append(loadingImgSmall); }
    $.get(urlPath + "home/NoteList/", { id: $('#CategoryId').val(), orderType: $('#NoteOrderType').val(), p: pageno, rn: Math.random() }, function(re) {
        $("#notelist").html(re);
    });
}

function GetNoteListByOrderId(orderid) {
    $.get(urlPath + "home/GetNoteListByOrderId/", { id: $('#CategoryId').val(), orderType: $('#NoteOrderType').val(), orderid: orderid, rn: Math.random() }, function(re) {
        $("#notelist").html(re);
        GoA('item' + orderid);
    });
}

function InitializeNoteForm() {
    var user = $('#UserName').val();
    if ($('#Email').length > 0) {
        var email = $('#Email').val();
        var web = $('#Url').val();
    }
    if ($('#noteform').html() == "") {
        var form = $('#formbox').html();
        $('#formbox').remove();
        $('#noteform').html(form);
        $('#UserName').val(user);
        if ($('#Email').length > 0) {
            $('#Email').val(email);
            $('#Url').val(web);
        }
    }
    ClearNoteForm();
    $('#noteform').show();
}

function ClearNoteForm() {
    $('#ParentId').val(0);
    $('#NoteId').val(0);
    $('.form-remark').html('');
    $('#Content').val('');
    $('.btn-reply-cancel').hide();
    $('.btn-edit-cancel').hide();
}

/*fun_note end*/

/*fun_userprofile start*/

function GetUserComment(pageno) {
    $.get(urlPath + "account/usercommentlist/", { p: pageno, rn: Math.random() }, function(re) {
        $("#usercomment").html(re);
    });
}

function GetUserNote(pageno) {
    $.get(urlPath + "account/usernotelist/", { p: pageno, rn: Math.random() }, function(re) {
        $("#usernote").html(re);
    });
}

/*fun_userprofile end*/

/*fun_common start*/

function Go(url) {
    window.location.href = url;
}

function GoA(location) {
    window.location.href = window.location.href.split('#')[0] + '#' + location;
    ScrollToTop($("a[name='" + location + "']"));
}

function ScrollToTop(el) {
    $('html, body').animate({ scrollTop: $(el).offset().top - 50 }, 'slow');
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}

function ArticlePageClick(no, tar) {
    if (Config.IsPagingAjax=="True") {
        AjaxGetArticleList(no, tar);
    } else {
        GetArticlePage(no); 
    }
}

function GetArticlePage(no) {
    var paracommend = "";
    var paraorder = "";
    if ($('#ArticleRecommend').val() == 1) {
        paracommend = "commend=1";
    }
    if ($('#ArticleListOrder').val() == "asc") {
        paraorder = "order=asc";
    }
    var url = $('#PagerRootUrl').val();
    if (no > 1) {
        url = $('#PagerRootUrl').val() + ($('#ArticleListType').val() == "index" ? "/" + no : "?p=" + no);
    }
    if (paracommend != "") {
        if (url.indexOf("?") > -1) {
            url = url + "&" + paracommend;
        } else {
            url = url + "?" + paracommend;
        }
    }
    if (paraorder != "") {
        if (url.indexOf("?") > -1) {
            url = url + "&" + paraorder;
        } else {
            url = url + "?" + paraorder;
        }
    }
    Go(url);
}

function AjaxGetArticleList(no, tar) {
    var paras = {
        "ArticleListType": $('#ArticleListType').val(),
        "PageId": no,
        "CategoryId": $('#CategoryId').val(),
        "AuthorName": $('#AuthorName').val(),
        "Tag": $('#Tag').val(),
        "SearchKey": $('#SearchKey').val(),
        "Year": $('#Year').val(),
        "Month": $('#Month').val(),
        "Day": $('#Day').val(),
        "Commend": $('#ArticleRecommend').val(),
        "Order": $('#ArticleListOrder').val()
    };
    var jsonParas = JSON.stringify(paras);
    tar.closest(".loadingwrap").prepend(loadingImgSmall);
    $.post(urlPath + "home/ajaxgetarticlelist/", { data: jsonParas, rn: Math.random() }, function(re) {
        $('#articleListWrap').html(re);
    });
}


function JudgeSingularOrPlural(count, singularKey, pluralKey) {
    return count > 1 ? pluralKey : singularKey;
}

function isEmail(str) {
    var reg = /^([\.a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
    return reg.test(str);
}

function TipTarget(obj) {
    return obj.closest(".input-wrap").find('.form_tip');
}
/*fun_common end*/
