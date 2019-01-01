// models/uptime.js
'use strict';

class Uptime {
    /**
     * 
     * @param {*} id 
     * @param {*} urlID 
     * @param {*} responseCode 
     * @param {*} duration 
     */
    constructor(id, urlID, responseCode, duration) {
        this.id = id;
        this.urlID = urlID;
        this.responseCode = responseCode;
        this.duration = duration
    }
}
 
module.exports = Uptime;