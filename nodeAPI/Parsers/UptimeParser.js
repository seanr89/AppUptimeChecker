// Parsers/UptimeParser.js
'use strict';

// './BaseParser';
const Uptime = require('../models/uptime');
const SqlParam = require('../Connections/sqlParam');
var TYPES = require('tedious').TYPES;

class UptimeParser {
    constructor()
    {
        console.log('UptimeParser');
    }

    parseSQLRowDataSetToRecord(row)
    {
        console.log('parseSQLRowDataSetToRecord');
        if(row === null)
            return;
        let obj = new Uptime(row[0].value, row[1].value, row[2].value, row[3].value);
        return obj;
    }

    /**
     *
     * @param {Array} rows
     * @param {Number} rowCount
     */
    parseSQLRowsToRecords(rows, rowCount)
    {
        console.log('parseSQLRowsToRecords');
        if(rows === null)
        {
            console.log('rows are null');
            return;
        }
        var array = [];
        for(var i=0; i < rowCount; i++)
        {
            let url = this.parseSQLRowDataSetToRecord(rows[i]);
            if(url !== null)
            array.push(url);
        }
        return array;
    }

    /**
     *
     * @param {*} uptime
     * @returns {SqlParam} : SqlParam with detailed records
     */
    createSqlParamsForObject(uptime)
    {
        var result = [];
        result.push(new SqlParam('URLID', TYPES.Int, uptime.urlID));
        result.push(new SqlParam('ResponseCode', TYPES.Int, uptime.responseCode));
        result.push(new SqlParam('Duration', TYPES.Int, uptime.duration));
        return result;
    }
}

const uptimeParser = new UptimeParser();

module.exports = uptimeParser;
