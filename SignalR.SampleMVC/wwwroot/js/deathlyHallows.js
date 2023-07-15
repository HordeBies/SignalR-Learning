var cloakSpan = document.getElementById("cloakCounter");
var stoneSpan = document.getElementById("stoneCounter");
var wandSpan = document.getElementById("wandCounter");
//create connection
var connection = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.None)
    .withUrl("/hubs/deathlyHallows").build();
//connect to methods that hub invokes aka receive notifications from hub
connection.on("updateDeathlyHallowCount", function (cloak, stone, wand) {
    cloakSpan.innerText = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
});
//start connection
function fulfilled() {
    console.log("Connection to User Hub Successfull");
    connection.invoke("GetVoteStatus").then((dict) => {
        cloakSpan.innerText = dict.cloak.toString();
        stoneSpan.innerText = dict.stone.toString();
        wandSpan.innerText = dict.wand.toString();
    });
}
function rejected(reason) {
    console.log("Connection to User Hub Failed because of: " + reason.toString());
}
connection.start().then(fulfilled, rejected);

function vote(to) {
    $.ajax({
        url: `/home/DeathlyHallows?type=${to}`
    });
}