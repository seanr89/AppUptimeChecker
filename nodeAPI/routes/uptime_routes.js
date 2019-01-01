// routes/uptime_routes.js
'use strict';

const Router = require('express');
const repo = require('../repos/uptimeRepository');

const getUptimeRoutes = (app) => {
    const router = new Router();

    app.use('/uptime', router);

    router
    .get('/all', (req, res) => {
        //Repo request with callback required!
        repo.getAll(function(data)
        {
            res.send(data);
        });
    })
    .get('/get/:urlID', (req, res) => {
        const id = parseInt(req.params.urlID);
        repo.getUptimeByURLID(id, function(data)
        {
            res.send(data);
        });
    });
};

module.exports = getUptimeRoutes;