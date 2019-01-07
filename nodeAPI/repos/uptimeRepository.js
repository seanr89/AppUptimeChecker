// repos/uptimeRepository.js
'use strict';

const Uptime = require('../models/uptime');
//Tedious SQL Server connection
const connection = require('../Connections/conn');
let parser = require('../Parsers/UptimeParser');

class UptimeRepository {
    constructor() {
        this.notes = new Map([
            [1, new Uptime(1, 1, 200, 89)],
        ]);
    }

    /**
     * request all Uptime records from the database
     * @param {Function(Error,any[])} callback : returning method on response
     */
    getAll(callback)
    {
      console.log('uptime:getAll');
      connection.executeStatement('SELECT TOP (100) * FROM [dbo].[Uptime] ORDER BY ID DESC', function(err, rowCount, rows){
        if(err !== null || err === undefined)
        {
            var data = parser.parseSQLRowsToRecords(rows, rowCount)
            callback(err,data);
        }
        else
          callback(err, null);
      });
    }

    /**
     *
     * @param {number} id : the id of the URL
     * @param {Function(Error,any[])} callback : returning method on response
     */
    getUptimeByURLID(id, callback)
    {
      console.log('uptime:getUptimeByURLID');
      connection.executeStatement(`SELECT * FROM [dbo].[Uptime] WHERE urlID = ${id}`, function(err, rowCount, rows){
        if(err !== null || err === undefined)
        {
            var data = parser.parseSQLRowsToRecords(rows, rowCount)
            callback(err,data);
        }
        else
          callback(err, null);
      });
    }

    /**
     *
     * @param {*} uptime
     * @param {*} callback
     */
    save(uptime, callback)
    {
      console.log('uptime:save');
      let statement = 'INSERT INTO [dbo].[Uptime] OUTPUT INSERTED.ID VALUES(@URLID, @ResponseCode, @Duration)';
      let params = parser.createSqlParamsForObject(uptime);

      connection.executeParameterizedStatementAndReturnID(statement, params, function(err, id){
        if(err !== null || err === undefined)
        {
            return callback(err, id);
        }
        else
          callback(err, null);
      });
    }
}

const uptimeRepository = new UptimeRepository();

module.exports = uptimeRepository;
