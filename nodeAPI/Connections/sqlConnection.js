// Connections/sqlConnection.js
'use strict';

// https://docs.microsoft.com/en-us/sql/connect/node-js/step-3-proof-of-concept-connecting-to-sql-using-node-js?view=sql-server-2017

var Connection = require('tedious').Connection;  
    var config = {  
        userName: 'yourusername',  
        password: 'yourpassword',  
        server: 'yourserver.database.windows.net',  
        // When you connect to Azure SQL Database, you need these next options.  
        options: {encrypt: true, database: 'AdventureWorks'}  
    }; 


    var connection = new Connection(config);  
    connection.on('connect', function(err) {  
        // If no error, then good to proceed.  
        console.log('Connected');  
        executeStatement();  
    });  

    function executeStatement() {  
    }
