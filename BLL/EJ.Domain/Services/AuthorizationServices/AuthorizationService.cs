using EJ.Entities.Models;
using EJ.Models.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace EJ.Domain.Services.AuthorizationServices
{
    public interface IAuthorizationService
    {
        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(int userId, string token);
    }

    public class TokenOptions
    {
        /// <summary>
        /// Default token provider name used by email confirmation, password reset, and change email.
        /// </summary>
        public static readonly string DefaultProvider = "Default";

        /// <summary>
        /// Default token provider name used by the email provider. />.
        /// </summary>
        public static readonly string DefaultEmailProvider = "Email";

        /// <summary>
        /// Default token provider name used by the phone provider. />.
        /// </summary>
        public static readonly string DefaultPhoneProvider = "Phone";

        /// <summary>
        /// Default token provider name used by the <see cref="Microsoft.AspNetCore.Identity.AuthenticatorTokenProvider{TUser}"/>.
        /// </summary>
        public static readonly string DefaultAuthenticatorProvider = "Authenticator";

        public string EmailConfirmationTokenProvider { get; set; } = DefaultProvider;
    }

    public class AuthorizationService : IAuthorizationService
    {
        protected readonly IRepository<User> UserRepository;
        public IdentityErrorDescriber ErrorDescriber { get; set; }

        internal static string FormatNoTokenProvider(object p0, object p1)
            => string.Format(CultureInfo.CurrentCulture, GetString("NoTokenProvider"), p0, p1);

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = ""; //_resourceManager.GetString(name);

            System.Diagnostics.Debug.Assert(value != null);

            if (formatterNames != null)
            {
                for (var i = 0; i < formatterNames.Length; i++)
                {
                    value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
                }
            }

            return value;
        }

        protected IDataProtector Protector { get; private set; }

        public TokenOptions Tokens { get; set; } = new TokenOptions();
        public const string ConfirmEmailTokenPurpose = "EmailConfirmation";

        private readonly Dictionary<string, Models.Interfaces.IUserTwoFactorTokenProvider<User>> _tokenProviders =
            new Dictionary<string, Models.Interfaces.IUserTwoFactorTokenProvider<User>>();

        public AuthorizationService(IRepository<User> userRepository)
        {
            /*Protector = new KeyRingBasedDataProtector(
                logger: _logger,
                keyRingProvider: _keyRingProvider,
                originalPurposes: null,
                newPurpose: purpose); dataProtectionProvider.CreateProtector("DataProtectorTokenProvider");*/
            UserRepository = userRepository;
            _tokenProviders[Tokens.EmailConfirmationTokenProvider] = new UserTwoFactorTokenProvider();
        }

        public virtual Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return GenerateUserTokenAsync(user, Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose);
        }

        public virtual Task<string> GenerateUserTokenAsync(User user, string tokenProvider, string purpose)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (tokenProvider == null)
            {
                throw new ArgumentNullException(nameof(tokenProvider));
            }

            return _tokenProviders[tokenProvider].GenerateAsync(purpose, user/*, Protector*/);
        }


        public virtual async Task<IdentityResult> ConfirmEmailAsync(int userId, string token)
        {
            var user = UserRepository.Find(userId);

            if (await VerifyUserTokenAsync(user, Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose,
                token) == IdentityResult.Failed())
            {
                return IdentityResult.Failed();
            }

            return await UpdateUserAsync(user);
        }

        public virtual async Task<IdentityResult> VerifyUserTokenAsync(User user, string tokenProvider, string purpose,
            string token)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (tokenProvider == null)
            {
                throw new ArgumentNullException(nameof(tokenProvider));
            }

            /*if (!_tokenProviders.ContainsKey(tokenProvider))
            {
                throw new NotSupportedException(Resources.FormatNoTokenProvider(nameof(TUser), tokenProvider));
            }*/
            // Make sure the token is valid
            var result = await _tokenProviders[tokenProvider].ValidateAsync(purpose, token, user/*, Protector*/);

            return result;
        }

        protected virtual async Task<IdentityResult> UpdateUserAsync(User user)
        {
            var userFromRepository = await UserRepository.FindAsync(user.Id);
            userFromRepository.EmailVerified = true;
            await UserRepository.UpdateAsync(userFromRepository);
            return UserRepository.Find(userFromRepository.Id).EmailVerified ? IdentityResult.Success : IdentityResult.Failed();
        }
    }
}
