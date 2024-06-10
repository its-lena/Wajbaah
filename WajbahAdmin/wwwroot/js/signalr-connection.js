var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7001/yourhub").build();

connection.on("ReceiveData", function (data) {
    console.log(data);
    document.getElementById('myElement').innerText = "Registeration Request from" +data;
    document.getElementById("yourLink");
    yourLink.href = "/ChefDetails/" + message;
});

connection.start().then(function () {
    console.log('Connected to hub');
}).catch(function (err) {
    return console.error(err.toString());
});
