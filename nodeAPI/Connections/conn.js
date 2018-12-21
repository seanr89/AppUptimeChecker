'use strict';

var config = require('config');
var Connection = require('tedious').Connection;  

class Conn
{
    constructor() {
        var connection = new Connection(config.get('connection'));  
    }
}

const conn = new Conn();
 
module.exports = conn;