using Microsoft.AspNetCore.Authentication;

namespace Sigma
{
    public class AuthOptions : AuthenticationOptions
    {
        public string AuthenticationScheme {
            get { return Authentication.Scheme; }
        }
    }
}