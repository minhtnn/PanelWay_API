namespace PanelWay_Backend.API.Constants;

public static class MessageConstant
{
    public static class PanelWaySystem
    {
        public const string SystemError = "Đã xảy ra lỗi ở hệ thống!";

        public const string ForeignKeyError =
            "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại thông tin liên kết giữa các bảng.";

        public const string EmptyField = "Một số trường dữ liệu bị trống.";
    }
    public static class Authentication
    {
        public const string InvalidUsernameOrPassword = "Tên đăng nhập hoặc mật khẩu không chính xác";
        public const string DeactivatedAccount = "Tài khoản đang bị vô hiệu hoá";
        public const string ExistEmailOrPhone = "Email hoặc số điện thoại đã được sử dụng";
        public const string UpdatePasswordSuccess = "Cập nhật mật khẩu thành công";
        public const string UpdatePasswordFail = "Cập nhật mật khẩu thất bại";
    }
    
    public static class Account
    {
        public const string EmptyAccountId = "Account id bị trống";
        public const string NotFindAccount = "Account không xác định";
        public const string CreateAccountSuccess = "Tạo Account thành công";
        public const string UpdateAccountSuccess = "Cập nhật Account thành công";
        public const string CreateAccountFail = "Tạo Account thất bại";
        public const string UpdateAccountFail  = "Cập nhật Account thất bại";
    }
    public static class AdContent
    {
        public const string EmptyAdContentCode = "AdContent code bị trống";
        public const string ExistAdContentCode = "AdContent code đã tồn tại";
        public const string NotFindAdContent = "AdContent không xác định";
        public const string CreateAdContentSuccess = "Tạo AdContent thành công";
        public const string UpdateAdContentSuccess = "Cập nhật AdContent thành công";
        public const string CreateAdContentFail = "Tạo AdContent thất bại";
        public const string UpdateAdContentFail  = "Cập nhật AdContent thất bại";
    }
    public static class Appointment
    {
        public const string EmptyAppointmentId = "Appointment id bị trống";
        public const string ExistAppointmentCode = "Appointment code đã tồn tại";
        public const string NotFindAppointment = "Appointment không xác định";
        public const string CreateAppointmentSuccess = "Tạo Appointment thành công";
        public const string UpdateAppointmentSuccess = "Cập nhật Appointment thành công";
        public const string CreateAppointmentFail = "Tạo Appointment thất bại";
        public const string UpdateAppointmentFail  = "Cập nhật Appointment thất bại";
    }
    public static class AppointmentHistory
    {
        public const string EmptyAppointmentHistoryId = "Appointment history id bị trống";
        public const string NotFindAppointmentHistory = "Appointment history không xác định";
        public const string CreateAppointmentHistorySuccess = "Tạo Appointment history thành công";
        public const string UpdateAppointmentHistorySuccess = "Cập nhật Appointment history thành công";
        public const string CreateAppointmentHistoryFail = "Tạo Appointment history thất bại";
        public const string UpdateAppointmentHistoryFail  = "Cập nhật Appointment history thất bại";
    }
    public static class PanelType
    {
        public const string EmptyPanelTypeId = "Panel type id bị trống";
        public const string ExistPanelTypeId = "PanelType id đã tồn tại";
        public const string NotFindPanelType = "Panel type không xác định";
        public const string CreatePanelTypeSuccess = "Tạo Panel type thành công";
        public const string UpdatePanelTypeSuccess = "Cập nhật Panel type thành công";
        public const string CreatePanelTypeFail = "Tạo Panel type thất bại";
        public const string UpdatePanelTypeFail  = "Cập nhật Panel type thất bại";
    }
    public static class Payment
    {
        public const string EmptyPaymentId = "Payment id bị trống";
        public const string NotFindPayment = "Payment không xác định";
        public const string CreatePaymentSuccess = "Tạo Payment thành công";
        public const string UpdatePaymentSuccess = "Cập nhật Payment thành công";
        public const string CreatePaymentFail = "Tạo Payment thất bại";
        public const string UpdatePaymentFail  = "Cập nhật Payment thất bại";
    }
    public static class PaymentType
    {
        public const string EmptyPaymentTypeId = "Payment type id bị trống";
        public const string NotFindPaymentType = "Payment type không xác định";
        public const string CreatePaymentTypeSuccess = "Tạo Payment type thành công";
        public const string UpdatePaymentTypeSuccess = "Cập nhật Payment type thành công";
        public const string CreatePaymentTypeFail = "Tạo Payment type thất bại";
        public const string UpdatePaymentTypeFail  = "Cập nhật Payment type thất bại";
    }
    public static class RegulatoryApproval
    {
        public const string EmptyRegulatoryApprovalId = "Regulatory approval id bị trống";
        public const string NotFindRegulatoryApproval = "Regulatory approval không xác định";
        public const string CreateRegulatoryApprovalSuccess = "Tạo Regulatory approval thành công";
        public const string UpdateRegulatoryApprovalSuccess = "Cập nhật Regulatory approval thành công";
        public const string CreateRegulatoryApprovalFail = "Tạo Regulatory approval thất bại";
        public const string UpdateRegulatoryApprovalFail  = "Cập nhật Regulatory approval thất bại";
    }
    public static class RegulatoryLicense
    {
        public const string EmptyRegulatoryLicenseId = "Regulatory license id bị trống";
        public const string NotFindRegulatoryLicense = "Regulatory license không xác định";
        public const string CreateRegulatoryLicenseSuccess = "Tạo Regulatory license thành công";
        public const string UpdateRegulatoryLicenseSuccess = "Cập nhật Regulatory license thành công";
        public const string CreateRegulatoryLicenseFail = "Tạo Regulatory license thất bại";
        public const string UpdateRegulatoryLicenseFail  = "Cập nhật Regulatory license thất bại";
    }
    public static class RentalLocation
    {
        public const string EmptyRentalLocationId = "Rental location id bị trống";
        public const string NotFindRentalLocation = "Rental location không xác định";
        public const string CreateRentalLocationSuccess = "Tạo Rental location thành công";
        public const string UpdateRentalLocationSuccess = "Cập nhật Rental location thành công";
        public const string CreateRentalLocationFail = "Tạo Rental location thất bại";
        public const string UpdateRentalLocationFail  = "Cập nhật Rental location thất bại";
    }
    public static class RentalLocationPanelType
    {
        public const string EmptyRentalLocationPanelTypeId = "Rental  location panel type id bị trống";
        public const string NotFindRentalLocationPanelType = "Rental  location panel type không xác định";
        public const string ExistRentalLocationPanelTypeId = "Rental  location panel type id đã tồn tại";
        public const string CreateRentalLocationPanelTypeSuccess = "Tạo Rental  location panel type thành công";
        public const string UpdateRentalLocationPanelTypeSuccess = "Cập nhật Rental  location panel type thành công";
        public const string CreateRentalLocationPanelTypeFail = "Tạo Rental  location panel type thất bại";
        public const string UpdateRentalLocationPanelTypeFail  = "Cập nhật Rental  location panel type thất bại";
    }
    public static class Subscription
    {
        public const string EmptySubscriptionId = "Subscription id bị trống";
        public const string NotFindSubscription = "Subscription không xác định";
        public const string CreateSubscriptionSuccess = "Tạo Subscription thành công";
        public const string UpdateSubscriptionSuccess = "Cập nhật Subscription thành công";
        public const string CreateSubscriptionFail = "Tạo Subscription thất bại";
        public const string UpdateSubscriptionFail  = "Cập nhật Subscription thất bại";
    }
    public static class Transaction
    {
        public const string EmptyTransactionId = "Transaction id bị trống";
        public const string NotFindTransaction = "Transaction không xác định";
        public const string CreateTransactionSuccess = "Tạo Transaction thành công";
        public const string UpdateTransactionSuccess = "Cập nhật Transaction thành công";
        public const string CreateTransactionFail = "Tạo Transaction thất bại";
        public const string UpdateTransactionFail  = "Cập nhật Transaction thất bại";
    }
    public static class User
    {
        public const string EmptyUserId = "User id bị trống";
        public const string NotFindUser = "User không xác định";
        public const string CreateUserSuccess = "Tạo User thành công";
        public const string UpdateUserSuccess = "Cập nhật User thành công";
        public const string CreateUserFail = "Tạo User thất bại";
        public const string UpdateUserFail  = "Cập nhật User thất bại";
    }
    public static class UserSubscription
    {
        public const string EmptyUserSubscriptionId = "User subscription id bị trống";
        public const string NotFindUserSubscription = "User subscription không xác định";
        public const string CreateUserSubscriptionSuccess = "Tạo User subscription thành công";
        public const string UpdateUserSubscriptionSuccess = "Cập nhật User subscription thành công";
        public const string CreateUserSubscriptionFail = "Tạo User subscription thất bại";
        public const string UpdateUserSubscriptionFail  = "Cập nhật User subscription thất bại";
    }
}