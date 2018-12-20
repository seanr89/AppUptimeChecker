// models/sqlConfig.js
'use strict';

class SQLConfig {
    constructor(userName, passWord, server) {
        this.userName = userName;
        this.passWord = passWord;
        this.server = server;

    }
}

module.exports = SQLConfig;