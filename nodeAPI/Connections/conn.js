'use strict';

const config = require('../Config/default.json');
const defaultConfig = config.connection;
var Connection = require('tedious').Connection;
let simpleCon = null;
const Request = require('tedious').Request;

class Conn
{
    constructor() {
        //console.log('Connection Open');
        simpleCon = new Connection(defaultConfig);

        simpleCon.on('error', function(err) {
            console.error(err.stack);
            this.closeConnection();
          });
        simpleCon.on('errorMessage', this.infoError);
    }


    openConnection()
    {
        //console.log('openConnection');
        simpleCon.on('connect', function(err) {
            if (err) {
                console.log(err);
                return;
            }
            // If no error, then good to proceed.
            console.log('Connected');
        });
    }

    /**
     * ensure we close the currently open connection
     */
    closeConnection()
    {
      simpleCon.close();
    }

    /**
     *
     * @param {*} statement
     * @param {Function(Error, number, any[])} callback (err, rowCount, rows)
     */
    executeStatement(statement, callback) {
        console.log('executeStatement');
        this.openConnection();
        if(statement === null)
        {
            console.log('no statement');
            return;
        }
        let request = new Request(statement, function(err, rowCount, rows) {
            if (err) {
                console.log(err);
            }
            //console.log(rowCount + ' rows returned');
            callback(err, rowCount, rows);
        });

        // useful event listener to check when a query is complete
        request.on('requestCompleted', function () {
            console.log('requestCompleted');
            //do we need to close the connection here??
            this.closeConnection();
         });
        simpleCon.execSql(request);
    }

    infoError(info) {
        console.log(info.number + ' : ' + info.message);
      }
}

const conn = new Conn();

module.exports = conn;
