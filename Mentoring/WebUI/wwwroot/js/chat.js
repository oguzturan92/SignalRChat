"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start();

// CLIENT EKLER
window.addEventListener("load", function (event) {
    setTimeout(() => {
        var userName = document.getElementById("senderUserName").value;
        connection.invoke("GetNickName", userName);
        event.preventDefault();
        
    }, 500);
});

// YENİ MESAJ GELDİĞİNDE, KONUŞMALAR LİSTESİNDE ESKİ BOX'I SİLER VE YENİ BOX EKLER
connection.on("Notification", function (newMessageChatId, message, fullname, dateTimeTime, dateTimeDate) {
    var chatBoxList = document.querySelectorAll(".notification");
    chatBoxList.forEach(element => {
        if (element.getAttribute("value") == newMessageChatId) {
            element.style.backgroundColor = "#F3D5D2";
            
            var userImage = element.children[0].children[0].getAttribute("alt");

            let chatLineMessageMavi = `<div style="position:relative;">
                <a style="background-color: rgb(243, 213, 210);" class="list-group-item list-group-item-action list-group-item-light rounded-0 notification" value="${newMessageChatId}" href="/Chat/Index/${newMessageChatId}">
                    <div class="media"><img src="/img/${userImage}" alt="${userImage}" width="50" class="rounded-circle">
                        <div class="media-body ml-4">
                            <div class="d-flex align-items-center justify-content-between mb-1">
                                <h6 class="mb-0"> ${fullname} </h6>
                                <small class="small font-weight-bold"> ${dateTimeDate} | ${dateTimeTime} </small>
                            </div>
                            <p class="font-italic mb-0 text-small">${message}</p>
                        </div>
                    </div>
                </a>
                <i value="${newMessageChatId}" style="position: absolute; bottom: 10px; right: 50px; z-index: 10; color: #ff0000;" class="fa-solid fa-bell fa-beat"></i>
                <form style="position: absolute; bottom: 10px; right: 15px; z-index: 10;" method="post" action="/Chat/ChatDelete?chatId=1">
                    <button class="btn btn-light" style="padding: 2px 4px; font-size: 12px;" type="submit">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </form>
            </div>`
            document.getElementById("messageBoxContainer").insertAdjacentHTML("afterbegin", chatLineMessageMavi);

            element.parentElement.innerHTML = "";
        }
    });
});

// GELEN MESAJI MESAJ İÇERİĞİNE AKTARIR
connection.on("ReceiveMessage", function (message, dateTimeTime, dateTimeDate, userName) {
    if (userName == document.getElementById("senderUserName").value) {
        let chatLineMessageMavi = `<div class="media w-50 ml-auto mb-3">
            <div class="media-body">
                <div class="bg-primary rounded py-2 px-3 mb-2">
                    <p class="text-small mb-0 text-white">${message}</p>
                </div>
                <p class="small text-muted"> ${dateTimeDate} | ${dateTimeTime} </p>
            </div>
        </div>`
        document.getElementById("messagesList").insertAdjacentHTML("beforeend", chatLineMessageMavi);
    } else
    {
        let chatLineMessageLight = `<div class="media w-50 mb-3">
            <div class="media-body ml-3">
                <div class="bg-light rounded py-2 px-3 mb-2">
                    <p class="text-small mb-0 text-muted">${message}</p>
                </div>
                <p class="small text-muted"> ${dateTimeTime} | ${dateTimeDate}</p>
            </div>
        </div>`
        document.getElementById("messagesList").insertAdjacentHTML("beforeend", chatLineMessageLight);
    }
});

// GELEN BOX RENGİNİ ESKİ HALİNE GETİRİR
document.getElementById("messageInput").addEventListener("focus", function (event) {
    var chatId = document.getElementById("focusChatId").value;
    var bells = document.querySelectorAll(".fa-bell");
    bells.forEach(element => {
        if (element.getAttribute("value") == chatId)
        {
            element.previousElementSibling.removeAttribute("style");
            element.remove();
        }
    });
    connection.invoke("MessageRead", chatId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// SEND BUTTON
document.getElementById("sendButton").addEventListener("click", function (event) {
    var senderUserName = document.getElementById("senderUserName").value;
    var focusChatId = Number(document.getElementById("focusChatId").value);    
    var message = document.getElementById("messageInput").value;
    var receiverUserId = Number(document.getElementById("receiverUserId").value);
    document.getElementById("messageInput").value = "";
    connection.invoke("SendMessage", message, senderUserName, focusChatId, receiverUserId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// GRUP OLUŞTURUR
window.addEventListener("load", function (event) {
    setTimeout(() => {
        var btnOdaOlustur = document.querySelector(".btnOdaOlustur");
        btnOdaOlustur.classList.remove("btnOdaOlustur");
        var groupName = document.getElementById("focusChatId").value;
        connection.invoke("AddGroup", groupName).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    }, 750);
});