using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace imgL
{
    public static class StringCipher
    {
        private static readonly byte[] Salt = { 0x52, 0x02, 0x8a, 0x76, 0x75, 0x4d, 0x7c, 0x23, 0x65, 0xd4, 0x1e, 0x5d, 0x63 };
        public static string Encrypt(string clearText)
        {
            var bytes = Encoding.Unicode.GetBytes(clearText);
            var pdb = new Rfc2898DeriveBytes(Core.ProjectName, Salt);
            using var encryptor = Aes.Create();

            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)) cs.Write(bytes, 0, bytes.Length);

            return Convert.ToBase64String(ms.ToArray());
        }
        public static string Decrypt(string text)
        {
            var bytes = Convert.FromBase64String(text.Replace(" ", "+"));
            var pdb = new Rfc2898DeriveBytes(Core.ProjectName, Salt);
            using var encrypt = Aes.Create();

            encrypt.Key = pdb.GetBytes(32);
            encrypt.IV = pdb.GetBytes(16);

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encrypt.CreateDecryptor(), CryptoStreamMode.Write)) cs.Write(bytes, 0, bytes.Length);

            return Encoding.Unicode.GetString(ms.ToArray());
        }
    }

}
