//create connection
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();
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
    connection.send("NewWindowLoaded");
}
//start connection
function fulfilled() {
    console.log("Connection to User Hub Successfull");
    newWindowLoadedOnClient();
}
function rejected(reason) {
    console.log("Connection to User Hub Failed because of: " + reason.toString());
}
connection.start().then(fulfilled,rejected);