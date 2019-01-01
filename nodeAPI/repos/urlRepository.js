// repos/noteRepository.js
'use strict';

const URL = require('../models/url');
//used by the sql to create the config
//const config = require('Config');
const connection = require('../Connections/conn');
let parser = require('../Parsers/URLParser');

class URLRepository {
    constructor() {
        //parser = new URLParser();
        this.urls = new Map([
            [1, new URL(1, 'https://www.google.com')],
            [2, new URL(1, 'https://www.google.com')],
            [3, new URL(1, 'https://www.google.com')],
        ]);
    }

    /**
     * 
     * @param {number} id 
     * @param {Function(Error, number, any[])} callback 
     */
    getById(id, callback) {
        connection.executeStatement(`SELECT * FROM [dbo].[URL] WHERE ID = ${id}`, function(err, rowCount, rows){
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
     * @param {Function(Array)} callback(data) with data being the responding data
     */
    getAll(callback) {
        console.log('url getAll');
        try {
            connection.executeStatement('SELECT * FROM [dbo].[URL]', function(err, rowCount, rows){
                if(err !== null)
                {        
                    var data = parser.parseSQLRowsToURLs(rows, rowCount)
                    callback(data);
                }
            }); 
        } catch (error) {
            console.log('error with sql Execution');
        }
        //return Array.from(this.urls.values());
    }

    /**
     * 
     * @param {*} callback 
     */
    remove(callback) {
        const keys = Array.from(this.urls.keys());
        this.urls.delete(keys[keys.length - 1]);
        callback();
    }

    /**
     * 
     * @param {*} url 
     * @param {*} callback 
     */
    save(url, callback) {
        if (this.getById(url.id) !== undefined) {
            this.urls[url.id] = url;
            //return 'Updated Url with id=' + url.id;
        }
        else {
            this.urls.set(url.id, url);
            //return 'Added Url with id=' + url.id;
        }
        callback();
    }
}

const urlRepository = new URLRepository();
 
module.exports = urlRepository;
