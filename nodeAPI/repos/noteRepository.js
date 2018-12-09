// repos/noteRepository.js
'use strict';

const Note = require('../models/note');

class NoteRepository {
    constructor() {
        this.persons = new Map([
            [1, new Note(1, 'FN1', 'LN1', 'email1@email.na')],
            [2, new Note(2, 'FN2', 'LN2', 'email2@email.na')],
            [3, new Note(3, 'FN3', 'LN3', 'email3@email.na')],
            [4, new Note(4, 'FN4', 'LN4', 'email4@email.na')]
        ]);
    }
}

const personRepository = new NoteRepository();
 
module.exports = noteRepository;