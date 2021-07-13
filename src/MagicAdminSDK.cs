using System.Text.RegularExpressions;

namespace Magic
{
    public class MagicAdminSDK
    {
        public readonly string ApiBaseUrl;
        public readonly string SecretApiKey;
        public readonly TokenModule Token;
        public readonly UtilsModule Utils;
        public readonly UsersModule Users;
        public MagicAdminSDK(string secretApiKey, MagicAdminSDKAdditionalConfiguration options = null)
        {
            if (secretApiKey == null || secretApiKey == string.Empty)
            {
                throw new MagicApiKeyMissingException();
            }
            
            var endpoint = options?.Endpoint ?? "https://api.magic.link";
            ApiBaseUrl = Regex.Replace(endpoint, @"\/+$", "");
            SecretApiKey = secretApiKey;
            Token = new TokenModule(this);
            Utils = new UtilsModule(this);
            Users = new UsersModule(this);
        }
    }
}
