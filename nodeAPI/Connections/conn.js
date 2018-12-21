'use strict';

var config = require('config');
var Connection = require('tedious').Connection;  
let simpleCon = null;

class Conn
{
    constructor() {
        simpleCon = new Connection(config.get('connection'));  
        this.openConnection();
    }

    openConnection()
    {
        simpleCon.on('connect', function(err) { 
            if (err) {  
                console.log(err); 
                return;
            } 
            // If no error, then good to proceed.  
            console.log('Connected');  
        });  
    }

    executeStatement() {
        
    }
}

const conn = new Conn();
 
module.exports = conn;