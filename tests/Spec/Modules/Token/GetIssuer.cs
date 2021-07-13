using Xunit;
using FluentAssertions;
using Magic;
using System;

namespace MagicAdminTests
{
    public class GetIssuerTests
    {
        [Fact]
        public void Successfully_Gets_Issuer_From_DIDT()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var issuer = sdk.Token.GetIssuer(Constants.VALID_DIDT);
            issuer.Should().Be(Constants.VALID_DIDT_PARSED_CLAIMS.Iss);
        }
    }
}