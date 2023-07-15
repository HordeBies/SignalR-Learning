let lbl_houseJoined = document.getElementById("lbl_houseJoined");

let btn_un_gryffindor = document.getElementById("btn_un_gryffindor");
let btn_un_slytherin = document.getElementById("btn_un_slytherin");
let btn_un_hufflepuff = document.getElementById("btn_un_hufflepuff");
let btn_un_ravenclaw = document.getElementById("btn_un_ravenclaw");
let btn_gryffindor = document.getElementById("btn_gryffindor");
let btn_slytherin = document.getElementById("btn_slytherin");
let btn_hufflepuff = document.getElementById("btn_hufflepuff");
let btn_ravenclaw = document.getElementById("btn_ravenclaw");

let trigger_gryffindor = document.getElementById("trigger_gryffindor");
let trigger_slytherin = document.getElementById("trigger_slytherin");
let trigger_hufflepuff = document.getElementById("trigger_hufflepuff");
let trigger_ravenclaw = document.getElementById("trigger_ravenclaw");

var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/houses").build();

btn_gryffindor.addEventListener("click", () => JoinHouse("Gryffindor"));
btn_hufflepuff.addEventListener("click", () => JoinHouse("Hufflepuff"));
btn_ravenclaw.addEventListener("click", () => JoinHouse("Ravenclaw"));
btn_slytherin.addEventListener("click", () => JoinHouse("Slytherin"));

btn_un_gryffindor.addEventListener("click", () => LeaveHouse("Gryffindor"));
btn_un_hufflepuff.addEventListener("click", () => LeaveHouse("Hufflepuff"));
btn_un_ravenclaw.addEventListener("click", () => LeaveHouse("Ravenclaw"));
btn_un_slytherin.addEventListener("click", () => LeaveHouse("Slytherin"));

trigger_gryffindor.addEventListener("click", () => triggerNotification("Gryffindor"));
trigger_hufflepuff.addEventListener("click", () => triggerNotification("Hufflepuff"));
trigger_ravenclaw.addEventListener("click", () => triggerNotification("Ravenclaw"));
trigger_slytherin.addEventListener("click", () => triggerNotification("Slytherin"));

function JoinHouse(house) {
    connection.invoke("JoinHouse", house).catch(err => console.error(err.toString()));
}
function LeaveHouse(house) {
    connection.invoke("LeaveHouse", house).catch(err => console.error(err.toString()));
}

connection.on("subscriptionStatus", (strGroupsJoined, houseName, hasSubscribed) => {
    lbl_houseJoined.innerText = strGroupsJoined;
    if (hasSubscribed) {
        switch (houseName) {
            case "Gryffindor":
                btn_gryffindor.style.display = "none";
                btn_un_gryffindor.style.display = "block";
                break;
            case "Hufflepuff":
                btn_hufflepuff.style.display = "none";
                btn_un_hufflepuff.style.display = "block";
                break;
            case "Ravenclaw":
                btn_ravenclaw.style.display = "none";
                btn_un_ravenclaw.style.display = "block";
                break;
            case "Slytherin":
                btn_slytherin.style.display = "none";
                btn_un_slytherin.style.display = "block";
                break;
            default:
                break;
        }
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: `You have joined to <b>${houseName}</b>`,
            showConfirmButton: false,
            timer: 1500
        });
    } else {
        switch (houseName) {
            case "Gryffindor":
                btn_gryffindor.style.display = "block";
                btn_un_gryffindor.style.display = "none";
                break;
            case "Hufflepuff":
                btn_hufflepuff.style.display = "block";
                btn_un_hufflepuff.style.display = "none";
                break;
            case "Ravenclaw":
                btn_ravenclaw.style.display = "block";
                btn_un_ravenclaw.style.display = "none";
                break;
            case "Slytherin":
                btn_slytherin.style.display = "block";
                btn_un_slytherin.style.display = "none";
                break;
            default:
                break;
        }
        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: `You have leaved from <b>${houseName}</b>`,
            showConfirmButton: false,
            timer: 1500
        });
    }
})

connection.on("userJoined", (houseName, userName) => {
    Swal.fire({
        position: 'top-end',
        icon: 'info',
        title: `<b class='text-center'>${houseName}</b><br>New user: <b>${userName}</b><br>joined to house`,
        showConfirmButton: false,
        timer: 1500
    });
});
connection.on("userLeaved", (houseName, userName) => {
    Swal.fire({
        position: 'top-end',
        icon: 'info',
        title: `<b class='text-center'>${houseName}</b><br>User: <b>${userName}</b> leaved`,
        showConfirmButton: false,
        timer: 1500
    });
});
connection.on("houseNotification", (userName, houseName, message) => {
    Swal.fire({
        position: 'top-end',
        icon: 'info',
        title: `<b class='text-center'>${houseName}</b><br> From: <b>${userName}</b><br>Message: <b>${message}</b>`,
        showConfirmButton: false,
        timer: 1500
    });
});

function triggerNotification(houseName) {
    connection.send("SendNotification", houseName, "Hello Guys!");
}
function fulfilled() {
    console.log("Connection to User Hub Successfull");
}
function rejected(reason) {
    console.log("Connection to User Hub Failed because of: " + reason.toString());
}
connection.start().then(fulfilled, rejected);