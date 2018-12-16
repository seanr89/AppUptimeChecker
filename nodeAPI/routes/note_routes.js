// routes/note_routes.js
'use strict';

const Router = require('express');
const repo = require('../repos/noteRepository');

const getNoteRoutes = (app) => {
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
        .get('/remove', (req, res) => {
            repo.remove();
            const result = 'Last note remove. Total count: '
                + repo.notes.size;
            res.send(result);
        })
        .post('/save', (req, res) => {
            const note = req.body;
            const result = repo.save(note);
            res.send(result);
        });
 
    app.use('/note', router);
};
 
module.exports = getNoteRoutes;