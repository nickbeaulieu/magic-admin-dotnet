using System;

namespace Magic
{
    public class UtilsModule : BaseModule
    {
        private readonly MagicAdminSDK SDK;

        public UtilsModule(MagicAdminSDK sdk) : base(sdk) 
        {
            SDK = sdk;
        }

        public string ParseAuthorizationHeader(string header)
        {
            if (!header.ToLower().StartsWith("bearer "))
            {
                throw new MagicExpectedBearerException();
            }

            return header.Substring(7);
        }
    }
}