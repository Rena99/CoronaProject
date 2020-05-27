
const helloTitle = document.createElement('header');
helloTitle.innerText = 'Epidemiology Report';
document.body.appendChild(helloTitle);

let locationsList = [];

let searchLocationsList = [];

let listNamesForColumns = ["city", "location", "startDate", "endDate"];

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

function syntaxHighlight(json) {
    if (typeof json != 'string') {
        json = JSON.stringify(json, undefined, 2);
    }
    json = json.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
        var cls = 'number';
        if (/^"/.test(match)) {
            if (/:$/.test(match)) {
                cls = 'key';
            } else {
                cls = 'string';
            }
        } else if (/true|false/.test(match)) {
            cls = 'boolean';
        } else if (/null/.test(match)) {
            cls = 'null';
        }

        return match;
    });
}

function styleTable(table) {
    table.style.textAlign = "center";
    table.setAttribute("id", "personTable");
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
        var table = document.createElement("table");
        styleTable(table);
        var col = [];
        //var numberOfProperties = Object.keys(locationsList[0].).length;
        var numberOfProperties = 4;
        var tHead = document.createElement("thead");
        var hRow = document.createElement("tr");
        for (let j = 0; j < numberOfProperties; j++) {
            var th = document.createElement("th");
            th.innerHTML = listNamesForColumns[j].charAt(0).toUpperCase() + listNamesForColumns[j].slice(1);
            hRow.appendChild(th);
        }
        tHead.appendChild(hRow);
        table.appendChild(tHead);
        var index = 0;
        var tBody = document.createElement("tbody");
        for (var c = 0; c < myObj.length; c++) {
          
            currentRowValues = myObj[c];
            //currentRowValues = myObj[c].path;
            var k = Object.keys(currentRowValues).length;
         // for (var i = 0; i < k; i++) {
                //for (var item in currentRowValues.numberOfProperties) {
                var arryEX = Object.values(currentRowValues);
                let bRow = document.createElement("tr");
                for (var item2 of arryEX)
                {
                    index++;
                    var td = document.createElement("td");
                    td.style.width = "50px";
                    if (index===3|| index==4) {
                        item2 = item2.substring(0, 10);
                      //  item2[4] = "/";
                    }
                    td.innerHTML = item2;
                    bRow.appendChild(td);
                }
                index = 0;
                tBody.appendChild(bRow);
            }
        //}
    }
    table.appendChild(tBody);
    b.appendChild(table);
}


//Call to build the table
//createTable(locationsList);

const buttonSearch = document.getElementById('Search-City');

const inputSearch = document.getElementById('nameOfCity');

const BASICURL = "https://localhost:44381/api/";

const refresh = document.getElementById('refresh');
var searchLocation = {
    City: String
}
refresh.addEventListener('click', function () {
    document.getElementById("personTable").remove();
    createTable(locationsList);
}
);
//When a user types a city for searching, the list is only shown to that city
inputSearch.addEventListener('mouseleave', function () {
    buttonSearch.addEventListener('click', function () {
        //Variable that hendle which city user want to check 
        let valueOfInput = document.getElementById('nameOfCity').value;
        //inputSearch.value = "";
        searchLocation.City = valueOfInput
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

        if (valueOfInput != "") {
            valueOfInput = "?locationSearch.City=" + valueOfInput;}
        xhttp.open("GET", "api/Location/" + valueOfInput, true);
        xhttp.send();

        //let item;
        //let flag = 0;
        //if (valueOfInput != "") {
        //    var i = 0;
        //    const city = document.getElementById('personTable');
        //    for (var r = 1, index = 1, n = city.rows.length; index < n; r++, index++) {
        //        item = city.rows[r].cells[0].innerHTML;

        //        if (item === valueOfInput) {
        //            flag = 1;
        //        }
        //        else {
        //            document.getElementById("personTable").deleteRow(r);
        //            r--;
        //        }
        //    }
        //    if (flag === 0) {
        //        document.getElementById("personTable").remove();
        //        createTable(locationsList);
        //    }
        //}
    });
});

//convert Date
function convertDate(d) {
    var p = d.split("-");
    reverseString(p);
    return +(p[2] + p[1] + p[0]);
}

//Sort a table By Date
function sortByDate() {
    var tbody = document.querySelector("#personTable tbody");
    // get trs as array for ease of use
    var rows = [].slice.call(tbody.querySelectorAll("tr"));
    rows.sort(function (a, b) {
        return convertDate(a.cells[2].innerHTML) - convertDate(b.cells[2].innerHTML);
    });
    rows.forEach(function (v) {
        tbody.appendChild(v); // note that .appendChild() *moves* elements
    });

}
document.querySelector("button").addEventListener("click", sortByDate);
document.getElementById("sortById").addEventListener("click", sortByDate);

function reverseString(str) {
    var arry=[];
    for (var i = 0, j = str.length; i <str.length; j--,i++) {
        arry[j] = str[i];
    }
    console.log(arry);

    return arry;
}



