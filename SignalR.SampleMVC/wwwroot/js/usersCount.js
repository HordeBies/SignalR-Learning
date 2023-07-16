//create connection
var connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    //.configureLogging(signalR.LogLevel.None)
    .withUrl("/hubs/userCount", signalR.HttpTransportType.WebSockets).build();
//connect to methods that hub invokes aka receive notifications from hub
connection.on("updateTotalViews", function (value) {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});
connection.on("updateTotalUsers", function (value) {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
});
//invoke hub methods aka send notifications to hub
function newWindowLoadedOnClient() {
    connection.invoke("NewWindowLoaded", "bies").then((value) => console.log(value)); //invoke returns a promise while send does not
}
//start connection

// SignalR events that can be used to handle connection state changes
connection.onclose((error) => {

});
connection.onreconnected((connectionId) => {

});
connection.onreconnecting((error) => {

});
function fulfilled() {
    console.log("Connection to User Hub Successfull");
    newWindowLoadedOnClient();
}
function rejected(reason) {
    console.log("Connection to User Hub Failed because of: " + reason.toString());
}
connection.start().then(fulfilled, rejected);