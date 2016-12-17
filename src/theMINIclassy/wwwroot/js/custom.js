

function renderPFQuantityTable(PFQantityList,FabricsList) {
    alert("MADE IT TO CUSTOM.JS");
    var html;

    for(var item in PFQantityList){
        html += item;
    }

    
    $('#PFQuant').append(html);
}

function addFabric() {
    var html;
}

function GetTitle(id, list) {
    for (var item in list) {
        if (item.Id === id) {
            return item;
        }
    }
    return id;
}
function startLogPage() {
    document.getElementById('logFileName').style.display = '';
    [].forEach.call(document.querySelectorAll('.showContent'), function (el) {
        el.style.visibility = 'hidden';
    });
    //document.getElementById('showContent').style.display = 'none';

    //document.getElementsByClassName('fileContent').style.display = 'none';
}
function hideElement(hide) {
    alert("hiding " + hide);
    document.getElementById(hide).style.display = 'none';
}
function readLogs(show) {
    document.getElementById('logFileName').style.display = 'none';
    document.getElementById(show).style.display = '';
    document.getElementById(show).style.visibility = '';
    document.getElementById(show).style.position = 'absolute';
    document.getElementById(show).style.top = '200px';
    [].forEach.call(document.querySelectorAll('.fileButtons'), function (el) {
        el.style.visibility = 'hidden';
    });
}

