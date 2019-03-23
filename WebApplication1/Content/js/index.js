

function guiTinNhan(tenDangNhap) {

}

$(".chat").hide();

$(document).ready(() => {
    let chat = $.connection.chatHub;
    chat.client.receiveMessage = (from, message) => {

    };

    $.connection.hub.start().done(() => {
        alert("done");
    });
});
