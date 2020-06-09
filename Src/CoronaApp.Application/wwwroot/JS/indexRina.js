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
    name: '',
    password: ''
}

const PasswordPatient = document.getElementById('PasswordPatient');
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
//const patientAge = document.getElementById('patientAge');
const patientsName = document.getElementById('patientName');
const deleted = document.getElementsByClassName('deleted');
//const getToken = document.getElementById('getToken');
const oReq = new XMLHttpRequest();
const urlPath = "https://localhost:44381/patient";
let token = '';

let changeHTML = function changeHTMLAttributes(patient) {
    PasswordPatient.style.display = 'none';
    patientID.style.display = 'none';
    //patientAge.style.display = 'none';
    patientsName.style.display = 'none';
    inputedPatientsID.innerText = "Welcome " + patient.name;
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
            addPatient(patient.id, patient.password, patient.name)
        },
        reject => alert("Bad Response")
    ).catch(e => {
        console.log(e);
    });

    oReq.open("POST", url/*+ '?access_token=' + encodeURIComponent(token)*/, true);
    let jsonString = JSON.stringify(patient);
    oReq.setRequestHeader("Content-Type", "application/json");
   // oReq.setRequestHeader("Authorization", token);
    oReq.send(jsonString);
}

let getTokenFunction = function getNewToken(id,password) {
    let url = urlPath + "/Authenticate/" + id + "/" + password;
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
            token = result.rawData;
        },
        reject => alert("Your password or id is incorrect")
    );
    oReq.open("Get", url+ '?access_token=' + encodeURIComponent(token), true);
    oReq.send();
};

let addPatient = function addAPatient(patientID, patientPassword, patientName) {
    patient.password = 0;
    patient.id = 0;
    patient.name = '';
    patient.age = 0;
    
    if (patientID !== "") {
        patient.id = parseInt(patientID);
    }
    if (patientPassword !== "") {
        patient.password = parseInt(patientPassword);
    }
    if (patientName !== "") {
        patient.name = patientName;
    }

    let url = urlPath + "/" + patient.id + "/" + patient.password + "/" + patient.name;
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
            token = result.token;
            for (let i = 0; i < result.path.length; i++) {
                addPath(result.path[i], hide);
            }
        },
        reject => AddNewPatient(patient)
    );
    oReq.open("Get", url, true);
    //oReq.Headers.Add("Authorization", "Bearer " + token);
    //oReq.setRequestHeader("Authorization", token);
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
    let url = urlPath + "/" + patient.id;
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
    oReq.open("DELETE", urlPath + "/" + token, true);
    oReq.setRequestHeader("Content-Type", "application/json");
    oReq.setRequestHeader("Authorization", "Bearer "+token);

    let jsonString = JSON.stringify(id);
    console.log(jsonString);
    oReq.send(jsonString);
}

let addPathObject = function addANewObjectToPatientPathArray(path) {
    let url = urlPath + "/" + patient.id;
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

    let b = url + '?access_token=' + encodeURIComponent(token);
    oReq.open("PUT", urlPath +"/"+ token, true);
    oReq.setRequestHeader("Content-Type", "application/json");
    oReq.setRequestHeader("Authorization", "Bearer "+token);
    


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

//getToken.addEventListener('click', function () {
//    if (patientID.value === '' || PasswordPatient.value === '') {
//        alert("no id and password inputed");
//    }
//    else {
//        getTokenFunction(patientID.value, PasswordPatient.value);
//        getToken.style.display = 'none';
//        newPatient.style.display = 'inline';
//        patientID.value = '';
//        patientID.style.display = 'inline';
//        patientAge.style.display = 'inline';
//        patientsName.style.display = 'inline';
//        PasswordPatient.style.display = 'none';

//    }
//});
newPatient.addEventListener('click', function () {
    addPatient(patientID.value, PasswordPatient.value, patientsName.value);
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
    patientsName.style.display = 'inline';
    patientsName.value = '';
    PasswordPatient.value = '';
    PasswordPatient.style.display = 'inline';
    patientAge.style.display = 'inline';
    newPatient.style.display = 'inline';
    table.style.display = 'none';
    inputedPatientsID.style.display = 'none';
    dataTable.style.display = 'none';
    switchPatient.setAttribute("className", 1);
    removeDataTable();
    deleteInput();
});

