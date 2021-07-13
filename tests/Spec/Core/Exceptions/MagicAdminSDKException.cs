using Xunit;
using FluentAssertions;
using Magic;

namespace MagicAdminTests
{
    public class SdkExceptionTests
    {
        [Fact]
        public void Instantiates_MagicAdminSDKError_With_Empty_Data_Property()
        {
            var error = new MagicException("test message");
            error.Should().BeOfType(typeof(MagicException));
            error.Message.Should().Be("Magic Admin SDK Error: test message");
            error.Code.Should().Be(ErrorCode.Exception);
            error.AdditionalData.Should().BeNull();
        }
    }
}
