using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magic
{

    public class UsersModule : BaseModule
    {

        private readonly MagicAdminSDK SDK;
        private readonly IRest Client;

        public UsersModule(MagicAdminSDK sdk, IRest restClient = null) : base(sdk)
        {
            SDK = sdk;
            Client = restClient ?? new Rest(SDK.SecretApiKey);
        }

        public async Task LogoutByIssuer(string issuer)
        {
            if (SDK.SecretApiKey == null || SDK.SecretApiKey == string.Empty)
            {
                throw new MagicApiKeyMissingException();
            }

            await Client.Post<IssuerDTO, object>($"{SDK.ApiBaseUrl}/v2/admin/auth/user/logout", new IssuerDTO { issuer = issuer });
        }

        public async Task LogoutByPublicAddress(string publicAddress)
        {
            var issuer = Issuer.GenerateIssuerFromPublicAddress(publicAddress);
            await LogoutByIssuer(issuer);
        }

        public async Task LogoutByToken(string didToken)
        {
            var issuer = SDK.Token.GetIssuer(didToken);
            await LogoutByIssuer(issuer);
        }

        private class IssuerDTO
        {
            public string issuer { get; set; }
        }
        
        public async Task<MagicUserMetadata> GetMetadataByIssuer(string issuer) 
        {
            if (SDK.SecretApiKey == null || SDK.SecretApiKey == string.Empty)
            {
                throw new MagicApiKeyMissingException();
            }

            var headers = new Dictionary<string, string>() { { "issuer", issuer } };

            var result = await Client.Get<MagicUserMetadata>($"{SDK.ApiBaseUrl}/v1/admin/auth/user/get", headers);

            return new MagicUserMetadata
            {
                Email = result?.Email ?? null,
                Issuer = result?.Issuer ?? null,
                PublicAddress = result?.PublicAddress ?? null,
            };
        }

        public async Task<MagicUserMetadata> GetMetadataByToken(string didToken)
        {
            var issuer = SDK.Token.GetIssuer(didToken);
            return await GetMetadataByIssuer(issuer);
        }

        public async Task<MagicUserMetadata> GetMetadataByPublicAddress(string publicAddress)
        {
            var issuer = Issuer.GenerateIssuerFromPublicAddress(publicAddress);
            return await GetMetadataByIssuer(issuer);
        }

        private class MetadataDTO
        {
            public string issuer { get; set; }
            public string publicAddress { get; set; }
            public string email { get; set; }
        }
    }
}