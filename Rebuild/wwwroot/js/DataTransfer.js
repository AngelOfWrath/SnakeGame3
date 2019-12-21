"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ControlHub").build();

connection.on("ReceiveData", function (data) {
    
    if (data == "w") {
        $('#result').html("/\\");
    }
    if (data == "s") {
        $('#result').html("\\/");
    }
    if (data == "a") {
        $('#result').html("<-");
    }
    if (data == "d") {
        $('#result').html("->");
    }
    previousDirection = data;
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});