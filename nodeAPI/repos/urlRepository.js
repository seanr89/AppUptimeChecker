// repos/noteRepository.js
'use strict';

const URL = require('../models/url');
//used by the sql to create the config
var config = require('Config');

class URLRepository {
    constructor() {
        this.urls = new Map([
            [1, new URL(1, 'https://www.google.com', 100)],
            [2, new URL(1, 'https://www.google.com', 67)],
            [3, new URL(1, 'https://www.google.com', 33)],
        ]);
    }

    getById(id) {
        return this.urls.get(id);
    }
    getAll() {
        return Array.from(this.urls.values());
    }
    remove() {
        const keys = Array.from(this.urls.keys());
        this.urls.delete(keys[keys.length - 1]);
    }
    save(url) {
        if (this.getById(url.id) !== undefined) {
            this.urls[url.id] = url;
            return 'Updated Url with id=' + url.id;
        }
        else {
            this.urls.set(url.id, url);
            return 'Added Url with id=' + url.id;
        }
    }
}

const urlRepository = new URLRepository();
 
module.exports = urlRepository;
