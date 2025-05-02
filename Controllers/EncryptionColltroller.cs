using System.IO;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System;


namespace wv.Encrypt
{
    /// <summary>
    /// Summary description for EncryptionColltroller
    /// </summary>
    public class EncColltroller
    {
        public string Ec(string clearText, string sKey)
        {
            try
            {
                string EncryptionKey = sKey;

                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

                using (Aes encryptor = Aes.Create())
                {

                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                    encryptor.Key = pdb.GetBytes(32);

                    encryptor.IV = pdb.GetBytes(16);

                    using (MemoryStream ms = new MemoryStream())
                    {

                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {

                            cs.Write(clearBytes, 0, clearBytes.Length);

                            cs.Close();

                        }

                        clearText = Convert.ToBase64String(ms.ToArray());

                    }

                }
            }
            catch
            { 
            }

            return clearText;

        }

        public string Dc(string cipherText, string sKey)
        {
            try
            {

                string EncryptionKey = sKey;

                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (Aes encryptor = Aes.Create())
                {

                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                    encryptor.Key = pdb.GetBytes(32);

                    encryptor.IV = pdb.GetBytes(16);

                    using (MemoryStream ms = new MemoryStream())
                    {

                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {

                            cs.Write(cipherBytes, 0, cipherBytes.Length);

                            cs.Close();

                        }

                        cipherText = Encoding.Unicode.GetString(ms.ToArray());

                    }

                }
            }
            catch
            {
            }
            return cipherText;

        }

    }
}