$(function () {

    var ajaxFormSubmit = function() {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-rka-target"));
            $target.replaceWith(data);
        });

        return false;
    };

    var updateSorting = function () {

        var options = {
            url: $("form").attr("action"),
            type: $("form").attr("method"),
            data: $("form").serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($("form").attr("data-rka-target"));
            $target.replaceWith(data);
        });
    }

    var getPage = function () {
        var $a = $(this);

        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function(data) {
            var target = $a.parents("div.pagedList").attr("data-rka-target");
            $(target).replaceWith(data);
        });
        return false;
    }

    var slideAccordian = function () {
        //slide up all the link lists
        $("#accordian ul ul").slideUp();
        //slide down the link list below the h3 clicked - only if its closed
        if (!$(this).next().is(":visible")) {
            $(this).next().slideDown();
        }
    }

    $("form[data-rka-ajax='true']").submit(ajaxFormSubmit);

    $(".main-content").on("click", ".sortInput", updateSorting);

    $(".main-content").on("click", ".pagedList a", getPage);

    $(".main-content").on("click", "#accordian h3", slideAccordian);

});

