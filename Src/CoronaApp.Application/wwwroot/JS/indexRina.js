'use strict!';
let patientsPath = {
    id: '',
    startDate: '',
    endDate: '',
    city: '',
    locationC: ''
};

let patient = {
    id: '',
    age: '',
}

const dataTable = document.getElementById('dataTable');
const newPatient = document.getElementById('addUser');
const newPath = document.getElementById('addNewRow');
const table = document.getElementById('table');
const inputedPatientsID = document.getElementById('inputedPatientsID');
const switchPatient = document.getElementById('switchPatient');
const patientID = document.getElementById('patientID');
const startDateOfPath = document.getElementById('startDate');
const endDateOfPath = document.getElementById('endDate');
const cityOfPath = document.getElementById('city');
const locationOfPath = document.getElementById('location');
const patientAge = document.getElementById('patientAge');
const deleted = document.getElementsByClassName('deleted');
const oReq = new XMLHttpRequest();
const urlPath = "https://localhost:44381/patient";

let changeHTML = function changeHTMLAttributes(patient) {
    patientID.style.display = 'none';
    patientAge.style.display = 'none';
    inputedPatientsID.innerText = patientID.value;
    inputedPatientsID.style.display = 'block';
    switchPatient.style.display = 'block';
    newPatient.style.display = 'none';
    if (patient.id === 0) {
        table.style.display = 'none';
    }
    else {
        table.style.display = 'block';
    }
};

let AddNewPatient = function AddNewPatientToDB(patient) {
    let url = urlPath;
    let promise = new Promise(function (resolve, reject) {
        oReq.onreadystatechange = function () {
            if (this.readyState === 4 && this.status === 200) {
                resolve();
            }
            if (this.readyState === 4 && this.status !== 200) {
                reject();

            }
        }
    }).then(
        result => {
            console.log("success");
        },
        reject => alert("Bad Response")
    ).catch(e => {
        console.log(e);
    });
    oReq.open("POST", url, true);
    let jsonString = JSON.stringify(patient);
    oReq.setRequestHeader("Content-Type", "application/json");
    oReq.send(jsonString);
}
let addPatient = function addAPatient(patientID, patientsAge) {
    patient.age = 0;
    patient.id = 0;
    if (patientID !== "") {
        patient.id = parseInt(patientID);
    }
    if (patientsAge !== "") {
        patient.age = parseInt(patientsAge);
    }
    let url = urlPath + "/" + patient.id + "/" + patient.age;
    let promise = new Promise(function (resolve, reject) {
        oReq.onreadystatechange = function () {
            if (this.readyState === 4 && this.status === 200) {
                resolve(JSON.parse(this.responseText));
            }
            if (this.readyState === 4 && this.status !== 200) {
                reject();
            }
        }
    }).then(
        result => {
            let hide = false;
            if (result.id === 0) {
                hide = true;
            }
            for (let i = 0; i < result.path.length; i++) {
                addPath(result.path[i], hide);
            }
        },
        reject => AddNewPatient(patient)
    );
    oReq.open("Get", url, true);
    oReq.send();
    changeHTML(patient);
};

let deleteInput = function deleteInputItems() {
    startDateOfPath.value = '';
    endDateOfPath.value = '';
    cityOfPath.value = '';
    locationOfPath.value = '';
};

let fillCell0 = function fillFirstCell(newCell, patientPath, numOfRows) {
    const string = document.createTextNode(patientPath.startDate);
    newCell.appendChild(string);
};

let fillCell1 = function fillSecondCell(newCell, patientPath, numOfRows) {
    const string = document.createTextNode(patientPath.endDate);
    newCell.appendChild(string);
};

let fillCell2 = function fillThirdCell(newCell, patientPath, numOfRows) {
    const string = document.createTextNode(patientPath.city);
    newCell.appendChild(string);
};

let fillCell3 = function fillFourthCell(newCell, patientPath, numOfRows) {
    const string = document.createTextNode(patientPath.locationC);
    newCell.appendChild(string);
};

let fillCell4 = function fillFifthCell(newCell, numOfRows, hide) {
    const deleted = document.createElement('button');
    deleted.innerText = 'X';
    deleted.setAttribute("id", numOfRows);
    deleted.setAttribute("class", "deleted");
    newCell.setAttribute("class", "button");
    oldPath(deleted);
    newCell.appendChild(deleted);
    if (hide === true) {
        newCell.style.display = 'none';
    }
    
};

let fillCell = function (newCell, cellId, numOfRows, patientPath, hide) {
    //button to delete
    if (cellId === 4) {
        fillCell4(newCell, numOfRows, hide);
    }
    else {
        //add start date
        if (cellId === 0) {
            fillCell0(newCell, patientPath, numOfRows);
        }
        //add end date
        else if (cellId === 1) {
            fillCell1(newCell, patientPath, numOfRows);
        }
        //add city
        else if (cellId === 2) {
            fillCell2(newCell, patientPath, numOfRows);
        }
        //addlocation
        else if (cellId === 3) {
            fillCell3(newCell, patientPath, numOfRows);
        }
    }

};

let addCells = function addCellsToRow(newRow, numOfRows, patientPath, hide) {
    for (let i = 0; i < 5; i++) {
        let newCell = newRow.insertCell(i);
        newCell.setAttribute("class", "cell");
        fillCell(newCell, i, numOfRows, patientPath, hide);
    }
};

let addPath = function addAPathToAPatient(patientPath, hide) {
    let numOfRows = dataTable.rows.length;
    if (numOfRows === 0 || dataTable.style.display === "none") {
        dataTable.style.display = 'block';
    }
    let newRow = dataTable.insertRow(numOfRows);
    newRow.setAttribute("class", "row");
    newRow.setAttribute("id", patientPath.id);
    addCells(newRow, numOfRows, patientPath, hide);
};

let DeletePaths = function savePathsOfPatient(id) {
    let url = urlPath + "/" + inputedPatientsID.innerText;
    let promise = new Promise(function (resolve, reject) {
        oReq.onreadystatechange = function () {
            if (this.readyState === 4 && this.status === 200) {
                resolve();
            }
            if (this.readyState === 4 && this.status !== 200) {
                reject();
            }
        }
    }).then(
        result => console.log("Worked"),
        reject => alert("Bad Response")
    );
    oReq.open("DELETE", url, true);
    oReq.setRequestHeader("Content-Type", "application/json");
    let jsonString = JSON.stringify(id);
    console.log(jsonString);
    oReq.send(jsonString);
}

let addPathObject = function addANewObjectToPatientPathArray(path) {
    let url = urlPath + "/" + inputedPatientsID.innerText;
    let promise = new Promise(function (resolve, reject) {
        oReq.onreadystatechange = function () {
            if (this.readyState === 4 && this.status === 200) {
                resolve(JSON.parse(this.responseText));
            }
            if (this.readyState === 4 && this.status !== 200) {
                reject();
            }
        }
    }).then(
        result => addPath(result),
        reject => alert("Bad Response")
    );
    oReq.open("PUT", url, true);
    oReq.setRequestHeader("Content-Type", "application/json");
    let jsonString = JSON.stringify(path);
    console.log(jsonString);
    oReq.send(jsonString);
};

let removePath = function removeApathFromAPatient(rowID) {
    let row = dataTable.rows[rowID];
    let removedRow = dataTable.deleteRow(rowID);
    DeletePaths(row.id);
};

let oldPath = function setsClickForDeleteButton(deleted) {
    deleted.addEventListener('click', function () {
        removePath(deleted.id);
    });
};

let removeDataTable = function removeDataTableFromDisplay() {
    let max = dataTable.rows.length;
    for (let i = 0; i < max; i++) {
        dataTable.deleteRow(0);
    }
};

newPatient.addEventListener('click', function () {
    addPatient(patientID.value, patientAge.value);
});

newPath.addEventListener('click', function () {
    patientsPath.startDate = startDateOfPath.value;
    patientsPath.endDate = endDateOfPath.value;
    patientsPath.city = cityOfPath.value;
    patientsPath.locationC = locationOfPath.value;
    patientsPath.id = 0;
    let patientPath = addPathObject(patientsPath);
    deleteInput();
});

switchPatient.addEventListener('click', function () {
    switchPatient.style.display = 'none';
    patientID.value = '';
    patientID.style.display = 'inline';
    patientAge.value = '';
    patientAge.style.display = 'inline';
    newPatient.style.display = 'inline';
    table.style.display = 'none';
    inputedPatientsID.style.display = 'none';
    dataTable.style.display = 'none';
    switchPatient.setAttribute("className", 1);
    removeDataTable();
    deleteInput();
});

