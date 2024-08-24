$(document).ready(function () {
    $('#subText').on('click', () => {
        var creator = $('#creatorId').val();
        var current = $('#currentUserId').val();
            $.ajax({
                type: 'POST',
                url: "/Subscriber/Sub",
                data: {
                    creator: creator,
                    current: current
                },
                success: function (data) {
                    console.log(data);
                    if (data == "Added") {
                        document.getElementById('subText').innerText = "Ви підписані";
                    } else {
                        document.getElementById('subText').innerText = "Підписатися";
                    }
                    
                }
            });
    });
});

