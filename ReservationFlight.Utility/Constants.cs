namespace ReservationFlight.Utility
{
    public class Constants
    {
        // App Settings
        public const string CONNECTION_STRING = "reservationFlightDb";
        public const string TOKEN = "Token";
        public const string BASE_ADDRESS = "BaseAddress";
        public const string IMAGE_CONTENT_FOLDER_NAME = "image-content";
        public const string BEARER = "Bearer";

        // Display Attribute
        public const string DISPLAY_ATTRIBUTE_USER_NAME_USER = "Tài khoản";
        public const string DISPLAY_ATTRIBUTE_NAME_USER = "Tên";
        public const string DISPLAY_ATTRIBUTE_EMAIL_USER = "Email";
        public const string DISPLAY_ATTRIBUTE_PHONE_NUMBER_USER = "Số điện thoại";
        public const string DISPLAY_ATTRIBUTE_ADDRESS_USER = "Địa chỉ";
        public const string DISPLAY_ATTRIBUTE_PASSWORD_USER = "Mật khẩu";
        public const string DISPLAY_ATTRIBUTE_CURRENT_PASSWORD_USER = "Mật khẩu hiện tại";
        public const string DISPLAY_ATTRIBUTE_NEW_PASSWORD_USER = "Mật khẩu mới";
        public const string DISPLAY_ATTRIBUTE_CONFIRM_PASSWORD_USER = "Mật khẩu xác nhận";

        public const string DISPLAY_ATTRIBUTE_AVIATION_CODE = "Mã đại diện";
        public const string DISPLAY_ATTRIBUTE_NAME_AVIATION = "Tên hãng hàng không";
        public const string DISPLAY_ATTRIBUTE_IMAGE_AVIATION = "Ảnh đại diện";

        public const string DISPLAY_ATTRIBUTE_NAME_AIRPORT = "Tên sân bay";
        public const string DISPLAY_ATTRIBUTE_IATA_AIRPORT = "Mã IATA";
        public const string DISPLAY_ATTRIBUTE_STATUS_AIRPORT = "Tình trạng";

        public const string DISPLAY_ATTRIBUTE_DEPARTURE_ID_FLIGHTROUTE = "Điểm đi";
        public const string DISPLAY_ATTRIBUTE_ARRIVAL_ID_FLIGHTROUTE = "Điểm đến";
        public const string DISPLAY_ATTRIBUTE_STATUS_FLIGHTROUTE = "Tình trạng tuyến bay";

        public const string DISPLAY_ATTRIBUTE_FLIGHT_ROUTE_ID_FLIGHTSCHEDULE = "Mã tuyến bay";
        public const string DISPLAY_ATTRIBUTE_AVIATION_ID_FLIGHTSCHEDULE = "Hãng hàng không";
        public const string DISPLAY_ATTRIBUTE_FLIGHT_NUMBER_FLIGHTSCHEDULE = "Số hiệu chuyến bay";
        public const string DISPLAY_ATTRIBUTE_PRICE_FLIGHTSCHEDULE = "Giá";
        public const string DISPLAY_ATTRIBUTE_DATE_FLIGHTSCHEDULE = "Ngày bay";
        public const string DISPLAY_ATTRIBUTE_SCHEDULED_TIME_DEPARTURE_FLIGHTSCHEDULE = "Dự kiến đi";
        public const string DISPLAY_ATTRIBUTE_SCHEDULED_TIME_ARRIVAL_FLIGHTSCHEDULE = "Dự kiến đến";
        public const string DISPLAY_ATTRIBUTE_SEAT_ECONOMY_FLIGHTSCHEDULE = "Ghế phổ thông";
        public const string DISPLAY_ATTRIBUTE_SEAT_BUSINESS_FLIGHTSCHEDULE = "Ghế thương gia";
        public const string DISPLAY_ATTRIBUTE_STATUS_FLIGHTSCHEDULE = "Tình trạng chuyến";

        // Default Value
        public const int DEFAULT_MAXIMUM_LENGTH_AVIATION_CODE = 3;
        public const int DEFAULT_MAXIMUM_LENGTH_NAME_AVIATION = 60;
        public const int DEFAULT_MAXIMUM_LENGTH_NAME_USER = 200;

        public const int DEFAULT_MAXIMUM_LENGTH_NAME_AIRPORT = 60;
        public const int DEFAULT_MAXIMUM_LENGTH_IATA_CODE = 3;

        public const int DEFAULT_MAXIMUM_LENGTH_DEPARTURE_ID_FLIGHTROUTE = 3;
        public const int DEFAULT_MAXIMUM_LENGTH_ARRIVAL_ID_FLIGHTROUTE = 3;

        public const int DEFAULT_MAXIMUM_LENGTH_PHONE_NUMBER_USER = 11;
        public const int DEFAULT_MAXIMUM_LENGTH_ADDRESS_USER = 500;
        public const int DEFAULT_MINIMUM_LENGTH_PASSWORD_USER = 8;
        public const int DEFAULT_MINIMUM_LENGTH_NEW_PASSWORD_USER = 8;
        public const int DEFAULT_MINIMUM_LENGTH_PHONE_NUMBER_USER = 11;

        public const string DEFAULT_VALUE_ADMIN = "admin";
        public const string DEFAULT_INDEX_PAGE = "Index";
        public const string DEFAULT_HOME_CONTROLLER = "Home";
        public const string DEFAULT_MEDIA_TYPE = "application/json";
        public const string DEFAUT_IMAGE_FILE = "no-image.png";

        public const int JOURNEY_TYPE_ONE_WAY = 1;
        public const int JOURNEY_TYPE_ROUND_TRIP = 2;

        // Flag Value
        public const int FLG_ACTIVE_STATUS = 1;
        public const int FLG_INACTIVE_STATUS = 0;

        // Error Contents
        public const string ERR_EMPTY = "{0} không được để trống";
        public const string ERR_MAXIMUM_LENGTH = "{0} không được quá {1} ký tự";
        public const string ERR_MINIMUM_LENGTH = "{0} phải ít nhất {1} ký tự";
        public const string ERR_FORMAT_EMAIL_USER = "Định dạng Email không đúng";
        public const string ERR_FORMAT_PASSWORD_USER = "Mật khẩu phải bao gồm chữ cái viết hoa, thường và một con số";
        public const string ERR_CONFIRM_PASSWORD_USER = "{0} không khớp với {1} xác nhận";
        public const string ERR_NOT_EXIST = "{0} không tồn tại";
        public const string ERR_EXISTED = "{0} đã tồn tại";
        public const string ERR_NOT_AUTHENTICATION = "Email chưa được xác thực. \nVui lòng xác thực Email của bạn trước khi đăng nhập";
        public const string ERR_DISABLE_ACCOUNT = "Tài khoản của bạn đã bị khóa";
        public const string ERR_WRONG_PASSWORD = "Mật khẩu của bạn không đúng";
        public const string ERR_FAIL_UPDATE = "Cập nhật {0} không thành công";
        public const string ERR_FAIL_RESET_PASSWORD = "Khôi phục mật khẩu không thành công";
        public const string ERR_NOT_FOUND = "Không tìm thấy người dùng có {0} là {1}";
        public const string ERR_FAIL_REGIST = "Đăng ký tài khoản không thành công";
        public const string ERR_FAIL_DELETE = "Xóa {0} không thành công";
        public const string ERR_NOT_PERMIT_TO_ACCESS_PAGE = "Bạn không có quyền truy cập vào trang này";
        public const string ERR_GREATER_THAN = "{0} phải lớn hơn {1}";

        //URL Api

        /// <summary>
        /// API_USER
        /// </summary>
        public const string API_USER = "/api/Users/{0}";
        public const string API_USER_UPDATE = "/api/Users/Update/{0}";
        public const string API_USER_DELETE = "/api/Users/Delete/{0}";
        public const string API_USER_ROLE_ASSIGN = "/api/Users/RoleAssign/{0}";
        public const string API_USER_GET_BY_ID = "/api/Users/GetById/{0}";
        public const string API_USER_GET_BY_USER_NAME = "/api/Users/GetByUserName/{0}";
        public const string API_USER_DISABLE_ACCOUNT = "/api/Users/DisableAccount/{0}";

        /// <summary>
        /// API_AVIATION
        /// </summary>
        public const string API_AVIATION = "/api/Aviations/{0}/";

        /// <summary>
        /// API_AIRPORT
        /// </summary>
        public const string API_AIRPORT = "/api/Airports/{0}/";

        /// <summary>
        /// API_FLIGHTROUTE
        /// </summary>
        public const string API_FLIGHTROUTE = "/api/FlightRoutes/{0}/";

        /// <summary>
        /// API_FLIGHTSCHEDULE
        /// </summary>
        public const string API_FLIGHTSCHEDULE = "/api/FlightSchedules/{0}/";

        public const string API_RESERVATION = "/api/Reservations/{0}/";

        public const string API_STATISTIC = "/api/Statistic/{0}";
    }
}