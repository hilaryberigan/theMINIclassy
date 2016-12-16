

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

function readLogs(fileName, logText) {
    document.getElementById('logFileName').style.display = 'none';
    document.getElementById('showLog').style.display = '';

    var logArray = logText.split('@');
    for(var item in logArray){
        $('#showLog').append(item);
    }
}

