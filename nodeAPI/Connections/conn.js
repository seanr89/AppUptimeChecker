'use strict';

const config = require('../Config/default.json');
const defaultConfig = config.connection;
var Connection = require('tedious').Connection;
let simpleCon = null;
const Request = require('tedious').Request;
var TYPES = require('tedious').TYPES;

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

    /**
     *
     * @param {*} statement
     * @param {*} parameters
     * @param {*} callback
     */
    executeParameterizedStatementAndReturnID(statement, parameters, callback)
    {
      console.log('executeStatement');
      if(statement === null || parameters === null){
        console.log('no statement or parameters'); return;
      }
      this.openConnection();

      let request = new Request(statement, function(err) {
        if (err) {
            console.log(err);
            callback(err, null);
        }
        for(var i = 0; i < parameters.length; ++i){
          request.addParameter(parameters[i].name, parameters[i].type, parameters[i].value);
        }

        request.on('row', function(columns) {
          columns.forEach(function(column) {
              if (column.value === null) {
                console.log('NULL');
                callback('Failed to insert', -1);
              } else {
                //console.log('id of inserted item is ' + column.value);
                callback(null, column.value);
              }
            });
          });
      simpleCon.execSql(request);
      });
    }

    infoError(info) {
        console.log(info.number + ' : ' + info.message);
      }
}

const conn = new Conn();

module.exports = conn;
