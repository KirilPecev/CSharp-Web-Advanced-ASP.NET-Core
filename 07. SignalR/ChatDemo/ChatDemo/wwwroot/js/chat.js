var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

// The Receive Message Client event. This will trigger when the Back-End calls the ReceiveMessage method
connection.on("ReceiveMessage",
    function (user, message) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt").replace(/>/g, "&gt");
        var encodedMessage = "[" + user + "]: " + msg;
        var messageElement = document.createElement("h3");
        messageElement.textContent = encodedMessage;

        document.getElementById("messageList").appendChild(messageElement);
    });

//An error handler for connection errors
connection.start().catch(function (err) {
    return console.error(err.toString());
});

//The Send message DOM event. This will trigger the Back-End SendMessage method
document.getElementById("sendButton").addEventListener("click", function(event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", user, message).catch(function(err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});