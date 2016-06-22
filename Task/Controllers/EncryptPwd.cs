using System;
using System.Collections.Generic;

using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;
/// <summary>
/// Summary description for EncryptPwd
/// </summary>
public static class EncryptPwd
{
   
    # region Function to encrypt String
    public static string EncryptText(string pass)
    {
        return Encrypt(pass, "&%#@?,:*");
    }
    #endregion

    #region Encrypt string
    /// <summary>
    ///   Encrypts  a particular string with a specific Key
    /// </summary>
    /// <param name="stringToEncrypt"></param>
    /// <param name="encryptionKey"></param>
    /// <returns></returns>

    private static string Encrypt(string stringToEncrypt, string encryptionKey)
    {
        byte[] key = { };
        byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
        byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length) 

        try
        {
            key = Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (System.Exception)
        {
            return (string.Empty);
        }
    }
    #endregion

    #region Function decrypt string
    public static string DecryptText(String pass)
    {
        return Decrypt(pass, "&%#@?,:*");
    }
    #endregion

    #region Decrypt String
    /// <summary>
    /// Decrypts  a particular string with a specific Key
    /// </summary>
    private static string Decrypt(string stringToDecrypt, string sEncryptionKey)
    {
        byte[] key = { };
        byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
        byte[] inputByteArray = new byte[stringToDecrypt.Length];
        try
        {
            key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (System.Exception)
        {
            return (string.Empty);
        }
    }
    #endregion

    #region encription 2nd Method
    /* This Function accept string and encrypt it in the form
         that can't be decrypted
         */
    private static string encryptpass(string passtext)
    {
        Byte[] mybyte;
        //Array[] mybyte = new Array[8];
        UTF8Encoding encoder = new UTF8Encoding();
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        mybyte = md5Hasher.ComputeHash(encoder.GetBytes(passtext));
        return mybyte[3].ToString();
    }
    #endregion

}
