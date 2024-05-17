using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Security.Principal;

namespace WuwaDB.Authentication
{
    public class CustomAuthentication : AuthenticationStateProvider
    {
        public readonly ProtectedSessionStorage _sessionstorage;
        private ClaimsPrincipal _guest = new(new ClaimsIdentity());
        public CustomAuthentication(ProtectedSessionStorage sessionStorage)
        {
            _sessionstorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                ProtectedBrowserStorageResult<LoginSession> loginSessionResult = await _sessionstorage.GetAsync<LoginSession>("LoginSession");
                LoginSession? loginSession = loginSessionResult.Success ? loginSessionResult.Value : null;

                ClaimsIdentity identity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.UserData, loginSession.Username)
                }, "CustomAuth");

                // Iterate over each role in the LoginSession and add a claim for it
                foreach (var role in loginSession.Role)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                }
                ClaimsPrincipal ClaimsPrincipal = new ClaimsPrincipal(identity);
                return await Task.FromResult(new AuthenticationState(ClaimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_guest));
            }
        }


        public async Task UpdateAuthenticationState(LoginSession? loginSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (loginSession != null)
            {
                await _sessionstorage.SetAsync("loginSession", loginSession);
                // Initialize a ClaimsIdentity with the username
                ClaimsIdentity identity = new ClaimsIdentity(new List<Claim>
                {
                     new Claim(ClaimTypes.UserData, loginSession.Username)
                }, "CustomAuth");

                // Iterate over each role in the login session and add it as a claim
                foreach (var role in loginSession.Role)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                }

                // Create a ClaimsPrincipal with the populated identity
                claimsPrincipal = new ClaimsPrincipal(identity);

            }
            else
            {
                await _sessionstorage.DeleteAsync("loginSession");
                claimsPrincipal = _guest;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}

