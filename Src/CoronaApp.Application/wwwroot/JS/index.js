const helloTitle = document.createElement('header');
helloTitle.innerText = 'Epidemiology Report';
document.body.appendChild(helloTitle);

let locationsList = [];
let searchLocationsList = [];
let listNamesForColumns = ["Id", "city", "location", "startDate", "endDate"];
var obj, dbParam, xmlhttp, myObj, x, txt = "", currentList;
const buttonSearch = document.getElementById('Search-City');
const sortDate = document.getElementById("sortDate");
const inputSearch = document.getElementById('nameOfCity');
const buttonSearchDate = document.getElementById("SearchDate");
const BASICURL = "https://localhost:44381/api/";
const refresh = document.getElementById('refresh');
let inputStartDate = document.getElementById("StartDate");
let inputEndDate = document.getElementById("EndDate");
let valueOfInput = document.getElementById('nameOfCity');
var searchLocation = {
    City: String,
    StartDate: Date,
    EndDate: Date
}
obj = { table: "demo", limit: 20 };
dbParam = JSON.stringify(obj);

load();
function load() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            xhttp.response;
            locationsList = xhttp.response;
            myObj = JSON.parse(this.responseText);
            createTable(myObj);
        }
    };
    xhttp.open("GET", "api/Location/GetAllList", true);
    xhttp.send();
}

//function styleTable(table) {
//    table.style.textAlign = "center";
//    table.setAttribute("id", "table");
//    table.style.width = '50%';
//    table.setAttribute('border', '1');
//    table.style.borderColor = "red";
//    table.setAttribute('cellspacing', '0');
//    table.setAttribute('cellpadding', '5');
//}
function fillCol(arryEX, bRow, k) {
    var index = 0;
    for (var item2 of arryEX) {
        if (index !== k) {
            if (index === 0) {
                item2 = arryEX[5];
            }
            index++;
            var td = document.createElement("td");
            td.style.width = "50px";
            if (index === 5 || index == 4) {
                item2 = item2.substring(0, 10);
            }
            td.innerHTML = item2;
            bRow.appendChild(td);
        }
    }
}

function CreateRows(tBody) {
    for (var c = 0; c < myObj.length; c++) {
        currentRowValues = myObj[c];
        var k = Object.keys(currentRowValues).length - 1;
        var arryEX = Object.values(currentRowValues);
        let bRow = document.createElement("tr");
        fillCol(arryEX, bRow,k);
        tBody.appendChild(bRow);
    }
}

function createTable(locationsList) {
    if (locationsList.length > 0) {
        var b = document.getElementsByTagName("body")[0];
        var table = document.getElementById("table");
        var tBody = document.createElement("tbody");
        tBody.setAttribute("id", "tBodyId");
        CreateRows(tBody)
        table.appendChild(tBody);
        b.appendChild(table);
    }
}

function delTable() {
    let lenghtTable = document.getElementById("table").rows.length;
    for (var i = 0, x = 2; i < lenghtTable - 2; i++) {
        document.getElementById("table").deleteRow(x);
    }
}
function cutDate() {
    if (inputStartDate.value != "" && inputEndDate.value != "") {
        inputStartDate.value = inputStartDate.value.substring(0, 10);
        inputEndDate.value = inputEndDate.value.substring(0, 10);
        return `?locationSearch.StartDate=${inputStartDate.value}&locationSearch.EndDate=${ inputEndDate.value}`;
    }
}
buttonSearchDate.addEventListener("click", function () {
    delTable();
    searchLocation.StartDate = inputStartDate.value;
    searchLocation.EndDate = inputEndDate.value;
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            xhttp.response;
            searchLocationsList = xhttp.response;
            searchLocationsList = JSON.parse(this.responseText);
            myObj = searchLocationsList;
            createTable(searchLocationsList);
        }
    }
    let valuesURL = cutDate();
    xhttp.open("GET", "api/Location/Get" + valuesURL, true);
    xhttp.send();
});

refresh.addEventListener('click', function () {
    let lenghtTable = document.getElementById("table").rows.length;
    for (var i = 0, x = 2; i < lenghtTable - 2; i++) {
        document.getElementById("table").deleteRow(x);
    }
    myObj = JSON.parse(locationsList);
    createTable(myObj);
});
    buttonSearch.addEventListener('click', function () {
        searchLocation.City = valueOfInput.value;
        document.getElementById('nameOfCity').value = "";
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                let lenghtTable = document.getElementById("table").rows.length;
                for (var i = 0, x = 2; i < lenghtTable - 2; i++) {
                    document.getElementById("table").deleteRow(x);
                }
                searchLocationsList = "";
                searchLocationsList = JSON.parse(this.responseText);
                myObj = searchLocationsList;
                createTable(searchLocationsList);
            }
        }
        if (searchLocation.City != "") {
            searchLocation.City = "?locationSearch.City=" + searchLocation.City;
        }
        xhttp.open("GET", "api/Location/Get" + searchLocation.City, true);
        xhttp.send();
});

sortDate.addEventListener("click", function () {
    var tbody = document.getElementById("tBodyId");
    var rows = [].slice.call(tbody.querySelectorAll("tr"));
    rows.sort((a, b) => a.cells[3].innerHTML.localeCompare(b.cells.innerHTML));
    rows.forEach(function (v) {
        tbody.appendChild(v); // note that .appendChild() *moves* elements
    });
})


