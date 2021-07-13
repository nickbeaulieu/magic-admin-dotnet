using Xunit;
using FluentAssertions;
using Magic;
using System.Collections.Generic;

namespace MagicAdminTests
{
    public class SdkExceptionMessageTests
    {
        [Theory]
        [MemberData(nameof(ExceptionsToTest))]
        public void Instantiates_MagicAdminSDKError_With_Empty_Data_Property(MagicException exception, ErrorCode code, string message)
        {
            exception.Code.Should().Be(code);
            exception.Message.Should().Be($"Magic Admin SDK Error: {message}");
        }

        public static IEnumerable<object[]> ExceptionsToTest
        {
            get
            {
                yield return new object[] 
                { 
                    new MagicTokenExpiredException(),
                    ErrorCode.TokenExpired,
                    "DID Token has expired. Request failed authentication."
                };

                yield return new object[] 
                { 
                    new MagicTokenCannotBeUsedYetException(),
                    ErrorCode.TokenCannotBeUsedYet,
                    "Given DID Token cannot be used at this time. Please check the `nbf` field and regenerate a new token with a suitable value."
                };

                yield return new object[] 
                { 
                    new MagicIncorrectSignerException(),
                    ErrorCode.IncorrectSignerAddress,
                    "Incorrect signer address for DID Token. Request failed authentication."
                };

                yield return new object[] 
                { 
                    new MagicFailedRecoveringProofException(),
                    ErrorCode.FailedRecoveryProof,
                    "Failed to recover proof. Request failed authentication."
                };

                yield return new object[] 
                { 
                    new MagicApiKeyMissingException(),
                    ErrorCode.ApiKeyMissing,
                    "Please provide a secret Fortmatic API key that you acquired from the developer dashboard."
                };

                yield return new object[] 
                { 
                    new MagicServiceException(),
                    ErrorCode.ServiceError,
                    "A service error occurred while communicating with the Magic API. Check the `data` key of this error object to see nested errors with additional context."
                };

                yield return new object[] 
                { 
                    new MagicServiceException("test message"),
                    ErrorCode.ServiceError,
                    "test message"
                };

                yield return new object[] 
                { 
                    new MagicExpectedBearerException(),
                    ErrorCode.ExpectedBearerString,
                    "Expected argument to be a string in the `Bearer {token}` format."
                };
            }
        }
    }
}