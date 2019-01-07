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
        repo.getAll(function(err,data)
        {
            res.send(data);
        });
    })
    .get('/get/:urlID', (req, res) => {
        const id = parseInt(req.params.urlID);
        repo.getUptimeByURLID(id, function(err, data)
        {
            res.send(data);
        });
    })
    .post('/save', (req, res) => {
      const uptime = req.body;
      repo.save(uptime, function(resp)
      {
          res.send(resp);
      });
    });
};

module.exports = getUptimeRoutes;
