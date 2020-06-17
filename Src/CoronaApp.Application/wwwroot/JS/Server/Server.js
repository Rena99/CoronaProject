const oReq = new XMLHttpRequest();

export class server {

    static function deleteCall(url, body, cookie) {
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
        oReq.open("DELETE", url + "/" + cookie.substring(6), true);
        oReq.setRequestHeader("Content-Type", "application/json");
        oReq.setRequestHeader("Authorization", "Bearer " + cookie.substring(6));

        let jsonString = JSON.stringify(body);
        console.log(jsonString);
        oReq.send(jsonString);
    }

    static function putCall(url, body, cookie) {
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
            result => { return result; },
            reject => alert("Bad Response")
        );

        oReq.open("PUT", url + "/" + cookie.substring(6), true);
        oReq.setRequestHeader("Content-Type", "application/json");
        oReq.setRequestHeader("Authorization", "Bearer " + cookie.substring(6));


        let jsonString = JSON.stringify(body);
        console.log(jsonString);
        oReq.send(jsonString);
    }

    static function postCall(url, body) {
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
            result => { return result; },
            reject => alert("Bad Response")
        );

        oReq.open("POST", url, true);
        oReq.setRequestHeader("Content-Type", "application/json");

        let jsonString = JSON.stringify(body);
        console.log(jsonString);
        oReq.send(jsonString);
    }

    static function getCall() {
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
                return result;
            },
            reject => {}
        );
        oReq.open("Get", url, true);
        oReq.send();
    }
}