// models/url.js
'use strict';

class URL {
    constructor(id, url, responseCode) {
        this.id = id;
        this.url = url;
        this.responseCode = responseCode;
    }
}
 
module.exports = URL;