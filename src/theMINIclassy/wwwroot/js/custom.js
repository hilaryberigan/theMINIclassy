

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
    document.getElementById('showLog').style.display = 'none';
}
function readLogs() {
    document.getElementById('logFileName').style.display = 'none';
    document.getElementById('showLog').style.display = '';
}

