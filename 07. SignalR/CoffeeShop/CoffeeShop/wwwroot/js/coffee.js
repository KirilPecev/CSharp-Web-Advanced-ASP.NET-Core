﻿let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/coffeehub")
        .build();

    connection.on("ReceiveOrderUpdate",
        (update) => {
            document.getElementById("status").innerHTML = update;
        });

    connection.on("NewOrder",
        function (order) {
            document.getElementById("status").innerHTML = "Someone ordered a " + order.product;
        });


    connection.on("finished",
        function () {
            connection.stop();
        });
};

document.getElementById("submit").addEventListener("click",
    e => {
        e.preventDefault();
        const product = document.getElementById("product").value;
        const size = document.getElementById("size").value;
        fetch("/Coffee/OrderCoffee",
            {
                method: "POST",
                body: JSON.stringify({ product, size }),
                headers: {
                    'content-type': "application/json"
                }
            })
            .then(response => response.text())
            .then(id => connection.start()
                .then(() => {
                    connection.invoke("GetUpdateForOrder", id);
                }));
    });