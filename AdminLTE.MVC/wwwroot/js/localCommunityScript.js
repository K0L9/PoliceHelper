function MarkToRed(event) {
    var i = event.target;
    var btn = i.closest("button");
    var row = btn.closest("td").closest("tr");
    row.setAttribute("class", "lcRow bg-danger");

    row.getElementsByClassName("btnOk")[0].hidden = false;
    row.getElementsByClassName("btnCancel")[0].hidden = false;

    row.getElementsByClassName("btnEdit")[0].hidden = true;
    btn.hidden = true;
}
function ClearField() {
    var inputTitle = document.getElementById("inputTitle");
    inputTitle.value = "";
}

function DenyRemove(event) {
    var i = event.target;
    var btn = i.closest("button");

    var row = btn.closest("td").closest("tr");

    row.getElementsByClassName("btnOk")[0].hidden = true;
    btn.hidden = true;

    row.setAttribute("class", "lcRow");

    row.getElementsByClassName("btnEdit")[0].hidden = false;
    row.getElementsByClassName("btnRemove")[0].hidden = false;
}

function Edit(event) {
    console.log("Before select mode to edit");
    console.log("After select mode to edit");
    var btn = event.target;
    //Get data
    var idOfEdit = btn.closest("td").getElementsByClassName("hiddenID")[0].value;
    console.log(idOfEdit)
    var titleOfEdit = btn.closest("td").closest("tr").getElementsByClassName("titleInput")[0].value;
    console.log(titleOfEdit)

    //write data to edit window
    var input = document.getElementById("inputTitle");
    var hiddenId = document.getElementById("hiddenId");

    input.value = titleOfEdit;
    hiddenId.value = idOfEdit;

    input.focus();
    input.select();

    //view
    document.getElementById("btnConfirmTitle").value = "Зберегти";
    document.getElementById("addTitle").innerHTML = "Редагування територіальної громади";

}

var btnsEdit = document.getElementsByClassName("btnEdit");
var btnsRemove = document.getElementsByClassName("btnRemove");

for (var i = 0; i < btnsEdit.length; i++) {
    btnsRemove[i].addEventListener("click", MarkToRed)
    btnsEdit[i].addEventListener("click", Edit);
}

var btnClear = document.getElementById("btnClearField");
btnClear.onclick = ClearField;

var btnsDeny = document.getElementsByClassName("btnCancel");

for (var i = 0; i < btnsDeny.length; i++) {
    btnsDeny[i].onclick = DenyRemove;
}

// $('#demo').pagination({
//    dataSource: [1, 2, 3, 4, 5, 6, 7, ... , 35],
//    pageSize: 5,
//    autoHidePrevious: true,
//    autoHideNext: true,
//    callback: function (data, pagination) {
//        // template method of yourself
//        var html = template(data);
//        dataContainer.html(html);
//    }
//}) 