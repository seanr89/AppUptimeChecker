// models/note.js
'use strict';

class Note {
    constructor(id, firstName, lastName, email) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
    }
}
 
module.exports = Note;