function deleteUser(userName) {
    $.ajax({
        url: "/admin/home/delete/" + userName,
        method: "DELETE",
        success: function (data) {
            if (data) {
                $(document.getElementById(userName)).remove();
            }
            else {
                alert("Can't delete item");
            }            
        },
        error: function() {
            alert("Can't delete item");
        }
    })
}
