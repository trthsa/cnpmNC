var AviationController = function () {
    this.initialize = function () {
        $('.btn-aviation').click(function () {
            window.location.href = $(this).data('url');
        });
    }
}

var AirportController = function () {
    this.initialize = function () {
        registerEvent();
        $('.btn-airport').click(function () {
            window.location.href = $(this).data('url');
        });
    }

    function registerEvent() {
        $('body').on('click', '.btn-airport-status', function (e) {
            e.preventDefault();
            const IATA = $(this).data('id');
            updateStatus(IATA);
        });
    }

    function updateStatus(IATA) {
        $.ajax({
            type: 'POST',
            url: '/Airport/EditStatus',
            data: {
                IATA: IATA
            },
            success: function (res) {
                localStorage.setItem("EditStatus", true);
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
        if (localStorage.getItem("EditStatus")) {
            localStorage.removeItem("EditStatus");
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Cập nhật tình trạng sân bay thành công',
                showConfirmButton: false,
                timer: 1500,
            })
        }
    });
}

var FlightRouteController = function () {
    this.initialize = function () {
        loadStatus();
        registerEvent();      
        $('.btn-flightroute').click(function () {
            window.location.href = $(this).data('url');
        });
    }

    function loadStatus() {
        var html = '';
        $.ajax({
            type: 'POST',
            url: '/FlightRoute/LoadStatus',
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.value + '">' + item.text + '</option>'
                    });
                    $('#ddlStatus').html(html);
                }
            }
        });
    }

    function registerEvent() {
        $('body').on('click', '.btn-flightroute-status', function (e) {
            e.preventDefault();
            const Id = $(this).data('id');
            updateStatus(Id);
        });
    }

    function updateStatus(Id) {
        $.ajax({
            type: 'POST',
            url: '/FlightRoute/EditStatus',
            data: {
                Id: Id
            },
            success: function (res) {
                localStorage.setItem("EditStatus", true);
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
        if (localStorage.getItem("EditStatus")) {
            localStorage.removeItem("EditStatus");
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Cập nhật tình trạng tuyến bay thành công',
                showConfirmButton: false,
                timer: 1500,
            })
        }
    });
}

var FlightScheduleController = function () {
    this.initialize = function () {
        loadStatus();
        registerEvent();
        $('.btn-flightschedule').click(function () {
            window.location.href = $(this).data('url');
        });
    }

    function loadStatus() {
        var html = '';
        $.ajax({
            type: 'POST',
            url: '/FlightSchedule/LoadStatus',
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.value + '">' + item.text + '</option>'
                    });
                    $('#ddlStatus').html(html);
                }
            }
        });
    }

    function registerEvent() {
        $('body').on('click', '.btn-flightschedule-status', function (e) {
            e.preventDefault();
            const Id = $(this).data('id');
            updateStatus(Id);
        });
    }

    function updateStatus(Id) {
        $.ajax({
            type: 'POST',
            url: '/FlightSchedule/EditStatus',
            data: {
                Id: Id
            },
            success: function (res) {
                localStorage.setItem("EditStatus", true);
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
        if (localStorage.getItem("EditStatus")) {
            localStorage.removeItem("EditStatus");
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Cập nhật tình trạng tuyến bay thành công',
                showConfirmButton: false,
                timer: 1500,
            })
        }
    });
}