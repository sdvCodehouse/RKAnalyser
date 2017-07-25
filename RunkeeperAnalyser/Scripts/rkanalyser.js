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

    $("form[data-rka-ajax='true']").submit(ajaxFormSubmit);

    $(".main-content").on("click", ".pagedList a", getPage);
});