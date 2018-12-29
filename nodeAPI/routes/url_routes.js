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
        repo.getAll(function(data)
        {
            res.send(data);
        });
        
    })
    .get('/remove', (req, res) => {
        repo.remove();
        const result = 'Last note remove. Total count: '
            + repo.urls.size;
        res.send(result);
    })
    .post('/save', (req, res) => {
        const url = req.body;
        const result = repo.save(url);
        res.send(result);
    });

    app.use('/url', router);
};

module.exports = getURLRoutes;