// routes/url_routes.js
'use strict';

const Router = require('express');
const repo = require('../repos/urlRepository');

const getURLRoutes = (app) => {
    const router = new Router();

    router
    .get('/get/:id', (req, res) => {
        const id = parseInt(req.params.id);
        const result = repo.getById(id);
        res.send(result);
    })
    .get('/all', (req, res) => {
        const result = repo.getAll();
        res.send(result);
    })

    app.use('/url', router);
};

module.exports = getURLRoutes;