using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Ht.AuthenticationCenter.Utility.RSA
{
    public class RSAHelper
    {
        /// <summary>
        /// 从本地文件中读取用来签发 Token 的 RSA Key
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <param name="withPrivate"></param>
        /// <param name="keyParameters"></param>
        /// <returns></returns>
        public static bool TryGetKeyParameters(string filePath, bool withPrivate, out RSAParameters keyParameters)
        {
            string filename = withPrivate ? "key.json" : "key.public.json";
            string fileTotalPath = Path.Combine(filePath, filename);
            keyParameters = default(RSAParameters);
            if (!File.Exists(fileTotalPath))
            {
                return false;
            }
            else
            {
                keyParameters = JsonConvert.DeserializeObject<RSAParameters>(File.ReadAllText(fileTotalPath));
                return true;
            }
        }
        /// <summary>
        /// 生成并保存 RSA 公钥与私钥
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <returns></returns>
        public static Tuple<string,string> GenerateAndSaveKey(string filePath)
        {
            RSAParameters publicKeys, privateKeys;
            using (var rsa = new RSACryptoServiceProvider(2048))//即时生成
            {
                try
                {
                    privateKeys = rsa.ExportParameters(true);
                    publicKeys = rsa.ExportParameters(false);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            string publicKey, privateKey;
            publicKey = JsonConvert.SerializeObject(publicKeys);
            privateKey = JsonConvert.SerializeObject(privateKeys);

            File.WriteAllText(Path.Combine(filePath, "key.json"), privateKey);
            File.WriteAllText(Path.Combine(filePath, "key.public.json"), publicKey);
            return new Tuple<string, string>(privateKey, publicKey);
        }
    }
}
