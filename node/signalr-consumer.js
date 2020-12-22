const signalR = require("@microsoft/signalr");

let connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/messagehub")
    .withAutomaticReconnect()
    .build();

connection.on("OnMessageReceived", data => {
    console.log(data);
});

connection.start()
    .then(() => connection.invoke("SendMessage", "Hello from Node.js"));