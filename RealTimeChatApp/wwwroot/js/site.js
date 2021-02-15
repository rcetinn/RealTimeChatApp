$(function () {


    var $nickName = "";
    var $message = $("#message");
    var $sendButton = $("#sendmessage");
    var $chatList = $("#chat");
 

    var signalRConnection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
    signalRConnection.on("ReceiveMessage", function (nickName, channelId, message) {
        if ($("#channels").find(":selected").val() == channelId) {
            $chatList.append("<strong>" + nickName + ":</strong>" + message + "</br>");
        }
       
    });

    $sendButton.click(function () {

        var _nickName = $nickName;
        var _message = $message.val();
        var _channelId = $("#channels").find(":selected").val();

        $message.val("");
        signalRConnection.invoke("SendMessage", _nickName, _channelId , _message);
  

        $.ajax({
            type:'GET',
            url: "/Home/SaveMessage",
            data: { channelId:_channelId, nickName: _nickName, message : _message },
            success: function () {
            },
            complete: function () {
            },
            failure: function () {
                alert("Failed!");

            }
        });
    });

    signalRConnection.start({ pingInterval: 6000 }).then(function () {

    }).catch(function (err) {
        return console.error(err.toString());
    });

    $("#nickNameText").change(function () {
        if ($("#nickNameText").val().trim() != "") {
            $nickName = $("#nickNameText").val();
            $("#channels").prop('disabled', false);
            $("#nickNameText").prop('disabled', true);
        }
        else {
            $("#channels").prop('disabled', true);
        }
    });


    $("#loginButton").click(function () {

        var _channelId = $("#channels").find(":selected").val();
        if ($("#channels").find(":selected").val()) {
            $("#message").prop('disabled', false);
            $("#sendmessage").prop('disabled', false);

            $.ajax({
                type: 'POST',
                url: "/Home/GetMessageHistoryByChannelId",
                data: { channelId: _channelId },
                success: function (result) {
                    $chatList.html("");

                    for (var i = 0; i < result.length; i++)
                    {
                        $chatList.append("<strong>" + result[i].nickName + ":</strong>" + result[i].message + "</br>");
                    }
                },
                complete: function () {
                },
                failure: function () {
                    alert("Failed!");

                }

            });
        }
        else {
            $("#message").prop('disabled', true);
            $("#sendmessage").prop('disabled', true);
        }
    });

})