﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace imgLoader_WPF
{
    public static class StringCipher
    {
        private static readonly byte[] salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76};
        public static string Encrypt(string clearText)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);

            using var encryptor = Aes.Create();

            var pdb = new Rfc2898DeriveBytes(Core.ProjectName, salt);

            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
            }

            return Convert.ToBase64String(ms.ToArray());
        }
        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(cipherText);

            using var encryptor = Aes.Create();

            var pdb = new Rfc2898DeriveBytes(Core.ProjectName, salt);

            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
            }

            return Encoding.Unicode.GetString(ms.ToArray());
        }
    }

}
