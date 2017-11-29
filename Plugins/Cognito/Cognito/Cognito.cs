using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

    public class CognitoUserManager : UserManager<CognitoUser>
    {

        private readonly AmazonCognitoIdentityProviderClient _client = 
            new AmazonCognitoIdentityProviderClient();
        private readonly string _clientId = ConfigurationManager.AppSettings["CLIENT_ID"];
        private readonly string _poolId = ConfigurationManager.AppSettings["USERPOOL_ID"];

        public static BooleanResult getResponse(String userName, String password)
            {
            try
            {
                var authReq = new AdminInitiateAuthRequest
                {
                    UserPoolId = ConfigurationManager.AppSettings["USERPOOL_ID"],
                    ClientId = ConfigurationManager.AppSettings["CLIENT_ID"],
                    AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
                };
                authReq.AuthParameters.Add("USERNAME", userName);
                authReq.AuthParameters.Add("PASSWORD", password);

                AdminInitiateAuthResponse authResp = await _client.AdminInitiateAuthAsync(authReq);

                return new BooleanResult() { Success = true };
            }
            catch
            {
                return new BooleanResult() { Success = false};
            }
        }

     

    }