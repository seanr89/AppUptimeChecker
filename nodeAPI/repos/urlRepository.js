// repos/noteRepository.js
'use strict';

const URL = require('../models/url');

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
}

const urlRepository = new URLRepository();
 
module.exports = urlRepository;
