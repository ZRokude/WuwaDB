//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
//using System.Security.Claims;

//namespace WuwaDB.Authentication
//{
//    public class CustomAuthentication : AuthenticationStateProvider
//    {
//        public readonly ProtectedSessionStorage _sessionstorage;
//        private ClaimsPrincipal _guest = new(new ClaimsIdentity());
//        public CustomAuthentication(ProtectedSessionStorage sessionStorage)
//        {
//            _sessionstorage = sessionStorage;
//        }

//        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//        {
//            try
//            {
//                ProtectedBrowserStorageResult<LoginSession> loginSessionResult = await _sessionstorage.GetAsync<LoginSession>("LoginSession");
//                LoginSession? loginSession = loginSessionResult.Success ? loginSessionResult.Value : null;

//                if (loginSessionResult == null) return await Task.FromResult(new AuthenticationState(_guest));
//                ClaimsPrincipal ClaimsPrincipal = new(new ClaimsIdentity(new List<Claim>
//                    {
//                        new Claim(ClaimTypes.UserData, loginSession.Username),
//                        new Claim(ClaimTypes.Role, loginSession.Role)
//                    },
//                    "CustomAuth"
//                ));
//                return await Task.FromResult(new AuthenticationState(ClaimsPrincipal));
//            }
//            catch
//            {
//                return await Task.FromResult(new AuthenticationState(_guest));
//            }
//        }


//        public async Task UpdateAuthenticationState(UserSession? userSession)
//        {
//            ClaimsPrincipal claimsPrincipal;

//            if (userSession != null)
//            {
//                await _sessionstorage.SetAsync("UserSession", userSession);
//                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
//                new Claim(ClaimTypes.Email , userSession.Email),
//                new Claim (ClaimTypes.Role, userSession.Role)
//                }));

//            }
//            else
//            {
//                await _sessionstorage.DeleteAsync("UserSession");
//                claimsPrincipal = _guest;
//            }
//            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
//        }
//    }
//}

