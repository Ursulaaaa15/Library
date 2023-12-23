using Library.BL.Auth.Entities;
using Library.DataAccess.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services; 
using Microsoft.Extensions.Http;
using IdentityModel.Client;



namespace Library.BL.Auth
{
    public class AuthProvider : IAuthProvider
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _identityServerUri;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AuthProvider(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager,
        IHttpClientFactory httpClientFactory,
        string identityServerUri,
        string clientId,
        string clientSecret)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _identityServerUri = identityServerUri;
            _httpClientFactory = httpClientFactory;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<TokensResponse> AuthorizeUser(string login, string password)
        {
            var user = await _userManager.FindByNameAsync(login);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var verificationPasswordResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!verificationPasswordResult.Succeeded)
            {
                throw new Exception("Invalid password");
            }

            var client = _httpClientFactory.CreateClient();
            var discoveryDoc = await client.GetDiscoveryDocumentAsync(_identityServerUri);
            if (discoveryDoc.IsError)
            {
                throw new Exception();
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryDoc.TokenEndpoint,
                GrantType = GrantType.ResourceOwnerPassword,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                UserName = user.UserName,
                Password = password,
                Scope = "api offline_access"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception($"Error requesting token: {tokenResponse.Error}");
            }

            return new TokensResponse
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken
            };
        }

        public async Task RegisterUser(RegisterUserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Login);
            if (user != null)
            {
                throw new Exception("User with this username already exists");
            }

            var newUser = new IdentityUser
            {
                UserName = model.Login,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Error creating user: {string.Join(", ", result.Errors)}");
            }
        }
    }
}
