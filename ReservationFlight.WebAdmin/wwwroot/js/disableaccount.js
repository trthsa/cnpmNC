var UserController = function () {
    this.initialize = function () {
        registerEvent();
        $('.btn-user').click(function () {
                window.location.href = $(this).data('url');
        });
    }

    function registerEvent() {
        $('body').on('click', '.btn-user-status', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            browserReview(id);
        });
    }

    function browserReview(id) {
        $.ajax({
            type: 'POST',
            url: '/User/DisableAccount',
            data: {
                id: id
            },
            success: function (res) {
                localStorage.setItem("disableAccount", true);
                location.reload();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    $(document).ready(function () {
        // This function will run on every page reload, but the alert will only 
        // happen on if the buttonClicked variable in localStorage == true
        if (localStorage.getItem("disableAccount")) {
            localStorage.removeItem("disableAccount");
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Cập nhật trạng thái tài khoản thành công',
                showConfirmButton: false,
                timer: 1500,
            })
        }
    });
}