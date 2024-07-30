using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MonPetitPoro
{
    public class AuthenticationToken
    {
        private static readonly char[] Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        public string GenerateToken(int length = 32)
        {
            byte[] data = new byte[length];
            using (var Rng = RandomNumberGenerator.Create())
            {
                Rng.GetBytes(data);
            }

            var token = new StringBuilder(length);
            foreach (byte b in data)
            {
                token.Append(Characters[b % Characters.Length]);
            }

            return token.ToString();
        }

        public async Task SaveAuthTokenAsync(string token)
        {
            await SecureStorage.SetAsync("auth_token", token);
        }

        public async Task<string> GetAuthTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync("auth_token");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void ResetAuthToken()
        {
            SecureStorage.Remove("auth_token");
        }
    }
}
