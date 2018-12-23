'use strict';

const config = require('../Config/default.json');
const defaultConfig = config.connection;
var Connection = require('tedious').Connection;  
let simpleCon = null;
const Request = require('tedious').Request;  

class Conn
{
    constructor() {
        console.log('Connection Open');
        simpleCon = new Connection(defaultConfig);

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
        if(statement === null)
        {
            console.log('no statement');
            return;
        }
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