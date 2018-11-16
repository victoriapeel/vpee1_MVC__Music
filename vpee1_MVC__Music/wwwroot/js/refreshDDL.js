function refreshDDL(ddl_ID, uri, addDefault, defaultText, fadeOutIn) {
    var theDDL = $("#" + ddl_ID);
    var selectedOption = theDDL.val();
    var URL = "/" + uri + "/" + selectedOption;
    $(function () {
        $.getJSON(URL, function (data) {
            if (data !== null && !jQuery.isEmptyObject(data)) {
                theDDL.empty();
                if (addDefault) {
                    theDDL.append($('<option/>', {
                        value: null,
                        text: defaultText
                    }));
                }
                $.each(data, function (index, item) {
                    theDDL.append($('<option/>', {
                        value: item.value,
                        text: item.text,
                        selected: item.selected
                    }));
                });
                theDDL.trigger("chosen:updated");
            }
        });
    });
    if (fadeOutIn) {
        theDDL.fadeToggle(400, function () {
            theDDL.fadeToggle(400);
        });
    }
    return;
}