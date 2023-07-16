let submitButton = document.getElementById("sendMessage");
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/basicChat").build();

submitButton.disabled = true;

connection.on("MessageReceived", function (user, message) {
    var li = document.createElement("li");
    li.textContent = `${user} - ${message}`;
    document.getElementById("messagesList").appendChild(li);
});

submitButton.addEventListener("click", function (event) {
    let sender = document.getElementById("senderEmail").value;
    let receiver = document.getElementById("receiverEmail").value;
    let message = document.getElementById("chatMessage").value;
    connection.invoke("SendMessage", sender, receiver, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.start().then(function () {
    submitButton.disabled = false;
});
