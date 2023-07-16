var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/chat")
    .withAutomaticReconnect([0, 1000, 5000, null])
    .build();

connection.on("UserConnected", function (userId, userName) {
    addMessage(`${userName} opened a connection`);
});
connection.on("UserDisconnected", function (userId, userName) {
    addMessage(`${userName} closed a connection`);
});
connection.on("NewRoomCreated", function (maxRoom, roomId, roomName, userId, userName) {
    addMessage(`${userName} created a new room ${roomName}`);
    fillRoomDropDown();
});
connection.on("RoomDeleted", function (deletedRoomId, deletedRoomName, selectedRoomId, userId, userName) {
    addMessage(`${userName} deleted a room ${deletedRoomName}`);
    fillRoomDropDown();
});

connection.on("PublicMessageReceived", function (roomId, roomName, userId, userName, message) {
    addMessage(`<b>[${roomName}]</b> ${userName} <b>:</b> ${message}`);
});

connection.on("PrivateMessageReceived", function (senderId, senderName, receiverId, receiverName, message, chatId) {
    addMessage(`<b>[PRIVATE]</b> ${senderName} <i class="bi bi-arrow-right"></i> ${receiverName} <b>:</b> ${message}`);
});

function sendPublicMessage() {
    let inputMsg = document.getElementById("txtPublicMessage");
    let ddlSelRoom = document.getElementById("ddlSelRoom");

    let roomId = ddlSelRoom.value;
    let roomName = ddlSelRoom.options[ddlSelRoom.selectedIndex].text;
    let msg = inputMsg.value;

    connection.send("SendPublicMessage", Number(roomId), roomName, msg);

    inputMsg.value = "";
}

function sendPrivateMessage() {
    let inputMsg = document.getElementById("txtPrivateMessage");
    let ddlSelUser = document.getElementById("ddlSelUser");

    let receiverId = ddlSelUser.value;
    let receiverName = ddlSelUser.options[ddlSelUser.selectedIndex].text;
    let msg = inputMsg.value;

    connection.send("SendPrivateMessage", receiverId, receiverName, msg);

    inputMsg.value = "";
}

function addnewRoom(maxRoom) {

    let createRoomName = document.getElementById('createRoomName');

    var roomName = createRoomName.value;

    if (roomName == null && roomName == '') {
        return;
    }

    /*POST*/
    $.ajax({
        url: '/ChatRooms/PostChatRoom',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: 0, name: roomName }),
        async: true,
        processData: false,
        cache: false,
        success: function (json) {

            connection.invoke("NewRoomCreated", maxRoom, json.id, json.name);
            createRoomName.value = '';

        },
        error: function (xhr) {
            alert('error');
        }
    })



}

function deleteRoom() {
    let ddlDelRoom = document.getElementById('ddlDelRoom');

    var roomId = ddlDelRoom.value;
    var roomName = ddlDelRoom.options[ddlDelRoom.selectedIndex].text;

    if (roomId == null && roomId == '') {
        return;
    }

    if (confirm(`Are you sure you want to delete ${roomName}?`) == false) return;

    $.ajax({
        url: `/ChatRooms/DeleteChatRoom/${roomId}`,
        dataType: "json",
        type: "DELETE",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (json) {
            connection.invoke("RoomDeleted", json.deleted, roomName, json.selected);

        },
        error: function (xhr) {
            alert('error');
        }
    })
}

document.addEventListener('DOMContentLoaded', (event) => {
    fillRoomDropDown();
    fillUserDropDown();
})


function fillUserDropDown() {

    $.getJSON('/ChatRooms/GetChatUser')
        .done(function (json) {

            var ddlSelUser = document.getElementById("ddlSelUser");

            ddlSelUser.innerText = null;

            json.forEach(function (item) {
                var newOption = document.createElement("option");

                newOption.text = item.userName;//item.whateverProperty
                newOption.value = item.id;
                ddlSelUser.add(newOption);


            });

        })
        .fail(function (jqxhr, textStatus, error) {

            var err = textStatus + ", " + error;
            console.log("Request Failed: " + jqxhr.detail);
        });

}

function fillRoomDropDown() {

    $.getJSON('/ChatRooms/GetChatRoom')
        .done(function (json) {
            var ddlDelRoom = document.getElementById("ddlDelRoom");
            var ddlSelRoom = document.getElementById("ddlSelRoom");

            ddlDelRoom.innerText = null;
            ddlSelRoom.innerText = null;

            json.forEach(function (item) {
                var newOption = document.createElement("option");

                newOption.text = item.name;
                newOption.value = item.id;
                ddlDelRoom.add(newOption);


                var newOption1 = document.createElement("option");

                newOption1.text = item.name;
                newOption1.value = item.id;
                ddlSelRoom.add(newOption1);

            });

        })
        .fail(function (jqxhr, textStatus, error) {

            var err = textStatus + ", " + error;
            console.log("Request Failed: " + jqxhr.detail);
        });

}

function addMessage(msg) {
    if (msg == null || msg == "") return;
    let ul = document.getElementById("messagesList");
    let li = document.createElement("li");
    li.innerHTML = msg;
    ul.appendChild(li);
}

connection.start().then(function () {
});
