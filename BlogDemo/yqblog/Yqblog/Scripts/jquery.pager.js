(function ($) {
    $.fn.pager = function (options) {
        var opts = $.extend({}, $.fn.pager.defaults, options);
        return this.each(function () {
            if (options.endPoint == undefined) {
                options.endPoint = 5;
            }
            if (options.thisPage == undefined) {
                options.thisPage = "true";
            }
            if (options.showEnd == undefined) {
                options.showEnd = "true";
            }
            if (options.showNext == undefined) {
                options.showNext = "true";
            }
            if (parseInt(options.pagecount) > 1) {
                $(this).empty().append(renderpager(parseInt(options.pagenumber), parseInt(options.pagecount), parseInt(options.endPoint), options.showNext, options.showEnd, options.thisPage, options.buttonClickCallback));
            }
            $(".pages li").mouseover(function () {
                document.body.style.cursor = "pointer";
            }).mouseout(function () {
                document.body.style.cursor = "auto";
            });
        });
    };

    var pageIndex = 0;
    function renderpager(pagenumber, pagecount, endPoint, showNext, showEnd, thisPage, buttonClickCallback) {
        pageIndex = 0;
        var $pager = $("<ul></ul>");
        var startPoint = 1;
        var temp = 2;
        switch (endPoint) {
            case 3:
                temp = 1;
                break;
            case 5:
                temp = 2;
                break;
            case 7:
                temp = 3;
                break;
            case 9:
                temp = 4;
                break;
            case 11:
                temp = 5;
                break;
            case 13:
                temp = 6;
                break;
            case 15:
                temp = 7;
                break;
        }
        if (pagenumber > temp) {
            startPoint = pagenumber - temp;
            endPoint = pagenumber + temp;
        }
        if (endPoint > pagecount) {
            startPoint = pagecount - (temp * 2);
            endPoint = pagecount;
        }
        if (startPoint < 1) {
            startPoint = 1;
        }
        if (showNext == "true") {
            $pager.append(renderButton(WebLang.Prev, pagenumber, pagecount, buttonClickCallback, startPoint, endPoint));
        }
        if (showEnd == "true") {
            $pager.append(renderButton("1...", pagenumber, pagecount, buttonClickCallback, startPoint, endPoint));
        }
        for (var page = startPoint; page <= endPoint; page++) {
            pageIndex++;
            var classstr = pageIndex == 1 ? " class=\"first\"" : "";
            var currentButton = $("<li " + classstr + "><a href=\"javascript:;\">" + (page) + "</a></li>");
            page == pagenumber ? currentButton.addClass("active") : currentButton.click(function () {
                buttonClickCallback($(this).find('a').text(), $(this));
            });
            currentButton.appendTo($pager);
        }
        if (showEnd == "true") {
            $pager.append(renderButton("..." + pagecount, pagenumber, pagecount, buttonClickCallback, startPoint, endPoint));
        }
        if (showNext == "true") {
            $pager.append(renderButton(WebLang.Next, pagenumber, pagecount, buttonClickCallback, startPoint, endPoint));
        }
        if (thisPage != "false") {
            var span = $("<li class='pageCount'>" + pagenumber + "/" + pagecount + "</li>");
            $pager.append(span);
        }
        return $pager;
    }

    function renderButton(buttonLabel, pagenumber, pagecount, buttonClickCallback, startPoint, endPoint) {
        pageIndex++;
        var $Button = $("<li><a href=\"javascript:;\">" + buttonLabel + "</a></li>");
        var destPage = 1;

        if (buttonLabel.indexOf('...') != -1) {
            destPage = buttonLabel.replace("...", "");
            if (destPage == startPoint || destPage == endPoint) {
                $Button.addClass("hide");
                pageIndex--;
            }
            if (destPage == startPoint - 1 || destPage == endPoint + 1)
            { $Button.html("<a href=\"javascript:;\">" + destPage + "</a>"); }
        }
        else {
            switch (buttonLabel) {
                case WebLang.First:
                    destPage = 1;
                    break;
                case WebLang.Prev:
                    destPage = pagenumber - 1;
                    break;
                case WebLang.Next:
                    destPage = pagenumber + 1;
                    break;
                case WebLang.Last:
                    destPage = pagecount;
                    break;
            }
        }
        if (buttonLabel == "1..." || buttonLabel == WebLang.First || buttonLabel == WebLang.Prev) {

            if (pagenumber <= 1) {
                $Button.addClass("hide");
                pageIndex--;
            }
            else {
                $Button.click(function () {
                    buttonClickCallback(destPage, $Button);
                });
            }

        } else {

            if (pagenumber >= pagecount) {
                $Button.addClass("hide");
                pageIndex--;
            }
            else {
                $Button.click(function () {
                    buttonClickCallback(destPage, $Button);
                });
            }
        }
        pageIndex = pageIndex < 0 ? 0 : pageIndex;
        return $Button;
    }
    $.fn.pager.defaults = { pagenumber: 1, pagecount: 1, endPoint: 9 };
})(jQuery);


