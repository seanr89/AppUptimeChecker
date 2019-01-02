// Parsers/URLParser.js
'use strict';

// './BaseParser';

class URLParser {
    constructor()
    {
        console.log('URLParser');
    }

    parseSQLRowDataSetToURL(row)
    {
        //console.log('parseSQLRowDataSetToURL');
        if(row === null)
            return;
        let obj = new URL(row[0].value, row[1].value);
        return obj;
    }

    /**
     *
     * @param {Array} rows
     * @param {Number} rowCount
     */
    parseSQLRowsToURLs(rows, rowCount)
    {
        //console.log('parseSQLRowsToURLs');
        if(rows === null)
        {
            console.log('rows are null');
            return;
        }
        var urlArray = [];
        for(var i=0; i < rowCount; i++)
        {
            let url = this.parseSQLRowDataSetToURL(rows[i]);
            if(url !== null)
                urlArray.push(url);
        }
        return urlArray;
    }
}

const urlParser = new URLParser();

module.exports = urlParser;
