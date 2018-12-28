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
        console.log('parseSQLRowDataSetToURL');
        if(row === null)
            return;
        let obj = new URL();
            obj.id = row[0].value;
            obj.url = row[1].value;

        return obj;
    }

    parseSQLRowsToURLs(rows)
    {
        console.log('parseSQLRowsToURLs');
        if(rows === null)
            return;
        var urlArray = [];
        for(var i=0; i < rows; i++)
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