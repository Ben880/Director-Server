"use strict";

var buttonTracker = {};

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/messages")
    .build();

connection.start().catch(function(err) {
    return console.error(err.toString());
});

connection.on("CommandUpdate", function(message) {
    var commands = message.split(",");
    var ids = commands.map(command => `${command.split(' ').join('')}`).filter(command => command !== '');
    Object.keys(buttonTracker).forEach(key => {
        if (ids.indexOf(key) < 0) {
            buttonTracker[key].remove();
        }
    });
    var buttunsDiv = document.getElementById("buttons");
    commands.forEach(element => {
        if (element != "")
        {
            var id = `${element.split(' ').join('')}`;
            if (buttonTracker[id]) {
                return;
            }
            var button = document.createElement("button");
            button.id = id;
            button.value = element;
            button.innerText = element;
            buttunsDiv.appendChild(button);
            var jq = $(`#${button.id}`);
            buttonTracker[id] = jq;
            jq.click(() => {
                connection.invoke("ClickedCommand", element).catch(function (err) {
                    return console.error(err.toString());
                });
                event.preventDefault();
            });
        }
    });
    connection.invoke("SendCommandsToUser").catch(function (err) {
        return console.error(err.toString());//if there is a transmission error it will stop forever
    });
});

connection.on("UserConnected", function(connectionId) {
    connection.invoke("SendDataToUser").catch(function (err) {
        return console.error(err.toString());//if there is a transmission error it will stop forever
    });
    connection.invoke("SendCommandsToUser").catch(function (err) {
        return console.error(err.toString());//if there is a transmission error it will stop forever
    });
});

document.getElementById("JoinGroup").addEventListener("click", function(event) {
    var group = document.getElementById("GroupText").value;
    Object.keys(buttonTracker).forEach(key => {
            buttonTracker[key].remove();
    });
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
    var div = document.createElement("div");
    div.innerHTML = message + "<hr/>";
    document.getElementById("data").innerHTML = "";
    document.getElementById("data").appendChild(div);
    connection.invoke("SendDataToUser").catch(function (err) {
        return console.error(err.toString());
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

connection.on("ReturnClicked", function(message) {
});




