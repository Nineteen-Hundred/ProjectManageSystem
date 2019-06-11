using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;



namespace DataCrypto
{
    /// <summary>
    /// 哈希加密类
    /// </summary>
    public class HashMethod
    {

        private HashAlgorithm HashCryptoService;
        /// <summary>
        /// 哈希加密类的构造函数
        /// </summary>
        public HashMethod()
        {
            HashCryptoService = new SHA1Managed();
        }
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public string Encrypto(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            byte[] bytOut = HashCryptoService.ComputeHash(bytIn);
            return Convert.ToBase64String(bytOut);
        }
    }
}


