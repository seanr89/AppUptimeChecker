// repos/noteRepository.js
'use strict';

const Note = require('../models/note');

class NoteRepository {
    constructor() {
        this.notes = new Map([
            [1, new Note(1, 'FN1', 'LN1', 'email1@email.na')],
            [2, new Note(2, 'FN2', 'LN2', 'email2@email.na')],
            [3, new Note(3, 'FN3', 'LN3', 'email3@email.na')],
            [4, new Note(4, 'FN4', 'LN4', 'email4@email.na')]
        ]);
    }

    getById(id) {
        return this.notes.get(id);
    }
 
    getAll() {
        return Array.from(this.notes.values());
    }

    remove() {
        const keys = Array.from(this.notes.keys());
        this.notes.delete(keys[keys.length - 1]);
    }

    save(note) {
        if (this.getById(note.id) !== undefined) {
            this.notes[note.id] = note;
            return 'Updated Note with id=' + note.id;
        }
        else {
            this.notes.set(note.id, note);
            return 'Added Note with id=' + note.id;
        }
    }
}

const noteRepository = new NoteRepository();
 
module.exports = noteRepository;