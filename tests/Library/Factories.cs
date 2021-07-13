using Magic;

namespace MagicAdminTests
{
    public static class Factories
    {
        public static MagicAdminSDK CreateMagicAdminSDK(string endpoint = Constants.API_FULL_URL)
            => new MagicAdminSDK(Constants.API_KEY, new MagicAdminSDKAdditionalConfiguration { Endpoint = endpoint } );
    }
}