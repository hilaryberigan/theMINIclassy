

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



