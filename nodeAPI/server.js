// server.js
'use strict';

// https://automationrhapsody.com/build-rest-api-express-node-js-run-docker/
// https://medium.freecodecamp.org/building-a-simple-node-js-api-in-under-30-minutes-a07ea9e390d2
// https://docs.microsoft.com/en-us/sql/connect/node-js/step-3-proof-of-concept-connecting-to-sql-using-node-js?view=sql-server-2017

const express = require('express');
//assign express to the app param
const app = new express();
const bodyParser = require('body-parser');
 
// register JSON parser middlewear
app.use(bodyParser.json());
//initialise the port to use!
const port = 8000;

//Middleware routing system!!
require('./routes/note_routes')(app); // handles /note
require('./routes/url_routes')(app); // handles /url
require('./routes/uptime_routes')(app); // handles /uptime

// (req, res) is a callback function - called in another function
app.get('/', (req, res) => {
    res.send('Hello World!');
});

app.listen(port, () => {
  console.log('We are live on ' + port);
});