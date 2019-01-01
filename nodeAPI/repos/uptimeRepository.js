// repos/uptimeRepository.js
'use strict';

const Uptime = require('../models/uptime');
//Tedious SQL Server connection
const connection = require('../Connections/conn');

class UptimeRepository {
    constructor() {
        this.notes = new Map([
            [1, new Uptime(1, 1, 200, 89)],
        ]);
    }

    /**
     * request all Uptime records from the database
     * @param {Function(Error, number, any[])} callback : returning method on response
     */
    getAll(callback)
    {
        console.log('uptime:getAll');
        connection.executeStatement('SELECT * FROM [dbo].[Uptime]', function(err, rowCount, rows){
            if(err !== null)
            {
                console.log(err);
            }
            else{
                callback(err, rowCount, rows);
            }
        });
    }

    /**
     * 
     * @param {number} id 
     * @param {Function(Error, number, any[])} callback : returning method on response
     */
    getUptimeByURLID(id, callback)
    {
        console.log('uptime:getUptimeByURLID');
        connection.executeStatement(`SELECT * FROM [dbo].[Uptime] WHERE urlID = ${id}`, function(err, rowCount, rows){
            if(err !== null)
            {
                console.log(err);
            }
            else{
                callback(err, rowCount, rows);
            }
        });
    }
}

const uptimeRepository = new UptimeRepository();
 
module.exports = uptimeRepository;