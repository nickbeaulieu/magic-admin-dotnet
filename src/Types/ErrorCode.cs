using System.ComponentModel;

namespace Magic
{
    public enum ErrorCode
    {
        Exception, 

        [Description("ERROR_MISSING_AUTH_HEADER")]
        MissingAuthHeader,

        [Description("ERROR_DIDT_EXPIRED")]
        TokenExpired,

        [Description("ERROR_DIDT_CANNOT_BE_USED_YET")]
        TokenCannotBeUsedYet,

        [Description("ERROR_INCORRECT_SIGNER_ADDR")]
        IncorrectSignerAddress,

        [Description("ERROR_FAILED_RECOVERING_PROOF")]
        FailedRecoveryProof,

        [Description("ERROR_SECRET_API_KEY_MISSING")]
        ApiKeyMissing,

        [Description("ERROR_MALFORMED_TOKEN")]
        MalformedTokenError,

        [Description("SERVICE_ERROR")]
        ServiceError,

        [Description("EXPECTED_BEARER_STRING")]
        ExpectedBearerString
    }
}