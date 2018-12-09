// server.js
'use strict';

//https://automationrhapsody.com/build-rest-api-express-node-js-run-docker/
//https://medium.freecodecamp.org/building-a-simple-node-js-api-in-under-30-minutes-a07ea9e390d2

const express = require('express');
const app = new express();
const bodyParser = require('body-parser');
 
// register JSON parser middlewear
app.use(bodyParser.json());
//initialise the port to use!
const port = 8000;

app.get('/', (req, res) => {
    res.send('Hello World!');
});

app.listen(port, () => {
  console.log('We are live on ' + port);
});