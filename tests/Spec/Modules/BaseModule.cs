using Xunit;
using FluentAssertions;
using Magic;

namespace MagicAdminTests
{
    public class BaseModuleTests
    {
        [Fact]
        public void Implements_Base_Module_Correctly()
        {
            var sdk = Factories.CreateMagicAdminSDK();
            var tokenModule = new TokenModule(sdk);
            var usersModule = new UsersModule(sdk);
            var utilsModule = new UtilsModule(sdk);

            tokenModule.Should().BeOfType(typeof(TokenModule));
            usersModule.Should().BeOfType(typeof(UsersModule));
            utilsModule.Should().BeOfType(typeof(UtilsModule));
        }
    }
}