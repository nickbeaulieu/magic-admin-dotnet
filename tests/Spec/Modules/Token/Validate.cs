using Xunit;
using FluentAssertions;
using Magic;
using System;

namespace MagicAdminTests
{
    public class ValidateTests
    {
        [Fact]
        public void Successfully_Validates_DIDT()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            Action act = () => sdk.Token.Validate(Constants.VALID_DIDT);
            act.Should().NotThrow();
        }

        [Fact]
        public void Fails_When_Signer_Address_Mismatches_Signature()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var expected = new MagicIncorrectSignerException();

            Action act = () => sdk.Token.Validate(Constants.INVALID_SIGNER_DIDT);
            act.Should()
                .ThrowExactly<MagicIncorrectSignerException>()
                .Where(x => x.Code == expected.Code)
                .Where(x => x.Message == expected.Message);
        }

        [Fact]
        public void Fails_When_Given_Expired_Token()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var expected = new MagicTokenExpiredException();

            Action act = () => sdk.Token.Validate(Constants.EXPIRED_DIDT);
            act.Should()
                .ThrowExactly<MagicTokenExpiredException>()
                .Where(x => x.Code == expected.Code)
                .Where(x => x.Message == expected.Message);
        }

        [Fact]
        public void Fails_When_Given_Token_With_Future_NBF()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var expected = new MagicTokenCannotBeUsedYetException();

            Action act = () => sdk.Token.Validate(Constants.VALID_FUTURE_MARKED_DIDT);
            act.Should()
                .ThrowExactly<MagicTokenCannotBeUsedYetException>()
                .Where(x => x.Code == expected.Code)
                .Where(x => x.Message == expected.Message);
        }

        [Fact]
        public void Fails_If_Decoding_Token_Fails()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var expected = new MagicMalformedTokenException();

            Action act = () => sdk.Token.Validate(Constants.INVALID_DIDT_MALFORMED_CLAIM);
            act.Should()
                .ThrowExactly<MagicMalformedTokenException>()
                .Where(x => x.Code == expected.Code)
                .Where(x => x.Message == expected.Message);
        }
    }
}