using EJ.Entities.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EJ.Domain.Services.AuthorizationServices
{
    public class UserTwoFactorTokenProvider : Models.Interfaces.IUserTwoFactorTokenProvider<User>
    {
        public async Task<string> GenerateAsync(string purpose, User user/*, IDataProtector protector*/)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using (var ms = new MemoryStream())
            {
                var userId = user.Id;
                using (var writer = new BinaryWriter(ms))
                {
                    Write(writer, DateTimeOffset.UtcNow);
                    writer.Write(userId.ToString());
                    writer.Write(purpose ?? "");
                }
                //var protectedBytes = protector.Protect(ms.ToArray());
                return Convert.ToBase64String(ms.ToArray()/*protectedBytes*/);
            }
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
        {
            throw new NotImplementedException();
        }
        private static void Write(BinaryWriter writer, DateTimeOffset value)
        {
            writer.Write(value.UtcTicks);
        }
        private static DateTimeOffset ReadDateTimeOffset(BinaryReader reader)
        {
            return new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
        }
        public TimeSpan TokenLifespan { get; set; } = TimeSpan.FromDays(1);
        public virtual async Task<IdentityResult> ValidateAsync(string purpose, string token, User user/*, IDataProtector protector*/)
        {
            try
            {
                var unprotectedData = /*protector.Unprotect(*/Convert.FromBase64String(token);//);
                using (var ms = new MemoryStream(unprotectedData))
                {
                    using (var reader = new BinaryReader(ms))
                    {
                        var creationTime = ReadDateTimeOffset(reader);
                        var expirationTime = creationTime + TokenLifespan;
                        if (expirationTime < DateTimeOffset.UtcNow)
                        {
                            return IdentityResult.Failed();
                        }

                        var userId = reader.ReadString();
                        var actualUserId = user.Id.ToString();
                        if (userId != actualUserId)
                        {
                            return IdentityResult.Failed();
                        }
                        var purp = reader.ReadString();
                        return !string.Equals(purp, purpose) ? IdentityResult.Failed() : IdentityResult.Success;
                    }
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
                // Do not leak exception
            }
            return IdentityResult.Failed();
        }
    }
}
