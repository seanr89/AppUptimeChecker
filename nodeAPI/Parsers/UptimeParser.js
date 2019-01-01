// Parsers/UptimeParser.js
'use strict';

// './BaseParser';
const Uptime = require('../models/uptime');

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
    parseSQLRowsToRecord(rows, rowCount)
    {
        console.log('parseSQLRowsToRecord');
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
}

const uptimeParser = new UptimeParser();

module.exports = uptimeParser;