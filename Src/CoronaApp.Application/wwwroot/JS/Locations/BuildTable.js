'use strict!';

import { server } from "../Server/Server";

const urlPath = "https://localhost:44381/patient";
const xhttp = new server();

export class buildTable {
    dataTable; 
    newPath; 
    table; 
    startDateOfPath;
    endDateOfPath;
    cityOfPath;
    locationOfPath; 
    deleted;

    constructor(dataTable, newPath, table, startDateOfPath, endDateOfPath, cityOfPath, locationOfPath, deleted) {
        this.dataTable = dataTable
        this.newPath = newPath;
        this.table = table;
        this.startDateOfPath = startDateOfPath;
        this.endDateOfPath = endDateOfPath;
        this.cityOfPath = cityOfPath;
        this.locationOfPath = locationOfPath;
        this.deleted = deleted;
    }     
    deleteInput = function deleteInputItems() {
        startDateOfPath.value = '';
        endDateOfPath.value = '';
        cityOfPath.value = '';
        locationOfPath.value = '';
    };

    fillCell0 = function fillFirstCell(newCell, patientPath) {
        const string = document.createTextNode(patientPath.startDate);
        newCell.appendChild(string);
    };
    
    fillCell1 = function fillSecondCell(newCell, patientPath) {
        const string = document.createTextNode(patientPath.endDate);
        newCell.appendChild(string);
    };
    
    fillCell2 = function fillThirdCell(newCell, patientPath) {
        const string = document.createTextNode(patientPath.city);
        newCell.appendChild(string);
    };
    
    fillCell3 = function fillFourthCell(newCell, patientPath) {
        const string = document.createTextNode(patientPath.locationC);
        newCell.appendChild(string);
    };
    
    fillCell4 = function fillFifthCell(newCell, numOfRows, hide) {
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
    
    fillCell = function (newCell, cellId, numOfRows, patientPath, hide) {
        //button to delete
        if (cellId === 4) {
            fillCell4(newCell, numOfRows, hide);
        }
        else {
            //add start date
            if (cellId === 0) {
                fillCell0(newCell, patientPath);
            }
            //add end date
            else if (cellId === 1) {
                fillCell1(newCell, patientPath);
            }
            //add city
            else if (cellId === 2) {
                fillCell2(newCell, patientPath);
            }
            //addlocation
            else if (cellId === 3) {
                fillCell3(newCell, patientPath);
            }
        }
    
    };
    
    addCells = function addCellsToRow(newRow, numOfRows, patientPath, hide) {
        for (let i = 0; i < 5; i++) {
            let newCell = newRow.insertCell(i);
            newCell.setAttribute("class", "cell");
            fillCell(newCell, i, numOfRows, patientPath, hide);
        }
    };
    
    addPath = function addAPathToAPatient(patientPath, hide) {
        let numOfRows = dataTable.rows.length;
        if (numOfRows === 0 || dataTable.style.display === "none") {
            dataTable.style.display = 'block';
        }
        let newRow = dataTable.insertRow(numOfRows);
        newRow.setAttribute("class", "row");
        newRow.setAttribute("id", patientPath.id);
        addCells(newRow, numOfRows, patientPath, hide);
    };
    
    DeletePaths = function savePathsOfPatient(id, cookie) {
        let url = urlPath + "/" + id;
        try {
            const data = await xhttp.deleteCall(url, id, cookie);
            success(data)
        }
        catch(e){
            failed(e)
        }
    }
    
    addPathObject = function addANewObjectToPatientPathArray(id, path, cookie) {
         let url = urlPath + "/" + id;
         try {
             const data = await xhttp.putCall(url, path, cookie);
             addPath(data);
         }
         catch (e) {
             failed(e)
         }
    };
    
    removePath = function removeApathFromAPatient(rowID) {
        let row = dataTable.rows[rowID];
        let removedRow = dataTable.deleteRow(rowID);
        DeletePaths(row.id);
    };

    removeDataTable = function removeDataTableFromDisplay() {
        let max = dataTable.rows.length;
        for (let i = 0; i < max; i++) {
            dataTable.deleteRow(0);
        }
    };
    
    oldPath = function setsClickForDeleteButton(deleted) {
        deleted.addEventListener('click', function () {
            removePath(deleted.id);
        });
    };
}
