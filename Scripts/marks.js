function saveData() {
    var arrMarks = [];
    $(Marks_table).find("tbody tr").each(function () {
        var item = {
            LessId: $(this).find("[name = LessId]").val(),
            StudentId: $(this).find("[name = StudentId]").val(),
            Presence: $(this).find("[name = Presence]").is(':checked'),
            Mark: $(this).find("[name = Mark]").val(),
            Comments: $(this).find("[name = Comments]").val(),
            Activity: $(this).find("[name = Activity]").is(':checked'),
        };
        arrMarks.push(item);
    })
    $(Marks).val(JSON.stringify(arrMarks));
}
function actRenew() {
    saveData();
    $(Action).val(0);
}
function actExec() {
    saveData();
    $(Action).val(1);
}
function actEdit() {
    saveData();
    $(Action).val(2);
}

function actCancel() {
    saveData();
    $(Action).val(3);
}

$(document).ready(function () {
    $(renew).click(actRenew);
    $(exec).click(actExec);
    $(edit).click(actEdit);
    $(btnCancel).click(actCancel);

});



