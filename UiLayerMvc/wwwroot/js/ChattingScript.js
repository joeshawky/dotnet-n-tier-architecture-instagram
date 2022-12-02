


let receiverId = -1;

let updateIntervalId;


let loggedInUserInfo;
let receiverProfilePicturePath;
let mainChatBlock = $('#chat-messages-main-block');
let currentMessages;
let scrollbarBlock = document.getElementById("chat-messages-main-block");

fetch('/api/users/GetCurrentUserInfo')
    .then(res => {
        if (res.ok) {
            return res.json();
        } else {
            console.log("/api/users/getcurrentuserid related error");
        }
    })
    .then(data => {
        loggedInUserInfo = data;
        console.log(data);
    });

function AddMessageFromOwner(username, message, userProfilePicturePath, messageDate) {
    let mainDiv = document.querySelector('#chat-messages-main-block');

    let newDivOne = document.createElement('div');
    newDivOne.className = 'chat-message-right pb-4';
    mainDiv.append(newDivOne);


    let newDivTwo = document.createElement('div');
    newDivOne.append(newDivTwo);

    let newImg = document.createElement('img');
    newImg.src = `${userProfilePicturePath}`
    newImg.className = 'rounded-circle mr-1'
    newImg.width = '40'
    newImg.height = '40'
    newDivTwo.append(newImg);

    let newDivThree = document.createElement('div');
    newDivThree.className = 'text-muted small text-nowrap mt-2';
    newDivThree.insertAdjacentText('afterbegin', `${messageDate}`);
    newDivTwo.append(newDivThree);

    let newDivFour = document.createElement('div');
    newDivFour.className = 'flex-shrink-1 bg-light rounded py-2 px-3 mr-3';

    let newDivFive = document.createElement('div');
    newDivFive.className = 'font-weight-bold mb-1';
    newDivFive.insertAdjacentText('beforeend', `${username}`);
    newDivFour.append(newDivFive);


    let newPre = document.createElement('pre');
    newPre.style.overflowX = 'hidden';
    newPre.style.overflowY = 'hidden';
    newDivFour.append(newPre);

    newPre.insertAdjacentText('beforeend', `${message}`);
    newDivOne.append(newDivFour);

}
function AddMessageFromUser(username, message, userProfilePicturePath, messageDate){

    let mainDiv = document.querySelector('#chat-messages-main-block');

    let newDivOne = document.createElement('div');
    newDivOne.className = 'chat-message-left pb-4';
    mainDiv.append(newDivOne);


    let newDivTwo = document.createElement('div');
    newDivOne.append(newDivTwo);

    let newImg = document.createElement('img');
    newImg.src = `${userProfilePicturePath}`
    newImg.className = 'rounded-circle mr-1'
    newImg.width = '40'
    newImg.height = '40'
    newDivTwo.append(newImg);

    let newDivThree = document.createElement('div');
    newDivThree.className = 'text-muted small text-nowrap mt-2';
    newDivThree.insertAdjacentText('afterbegin', `${messageDate}`);
    newDivTwo.append(newDivThree);

    let newDivFour = document.createElement('div');
    newDivFour.className = 'flex-shrink-1 bg-light rounded py-2 px-3 mr-3';

    let newDivFive = document.createElement('div');
    newDivFive.className = 'font-weight-bold mb-1';
    newDivFive.insertAdjacentText('beforeend', `${username}`);
    newDivFour.append(newDivFive);

    newDivFour.insertAdjacentText('beforeend', `${message}`);
    newDivOne.append(newDivFour)


}

function GetCurrentTime() {
    const date = new Date();

    const currentTime = date.toLocaleTimeString('en-US', {
        // en-US can be set to 'default' to use user's browser settings
        hour: '2-digit',
        minute: '2-digit',
    });

    return currentTime;

}

function AddAllMessages(messages, receiverUsername, receiverProfilePicture) {
    mainChatBlock.empty();

    messages.forEach(m => {
        if (m.sender === receiverUsername.text()) {
            AddMessageFromUser(m.sender, m.message, receiverProfilePicture, m.messageSentDate);
        }
        else {
            AddMessageFromOwner(m.sender, m.message, loggedInUserInfo.profilePicturePath, m.messageSentDate);

        }

    });
}

function findMessageCountElement() {

    let newMessageElements = document.getElementsByClassName('badge');
    let nmElement;
    for (var i = 0; i < newMessageElements.length; i++) {
        let nm = newMessageElements[i];
        let nmId = nm.getAttribute('data-user-id');
        if (nmId == receiverId) {
            nmElement = nm;
        }
    }

    return nmElement;
}

function FilterMessages(currentMessages, newMessages) {

    const resultMessages = [];

    const currentMessagesIds = [];
    currentMessages.forEach(m => currentMessagesIds.push(m.messageId));

    newMessages.forEach(nm => {

        let r = currentMessagesIds.includes(nm.messageId);
        if (r == false) {
            resultMessages.push(nm);
        }
    });



    if (resultMessages.length == 0) {
        return;
    };

    resultMessages.forEach(rm => {
        let messageContent = {
            username: rm.username,
            message: rm.message
        };
        if (rm.sender == loggedInUserInfo.username) {
            return;
        }


        AddMessageFromUser(rm.sender, rm.message, receiverProfilePicturePath, GetCurrentTime());
        currentMessages.push(rm);
        scrollbarBlock.scrollTop = scrollbarBlock.scrollHeight;

    });
}
       

function UpdateMessages(currentMessages, senderId, receiverId) {

    
    const interval = setInterval(function () {
        let chatMessages = fetch(`/api/chats/GetAllMessagesByUsernames?firstUserId=${senderId}&secondUserId=${receiverId}`)
            .then(res => res.json())
            .then(data => {
                FilterMessages(currentMessages, data)
                return data;
            });

        

    }, 1000);
    updateIntervalId = interval;

}

$(document).ready(function () {
    mainChatBlock.empty();


    $('.chat-users-block').on('click', '.chat-user', async function () {


        mainChatBlock.empty();
        
        const chatBlock = $(this);

        const receiverProfilePic = $('#receiver-profile-picture-path');
        const receiverUsername = $('#receiver-username');
        const chatMainBlock = $('#chat-messages-main-block');

        


        let chatBlockUserId = chatBlock.attr('data-user-id');

        receiverId = chatBlockUserId;



        await fetch(`/api/chats/getuserinfo?userId=${chatBlockUserId}`)
            .then(res => res.json())
            .then(data => {
                receiverProfilePic.attr('src', data.profilePicturePath);
                receiverProfilePicturePath = receiverProfilePic.attr('src');

                receiverUsername.text(data.username);
                $('#send-message').attr('data-receiver-id', data.userId);
            });

       

        let chatMessages = await fetch(`/api/chats/GetAllMessagesByUsernames?firstUserId=${loggedInUserInfo.userId}&secondUserId=${receiverId}`)
            .then(res => res.json())
            .then(data => {
                return data;
            });


        document.getElementById("footer-chat-block").style.visibility = "visible";
        document.getElementById("main-page-header2").style.visibility = "visible";

        AddAllMessages(chatMessages, receiverUsername, receiverProfilePic.attr('src'));


        if (typeof updateIntervalId !== "undefined") {
            console.log(`stopping ${updateIntervalId}`);
            clearInterval(updateIntervalId);
        } 


        UpdateMessages(chatMessages, loggedInUserInfo.userId, receiverId);


        
        scrollbarBlock.scrollTop = scrollbarBlock.scrollHeight;
    }); 

    $('#send-message').on('click', async function () {


        let receiverId = $(this).attr('data-receiver-id');

        if (receiverId === '-1')
            return;

        let inputBox = $(this).siblings('#message-contents');
        let message = inputBox.val();

        if (message == null || message.trim() === '')
            return;



        messageObject = {
            senderId: loggedInUserInfo.userId,
            receiverId: receiverId,
            message: message
        };

        await fetch('/api/chats/SendMessage', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(messageObject)
        })
            .then(res => {
                inputBox.val('');
                AddMessageFromOwner(loggedInUserInfo.username, message, loggedInUserInfo.profilePicturePath, GetCurrentTime());
                scrollbarBlock.scrollTop = scrollbarBlock.scrollHeight;

            });

        

    });
});