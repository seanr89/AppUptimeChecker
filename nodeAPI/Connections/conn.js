'use strict';

var config = require('config');
var Connection = require('tedious').Connection;  
let simpleCon = null;
const Request = require('tedious').Request;  

class Conn
{
    constructor() {
        simpleCon = new Connection(config.get('connection'));  

        simpleCon.on('error', function(err) {
            console.error(err.stack); }
        );
        this.openConnection();
    }


    openConnection()
    {
        console.log('openConnection');
        simpleCon.on('connect', function(err) { 
            if (err) {  
                console.log(err); 
                return;
            } 
            // If no error, then good to proceed.  
            console.log('Connected');  
        });  
    }

    executeStatement(statement) {
        let request = new Request(statement, function(err) {  
        if (err) {  
            console.log(err);}  
        }); 

        request.on('done', function(rowCount, more) {  
            if(more !== null)
            {
                console.log(rowCount + ' rows returned');
            }
        });  
        simpleCon.execSql(request); 
    }
}

const conn = new Conn();
 
module.exports = conn;