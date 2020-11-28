namespace ApiService.Models.Api.Common
{
    public enum ErrorMessagesEnum
    {
        UserNotFound = 0,
        UserNotFoundOrAlreadyActivated = 1,
        UserEmailExist = 2,
        ModelValidationFailed = 3,
        UserActivationFailed = 4,
        EmailNotAvailable = 5,
        RegistrationFailed = 6,
        ResetPasswordFailed = 7,
        ResetPasswordTimeout = 8,
        BadRecoveryCode = 9,
        RecoveryCodeCheckFailed = 10,
        GoogleAuthServiceError = 11,
        PictureNotFound = 12,
        MessageNotFound = 13,
        FacebookAuthServiceError = 14,
        EmailOrUserNameNotAvailable = 15,
        UserNameNotAvailable = 16,
        NoEnoughRightsToDeleteMessage = 17,

        AntiSpamCannotPostNewMessage = 18,
        AntiSpamCannotPostNewComment = 19,

        PushSubscriptionFailedValidationFailed = 20,
        MessageGroupNotFound = 21,
        MessageGroupMessageNotFound = 22,
        MessageGroupMessageNoEditRights = 23
    }
}