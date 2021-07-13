namespace Magic
{
    public class MagicException : System.Exception
    {
        public ErrorCode Code { get; internal set; }
        public object[] AdditionalData { get; internal set; }
        public MagicException() { }
        public MagicException(string message) : base($"Magic Admin SDK Error: {message}") { }
    }

    public class MagicTokenExpiredException : MagicException 
    {
        public MagicTokenExpiredException(string message = "DID Token has expired. Request failed authentication.")
            : base(message) 
        {
            Code = ErrorCode.TokenExpired;
        }
    }

    public class MagicTokenCannotBeUsedYetException : MagicException 
    {
        public MagicTokenCannotBeUsedYetException(string message = "Given DID Token cannot be used at this time. Please check the `nbf` field and regenerate a new token with a suitable value.")
            : base(message) 
        {
            Code = ErrorCode.TokenCannotBeUsedYet;
        }
    }

    public class MagicIncorrectSignerException : MagicException 
    {
        public MagicIncorrectSignerException(string message = "Incorrect signer address for DID Token. Request failed authentication.")
            : base(message) 
        {
            Code = ErrorCode.IncorrectSignerAddress;
        }
    }

    public class MagicFailedRecoveringProofException : MagicException 
    {
        public MagicFailedRecoveringProofException(string message = "Failed to recover proof. Request failed authentication.")
            : base(message) 
        {
            Code = ErrorCode.FailedRecoveryProof;
        }
    }

    public class MagicApiKeyMissingException : MagicException 
    {
        public MagicApiKeyMissingException(string message = "Please provide a secret Fortmatic API key that you acquired from the developer dashboard.")
            : base(message) 
        {
            Code = ErrorCode.ApiKeyMissing;
        }
    }

    public class MagicMalformedTokenException : MagicException 
    {
        public MagicMalformedTokenException(string message = "The DID token is malformed or failed to parse.")
            : base(message) 
        {
            Code = ErrorCode.MalformedTokenError;
        }
    }

    public class MagicServiceException : MagicException 
    {
        public MagicServiceException(
            string message = "A service error occurred while communicating with the Magic API. Check the `data` key of this error object to see nested errors with additional context.",
            object[] additionalErrors = null)
            : base(message) 
        {
            Code = ErrorCode.ServiceError;
            AdditionalData = additionalErrors;
        }
    }

    public class MagicExpectedBearerException : MagicException 
    {
        public MagicExpectedBearerException(string message = "Expected argument to be a string in the `Bearer {token}` format.")
            : base(message) 
        {
            Code = ErrorCode.ExpectedBearerString;
        }
    }
}