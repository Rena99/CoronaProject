
const helloTitle = document.createElement('header');
helloTitle.innerText = 'Epidemiology Report';
document.body.appendChild(helloTitle);

let locationsList = [];

let searchLocationsList = [];

let listNamesForColumns = ["Id", "city", "location", "startDate", "endDate"];

var obj, dbParam, xmlhttp, myObj, x, txt = "", currentList;
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

function styleTable(table) {
    table.style.textAlign = "center";
    table.setAttribute("id", "table");
    table.style.width = '50%';
    table.setAttribute('border', '1');
    table.style.borderColor = "red";
    table.setAttribute('cellspacing', '0');
    table.setAttribute('cellpadding', '5');
}
//Build the table according to the variables entered
function createTable(locationsList) {
    if (locationsList.length > 0) {
        var b = document.getElementsByTagName("body")[0];
        var table = document.getElementById("table");
        var col = [];
        var index = 0;
        var tBody = document.createElement("tbody");
        tBody.setAttribute("id", "tBodyId")
        for (var c = 0; c < myObj.length; c++) {

            currentRowValues = myObj[c];
            var k = Object.keys(currentRowValues).length-1;
            var arryEX = Object.values(currentRowValues);
            let bRow = document.createElement("tr");
            for (var item2 of arryEX) {
                if (index!==k) {
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
            index = 0;
            tBody.appendChild(bRow);
        }
    }
    table.appendChild(tBody);
    b.appendChild(table);
}
var searchLocation = {
    City: String,
    StartDate: Date,
    EndDate: Date

}
const buttonSearch = document.getElementById('Search-City');
const sortDate = document.getElementById("sortDate");
const inputSearch = document.getElementById('nameOfCity');
const buttonSearchDate = document.getElementById("SearchDate");
const BASICURL = "https://localhost:44381/api/";
const refresh = document.getElementById('refresh');

buttonSearchDate.addEventListener("click", function () {
    let lenghtTable = document.getElementById("table").rows.length;
    for (var i = 0, x = 2; i < lenghtTable - 2; i++) {
        document.getElementById("table").deleteRow(x);
    }
    let inputStartDate = document.getElementById("StartDate").value;
    let inputEndDate = document.getElementById("EndDate").value;
    searchLocation.StartDate = inputStartDate;
    searchLocation.EndDate = inputEndDate;
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
    let values;
    if (inputStartDate != "" && inputEndDate != "") {
        inputStartDate = inputStartDate.substring(0, 10);
        inputEndDate = inputEndDate.substring(0, 10);
        values = "?locationSearch.StartDate=" + inputStartDate + "&locationSearch.EndDate=" + inputEndDate;
    }
    xhttp.open("GET", "api/Location/Get" + values, true);
    xhttp.send();
});





refresh.addEventListener('click', function () {
    let lenghtTable = document.getElementById("table").rows.length;
    for (var i = 0, x = 2; i < lenghtTable - 2; i++) {
        document.getElementById("table").deleteRow(x);
    }
    myObj = JSON.parse(locationsList);
    createTable(myObj);
}
);
//When a user types a city for searching, the list is only shown to that city
inputSearch.addEventListener('mouseleave', function () {
    buttonSearch.addEventListener('click', function () {
        let valueOfInput = document.getElementById('nameOfCity').value;
        searchLocation.City = valueOfInput
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

        if (valueOfInput != "") {
            valueOfInput = "?locationSearch.City=" + valueOfInput;
        }
        xhttp.open("GET", "api/Location/Get" + valueOfInput, true);
        xhttp.send();
    });
});


sortDate.addEventListener("click", function () {
    var tbody = document.getElementById("tBodyId");
    var rows = [].slice.call(tbody.querySelectorAll("tr"));
    rows.sort((a, b) => a.cells[3].innerHTML.localeCompare(b.cells.innerHTML));
    rows.forEach(function (v) {
        tbody.appendChild(v); // note that .appendChild() *moves* elements
    });

})


