namespace PanelWay_Backend.API.Constants;

public static class ApiEndpointConstant
{
    private const string RootEndPoint = "/api";
    private const string ApiVersion = "/v1";
    public const string ApiEndpoint = RootEndPoint + ApiVersion;
    
    public static class Authentication
    {
        public const string AuthenticationEndpoint = ApiEndpoint + "/auth";
        public const string Login = AuthenticationEndpoint + "/login";
        public const string SignUp = AuthenticationEndpoint + "/sign-up";
        public const string UpdatePassword = AuthenticationEndpoint + "/change-pass";
    }
    
    public static class Account
    {
        private const string AccountEndpoint = "/accounts";
        public const string AccountApiEndpoint = ApiEndpoint + AccountEndpoint;
        public const string FindAccountByIdApiEndpoint = AccountApiEndpoint + "/{id}";
        public const string FindAccountByUserIdApiEndpoint = AccountApiEndpoint + "/user/{id}";
    }
    public static class AdContent
    {
        private const string AdContentEndpoint = "/ad-contents";
        public const string AdContentApiEndpoint = ApiEndpoint + AdContentEndpoint;
        public const string FindAdContentByIdApiEndpoint = AdContentApiEndpoint + "/{id}";
        public const string FindAdContentByAdvertisingClientIdApiEndpoint = AdContentApiEndpoint + "/accounts/{id}";
    }
    public static class Appointment
    {
        private const string AppointmentEndpoint = "/appointments";
        public const string AppointmentApiEndpoint = ApiEndpoint + AppointmentEndpoint;
        public const string FindAppointmentByIdApiEndpoint = AppointmentApiEndpoint + "/{id}";
        public const string FindAppointmentByRentalLocationIdApiEndpoint = AppointmentApiEndpoint + "/rental-location/{id}";
    }
    public static class AppointmentHistory
    {
        private const string AppointmentHistoryEndpoint = "/appointment-histories";
        public const string AppointmentHistoryApiEndpoint = ApiEndpoint + AppointmentHistoryEndpoint;
        public const string FindAppointmentHistoryByIdApiEndpoint = AppointmentHistoryApiEndpoint + "/{id}";
        public const string FindAppointmentHistoryByAppointmentIdApiEndpoint = AppointmentHistoryApiEndpoint + "/appointment/{id}";
    }
    public static class PanelType
    {
        private const string PanelTypeEndpoint = "/panel-types";
        public const string PanelTypeApiEndpoint = ApiEndpoint + PanelTypeEndpoint;
        public const string FindAppointmentByIdApiEndpoint = PanelTypeApiEndpoint + "/{id}";
    }
    public static class Payment
    {
        private const string PaymentEndpoint = "/payments";
        public const string PaymentApiEndpoint = ApiEndpoint + PaymentEndpoint;
        public const string FindPaymentByIdApiEndpoint = PaymentApiEndpoint + "/{id}";
    }
    public static class PaymentType
    {
        private const string PaymentTypeEndpoint = "/payment-types";
        public const string PaymentTypeApiEndpoint = ApiEndpoint + PaymentTypeEndpoint;
        public const string FindPaymentTypeByIdApiEndpoint = PaymentTypeApiEndpoint + "/{id}";
    }
    public static class RegulatoryApproval
    {
        private const string RegulatoryApprovalEndpoint = "/regulatory-approvals";
        public const string RegulatoryApprovalApiEndpoint = ApiEndpoint + RegulatoryApprovalEndpoint;
        public const string FindRegulatoryApprovalByIdApiEndpoint = RegulatoryApprovalApiEndpoint + "/{id}";
        public const string FindRegulatoryApprovalByRentalLocationIdApiEndpoint = RegulatoryApprovalApiEndpoint + "/rental-location/{id}";
    }
    public static class RegulatoryLicense
    {
        private const string RegulatoryLicenseEndpoint = "/regulatory-licenses";
        public const string RegulatoryLicenseApiEndpoint = ApiEndpoint + RegulatoryLicenseEndpoint;
        public const string FindRegulatoryLicenseByIdApiEndpoint = RegulatoryLicenseApiEndpoint + "/{id}";
        public const string FindRegulatoryLicenseByRegulatoryApproveIdApiEndpoint = RegulatoryLicenseApiEndpoint + "/regulatory-approve/{id}";
    }
    public static class RentalLocation
    {
        private const string RentalLocationEndpoint = "/rental-locations";
        public const string RentalLocationApiEndpoint = ApiEndpoint + RentalLocationEndpoint;
        public const string FindRentalLocationByIdApiEndpoint = RentalLocationApiEndpoint + "/{id}";
    }
    public static class RentalLocationPanelType
    {
        private const string RentalLocationPanelTypeEndpoint = "/rental-location-panel-types";
        public const string RentalLocationPanelTypeApiEndpoint = ApiEndpoint + RentalLocationPanelTypeEndpoint;
        public const string FindRentalLocationPanelTypeByRentalLocationIdApiEndpoint = RentalLocationPanelTypeApiEndpoint + "/rental-location/{id}";
    }
    public static class Subscription
    {
        private const string SubscriptionEndpoint = "/subscriptions";
        public const string SubscriptionApiEndpoint = ApiEndpoint + SubscriptionEndpoint;
        public const string FindSubscriptionByIdApiEndpoint = SubscriptionApiEndpoint + "/{id}";
    }
    public static class Transaction
    {
        private const string TransactionEndpoint = "/transactions";
        public const string TransactionApiEndpoint = ApiEndpoint + TransactionEndpoint;
        public const string FindTransactionByIdApiEndpoint = TransactionApiEndpoint + "/{id}";
        public const string FindTransactionByAccountIdApiEndpoint = TransactionApiEndpoint + "/account/{id}";
        public const string FindTransactionByUserSubscriptionIdAndPaymentIdApiEndpoint = TransactionApiEndpoint + "/user-subscription/{userSubscriptionId}/payment/{paymentId}";
    }
    public static class User
    {
        private const string UserEndpoint = "/users";
        public const string UserApiEndpoint = ApiEndpoint + UserEndpoint;
        public const string FindUserByIdApiEndpoint = UserApiEndpoint + "/{id}";
    }
    public static class UserSubscription
    {
        private const string UserSubscriptionEndpoint = "/user-subscriptions";
        public const string UserSubscriptionApiEndpoint = ApiEndpoint + UserSubscriptionEndpoint;
        public const string FindUserSubscriptionByIdApiEndpoint = UserSubscriptionApiEndpoint + "/{id}";
        public const string FindUserSubscriptionByAccountIdApiEndpoint = UserSubscriptionApiEndpoint + "/account/{id}";
    }
}