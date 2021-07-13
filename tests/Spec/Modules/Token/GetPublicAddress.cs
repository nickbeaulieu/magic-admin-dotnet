using Xunit;
using FluentAssertions;
using Magic;
using System;

namespace MagicAdminTests
{
    public class GetPublicAddressTests
    {
        [Fact]
        public void Successfully_Gets_PublicAddress_From_DIDT()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var publicAddress = sdk.Token.GetPublicAddress(Constants.VALID_DIDT);
            publicAddress.Should().Be(Constants.VALID_DIDT_PARSED_CLAIMS.Iss.Split(":")[2]);
        }
    }
}