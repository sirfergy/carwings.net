using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Text;

namespace carwings.net.login.bouncycastle
{
    public class LoginProvider : ILoginProvider
    {
        public LoginProvider(string username, string plainTextPassword)
        {
            this.Username = username;

            // Password must be 8 bute aligned
            byte[] passwordByteArray = Encoding.ASCII.GetBytes(plainTextPassword);
            int paddingBytesRequired = 8 - (passwordByteArray.Length % 8);
            if(paddingBytesRequired > 0)
            {
                this.Password = new byte[passwordByteArray.Length + paddingBytesRequired];
                Buffer.BlockCopy(passwordByteArray, 0, this.Password, 0, passwordByteArray.Length);

                // Password must be padded using PKCS5Padding - BouncyCastle provides a Pkcs7Padding class
                // PKCS5Padding is identical to Pkcs7Padding, providing you only pad to exactly 8 bytes
                Pkcs7Padding passwordPadder = new Pkcs7Padding();
                passwordPadder.AddPadding(this.Password, passwordByteArray.Length);
            }
            else
            {
                this.Password = passwordByteArray;
            }
        }

        public string Username { get; private set; }

        public string GetEncryptedPassword(string encryptionKey)
        {
            // Encrypt password using Blowfish
            BufferedBlockCipher blowfishCipher = new BufferedBlockCipher(new BlowfishEngine());
            KeyParameter blowfishKey = new KeyParameter(Encoding.ASCII.GetBytes(encryptionKey));
            blowfishCipher.Init(true, blowfishKey);

            byte[] encryptedPasswordArray = blowfishCipher.ProcessBytes(Password);

            return Convert.ToBase64String(encryptedPasswordArray);
        }

        private byte[] Password { get; set; } 
    }
}
