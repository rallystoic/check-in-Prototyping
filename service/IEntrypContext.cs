using System;
using System.IO;
using System.Text.Json;
using QC.models;
using System.Security.Cryptography;
namespace QC.services
{
    public interface IEntrypContext {
        // Encrypt string to byte
        byte[] EncryptStringToBytes_Aes(string Jsonstring, byte[] Key, byte[] IV);
        // Decrypt nessage from  byte[] to string
        string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV);
    }
    public class EntrypContext: IEntrypContext {
        public EntrypContext(){
        }

        public byte[] EncryptStringToBytes_Aes(string Jsonstring, byte[] Key, byte[] IV){
            // Check arguments.
            if (Jsonstring == null || Jsonstring.Length <= 0)
                throw new ArgumentNullException("Jsonstring");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using(Aes aes = Aes.Create()){
                aes.Key = Key;
                aes.IV = IV;
                // Create an Encryptor to perform stream transform
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(Jsonstring);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV){
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                // Create a decryptor to perform the stream transform.

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                      using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        try
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Invalide Signature");
                        }
                    }
                }

            }
            }
            return plaintext;
        }



    }
}
