'use strict!';
let patientsPath = {
    id: '',
    startDate: '',
    endDate: '',
    city: '',
    locationC: ''
};
import { buildTable } from "../Locations/BuildTable";
import { server } from "../Server/Server";

let patient = {
    id: '',
    age: '',
    name: '',
    password: ''
}


const dataTable = document.getElementById('dataTable');
const newPath = document.getElementById('addNewRow');
const table = document.getElementById('table');
const startDateOfPath = document.getElementById('startDate');
const endDateOfPath = document.getElementById('endDate');
const cityOfPath = document.getElementById('city');
const locationOfPath = document.getElementById('location');
const deleted = document.getElementsByClassName('deleted');
const PasswordPatient = document.getElementById('PasswordPatient');
const newPatient = document.getElementById('addUser');
const inputedPatientsID = document.getElementById('inputedPatientsID');
const switchPatient = document.getElementById('switchPatient');
const patientID = document.getElementById('patientID');
//const patientAge = document.getElementById('patientAge');
const patientsName = document.getElementById('patientName');
const buildTableClass = new buildTable(dataTable, newPath, table, startDateOfPath, endDateOfPath, cityOfPath, locationOfPath, deleted);
const xhttp = new server();
const urlPath = "https://localhost:44381/patient";
let cookie = document.cookie;

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
    try {
        const data = await xhttp.postCall(url, patient);
        addPatient(patient.id, patient.password, patient.name);
    }
    catch (e) {
        failed(e);
    }
}

let setCookie = function setNewCookie(token) {
    document.cookie = "token=" + token + ";";
}

let setPatient = function setCurrentPatient(patientID, patientPassword, patientName) {
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
}

let addPatient = function addAPatient(patientID, patientPassword, patientName) {
    setPatient(patientID, patientPassword, patientName);
    let url = urlPath + "/" + patient.id + "/" + patient.password + "/" + patient.name;
    try {
        const data = await xhttp.deleteCall(url);
        let hide = false;
        if (data.id === 0) {
            hide = true;
        }
        setCookie(data.token);
        for (let i = 0; i < data.path.length; i++) {
            buildTableClass.addPath(data.path[i], hide, cookie);
        }
        changeHTML(patient);
    }
    catch (e) {
        AddNewPatient(patient);
        failed(e);
    }
};

newPatient.addEventListener('click', function () {
    addPatient(patientID.value, PasswordPatient.value, patientsName.value);
});

let switchPatientHtml = function htmlChanges() {
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
    buildTableClass.removeDataTable();
    buildTableClass.deleteInput();
}

switchPatient.addEventListener('click', function () {
    switchPatientHtml();
});
newPath.addEventListener('click', function () {
    patientsPath.startDate = startDateOfPath.value;
    patientsPath.endDate = endDateOfPath.value;
    patientsPath.city = cityOfPath.value;
    patientsPath.locationC = locationOfPath.value;
    patientsPath.id = 0;
    let patientPath = buildTableClass.addPathObject(patient.id, patientsPath, cookie);
    buildTableClass.deleteInput();
});
