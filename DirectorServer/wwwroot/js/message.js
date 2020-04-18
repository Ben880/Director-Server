﻿"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/messages")
    .build();
//================================================================
//==================== Unused Example Methods ====================
//====================== (See next section) ======================
//================================================================

connection.on("ReceiveMessage", function(message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var div = document.createElement("div");
    div.innerHTML = msg + "<hr/>";
    document.getElementById("messages").appendChild(div);
});
connection.on("UserDisconnected", function(connectionId) {
    var groupElement = document.getElementById("group");
    for(var i = 0; i < groupElement.length; i++) {
        if (groupElement.options[i].value == connectionId) {
            groupElement.remove(i);
        }
    }
});
connection.start().catch(function(err) {
    return console.error(err.toString());
});
/*
document.getElementById("sendButton").addEventListener("click", function(event) {
    var message = document.getElementById("message").value;
    var groupElement = document.getElementById("group");
    var groupValue = groupElement.options[groupElement.selectedIndex].value;

    if (groupValue === "All" || groupValue === "Myself") {
        var method = groupValue === "All" ? "SendMessageToAll" : "SendMessageToCaller";
        connection.invoke(method, message).catch(function (err) {
            return console.error(err.toString());
        });
    } else if (groupValue === "PrivateGroup") {
        connection.invoke("SendMessageToGroup", "PrivateGroup", message).catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        connection.invoke("SendMessageToUser", groupValue, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    event.preventDefault();
});
 */
//==========================================================
//==================== My Methods ==========================
//==========================================================
connection.on("UserConnected", function(connectionId) {
    connection.invoke("SendDataToUser").catch(function (err) {
        return console.error(err.toString());//if there is a transmission error it will stop forever
    });
});

document.getElementById("JoinGroup").addEventListener("click", function(event) {
    var group = document.getElementById("GroupText").value;
    connection.invoke("JoinGroup", group).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("SendData").addEventListener("click", function(event) {
    connection.invoke("SendDataToUser").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.on("DataUpdate", function(message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var div = document.createElement("div");
    div.innerHTML = msg + "<hr/>";
    document.getElementById("data").innerHTML = "";
    document.getElementById("data").appendChild(div);
    connection.invoke("SendDataToUser").catch(function (err) {
        return console.error(err.toString());//if there is a transmission error it will stop forever
    });
});

connection.on("AddGroup", function(groupID) {
    var groupElement = document.getElementById("group");
    var option = document.createElement("option");
    option.text = groupID;
    option.value = groupID;
    groupElement.add(option);
});

connection.on("RemoveGroup", function(groupID) {
    var groupElement = document.getElementById("group");
    for(var i = 0; i < groupElement.length; i++) {
        if (groupElement.options[i].value == groupID) {
            groupElement.remove(i);
        }
    }
});




